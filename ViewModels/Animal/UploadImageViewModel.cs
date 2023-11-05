using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Animal;

public class UploadImageViewModel
{
    [Required(ErrorMessage = "Imagem inválida.")]
    public string[] Base64Images { get; set; }
}