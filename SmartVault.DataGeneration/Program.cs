using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartVault.DataGeneration.Commands;
using SmartVault.DataGeneration.FileContent;
using SmartVault.DataGeneration.Parameters;
using SmartVault.Library;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SmartVault.DataGeneration
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            SQLiteConnection.CreateFile(configuration["DatabaseFileName"]);

            File.WriteAllText("TestDoc.txt", new GenerateContent().GetContent("This is my test document"));

            int accountAndUsersToCreate = 100;
            int documentsToCreate = 10000;

            var x = new SQLiteConnection(string.Format(configuration?["ConnectionStrings:DefaultConnection"] ?? "", configuration?["DatabaseFileName"]));
            using (var connection = new SQLiteConnection(string.Format(configuration?["ConnectionStrings:DefaultConnection"] ?? "", configuration?["DatabaseFileName"])))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    ExecuteBusinessObjectScript(connection);

                    CreateDocuments(connection, documentsToCreate, CreateAccountAndUsers(connection, accountAndUsersToCreate));

                    transaction.Commit();
                }

                PrintResults(connection);
            }
        }

        /// <summary>
        /// Executes the script that creates the database
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>void</returns>
        static void ExecuteBusinessObjectScript(SQLiteConnection connection)
        {
            var files = Directory.GetFiles(@"..\..\..\..\BusinessObjectSchema");
            for (int i = 0; i <= 2; i++)
            {
                var serializer = new XmlSerializer(typeof(BusinessObject));
                var businessObject = serializer.Deserialize(new StreamReader(files[i])) as BusinessObject;
                connection.Execute(businessObject?.Script);
            }
        }

        /// <summary>
        /// Print the result of the created rows
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>void</returns>
        static void PrintResults(SQLiteConnection connection)
        {
            var accountData = connection.Query("SELECT COUNT(*) FROM Account;");
            Console.WriteLine($"AccountCount: {JsonConvert.SerializeObject(accountData)}");
            var documentData = connection.Query("SELECT COUNT(*) FROM Document;");
            Console.WriteLine($"DocumentCount: {JsonConvert.SerializeObject(documentData)}");
            var userData = connection.Query("SELECT COUNT(*) FROM User;");
            Console.WriteLine($"UserCount: {JsonConvert.SerializeObject(userData)}");
        }

        /// <summary>
        /// Creates documents based on all created accounts
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="documentsToCreate"></param>
        /// <param name="accountIdList"></param>
        /// <returns>void</returns>
        static void CreateDocuments(SQLiteConnection connection, int documentsToCreateByAccountId, int[] accountIdList)
        {
            var documentPath = new FileInfo("TestDoc.txt").FullName;
            var documentLength = new FileInfo(documentPath).Length;
            int accountId = 0;
            int documentsCreated = 0;
            var documentNumber = 0;

            var documentCommand = new DocumentCommand(connection);

            for (int d = 0; d < (accountIdList.Count() * documentsToCreateByAccountId); d++, documentNumber++)
            {
                if (documentsCreated <= (documentsToCreateByAccountId - 1) && accountIdList.Contains(accountId))
                {
                    documentCommand.AddParameters(new DocumentParameters(documentNumber,
                        $"Document{accountIdList[accountId]}-{d}.txt",
                        documentPath,
                        documentLength,
                        accountId));

                    documentsCreated++;

                    documentCommand.ExecuteNonQuery();
                }
                else
                {
                    accountId++;
                    documentsCreated = 0;
                    documentNumber--;
                    d--;
                }
            }
        }

        /// <summary>
        /// Creates Users and Accounts
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="accountAndUsersToCreate"></param>
        /// <returns>int[]</returns>
        static int[] CreateAccountAndUsers(SQLiteConnection connection, int accountAndUsersToCreate)
        {
            var accountIdList = new int[accountAndUsersToCreate];
            var userCommand = new UserCommand(connection);
            var accountCommand = new AccountCommand(connection);

            for (int i = 0; i < accountAndUsersToCreate; i++)
            {


                var randomDayIterator = RandomDay().GetEnumerator();
                randomDayIterator.MoveNext();

                userCommand.AddParameters(new UserParameters(i, $"FName{i}",
                    $"LName{i}",
                    randomDayIterator.Current.ToString("yyyy-MM-dd"),
                    i,
                    $"UserName-{i}",
                    "e10adc3949ba59abbe56e057f20f883e"));

                accountCommand.AddParameters(new AccountParameters(i,
                    $"Account{i}"));


                accountIdList[i] = i;
                userCommand.ExecuteNonQuery();
                accountCommand.ExecuteNonQuery();
            }

            return accountIdList;
        }

        /// <summary>
        /// Generate a random day
        /// </summary>
        /// <returns>IEnumerable<DateTime></returns>
        static IEnumerable<DateTime> RandomDay()
        {
            DateTime start = new DateTime(1985, 1, 1);
            Random gen = new Random();
            int range = (DateTime.Today - start).Days;
            while (true)
                yield return start.AddDays(gen.Next(range));
        }
    }
}