using System;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class BooksOutOnLoan : IClassModels
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string DateIssued { get; set; }
		public string DueDate { get; set; }
		public string DateReturned { get; set; }

		public BooksOutOnLoan()
		{
		}

		public BooksOutOnLoan(int Id, int BookId, string DateIssued, string DueDate, string DateReturned)
		{
			this.Id = Id;
			this.BookId = BookId;
			this.DateIssued = DateIssued;
			this.DueDate = DueDate;
			this.DateReturned = DateReturned;
		}

        public override string ToString()
        {
			return
				$"User : {Id} - Book : {BookId} - Date Issued: {DateIssued} - " +
				$"Due Date: {DueDate} - Date Returned: {DateReturned}";
        }
    }
}

