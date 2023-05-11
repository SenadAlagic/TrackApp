﻿using System;
namespace TrackApp.Core
{
	public class InMemoryDb
	{
		public static List<Item> Items = GenerateItems(); 
		public static List<List> Lists = GenerateLists(); 
		public static List<ItemList> ItemsLists = GenerateItemLists();
		public static List<Category> Categories = GenerateCategories();

        public static List<Category>GenerateCategories()
		{
			return new List<Category>()
			{
				new Category() { Id = 1, Name = "Produce" },
				new Category() { Id = 2, Name = "Hygiene" }
			};
        }
        public static List<Item> GenerateItems()
        {
            return new List<Item>()
            {
                new Item() { Id=1, Name="Water", Unit="l", CategoryId=1},
                new Item() { Id=2, Name="Eggs", Unit="carton", CategoryId=1},
                new Item() { Id=3, Name="Tissues", Unit="box", CategoryId=2}
            };
        }
        public static List<ItemList> GenerateItemLists()
        {
            return new List<ItemList>()
            {
                new ItemList { Id=1, ItemId=1, ListId=1, Quantity=5, DateCreated=DateTime.Now, DateModified=DateTime.Now },
                new ItemList { Id=2, ItemId=2, ListId=1, Quantity=2, DateCreated=DateTime.Now, DateModified=DateTime.Now }
            };
        }

        public static List<List> GenerateLists()
        {
            return new List<List>()
            {
                new List { Id=1, TotalPrice=0, MonthOfYear=DateOnly.FromDateTime(DateTime.Now), DateModified=DateTime.Now, IsVisible=true, CurrentWorkingList=true},
                new List { Id=2, TotalPrice=100, MonthOfYear=DateOnly.FromDateTime(DateTime.Now), DateModified=DateTime.Now, IsVisible=false}
            };
        }
    }
}

