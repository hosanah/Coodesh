using Coodesh.ViewModels.ResultViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coodesh.Controllers;

[ApiController]
[AllowAnonymous]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> Get()
    {
        return Ok(new ResultViewModel<dynamic>(new
            {
                message = "Fullstack Challenge 🏅 - Dictionary" 
            }));

    }
}