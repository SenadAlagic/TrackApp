using System;
namespace TrackApp.Core
{
	public class Purchase
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public DateTime DateOfPurchase { get; set; }
		public int Quantity { get; set; }
		public bool IsVisible { get; set; }
		public Purchase()
		{
		}
	}
}

