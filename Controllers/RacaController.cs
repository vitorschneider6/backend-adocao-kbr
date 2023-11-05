using Adocao.Data;
using Adocao.ExtensionMethods;
using Adocao.Models;
using Adocao.ViewModels;
using Adocao.ViewModels.Raca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Controllers;
[Authorize]
[ApiController]
public class RacaController : ControllerBase
{
    private readonly AdocaoDevDataContext _context;
    
    [HttpGet("v1/admin/racas")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var racas = await _context.Racas.ToListAsync();
            return Ok(new ResultViewModel<List<Raca>>(racas));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Raca>("Ops... Algo deu errado."));
        }
    }
    
    
    [AllowAnonymous]
    [HttpGet("v1/racas/{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        try
        {
            var raca = await _context.Racas.FirstOrDefaultAsync(r => r.Id == id);

            if (raca == null)
            {
                return NotFound(new ResultViewModel<Raca>("Raça não encontrado!"));
            }
            return Ok(new ResultViewModel<Raca>(raca));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Raca>("Ops... Algo deu errado."));
        }
    }
    
    [AllowAnonymous]
    [HttpGet("v1/all/racas/{id:int}")]
    public async Task<IActionResult> GetByEspecie(
        [FromRoute] int id
            )
    {
        try
        {
            var raca = await _context.Racas.AsNoTracking().Where(r => r.EspecieId == id).ToListAsync();
            
            return Ok(new ResultViewModel<List<Raca>>(raca));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Raca>("Ops... Algo deu errado."));
        }
    }
    
    
    [HttpPost("v1/racas")]
    public async Task<IActionResult> Post(
        [FromBody] EditorRacaViewModel model
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Raca>(ModelState.GetErrors()));
        }

        try
        {
            var raca = new Raca
            {
                Nome = model.Nome,
                Descricao = model.Descricao,
                EspecieId = model.EspecieId
            };
            _context.Racas.Add(raca);
            await _context.SaveChangesAsync();
            return StatusCode(201, new ResultViewModel<Raca>(raca));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }
    
    
    [HttpPut("v1/racas/{id:int}")]
    public async Task<IActionResult> Put(
        [FromBody] EditorRacaViewModel model,
        [FromRoute] int id
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Raca>(ModelState.GetErrors()));
        }
        try
        {
            var raca = await _context.Racas.FirstOrDefaultAsync(r => r.Id == id);
            if (raca == null)
            {
                return NotFound(new ResultViewModel<Raca>("Raça não encontrada."));
            }

            raca.Nome = model.Nome;
            raca.Descricao = model.Descricao;
            raca.EspecieId = model.EspecieId;
            _context.Racas.Update(raca);
            await _context.SaveChangesAsync();

            return StatusCode(201, new ResultViewModel<Raca>(raca));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }
    
    [HttpDelete("v1/racas/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id
    )
    {
        try
        {
            var raca = await _context.Racas.FirstOrDefaultAsync(r => r.Id == id);
            if (raca == null)
            {
                return NotFound(new ResultViewModel<Raca>("Raça não encontrada."));
            }
            
            _context.Racas.Remove(raca);
            await _context.SaveChangesAsync();

            return StatusCode(200, new ResultViewModel<Raca>(raca));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }

    
    public RacaController(AdocaoDevDataContext context)
    {
        this._context = context;
    }
}