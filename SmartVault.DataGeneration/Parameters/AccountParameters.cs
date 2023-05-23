using System;

namespace SmartVault.DataGeneration.Parameters
{
    public class AccountParameters : EntityParameters
    {
        public string Name { get; set; }

        public AccountParameters(int id, string name)
        {
            Id = id;
            Name = name;
            CreatedOn = DateTime.Now;
        }
    }
}
