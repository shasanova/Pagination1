using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pustok2.Areas.Manage.ViewModels;
using Pustok2.DAL;
using Pustok2.Entities;
using Pustok2.Helpers;

namespace Pustok2.Areas.Manage.Controllers
{

    [Area("manage")]

    public class SliderController : Controller
    {
        private readonly Pustok2DbContext _context;
        private readonly IWebHostEnvironment _env;


        public SliderController(Pustok2DbContext context, IWebHostEnvironment env )
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page = 1)
        {
            var query = _context.Sliders.AsQueryable();
            return View(PaginatedList<Slider>.Create(query, page, 2));
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (!ModelState.IsValid) return View();

            if(slider.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "ImageFile is required");
                return View();
            }

            if(slider.ImageFile.Length > 2 *1024 * 1024)
            {
                ModelState.AddModelError("ImageFile", "ImageFile max size is 2MB");
                return View();
            }

            if (slider.ImageFile.ContentType!= "image/jpeg" && slider.ImageFile.ContentType!= "image/png")
            {
                ModelState.AddModelError("ImageFile", "ImageFile must be .jpg, .jpeg or .png");
                return View();

            }




            slider.ImageName = FileManager.Save(slider.ImageFile, _env.WebRootPath,"manage/uploads/sliders/");

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }




        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.Find(id);

            if (slider == null) return View("Error");

            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            Slider existSlider = _context.Sliders.Find(slider.Id);
            if (existSlider == null) return View("Error");

            existSlider.Title1 = slider.Title1;
            existSlider.Title2 = slider.Title2;
            existSlider.Description = slider.Description;
            existSlider.BtnText = slider.BtnText;
            existSlider.BtnUrl = slider.BtnUrl;
            existSlider.Order = slider.Order;


            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}

