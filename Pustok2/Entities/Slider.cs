using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok2.Entities
{
	public class Slider
	{
		public int Id { get; set; }

        public int Order { get; set; }


        [MaxLength(50)]
        public string Title1 { get; set; }

        [MaxLength(50)]
        public string Title2 { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [MaxLength(150)]
        public string BtnText { get; set; }

        [MaxLength(150)]
        public string BtnUrl { get; set; }

        [MaxLength(100)]
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }


    }
}

