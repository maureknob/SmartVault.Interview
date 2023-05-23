using System.Text;

namespace SmartVault.DataGeneration.FileContent
{
    public class GenerateContent
    {
        private StringBuilder contentBuilder = new StringBuilder();

        public string GetContent(string content)
        {
            for (int i = 0; i < 101; i++)
            {
                contentBuilder.AppendLine(content);
            }

            return contentBuilder.ToString();
        }
    }
}
