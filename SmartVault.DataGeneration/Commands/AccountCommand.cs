using SmartVault.DataGeneration.Parameters;
using System;
using System.Data.SQLite;

namespace SmartVault.DataGeneration.Commands
{
    public class AccountCommand : Command<AccountParameters>
    {
        public AccountCommand(SQLiteConnection connection)
        {
            _command = connection.CreateCommand();
            _command.CommandText = @"INSERT INTO Account (Id, Name, CreatedOn) VALUES(@Id, @Name, @CreatedOn)";
        }

        public override void AddParameters(AccountParameters parameters)
        {
            _command.Parameters.Add(new SQLiteParameter("@Id", parameters.Id));
            _command.Parameters.Add(new SQLiteParameter("@Name", parameters.Name));
            _command.Parameters.Add(new SQLiteParameter("@CreatedOn", DateTime.Now));
        }

        public override void ExecuteNonQuery()
        {
            _command.ExecuteNonQuery();
        }
    }
}
