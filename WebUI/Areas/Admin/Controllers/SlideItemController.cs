using Core.Models;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NuGet.ContentModel;
using WebUI.Areas.Admin.ViewModels;
using WebUI.Areas.Admin.ViewModels.SiderViewModels;
using WebUI.Utilities;
using static NuGet.Packaging.PackagingConstants;

namespace WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class SlideItemController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private int _count;
    public SlideItemController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _count = _context.SlideItems.Count();
    }

    public IActionResult Index()
    {
        ViewBag.Count = _count;
        return View(_context.SlideItems);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var slideItem = await _context.SlideItems.FindAsync(id);
        if (slideItem == null) return NotFound();
        return View(slideItem);
    }

    public IActionResult Creat()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Creat(SlideItemVM slideItemVM)
    {
        if (!ModelState.IsValid) return View(slideItemVM);
        if (slideItemVM.Image == null) return NotFound();
        if (!slideItemVM.Image.CheckFileSize(300))
        {
            ModelState.AddModelError("Image", "Image size must be less than 300 kb");
            return View(slideItemVM);
        }
        if (!slideItemVM.Image.CheckFileFormat("image/"))
        {
            ModelState.AddModelError("Image", "You must choose image file type");
            return View(slideItemVM);
        }
        string? wwwroot = _webHostEnvironment.WebRootPath;
        string imageName = slideItemVM.Image.CopyFilee(wwwroot, "assets", "img", "slider");

        SlideItems slideItem = new SlideItems
        {
            Description = slideItemVM.Description,
            Tittle = slideItemVM.Tittle,
            SubTittle = slideItemVM.SubTittle,
            Image = imageName
        };
        _context.SlideItems.Add(slideItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (_count == 1)
        {
            return BadRequest();
        }
        var slideItem = await _context.SlideItems.FindAsync(id);
        if (slideItem == null) return NotFound();
        return View(slideItem);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
        if (_count == 1)
        {
            return BadRequest();
        }
        var slideItem = await _context.SlideItems.FindAsync(id);
        if (slideItem == null) return NotFound();
        if (slideItem.Image.ToString() == null) return NotFound();

        string fileName = slideItem.Image;
        string? wwwroot = _webHostEnvironment.WebRootPath;

        try
        {
            Helper.DelteFile(wwwroot, "assets", "img", "slider", fileName);
        }
        catch (IOException ioExp)
        {
            return BadRequest(ioExp.Message);
        }

        _context.SlideItems.Remove(slideItem);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {

        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        UpdateSlideItemVM updateSlideItem = new UpdateSlideItemVM
        {
            Tittle = model.Tittle,
            SubTittle = model.SubTittle,
            Description = model.Description,
            ImageName = model.Image
        };

        return View(updateSlideItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, UpdateSlideItemVM updateSlideItem)
    {

        if (!ModelState.IsValid) return View(updateSlideItem);
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        updateSlideItem.ImageName=model.Image;

        if (updateSlideItem.Image != null)
        {

            if (!updateSlideItem.Image.CheckFileSize(300))
            {
                ModelState.AddModelError("Image", "Image size must be less than 300 kb");
                return View(updateSlideItem);
            }
            if (!updateSlideItem.Image.CheckFileFormat("image/"))
            {
                ModelState.AddModelError("Image", "You must choose image file type");
                return View(updateSlideItem);
            }
            string? wwwroot = _webHostEnvironment.WebRootPath;
            updateSlideItem.ImageName = updateSlideItem.Image.CopyFilee(wwwroot, "assets", "img", "slider");

            try
            {
                Helper.DelteFile(wwwroot, "assets", "img", "slider", model.Image);
            }
            catch (IOException ioExp)
            {
                return BadRequest(ioExp.Message);
            }
            

        }


        model.Description = updateSlideItem.Description;
        model.Tittle = updateSlideItem.Tittle;
        model.SubTittle = updateSlideItem.SubTittle;
        model.Image = updateSlideItem.ImageName;

        _context.SaveChanges();
        return RedirectToAction(nameof(Index));


    }
}
