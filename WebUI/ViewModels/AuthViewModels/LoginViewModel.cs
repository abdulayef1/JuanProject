
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels.AuthViewModels;

public class LoginViewModel
{
    [Required]
    public string? EmailOrUsername { get; set; }


    [Required,DataType(DataType.Password)]
    public string? Password { get; set; }

    public bool RememberMe { get; set; }
}
