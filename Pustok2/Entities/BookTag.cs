using System;
namespace Pustok2.Entities
{
	public class BookTag
	{
		public int BookId { get; set; }

        public int TagId { get; set; }

        public Book Book { get; set; }

        public Tag Tag { get; set; }


    }
}

