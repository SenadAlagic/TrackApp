using System;
using TrackApp.Core;

namespace TrackApp.Service.ViewModels
{
	public class ItemsByCategoryVM
	{
		public string CategoryName { get; set; }
		public List<Item> Items { get; set; }
		public ItemsByCategoryVM()
		{
		}
	}
}

