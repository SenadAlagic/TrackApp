using System;
using TrackApp.Core;

namespace TrackApp.Service.ViewModels
{
	public class ItemHistoryVM
	{
		public List<GetItemsGroupedVM> Items { get; set; }
		public DateOnly MonthOfYear { get; set; }
		public ItemHistoryVM()
		{
		}
	}
}

