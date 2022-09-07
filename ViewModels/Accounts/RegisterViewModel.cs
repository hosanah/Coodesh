using System.ComponentModel.DataAnnotations;

namespace Coodesh.ViewModels.Accounts;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "O E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O E-mail é inválido")]
    public string Email { get; set; }
}