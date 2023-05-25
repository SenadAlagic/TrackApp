using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		public string Name { get; set; }

		public Category()
		{
		}
	}
}

