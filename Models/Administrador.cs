namespace Adocao.Models;
public class Administrador
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public RecuperacaoSenha Recuperacao { get; set; }
}