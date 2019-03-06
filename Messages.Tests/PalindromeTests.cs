using System;
using Messages.Api.Helpers;
using Xunit;

namespace MessagesTests
{
    public class PalindromeTests
    {
        [Fact]
        public void GivenANullString_ThrowsException()
        {
            string word = null;
            Assert.Throws<ArgumentNullException>(() => word.IsPalindrome());
        }

        [Fact]
        public void GivenAnEmptyString_ThrowsException()
        {
            var word = string.Empty;
            Assert.Throws<ArgumentNullException>(() => word.IsPalindrome());
        }
    }
}