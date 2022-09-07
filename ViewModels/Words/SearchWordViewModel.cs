using System.ComponentModel.DataAnnotations;

namespace Coodesh.ViewModels.Accounts;

public class SearchWordViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string Name { get; set; }
    
}