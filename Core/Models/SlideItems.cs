using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class SlideItems
{
    public int Id { get; set; } 
    [Required]
    public string? Image { get; set; }
    [Required, MaxLength(20)]
    public string? Tittle { get; set; }
    [Required,MaxLength(20)]
    public string? SubTittle { get; set; }
    [Required,MaxLength(100)]
    public string? Description { get; set; }

}
