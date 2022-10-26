using System;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class Categories : IClassModels
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public Categories()
		{
		}

		public Categories(int Id, string Name)
		{
			this.Id = Id;
			this.Name = Name;
		}

        public override string ToString()
        {
			return
				$"{Id} - {Name}";
        }
    }
}

