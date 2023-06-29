using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok2.DAL;
using Pustok2.Entities;
using Pustok2.ViewModels;

namespace Pustok2.Services
{
    public class LayoutService
    {
        private readonly Pustok2DbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public LayoutService(Pustok2DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public Dictionary<string, string> GetSettings()
        {

            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }



        public BasketViewModel GetBasket()
        {
            var basketVM = new BasketViewModel();

            var basketSTr = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

            List<BasketCookieViewModel> cookieItems = null;

            if (basketSTr == null)
            {
                cookieItems = new List<BasketCookieViewModel>();
            }
            else
                cookieItems = JsonConvert.DeserializeObject<List<BasketCookieViewModel>>(basketSTr);




            foreach (var cookieItem in cookieItems)
            {
                BasketItemViewModel item = new BasketItemViewModel
                {
                    Count = cookieItem.Count,
                    Book = _context.Books.Include(x => x.BookImages).FirstOrDefault(x => x.Id == cookieItem.BookId)
                };

                basketVM.Items.Add(item);
                basketVM.TotalAmount += (item.Book.DiscountPercent > 0 ? item.Book.SalePrice * (100 - item.Book.DiscountPercent) / 100 : item.Book.SalePrice) * item.Count;
            }

            return basketVM;

        }
    }
}
