using System.Text;

namespace EmptyWeb.Models
{
    public static class Csv
    {
        public static string Make(params string[] items)
        {
            var builder = new StringBuilder();
            foreach (var item in items)
            {
                builder.Append(item);
                builder.Append(",");
            }

            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}