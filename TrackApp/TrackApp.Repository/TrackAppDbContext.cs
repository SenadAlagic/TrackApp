using System;
using Microsoft.EntityFrameworkCore;
using TrackApp.Core;

namespace TrackApp.Repository
{
	public class TrackAppDbContext : DbContext
	{
		public TrackAppDbContext()
		{
		}
		public TrackAppDbContext(DbContextOptions<TrackAppDbContext> options) :base(options)
		{

		}
		public DbSet<Item> Items { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ItemList> ItemLists { get; set; }
		public DbSet<List> Lists { get; set; }
		public DbSet<Purchase> Purchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=TrackApp;User Id=admin;Password=valens;\n");
        }
    }
}

