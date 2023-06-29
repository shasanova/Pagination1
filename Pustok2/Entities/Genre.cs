using System;
using System.ComponentModel.DataAnnotations;

namespace Pustok2.Entities
{
	public class Genre
	{
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage ="MaxLength of Name is 20!")]
        public string GenreName { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}

