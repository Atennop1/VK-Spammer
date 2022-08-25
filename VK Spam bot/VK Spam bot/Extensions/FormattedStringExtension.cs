using System.Text;

namespace SpamBot.Extensions
{
    public static class FormattedStringExtension
    {
        public static string Format(this string message, int shift)
        {
            string[] subStrings = message.Split(' ');

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < subStrings.Length - shift; i++)
                stringBuilder.Append(subStrings[i + shift] + " ");

            return stringBuilder.ToString();
        }
    }
}
