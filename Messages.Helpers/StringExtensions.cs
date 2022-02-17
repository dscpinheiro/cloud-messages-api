using System;
using System.Globalization;
using System.Text;

namespace Messages.Helpers;

public static class StringExtensions
{
    public static bool IsPalindrome(this string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentNullException(nameof(message));
        }

        // Filter the message to include only letters and numbers
        var stringBuilder = new StringBuilder(message.Length);
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

        // Ignore case and accented letters: https://stackoverflow.com/a/7720903
        var culture = CultureInfo.CurrentCulture;
        var compareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace;

        for (var i = 0; i < filtered.Length / 2; i++)
        {
            var leftLetter = filtered[i].ToString();
            var rightLetter = filtered[filtered.Length - i - 1].ToString();

            var areEqual = string.Compare(leftLetter, rightLetter, culture, compareOptions) == 0;
            if (!areEqual)
            {
                return false;
            }
        }

        return true;
    }
}
