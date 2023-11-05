using System.Text.RegularExpressions;
using Adocao.Data;
using Adocao.ExtensionMethods;
using Adocao.Models;
using Adocao.ViewModels;
using Adocao.ViewModels.Animal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Controllers;

[Authorize]
[ApiController]
public class AnimalController : ControllerBase
{
    private readonly AdocaoDevDataContext _context;
    
    [HttpGet("v1/admin/animais")]
    public async Task<IActionResult> Get(
        [FromQuery] string nome = "",
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 6
        )
    {
        try
        {
            var count = await _context.Animais.AsNoTracking().CountAsync();
            var animais = _context.Animais.AsNoTracking();

            
            if (!string.IsNullOrEmpty(nome))
            {
                animais = animais.Where(a => a.Nome.Contains(nome));
            }

            var listaAnimais = await animais  
                .Include(a => a.Especie)
                .Include(a => a.Raca)
                .Include(a => a.Fotos)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page = page,
                pageSize = pageSize,
                animais = listaAnimais
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Animal>("Erro: " + e.Message));
        }
    }
    
    [AllowAnonymous]
    [HttpGet("v1/active/animais")]
    public async Task<IActionResult> GetAllActive(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 12
        )
    {
        try
        {
            var count = await _context.Animais.AsNoTracking().CountAsync();
            var animais = await _context.Animais.AsNoTracking()
                .Include(a => a.Fotos)
                .Select(a => new ListAnimalsViewModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Local = a.Local,
                    Sexo = a.Sexo,
                    Imagens = a.Fotos
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                animais
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Animal>("Ops... Ocorreu algum erro."));
        }
    }
    
    [AllowAnonymous]
    [HttpGet("v1/filtrar/animais/")]
    public async Task<IActionResult> GetByFilter(
        [FromQuery] int especie = 0,
        [FromQuery] int raca = 0,
        [FromQuery] string local = "",
        [FromQuery] string porte = "",
        [FromQuery] string sexo = "",
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 12
        )
    {
        try
        {
            var count = await _context.Animais.AsNoTracking().CountAsync();
            var animais = _context.Animais.AsNoTracking().Where(a => a.Ativo == true);
            
            if (especie != 0)
            {
                animais = animais.Where(a => a.EspecieId == especie);
            }
            
            if (raca != 0)
            {
                animais = animais.Where(a => a.RacaId == raca);
            }

            if (!string.IsNullOrEmpty(local))
            {
                animais = animais.Where(a => a.Local.Contains(local.Trim()));
            }

            if (!string.IsNullOrEmpty(porte))
            {
                animais = animais.Where(a => a.Porte == porte);
            }
            
            if (!string.IsNullOrEmpty(sexo))
            {
                animais = animais.Where(a => a.Sexo == sexo);
            }
            
            var animaisFiltro = await animais
                .Include(a => a.Fotos)
                .Select(a => new ListAnimalsViewModel
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Local = a.Local,
                    Sexo = a.Sexo,
                    Imagens = a.Fotos
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page,
                pageSize,
                animaisFiltro
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Animal>("Ops... Ocorreu algum erro."));
        }
    }
    
    
    
    [AllowAnonymous]
    [HttpGet("v1/animais/{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        try
        {
            var animal = await _context
                .Animais
                .AsNoTracking()
                .Include(a => a.Especie)
                .Include(a => a.Raca)
                .Include(a => a.Fotos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
                return NotFound(new ResultViewModel<Animal>("Animal não encontrado"));

            return Ok(new ResultViewModel<Animal>(animal));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<Especie>("Falha interna no servidor"));
        }
    }
    
    [HttpPost("v1/animais")]
    public async Task<IActionResult> Post(
        [FromBody] EditorAnimalViewModel model
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Animal>(ModelState.GetErrors()));
        }

        var animalExistente = await _context.Animais.FirstOrDefaultAsync(a =>
            a.EspecieId == model.EspecieId && a.Nome == model.Nome && a.Idade == model.Idade);

        if (animalExistente != null)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Animal já existente."));
        }
        
        try
        {
            var animal = new Animal
            {
                Nome = model.Nome,
                Idade = model.Idade,
                Peso = model.Peso,
                Sobre = model.Sobre,
                Local = model.Local,
                Porte = model.Porte,
                Sexo = model.Sexo,
                EspecieId = model.EspecieId,
                RacaId = model.RacaId
            };
            _context.Animais.Add(animal);
            await _context.SaveChangesAsync();
            return StatusCode(201, new ResultViewModel<dynamic>(new
            {
                id = animal.Id,
                nome = animal.Nome
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }
    
    
    [HttpPut("v1/animais/{id:int}")]
    public async Task<IActionResult> Put(
        [FromBody] EditorAnimalViewModel model,
        [FromRoute] int id
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Animal>(ModelState.GetErrors()));
        }
        try
        {
            var animal = await _context.Animais.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return NotFound(new ResultViewModel<Raca>("Animal não encontrado."));
            }

            animal.Nome = model.Nome;
            animal.Idade = model.Idade;
            animal.Peso = model.Peso;
            animal.Sobre = model.Sobre;
            animal.Local = model.Local;
            animal.EspecieId = model.EspecieId;
            animal.RacaId = model.RacaId;
            
            _context.Animais.Update(animal);
            await _context.SaveChangesAsync();

            return StatusCode(201, new ResultViewModel<Animal>(animal));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Animal>("Ops... Algo deu errado."));
        }
    }
    
    [HttpDelete("v1/animais/{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id
    )
    {
        try
        {
            var animal = await _context.Animais.FirstOrDefaultAsync(a => a.Id == id);
            if (animal == null)
            {
                return NotFound(new ResultViewModel<Animal>("Animal não encontrado."));
            }
            
            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();

            return StatusCode(200, new ResultViewModel<Animal>(animal));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Animal>("Ops... Algo deu errado."));
        }
    }
    

    public AnimalController(AdocaoDevDataContext context)
    {
        this._context = context;
    }

}