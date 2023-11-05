using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Raca;

public class EditorRacaViewModel
{
    [Required(ErrorMessage = "Campo nome é obrigatório.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Campo descrição é obrigatório.")]
    [MaxLength(400, ErrorMessage = "Campo descrição deve ter no máximo 400 caracteres.")]
    public string Descricao { get; set; }
    [Required(ErrorMessage = "A espécie deve ser referenciado.")]
    public int EspecieId { get; set; }
}