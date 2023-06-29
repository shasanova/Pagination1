using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok2.DAL;
using Pustok2.Entities;
using Pustok2.ViewModels;

namespace Pustok1.Controllers
{
    public class HomeController : Controller
    {
        private readonly Pustok2DbContext _context;

        public HomeController(Pustok2DbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                Features = _context.Features.Take(4).ToList(),
                FeaturedBooks = _context.Books
                                     .Include(x => x.Author)
                                     .Include(x => x.BookImages.Where(x=>x.PosterStatus !=null))
                                     .Where(x => x.IsFeatured).Take(10).ToList(),
                NewBooks = _context.Books
                                     .Include(x => x.Author)
                                     .Include(x => x.BookImages.Where(x => x.PosterStatus != null))
                                     .Where(x => x.IsNew).Take(10).ToList(),
                DiscountedBooks = _context.Books
                                     .Include(x => x.Author)
                                     .Include(x => x.BookImages.Where(x => x.PosterStatus != null))
                                     .Where(x => x.DiscountPercent>0).Take(10).ToList()

            };
            return View(model);
        }
    }
}

