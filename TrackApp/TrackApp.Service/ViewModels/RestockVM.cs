using System;
namespace TrackApp.Service.ViewModels
{
	public class RestockVM
	{
		public int  ItemId { get; set; }
		//public int ListId { get; set; }
		public int  Quantity { get; set; }
		public double  TotalPrice { get; set; }
		public string PurchasedBy { get; set; }
		public string ImageBase64 { get; set; }
        public RestockVM()
		{
		}
	}
}

