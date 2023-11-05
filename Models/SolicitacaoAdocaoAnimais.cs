namespace Adocao.Models;
public class SolicitacaoAdocaoAnimais
{
    public int Id { get; set; }
    public string NomeSolicitante { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string Celular { get; set; }
    public DateTime DataNascimento { get; set; }
    public DateTime DataSolicitacao { get; set; }
    public int AnimalId { get; set; }
    public Animal Animal { get; set; }

}