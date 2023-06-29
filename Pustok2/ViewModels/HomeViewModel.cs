using System;
using Pustok2.Entities;

namespace Pustok2.ViewModels
{
	public class HomeViewModel
	{
		public List<Slider> ?Sliders { get; set; }
        public List<Feature> ?Features { get; set; }
        public List<Book> ?FeaturedBooks { get; set; }
        public List<Book> ?NewBooks { get; set; }
        public List<Book> ?DiscountedBooks { get; set; }
    }
}

