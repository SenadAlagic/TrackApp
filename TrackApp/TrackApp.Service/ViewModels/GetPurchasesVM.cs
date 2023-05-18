using System;
namespace TrackApp.Service.ViewModels
{
	public class GetPurchasesVM
	{
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool IsVisible { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }

		public GetPurchasesVM()
		{
		}
	}
}

