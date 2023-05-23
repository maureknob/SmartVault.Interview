using SmartVault.DataGeneration.Parameters;
using System;
using System.Data.SQLite;

namespace SmartVault.DataGeneration.Commands
{
    public class DocumentCommand : Command<DocumentParameters>
    {
        public DocumentCommand(SQLiteConnection connection)
        {
            _command = connection.CreateCommand();
            _command.CommandText = @"INSERT INTO Document (Id, Name, FilePath, Length, AccountId, CreatedOn) VALUES (@Id, @Name, @FilePath, @Length, @AccountId, @CreatedOn)";
        }

        public override void AddParameters(DocumentParameters parameters)
        {
            _command.Parameters.Add(new SQLiteParameter("@Id", parameters.Id));
            _command.Parameters.Add(new SQLiteParameter("@Name", parameters.Name));
            _command.Parameters.Add(new SQLiteParameter("@FilePath", parameters.FilePath));
            _command.Parameters.Add(new SQLiteParameter("@Length", parameters.Length));
            _command.Parameters.Add(new SQLiteParameter("@AccountId", parameters.AccountId));
            _command.Parameters.Add(new SQLiteParameter("@CreatedOn", DateTime.Now));
        }

        public override void ExecuteNonQuery()
        {
            _command.ExecuteNonQuery();
        }

        public SQLiteCommand GetCreatedCommand()
        {
            return _command;
        }
    }
}
