using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewModels;

public class SlideItemVM
{
    [Required]
    public IFormFile? Image { get; set; }
    [Required, MaxLength(20)]
    public string? Tittle { get; set; }
    [Required, MaxLength(20)]
    public string? SubTittle { get; set; }
    [Required, MaxLength(100)]
    public string? Description { get; set; }
}
