using System;
using System.Collections.Generic;

namespace SmartVault.Program
{
    public class MokDocuments
    {
        private List<BusinessObjects.Document> _documents = new List<BusinessObjects.Document>();

        public void GenerateDocuments()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    var newDocument = new BusinessObjects.Document
                    {
                        Id = j,
                        Name = $"Document{i}-{j}.txt",
                        FilePath = "HardChallenge\\SmartVault.DataGeneration\\bin\\Debug\\net5.0\\TestDoc.txt",
                        Length = 2626,
                        AccountId = i,
                        CreatedOn = DateTime.Now.ToString()
                    };
                    _documents.Add(newDocument);
                }
            }
        }

        public List<BusinessObjects.Document> GetDocuments()
        {
            return _documents;
        }
    }
}
