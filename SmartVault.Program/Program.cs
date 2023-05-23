using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartVault.Program
{
    partial class Program
    {
        static void Main(string[] args)
        {
            /*            if (args.Length == 0)
                        {
                            return;
                        }*/

            WriteEveryThirdFileToFile("0");
            GetAllFileSizes();
        }

        private static void GetAllFileSizes()
        {
            var documents = GenerateDocuments();

            Int64 fileSizeCount = documents.Sum(d => (long)d.Length);

            Console.WriteLine($"File size count {fileSizeCount}");
        }

        private static void WriteEveryThirdFileToFile(string accountId)
        {
            var documents = GenerateDocuments();
            var accountFiles = documents.Where(d => d.AccountId == int.Parse(accountId)).ToList();
            var fileContentBuilder = new StringBuilder();

            for (int i = 2; i < accountFiles.Count(); i += 3)
            {
                var filePath = accountFiles
                    .Where(f => f.Id.Equals(i))
                    .FirstOrDefault()
                    .FilePath;

                if (File.Exists(filePath))
                {
                    fileContentBuilder.Append(File.ReadAllLines(filePath));
                }
            }

            File.WriteAllText("TestDoc.txt", fileContentBuilder.ToString());
            Console.WriteLine("File created");
        }

        private static List<BusinessObjects.Document> GenerateDocuments()
        {
            var mokDocuments = new MokDocuments();

            mokDocuments.GenerateDocuments();

            return mokDocuments.GetDocuments();
        }
    }
}