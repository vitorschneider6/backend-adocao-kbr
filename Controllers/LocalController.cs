using Adocao.Data;
using Adocao.Models;
using Adocao.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Controllers;

[ApiController]
public class LocalController : ControllerBase
{
    private readonly AdocaoDevDataContext _context;
    [HttpGet("v1/locais")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var locais = await _context.Locais.ToListAsync();
            return Ok(new ResultViewModel<List<Local>>(locais));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Local>("Ops... Algo deu errado."));
        }
    }

    [AllowAnonymous]
    [HttpGet("v1/locais/{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        try
        {
            var local = await _context.Locais.FirstOrDefaultAsync();

            if (local == null)
            {
                return NotFound(new ResultViewModel<Local>("Local não encontrado!"));
            }

            return Ok(new ResultViewModel<Local>(local));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Local>("Ops... Algo deu errado."));
        }
    }

    public LocalController(AdocaoDevDataContext context)
    {
        this._context = context;
    }
}