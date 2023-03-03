using System;
using Core.Entities;

namespace Data.Contexts
{
	public static class DbContext
	{
		static DbContext()
		{
			Owners = new List<Owner>();
			Drugs = new List<Drug>();
			Drugstores = new List<Drugstore>();
			Druggists = new List<Druggist>();
			Admins = new List<Admin>();
			
		}

		public static List<Owner> Owners { get; set; }
		public static List<Drug> Drugs { get; set; }
		public static List<Drugstore> Drugstores { get; set; }
		public static List<Druggist> Druggists { get; set; }
		public static List<Admin> Admins { get; set; }
	}
}

