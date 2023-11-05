using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Administrador;

public class CreateAdministradorViewModel : UpdateAdministradorViewModel
{
    [Required(ErrorMessage = "Campo senha é obrigatório")]
    [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve conter de 8 a 20 caracteres")]
    public string Senha { get; set; }
}