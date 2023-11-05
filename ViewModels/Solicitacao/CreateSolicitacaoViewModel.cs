using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Solicitacao;

public class CreateSolicitacaoViewModel
{
    [Required (ErrorMessage = "Campo nome é obrigatório.")]
    public string NomeSolicitante { get; set; }
    [Required (ErrorMessage = "Campo CPF é obrigatório.")]
    [MaxLength(11, ErrorMessage = "Campo CPF deve conter no máximo 11 caracteres.")]
    public string Cpf { get; set; }
    [Required (ErrorMessage = "Campo email é obrigatório.")]
    public string Email { get; set; }
    [Required (ErrorMessage = "Campo celular é obrigatório.")]
    public string Celular { get; set; }
    [Required (ErrorMessage = "Campo data de nascimento é obrigatória.")]
    public DateTime DataNascimento { get; set; }
    [Required (ErrorMessage = "É necessário referenciar algum animal.")]
    public int AnimalId { get; set; }
}