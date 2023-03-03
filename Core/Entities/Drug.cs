using System;
namespace Core.Entities
{
	public class Drug:BaseEntity
	{
		public string Name { get; set; }
		public int Price { get; set; }
		public int Count { get; set; }
		public Drugstore Drugstore { get; set; }
	}
}

