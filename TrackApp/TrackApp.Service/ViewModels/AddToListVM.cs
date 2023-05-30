using System;
namespace TrackApp.Service.ViewModels
{
	public class AddToListVM
	{
		public int Quantity { get; set; }
		public int ItemId { get; set; }
		//public int ListId { get; set; }
		public bool CrossedOff { get; set; }
		public string? AddedBy { get; set; }

		public AddToListVM()
		{
		}
	}
}

