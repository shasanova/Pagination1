using System;
using Microsoft.EntityFrameworkCore;
using Pustok2.Entities;

namespace Pustok2.DAL
{
	public class Pustok2DbContext:DbContext
	{
		public Pustok2DbContext(DbContextOptions<Pustok2DbContext> options):base(options){ }


		public DbSet<Slider> Sliders { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookImage> BookImages { get; set; }

        public DbSet<BookTag> BookTags { get; set; }

        public DbSet<Feature> Features { get; set; }
        public DbSet<Setting> Settings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookTag>().HasKey(x => new { x.BookId, x.TagId });
            base.OnModelCreating(modelBuilder);
        }

    }
}

