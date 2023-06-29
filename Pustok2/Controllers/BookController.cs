using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok2.DAL;
using Pustok2.Entities;
using Pustok2.ViewModels;

namespace Pustok2.Controllers
{
    public class BookController : Controller
    {

        private readonly Pustok2DbContext _context;

        public BookController(Pustok2DbContext context)
        {
            _context = context;
        }


        public IActionResult GetDetail(int id)
        {
            Book book = _context.Books.Include(x => x.BookImages)
                                      .Include(x => x.Genre)
                                      .Include(x => x.Author)
                                      .FirstOrDefault(x => x.Id == id);

            return PartialView("_BookModalPartial", book);
        }

        public IActionResult AddToBasket(int id)
        {
            var basketStr = Request.Cookies["basket"];

            List<BasketCookieViewModel> cookieItems = null;

            if (basketStr == null)
            {
                cookieItems = new List<BasketCookieViewModel>();
            }
            else
                cookieItems = JsonConvert.DeserializeObject<List<BasketCookieViewModel>>(basketStr);


            BasketCookieViewModel cookieItem = cookieItems.FirstOrDefault(x => x.BookId == id);

            if(cookieItem == null)
            {
                cookieItem = new BasketCookieViewModel()
                {
                    BookId = id,
                    Count = 1
                };
                cookieItems.Add(cookieItem);
            }
            else
            {
                cookieItem.Count++;
            }

            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(cookieItems));

            BasketViewModel basketVM = new BasketViewModel();
            foreach (var ci in cookieItems)
            {
                BasketItemViewModel item = new BasketItemViewModel()
                {
                    Count = ci.Count,
                    Book = _context.Books.Include(x => x.BookImages.Where(x => x.PosterStatus == true)).FirstOrDefault(x => x.Id == ci.BookId)
                };
                basketVM.Items.Add(item);
                basketVM.TotalAmount += (item.Book.DiscountPercent > 0 ? item.Book.SalePrice * (100 - item.Book.DiscountPercent) / 100 : item.Book.SalePrice) * item.Count;
            }
            return PartialView("_BasketPartial",basketVM);
            
        }

        public IActionResult ShowBasket()
        {
            var dataStr = HttpContext.Request.Cookies["basket"];
            var data = JsonConvert.DeserializeObject<List<BasketCookieViewModel>>(dataStr);
            return Json(data);
        }

    }
}

