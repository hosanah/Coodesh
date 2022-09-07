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

            return Ok(new ResultViewModel<Word>(word));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0202X Não foi possível buscar a palavras! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("02002C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }

    [HttpGet("/entries/en/:word/favorite")]
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

            return Ok(new ResultViewModel<Word>(word));
        }
        catch (DbUpdateException ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("0202X Não foi possível buscar a palavras! Favor contate o suporte"));
        }
        catch (Exception ex){
            return StatusCode(500, new ResultViewModel<List<Word>>("02002C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }

    private async void RegisterHistory(
        [FromServices]CoodeshDbContext context, 
        RegisterAccessHistoriesViewModel registerAccessHistoriesViewModel)
    {
        try
            {
                var accessHistory = new AccessHistory()
                {
                    Id = 0,
                    User = registerAccessHistoriesViewModel.User,
                    Word = registerAccessHistoriesViewModel.Word,
                    AccessedWhen = DateTime.UtcNow
                };

                await context.AccessHistory.AddAsync(accessHistory);
                await context.SaveChangesAsync();


            }
            catch (Exception ex){
            StatusCode(500, new ResultViewModel<List<Word>>("02002C Erro interno do servidor! Favor contate o suporte"));
        }
        
    }
    
}