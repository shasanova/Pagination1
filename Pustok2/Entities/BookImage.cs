using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok2.Entities
{
    public class BookImage
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ImageName { get; set; }
        public bool? PosterStatus { get; set; }


        public Book Book { get; set; }

    }
}
