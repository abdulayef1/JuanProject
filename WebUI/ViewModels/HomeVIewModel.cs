using Core.Models;
using DataAccess.Contexts;

namespace WebUI.ViewModels
{
    public class HomeVIewModel
    {
        public IEnumerable<SlideItems> SlideItems { get; set; } = null!;

    }
}
