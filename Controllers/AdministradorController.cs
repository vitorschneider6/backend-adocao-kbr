using Adocao.Data;
using Adocao.ExtensionMethods;
using Adocao.Models;
using Adocao.ViewModels;
using Adocao.ViewModels.Administrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Adocao.Controllers;

[Authorize]
[ApiController]
public class AdministradorController : ControllerBase
{
    private AdocaoDevDataContext _context;
    // READ - GET
    [HttpGet("/v1/administradores")]
    public async Task<IActionResult> Get(
        [FromQuery] string nome = "",
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 6
    )
    
    {
        try
        {
            var count = await _context.Administradores.AsNoTracking().CountAsync();
            var administradores = _context.Administradores.AsNoTracking();
            if (!string.IsNullOrEmpty(nome))
            {
                administradores = administradores.Where(a => a.Nome.Contains(nome));
            }

            var listaAdministradores = await administradores
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page = page,
                pageSize = pageSize,
                administradores =listaAdministradores
            }));
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<Administrador>("Falha interna no servidor."));
        }
    }
    
    // READ - GET by Id
    [AllowAnonymous]
    [HttpGet("v1/administradores/{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id)
    {
        try
        {
            var administrador = await _context
                .Administradores
                .FirstOrDefaultAsync(x => x.Id == id);

            if (administrador == null)
                return NotFound(new ResultViewModel<Administrador>("Administrador não encontrado"));

            return Ok(new ResultViewModel<Administrador>(administrador));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Administrador>("Falha interna no servidor"));
        }
    }
    

    
    // UPDATE - PUT
    [HttpPut("v1/administradores/{id:guid}")]
    public async Task<IActionResult> Put(
        [FromBody] UpdateAdministradorViewModel model,
        [FromRoute] Guid id
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Administrador>(ModelState.GetErrors()));
        }
        var administrador = await _context.Administradores.FirstOrDefaultAsync(a => a.Id == id);

        if (administrador == null)
        {
            return NotFound(new ResultViewModel<Administrador>("Administrador não encontrado."));
        }
        
        administrador.Email = model.Email;
        administrador.Nome = model.Nome;
        if (!string.IsNullOrEmpty(model.Senha) && model.Senha != "")
        {
            administrador.Senha = PasswordHasher.Hash(model.Senha.Trim());
        }

        _context.Administradores.Update(administrador);
        await _context.SaveChangesAsync();
        return Created($"v1/administradores/{administrador.Id}", new ResultViewModel<Administrador>(administrador));
        
    }
    
    // DELETE
    [HttpDelete("v1/administradores/{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id
    )
    {
        var administrador = await _context.Administradores.FirstOrDefaultAsync(a => a.Id == id);

        if (administrador == null)
        {
            return NotFound(new ResultViewModel<Administrador>("Administrador não encontrado."));
        }
        
        _context.Administradores.Remove(administrador);
        await _context.SaveChangesAsync();
        
        return Ok(new ResultViewModel<Administrador>(administrador));
    }


    public AdministradorController(AdocaoDevDataContext context)
    {
        this._context = context;
    }
}