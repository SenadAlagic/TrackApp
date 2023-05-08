using System;
namespace TrackApp.Core
{
	public class ItemList
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		//public int ItemId { get; set; }
		//public int ListId { get; set; }

		public ItemList()
		{
		}
	}
}

