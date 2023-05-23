using System;
namespace TrackApp.Core
{
	public class InMemoryDb
	{
		public static List<Item> Items = GenerateItems(); 
		public static List<List> Lists = GenerateLists(); 
		public static List<ItemList> ItemsLists = GenerateItemLists();
		public static List<Category> Categories = GenerateCategories();
        public static List<Purchase> Purchases=new List<Purchase>();

        public static List<Category>GenerateCategories()
		{
			return new List<Category>()
			{
				new Category() { CategoryId = 1, Name = "Produce" },
				new Category() { CategoryId = 2, Name = "Sweets" },
                new Category() { CategoryId = 3, Name = "Hygiene" }
			};
        }
        public static List<Item> GenerateItems()
        {
            return new List<Item>()
            {
                new Item() { ItemId=1, Name="Water", Unit="l", CategoryId=1},
                new Item() { ItemId=2, Name="Eggs", Unit="carton(s)", CategoryId=1},
                new Item() { ItemId=3, Name="Tissues", Unit="box(es)", CategoryId=2},
            };
        }
        public static List<ItemList> GenerateItemLists()
        {
            return new List<ItemList>()
            {
                new ItemList { ItemListId=1, ItemId=1, ListId=1, Quantity=5, DateCreated=DateTime.Now, DateModified=DateTime.Now },
                new ItemList { ItemListId=2, ItemId=2, ListId=1, Quantity=2, DateCreated=DateTime.Now, DateModified=DateTime.Now }
            };
        }

        public static List<List> GenerateLists()
        {
            return new List<List>()
            {
                new List { ListId=1, TotalPrice=0, MonthOfYear=DateOnly.FromDateTime(DateTime.Now), DateModified=DateTime.Now, IsVisible=true, CurrentWorkingList=true},
                new List { ListId=2, TotalPrice=100, MonthOfYear=DateOnly.FromDateTime(DateTime.Now), DateModified=DateTime.Now, IsVisible=true, CurrentWorkingList=false}
            };
        }
    }
}

