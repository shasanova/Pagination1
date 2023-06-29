using System;
using System.ComponentModel.DataAnnotations;

namespace Pustok2.Entities
{
	public class Tag
	{
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string GenreName { get; set; }

        public List<BookTag> BookTags { get; set; }

    }
}

