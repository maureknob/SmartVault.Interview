using System.Data.SQLite;

namespace SmartVault.DataGeneration.Commands
{
    public abstract class Command<T>
    {
        public SQLiteCommand _command;

        public virtual void AddParameters(T parameters) { }

        public virtual void ExecuteNonQuery() { }
    }
}
