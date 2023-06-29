using System;
using System.ComponentModel.DataAnnotations;

namespace Pustok2.Entities
{
	public class Author
	{
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Fullname { get; set; }

        public ICollection<Book> ?Books { get; set; }

    }
}

