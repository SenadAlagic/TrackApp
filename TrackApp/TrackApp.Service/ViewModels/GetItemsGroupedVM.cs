using System;
namespace TrackApp.Service.ViewModels
{
	public class GetItemsGroupedVM
	{
        public string CategoryName { get; set; }
		public List<GetItemsVM> Items { get; set; }
        public GetItemsGroupedVM()
		{
		}
	}
}

