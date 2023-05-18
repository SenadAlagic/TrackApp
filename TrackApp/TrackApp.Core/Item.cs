using System;
using System.ComponentModel.DataAnnotations;

namespace TrackApp.Core;

public class Item
{
	[Key]
	public int Id { get; set; }
	public string Name { get; set; }
	public string Unit { get; set; }
	public int CategoryId { get; set; }

	public Item()
	{
	}
}

