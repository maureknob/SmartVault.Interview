using SmartVault.DataGeneration.Parameters;
using System;
using System.Data.SQLite;

namespace SmartVault.DataGeneration.Commands
{
    public class UserCommand : Command<UserParameters>
    {
        public UserCommand(SQLiteConnection connection)
        {
            _command = connection.CreateCommand();
            _command.CommandText = @"INSERT INTO User (Id, FirstName, LastName, DateOfBirth, AccountId, Username, Password, CreatedOn) VALUES(@Id, @FirstName, @LastName, @DateOfBirth, @AccountId, @Username, @Password, @CreatedOn)";
        }

        public override void AddParameters(UserParameters parameters)
        {
            _command.Parameters.Add(new SQLiteParameter("@Id", parameters.Id));
            _command.Parameters.Add(new SQLiteParameter("@FirstName", parameters.FirstName));
            _command.Parameters.Add(new SQLiteParameter("@LastName", parameters.LastName));
            _command.Parameters.Add(new SQLiteParameter("@DateOfBirth", parameters.DateOfBirth));
            _command.Parameters.Add(new SQLiteParameter("@AccountId", parameters.AccountId));
            _command.Parameters.Add(new SQLiteParameter("@Username", parameters.UserName));
            _command.Parameters.Add(new SQLiteParameter("@Password", parameters.Password));
            _command.Parameters.Add(new SQLiteParameter("@CreatedOn", DateTime.Now));
        }

        public override void ExecuteNonQuery()
        {
            _command.ExecuteNonQuery();
        }
    }
}