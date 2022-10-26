using System;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class Author : IClassModels
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string SurName { get; set; }

		public Author()
		{
		}

		public Author(int Id, string FirstName, string SurName)
		{
			this.Id = Id;
			this.FirstName = FirstName;
			this.SurName = SurName;
		}


        public override string ToString()
        {
			return
				$"{Id} - {FirstName} {SurName}";
        }
    }
}

