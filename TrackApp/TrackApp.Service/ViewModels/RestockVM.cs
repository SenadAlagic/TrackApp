using System;
namespace TrackApp.Service.ViewModels
{
	public class RestockVM
	{
		public int  ItemId { get; set; }
		public int ListId { get; set; }
		public int  Quantity { get; set; }
		public double  TotalPrice { get; set; }
        public RestockVM()
		{
		}
	}
}

