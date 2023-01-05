using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            HomeVIewModel homeVIewModel=new HomeVIewModel{
                SlideItems=_context.SlideItems.AsNoTracking()
            };
            return View(homeVIewModel);
        }
    }
}
