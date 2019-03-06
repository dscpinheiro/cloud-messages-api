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

        [Theory]
        [InlineData("madam")]
        [InlineData("racecar")]
        [InlineData("arara")]
        [InlineData("civic")]
        [InlineData("kayak")]
        [InlineData("radar")]
        [InlineData("level")]
        [InlineData("mom")]
        [InlineData("dad")]
        [InlineData("noon")]
        public void GivenAPalindromeWord_ItReturnsTrue(string word) => Assert.True(word.IsPalindrome());

        [Theory]
        [InlineData("Anna")]
        [InlineData("Hanah")]
        [InlineData("Hannah")]
        [InlineData("Bob")]
        [InlineData("Otto")]
        [InlineData("Lon Nol")]
        [InlineData("Eevee")]
        public void GivenAPalindromeName_ItReturnsTrue(string name) => Assert.True(name.IsPalindrome());

        [Fact]
        public void GivenAOneLetterWord_ItReturnsTrue()
        {
            var word = "x";
            Assert.True(word.IsPalindrome());
        }

        [Fact]
        public void GivenAWordContainingOnlyPunctuation_ItReturnsFalse()
        {
            var word = "!.,,.!";
            Assert.False(word.IsPalindrome());
        }

        [Fact]
        public void GivenASequenceOfRepeatedNumbers_ItReturnsTrue()
        {
            var sequence = "1111111111";
            Assert.True(sequence.IsPalindrome());
        }

        [Fact]
        public void GivenASequenceOfRandomNumbers_ItReturnsFalse()
        {
            var sequence = "865357943037344";
            Assert.False(sequence.IsPalindrome());
        }

        [Theory]
        [InlineData("A man, a plan, a canal, Panama!")]
        [InlineData("Was it a car or a cat I saw?")]
        [InlineData("No 'x' in Nixon")]
        [InlineData("Live on time, emit no evil")]
        [InlineData("Madam, I'm Adam.")]
        [InlineData("Some men interpret nine memos.")]
        [InlineData("Do geese see God?")]
        [InlineData("Too bad--I hid a boot.")]
        [InlineData("Gateman sees name, garageman sees name tag.")]
        [InlineData("Norma is as selfless as I am, Ron.")]
        [InlineData("Don’t nod.")]
        [InlineData("Never odd or even.")]
        [InlineData("Cigar? Toss it in a can. It is so tragic.")]
        [InlineData("Dammit, I’m mad!")]
        [InlineData("He did, eh?")]
        [InlineData("I prefer pi")]
        public void GivenAPalindromeMessage_ItReturnsTrue(string message) => Assert.True(message.IsPalindrome());

        [Theory]
        [InlineData("Socorram-me, subi no ônibus em Marrocos")]
        [InlineData("In girum imus nocte et consumimur igni")]
        [InlineData("All'Unicef non feci nulla")]
        [InlineData("Mon nom")]
        [InlineData("¿Son mulas o cívicos alumnos?")]
        [InlineData("Eh ! ça va la vache")]
        [InlineData("Noël a trop par rapport à Léon")]
        [InlineData("Eine güldne, gute Tugend: Lüge nie!")]
        [InlineData("O vôo do ovo")]
        [InlineData("Oi, raro horário!")]
        [InlineData("Català a l'atac.")]
        public void GivenANonEnglishPalindromeMessage_ItReturnsTrue(string message) => Assert.True(message.IsPalindrome());

        [Theory]
        [InlineData("A palindrome is a word, number, sentence, or verse that reads the same backward or forward.")]
        [InlineData("this is not a palindrome")]
        [InlineData("Data has the power to transform business and improve society.")]
        [InlineData("Data should be explored, not just queried.")]
        [InlineData("Data is the new language of business.")]
        public void GivenANonPalindromeMessage_ItReturnsFalse(string message) => Assert.False(message.IsPalindrome());
    }
}