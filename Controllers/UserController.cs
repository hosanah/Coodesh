using Coodesh.Data;
using Coodesh.Extensions;
using Coodesh.Models;
using Coodesh.ViewModels.Accounts;
using Coodesh.ViewModels;
using Coodesh.ViewModels.ResultViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Coodesh.Controllers;

[ApiController]
public class Usercontroller : ControllerBase
{
    [HttpGet("/user/me")]
    public async Task<IActionResult> GetUser(
        [FromServices] CoodeshDbContext context,
        [FromServices] TokenService tokenService)
    {
        return StatusCode(200, new ResultViewModel<string>(User.Identity.Name));
    }


    [HttpGet("/user/me/history")]
    public async Task<IActionResult> GetUserHistory(
        [FromServices]CoodeshDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var accessHistory = await context
            .AccessHistory
            .Include(e => e.Who)
            .Where(x => x.Who.Email == User.Identity.Name)
            .Skip(page * pageSize)
            .Take(pageSize).
            ToListAsync();

            return Ok(new ResultViewModel<List<AccessHistory>>(accessHistory));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<AccessHistory>>("0500X Não foi possível buscar o histórico! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<AccessHistory>>("0500C Erro interno do servidor! Favor contate o suporte"));
        }
    }

    [HttpGet("/user/me/favorites")]
    public async Task<IActionResult> GetUserFavorites(
        [FromServices]CoodeshDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var favoriteWords = await context
            .FavoriteWord
            .Include(e => e.Who)
            .Where(x => x.Who.Email == User.Identity.Name)
            .Skip(page * pageSize)
            .Take(pageSize).
            ToListAsync();

            return Ok(new ResultViewModel<List<FavoriteWord>>(favoriteWords));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<FavoriteWord>>("0500F Não foi possível buscar o as palavras favoritadas! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<FavoriteWord>>("0500U Erro interno do servidor! Favor contate o suporte"));
        }
    }
}