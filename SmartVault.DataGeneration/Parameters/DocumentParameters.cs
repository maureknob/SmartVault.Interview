using System;

namespace SmartVault.DataGeneration.Parameters
{
    public class DocumentParameters : EntityParameters
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public long Length { get; set; }
        public int AccountId { get; set; }

        public DocumentParameters(int id, string name, string filePath, long length, int accountId)
        {
            Id = id;
            Name = name;
            FilePath = filePath;
            Length = length;
            AccountId = accountId;
        }
    }
}
