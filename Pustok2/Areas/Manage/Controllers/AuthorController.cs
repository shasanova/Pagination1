using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok2.Areas.Manage.ViewModels;
using Pustok2.DAL;
using Pustok2.Entities;

namespace Pustok2.Areas.Manage.Controllers
{

    [Area("manage")]

    public class AuthorController : Controller
    {

        private readonly Pustok2DbContext _context;

        public AuthorController(Pustok2DbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page=1)
        {
            var query = _context.Authors.Include(x => x.Books);

            return View(PaginatedList<Author>.Create(query, page, 2));
        }




        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Authors.Add(author);

            _context.SaveChanges();

            return RedirectToAction("index");
        }




        public IActionResult Edit(int id)
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == id);

            if (author == null) return View("error");


            return View(author);
        }


        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Author existAuthor = _context.Authors.FirstOrDefault(x => x.Id == author.Id);

            if (author == null) return View("error");

            existAuthor.Fullname = author.Fullname;

            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}

