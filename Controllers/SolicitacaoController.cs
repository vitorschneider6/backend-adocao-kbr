using Adocao.Data;
using Adocao.ExtensionMethods;
using Adocao.Models;
using Adocao.Services;
using Adocao.ViewModels;
using Adocao.ViewModels.Solicitacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adocao.Controllers;

[Authorize]
[ApiController]
public class SolicitacaoController : ControllerBase
{
    private readonly AdocaoDevDataContext _context;
    
    
    [HttpGet("v1/solicitacoes")]
    public async Task<IActionResult> Get(
        [FromQuery] DateTime? dataFinal,
        [FromQuery] DateTime? dataInicial,
        [FromQuery] string nomeSolicitante = "",
        [FromQuery] string nomeAnimal = "",
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 6
        )
    {
        try
        {
            var count = await _context.Solicitacoes.AsNoTracking().CountAsync();
            var solicitacoes = _context.Solicitacoes.AsNoTracking();

            if (dataFinal != null)
            {
                solicitacoes = solicitacoes.Where(s => s.DataSolicitacao <= dataFinal);
            }

            if (dataInicial != null)
            {
                solicitacoes = solicitacoes.Where(s => s.DataSolicitacao >= dataInicial);
            }
            
            solicitacoes = solicitacoes.Include(s => s.Animal);

            if (!string.IsNullOrEmpty(nomeAnimal))
            {
                solicitacoes = solicitacoes.Where(s => s.Animal.Nome.Contains(nomeAnimal));
            }
            
            if (!string.IsNullOrEmpty(nomeSolicitante))
            {
                solicitacoes = solicitacoes.Where(s => s.NomeSolicitante.Contains(nomeSolicitante));
            }
            var listaSolicitacoes = await solicitacoes
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderBy(s => s.DataSolicitacao)
                .ToListAsync();
            return Ok(new ResultViewModel<dynamic>(new
            {
                total = count,
                page = page,
                pageSize = pageSize,
                solicitacoes = listaSolicitacoes
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, "Ops... Algo eu errado.");
        }
    }
    
    [HttpGet("v1/solicitacoes/{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        try
        {
            var solicitacao = await _context.Solicitacoes
                .AsNoTracking()
                .Include(s => s.Animal)
                .Include(s => s.Animal.Raca)
                .Include(s => s.Animal.Especie)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitacao == null)
            {
                return NotFound(new ResultViewModel<SolicitacaoAdocaoAnimais>("Solicitação não encontrada!"));
            }
            return Ok(new ResultViewModel<SolicitacaoAdocaoAnimais>(solicitacao));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Raca>("Ops... Algo deu errado."));
        }
    }
    
    [AllowAnonymous]
    [HttpPost("v1/solicitacoes")]
    public async Task<IActionResult> Post(
        [FromBody] CreateSolicitacaoViewModel model,
        [FromServices] EmailService emailService
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<SolicitacaoAdocaoAnimais>(ModelState.GetErrors()));
        }

        try
        {

            var solicitacao = new SolicitacaoAdocaoAnimais()
            {
                NomeSolicitante = model.NomeSolicitante,
                Cpf = model.Cpf,
                Email = model.Email,
                Celular = model.Celular,
                DataNascimento = model.DataNascimento,
                AnimalId = model.AnimalId
                
            };
            _context.Solicitacoes.Add(solicitacao);
            await _context.SaveChangesAsync();
            var nomeAnimal = await _context.Animais.Where(a => a.Id == solicitacao.AnimalId).Select(a => a.Nome).FirstOrDefaultAsync();
            var corpoEmail = $"Olá, muito obrigado pelo interesse em adotar!<br>" +
                             $"Segue informações da sua solicitação:<br>" +
                             $"Nome: {solicitacao.NomeSolicitante}<br>" +
                             $"Celular: {solicitacao.Celular}<br>" +
                             $"Data de nascimento: {solicitacao.DataNascimento}<br>" +
                             $"Nome do animal: {nomeAnimal}";
            var teste = emailService.sendMessage(
                solicitacao.NomeSolicitante,
                solicitacao.Email,
                "Solicitação de adoção",
                corpoEmail);
            return StatusCode(201, new ResultViewModel<SolicitacaoAdocaoAnimais>(solicitacao));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<Especie>("Ops... Algo deu errado."));
        }
    }

    public SolicitacaoController(AdocaoDevDataContext context)
    {
        this._context = context;
    }
}