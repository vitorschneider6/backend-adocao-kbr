using Adocao.Data;
using Adocao.ExtensionMethods;
using Adocao.Models;
using Adocao.Services;
using Adocao.ViewModels;
using Adocao.ViewModels.Administrador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Adocao.Controllers;
[Authorize]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly AdocaoDevDataContext _context;
    
    
    [AllowAnonymous]
    [HttpPost("v1/login")]
    public async Task<IActionResult> Auth(
        [FromBody] LoginAdministradorViewModel model
    )
    {
        if (!ModelState.IsValid)
        {
            List<string> listaErros;
            listaErros = ModelState.GetErrors();
            return BadRequest(new ResultViewModel<Administrador>(listaErros));
        }

        var administrador = await _context.Administradores.AsNoTracking().FirstOrDefaultAsync(a => a.Email == model.Email);

        if (administrador == null)
        {
            return StatusCode(401, new ResultViewModel<Administrador>("Email ou senha incorretos."));
        }

        if (!PasswordHasher.Verify(administrador.Senha, model.Senha))
        {
            return StatusCode(401, new ResultViewModel<Administrador>("Email ou senha incorretos."));
        }

        try
        {
            return Ok(new ResultViewModel<object>(_tokenService.GerarToken(administrador)));
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<Administrador>("Erro interno no servidor."));
        }
        
        
    }
    
    [AllowAnonymous]
    [HttpPost("/v1/register")]
    public async Task<IActionResult> Post(
        [FromBody] CreateAdministradorViewModel model,
        [FromServices] EmailService emailService
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultViewModel<Administrador>(ModelState.GetErrors()));
        }
        
        var administrador = new Administrador
        {
            Nome = model.Nome,
            Email = model.Email.Trim(),
            Senha = PasswordHasher.Hash(model.Senha.Trim())
        };
        
        try
        {
            _context.Administradores.Add(administrador);
            await _context.SaveChangesAsync();


            
            return StatusCode(201, new ResultViewModel<dynamic>(new
            {
                administrador.Id,
                administrador.Nome,
                administrador.Email,
                model.Senha,
            }));
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<Administrador>("Erro: " + e.Data));
        }
       
    }
    
    public AccountController(TokenService tokenService, AdocaoDevDataContext context)
    {
        this._tokenService = tokenService;
        this._context = context;
    }


    [AllowAnonymous]
    [HttpPost("v1/gerar-recuperacao/{email}")]
    public async Task<IActionResult> RecuperarSenha(
            [FromRoute] string email,
            [FromServices] EmailService emailService
        )
    {
        var administrador = await _context.Administradores.FirstOrDefaultAsync(a => a.Email == email);

        if (administrador == null)
        {
            return StatusCode(200, "Conta inexistente.");
        }

        var recuperacaoSenhaOld = await
            _context.RecuperacaoSenhas.FirstOrDefaultAsync(r => r.AdministradorId == administrador.Id);


        if (recuperacaoSenhaOld != null)
        {
            _context.RecuperacaoSenhas.Remove(recuperacaoSenhaOld);
            await _context.SaveChangesAsync();
        }

        var recuperacaoSenha = new RecuperacaoSenha
        {
            AdministradorId = administrador.Id,
            Expiration = DateTime.Now.AddHours(2)
        };

        _context.RecuperacaoSenhas.Add(recuperacaoSenha);
        await _context.SaveChangesAsync();

        try
        {
            emailService.sendMessage(administrador.Nome, email,
                "Recuperação de senha",
                $"Seu link de recuperação de senha é: http://localhost:4200/trocar-senha/{recuperacaoSenha.Id}", "Adoção KBR");
            return Ok("Link enviado com sucesso!");
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<RecuperacaoSenha>("Erro interno no servidor"));
        }
    }

    [AllowAnonymous]
    [HttpPut("v1/recuperar-senha/{codigo:guid}")]
    public async Task<IActionResult> RecuperarSenha(
        [FromRoute] Guid codigo,
        [FromBody] UpdatePasswordAdministradorViewModel model
    )
    {
        if (!ModelState.IsValid)
        {
            var erros = ModelState.GetErrors();
            return BadRequest(new ResultViewModel<RecuperacaoSenha>(erros));
        }
        
        var recuperacaoSenha = await _context.RecuperacaoSenhas
            .Include(r => r.Administrador).
            AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == codigo);

        if (recuperacaoSenha == null)
        {
            return StatusCode(404,
                new ResultViewModel<RecuperacaoSenha>("Ocorreu um erro ao tentar realizar a sua solicitação."));
        }

        if (recuperacaoSenha.Expiration < DateTime.Now)
        {
            return StatusCode(401, new ResultViewModel<RecuperacaoSenha>($"O código não é mais válido {recuperacaoSenha.Expiration} e ${DateTime.Now}"));
        }

        var administrador = recuperacaoSenha.Administrador;
        administrador.Senha = PasswordHasher.Hash(model.Senha.Trim());

        _context.Administradores.Update(administrador);
        await _context.SaveChangesAsync();

        _context.RecuperacaoSenhas.Remove(recuperacaoSenha);
        await _context.SaveChangesAsync();

        return StatusCode(201, "Senha alterada com sucesso.");
    }

    [HttpPost("v1/auth")]
    public IActionResult Authorize(
        )
    {
        try
        {
            return Ok("Usuário logado");
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewModel<string>("Ops... Algo deu errado."));
        }
    }
}