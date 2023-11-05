using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Animal;

public class EditorAnimalViewModel
{
    [Required(ErrorMessage = "Campo nome é obrigatório.")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Campo idade é obrigatório.")]
    public int Idade { get; set; }
    public double? Peso { get; set; }
    [Required(ErrorMessage = "Campo sobre é obrigatório.")]
    [MaxLength(400, ErrorMessage = "Campo sobre deve ter no máximo 400 caracteres.")]
    public string Sobre { get; set; }
    [Required(ErrorMessage = "Campo local é obrigatório.")]
    public string Local { get; set; }
    [Required(ErrorMessage = "Campo sexo é obrigatório.")]
    public string Sexo { get; set; }
    [Required(ErrorMessage = "Campo porte é obrigatório.")]
    public string Porte { get; set; }
    
    [Required(ErrorMessage = "Espécie deve ser referenciada.")]
    public int EspecieId { get; set; }
    [Required(ErrorMessage = "Raça deve ser referenciada.")]
    public int RacaId { get; set; }
}