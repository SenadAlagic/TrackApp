using System;
namespace TrackApp.Service.ViewModels
{
	public class AddPurchaseVM
	{
		public int ItemId { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public string PurchasedBy { get; set; }
		public AddPurchaseVM()
		{
		}
	}
}

