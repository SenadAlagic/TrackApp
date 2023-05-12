using System;
namespace TrackApp.Service.ViewModels
{
	public class GetItemsVM
	{
		public int Quantity { get; set; }
		public string Unit { get; set; }
		public string Name { get; set; }
		public int ItemId { get; set; }
		public string CategoryName { get; set; }
        public GetItemsVM()
		{
		}
	}
}

