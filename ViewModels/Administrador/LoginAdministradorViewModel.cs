using System.ComponentModel.DataAnnotations;

namespace Adocao.ViewModels.Administrador;

public class LoginAdministradorViewModel
{
    [Required(ErrorMessage = "Campo email é obrigatório")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Campo senha é obrigatório")]
    public string Senha { get; set; }
}