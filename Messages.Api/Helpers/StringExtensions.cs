using System;

namespace Messages.Api.Helpers
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                throw new ArgumentNullException(nameof(word));
            }

            return false;
        }
    }
}