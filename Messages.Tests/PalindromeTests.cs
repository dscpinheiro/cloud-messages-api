using System;
using System.Collections.Generic;
using Messages.Api.Helpers;
using Xunit;

namespace MessagesTests
{
    public class PalindromeTests
    {
        [Fact]
        public void GivenANullString_ItThrowsException()
        {
            string message = null;
            Assert.Throws<ArgumentNullException>(() => message.IsPalindrome());
        }

        [Fact]
        public void GivenAnEmptyString_ItThrowsException()
        {
            var message = string.Empty;
            Assert.Throws<ArgumentNullException>(() => message.IsPalindrome());
        }

        [Fact]
        public void GivenANonPalindromeMessage_ItReturnsFalse()
        {
            var message = "this is not a palindrome";
            Assert.False(message.IsPalindrome());
        }

        [Fact]
        public void GivenAPalindromeWord_ItReturnsTrue()
        {
            var messages = new List<string> { "madam", "racecar", "arara" };
            foreach (var message in messages)
            {
                Assert.True(message.IsPalindrome());
            }
        }

        [Fact]
        public void GivenAOneLetterWord_ItReturnsTrue()
        {
            var message = "x";
            Assert.True(message.IsPalindrome());
        }

        [Fact]
        public void GivenASequenceOfRepeatedNumbers_ItReturnsTrue()
        {
            var message = "1111111111";
            Assert.True(message.IsPalindrome());
        }

        [Fact]
        public void GivenASequenceOfRandomNumbers_ItReturnsFalse()
        {
            var message = "865357943037344";
            Assert.False(message.IsPalindrome());
        }
    }
}