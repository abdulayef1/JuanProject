using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewModels.SiderViewModels;

public class UpdateSlideItemVM
{



    public IFormFile? Image { get; set; }
   
    public string? ImageName { get; set; }
    
    [Required, MaxLength(20)]
    public string? Tittle { get; set; }
    
    [Required, MaxLength(20)]
    public string? SubTittle { get; set; }
    
    [Required, MaxLength(100)]
    public string? Description { get; set; }
}
