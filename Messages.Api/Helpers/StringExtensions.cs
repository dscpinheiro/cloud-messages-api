using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Messages.Api.Helpers
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            // Filter the message to include only letters and numbers
            var stringBuilder = new StringBuilder();
            foreach (var letter in message)
            {
                if (char.IsLetterOrDigit(letter))
                {
                    stringBuilder.Append(letter);
                }
            }

            var filtered = stringBuilder.ToString();
            if (string.IsNullOrWhiteSpace(filtered))
            {
                return false;
            }

            for (var i = 0; i < filtered.Length / 2; i++)
            {
                // Converting the characters to strings is not the most efficient method,
                // but it allows comparison using of different cultures: https://stackoverflow.com/a/1394898
                var leftLetter = filtered[i].ToString();
                var rightLetter = filtered[filtered.Length - i - 1].ToString();

                if (!string.Equals(leftLetter, rightLetter, StringComparison.InvariantCultureIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }
    }
}