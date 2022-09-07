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
public class AccountController : ControllerBase
{
    [HttpPost("/auth/signup")]
    public async Task<IActionResult> Post(
        [FromBody] RegisterViewModel model,
        [FromServices] CoodeshDbContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var usuario = new Usuario
        {
            Nome = model.Name,
            Email = model.Email
        };

        usuario.PasswordHash = PasswordHasher.Hash(model.Password);

        try
        {
            await context.Usuario.AddAsync(usuario);
            await context.SaveChangesAsync();

            var token = tokenService.GenerateToken(usuario);

            return Ok(new ResultViewModel<dynamic>(new
            {   
                id = usuario.Id,
                name = usuario.Nome, 
                token
            }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("0199X - Este E-mail já está cadastrado"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("0199C - Falha interna no servidor"));
        }
    }

    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] CoodeshDbContext context,
        [FromServices] TokenService tokenService)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var usuario = await context
            .Usuario
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (usuario == null)
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(usuario.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = tokenService.GenerateToken(usuario);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("01200X - Falha interna no servidor"));
        }
    }
}