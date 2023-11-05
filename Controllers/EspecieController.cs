using Adocao.Data;
using Adocao.ExtensionMethods;
using Adocao.Models;
using Adocao.ViewModels;
using Adocao.ViewModels.Especie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Controllers;

[Authorize]
[ApiController]
public class EspecieController : ControllerBase
{
    private readonly AdocaoDevDataContext _context; 
    [AllowAnonymous]
    [HttpGet("v1/especies")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var especies = await _context.Especies.ToListAsync();
            return Ok(new ResultViewModel<List<Especie>>(especies));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Erro interno no servidor"));
        }   
    }

    
    [HttpGet("v1/especies/{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        try
        {
            var especie = await _context
                .Especies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (especie == null)
                return NotFound(new ResultViewModel<Especie>("Espécie não encontrada"));

            return Ok(new ResultViewModel<Especie>(especie));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Especie>("Falha interna no servidor"));
        }
    }

    [HttpPost("v1/especies")]
    public async Task<IActionResult> Post(
        [FromBody] EditorEspecieViewModel model
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Especie>(ModelState.GetErrors()));
        }
        try
        {
            var especie = new Especie
            {
                Nome = model.Nome
            };
            _context.Especies.Add(especie);
            await _context.SaveChangesAsync();
            return StatusCode(201, new ResultViewModel<Especie>(especie));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }

    [HttpPut("v1/especies/{id:int}")]
    public async Task<IActionResult> Put(
        [FromBody] EditorEspecieViewModel model,
        [FromRoute] int id
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Especie>(ModelState.GetErrors()));
        }
        try
        {
            var especie = await _context.Especies.FirstOrDefaultAsync(e => e.Id == id);
            if (especie == null)
            {
                return NotFound(new ResultViewModel<Especie>("Espécie não encontrada."));
            }

            especie.Nome = model.Nome;
            _context.Especies.Update(especie);
            await _context.SaveChangesAsync();

            return StatusCode(201, new ResultViewModel<Especie>(especie));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }

    
    [HttpDelete("v1/especies/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id
    )
    {
        try
        {
            var especie = await _context.Especies.FirstOrDefaultAsync(e => e.Id == id);
            if (especie == null)
            {
                return NotFound(new ResultViewModel<Especie>("Espécie não encontrada."));
            }
            
            _context.Especies.Remove(especie);
            await _context.SaveChangesAsync();

            return StatusCode(200, new ResultViewModel<Especie>(especie));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }


    public EspecieController(AdocaoDevDataContext context)
    {
        this._context = context;
    }
}