using System;
using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core
{
	public class ItemList
	{
		[Key]
		public int Id { get; set; }
		public int Quantity { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public int ItemId { get; set; }
		public int ListId { get; set; }
		public bool CrossedOff { get; set; } = false;

		public ItemList()
		{
		}
	}
}

