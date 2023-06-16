using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

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

