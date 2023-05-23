using System;
using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core;

public class Item
{
	[Key]
	public int ItemId { get; set; }
	[Required]
	[MinLength(4),MaxLength(25)]
	public string Name { get; set; }
	[Required]
	[MinLength(1),MaxLength(15)]
    public string Unit { get; set; }
	[Required]
	public int CategoryId { get; set; }

	public Item()
	{
	}
}

