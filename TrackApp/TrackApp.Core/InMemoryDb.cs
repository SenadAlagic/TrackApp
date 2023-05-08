using System;
namespace TrackApp.Core
{
	public class InMemoryDb
	{
		public static List<Item> Items = new List<Item>(); 
		public static List<List> Lists = new List<List>(); 
		public static List<ItemList> ItemsLists = new List<ItemList>();
		public static List<Category> Categories = GenerateCategories();

        public static List<Category>GenerateCategories()
		{
			return new List<Category>()
			{
				new Category() { Id = 1, Name = "Produce" },
				new Category() { Id = 2, Name = "Hygiene" }
			};
        }
	}
}

