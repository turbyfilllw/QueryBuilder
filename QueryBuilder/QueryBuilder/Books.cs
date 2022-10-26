using System;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class Books : IClassModels
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Isbn { get; set; }
		public string DateOfPublication { get; set; }

		public Books()
		{
		}

		public Books(int Id, string Title, string Isbn, string DateOfPublication)
		{
			this.Id = Id;
			this.Title = Title;
			this.Isbn = Isbn;
			this.DateOfPublication = DateOfPublication;
		}


        public override string ToString()
        {
			return
				$"{Id} - {Title} : {Isbn} : {DateOfPublication}";
        }
    }
}

