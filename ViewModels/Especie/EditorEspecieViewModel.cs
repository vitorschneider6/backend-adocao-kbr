using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Especie;

public class EditorEspecieViewModel
{
    [Required(ErrorMessage = "Campo nome é obrigatório")]
    public string Nome { get; set; }
}