using System;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class Users : IClassModels
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string UserAddress { get; set; }
		public string OtherUserDetails { get; set; }
		public int AmountOfFine { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public Users()
		{
		}

		public Users(int Id, string UserName, string UserAddress, string UserDetails, int AmountOfFine,string UserEmail, string phone)
		{
			this.Id = Id;
			this.UserName = UserName;
			this.UserAddress = UserAddress;
			this.OtherUserDetails = UserDetails;
			this.AmountOfFine = AmountOfFine;
			this.Email = UserEmail;
			this.PhoneNumber = PhoneNumber;
		}

		
        public override string ToString()
        {
            return
				$"{Id} - {UserName} : {UserAddress} : {OtherUserDetails} : " +
				$"{AmountOfFine} : {Email} : {PhoneNumber}";
        }
    }
}

