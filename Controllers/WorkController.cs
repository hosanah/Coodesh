using Coodesh.Data;
using Coodesh.Models;
using Coodesh.ViewModels.Accounts;
using Coodesh.ViewModels.ResultViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coodesh.Controllers;

[ApiController]
public class WorkController : ControllerBase
{

    [HttpGet("/entries/en")]
    public async Task<IActionResult> GetAsyc(
        [FromServices]CoodeshDbContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var words = await context.Word.Skip(page * pageSize).Take(pageSize).ToListAsync();
            return Ok(new ResultViewModel<List<Word>>(words));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0200X Não foi possível buscar todas as palavras! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0200C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }

    [HttpGet("/entries/en")]
    public async Task<IActionResult> GetByWorkAsyc(
        [FromServices]CoodeshDbContext context,
        [FromBody] SearchWordViewModel searchWord,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var word = await context
            .Word
            .AsNoTracking()
            .Where(e => e.Name.Contains(searchWord.Name))
            .Skip(page * pageSize)
            .Take(pageSize)
            .FirstOrDefaultAsync();

            if(word == null)
            return NotFound(new ResultViewModel<Word>("0202Z Não foi encontrada nenhuma palavra! Favor tente novamente outra palavra"));


            var user = await context
            .User
            .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            if(user == null)
            return NotFound(new ResultViewModel<FavoriteWord>("0302F Usuário não encontrado"));


            var accessHistory = new AccessHistory()
                {
                    Id = 0,
                    Who = user,
                    AccessedWhen = DateTime.UtcNow
                };

                await context.AccessHistory.AddAsync(accessHistory);
                await context.SaveChangesAsync();


            return Ok(new ResultViewModel<Word>(word));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0202X Não foi possível buscar a palavras! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("02002C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }

    [HttpPost("/entries/en/:word/favorite")]
    public async Task<IActionResult> FavoriteWorkAsyc(
        [FromServices]CoodeshDbContext context,
        [FromBody] SearchWordViewModel searchWord)
    {
        try
        {
            var word = await context
            .Word
            .AsNoTracking()
            .Where(e => e.Name.Contains(searchWord.Name))
            .FirstOrDefaultAsync();

            if(word == null)
            return NotFound(new ResultViewModel<Word>("0302Z Não foi encontrada nenhuma palavra! Favor tente novamente outra palavra"));

            var user = await context
            .User
            .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            if(user == null)
            return NotFound(new ResultViewModel<FavoriteWord>("0302F Usuário não encontrado"));


            var favoriteWork = new FavoriteWord()
                {
                    Id = 0,
                    When = DateTime.Now,
                    Word = word,
                    Who = user
                };

                await context.FavoriteWord.AddAsync(favoriteWork);
                await context.SaveChangesAsync();

            return Ok(new ResultViewModel<FavoriteWord>(favoriteWork));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0302X Não foi possível buscar a palavras! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("03002C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }

    [HttpDelete("/entries/en/:word/unfavorite")]
    public async Task<IActionResult> UnfavoriteWorkAsyc(
        [FromServices]CoodeshDbContext context,
        [FromBody] SearchWordViewModel searchWord)
    {
        try
        {
            var favoriteWord = await context
            .FavoriteWord
            .AsNoTracking()
            .Include(e => e.Word)
            .Where(e => e.Word.Name.Contains(searchWord.Name))
            .FirstOrDefaultAsync();

            if(favoriteWord == null)
            return NotFound(new ResultViewModel<Word>("0402Z Não foi encontrada nenhuma palavra favoritada! Favor tente novamente outra palavra"));

            context.FavoriteWord.Remove(favoriteWord);
            await context.SaveChangesAsync();

            return Ok(new ResultViewModel<FavoriteWord>(favoriteWord));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0302X Não foi possível buscar a palavras! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("03002C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }
}