using System;
using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core
{
	public class Purchase
	{
		[Key]
		public int Id { get; set; }
		public int ItemId { get; set; }
		public DateTime DateOfPurchase { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public bool IsVisible { get; set; }
		public Purchase()
		{
		}
	}
}

