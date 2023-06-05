using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core
{
	public class ItemList
	{
		[Key]
		public int ItemListId { get; set; }
		public int Quantity { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		[Required]
		public int ItemId { get; set; }
		[Required]
		public int ListId { get; set; }
		public bool CrossedOff { get; set; } = false;
		public string AddedBy { get; set; }

		public ItemList()
		{
		}
	}
}

