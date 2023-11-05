namespace Adocao.Models;

public class RecuperacaoSenha
{
    public Guid Id { get; set; }
    public Guid AdministradorId { get; set; }
    public DateTime Expiration { get; set; }
    public Administrador Administrador { get; set; }
}