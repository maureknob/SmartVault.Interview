using System;

namespace SmartVault.DataGeneration.Parameters
{
    public class UserParameters : EntityParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserParameters(int id, string firstName, string lastName, string dateOfBirth, int accountId, string userName, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            AccountId = accountId;
            UserName = userName;
            Password = password;
            CreatedOn = DateTime.Now;
        }
    }
}
