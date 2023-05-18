using System;
using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core
{
	public class List
	{
		[Key]
		public int Id { get; set; }
		public double TotalPrice { get; set; }
		public DateOnly MonthOfYear { get; set; }
		public DateTime DateModified { get; set; }
		public bool IsVisible { get; set; } = true;
		public bool CurrentWorkingList { get; set; }

		public List()
		{
		}
	}
}