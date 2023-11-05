using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Adocao.ViewModels.Administrador;

public class UpdateAdministradorViewModel
{
    [Required(ErrorMessage = "Campo nome é obrigatório.")]
    [MinLength(3, ErrorMessage = "Nome deve conter no mínimo 3 caracteres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Campo email é obrigatório")]
    public string Email { get; set; }
    
    [AllowNull]
    public string Senha { get; set; }
}