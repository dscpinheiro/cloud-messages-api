using Messages.Helpers;
using System;
using Xunit;

namespace Messages.Tests.Helpers
{
    [Trait("Category", "Unit")]
    public class PalindromeTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsPalindrome_InvalidParameter_ThrowsException(string message) => Assert.Throws<ArgumentNullException>(() => message.IsPalindrome());

        [Theory]
        [InlineData("madam")]
        [InlineData("racecar")]
        [InlineData("civic")]
        [InlineData("kayak")]
        [InlineData("radar")]
        [InlineData("level")]
        [InlineData("mom")]
        [InlineData("dad")]
        [InlineData("été")]
        [InlineData("ici")]
        [InlineData("tôt")]
        [InlineData("rêver")]
        [InlineData("arara")]
        [InlineData("reviver")]
        [InlineData("omissíssimo")]
        [InlineData("saippuakivikauppias")]
        public void IsPalindrome_PalindromeWord_ReturnsTrue(string word) => Assert.True(word.IsPalindrome());

        [Theory]
        [InlineData("Ana")]
        [InlineData("Anna")]
        [InlineData("Hanah")]
        [InlineData("Hannah")]
        [InlineData("Bob")]
        [InlineData("Otto")]
        [InlineData("Lon Nol")]
        [InlineData("Natan")]
        [InlineData("Laval")]
        public void IsPalindrome_PalindromeName_ReturnsTrue(string name) => Assert.True(name.IsPalindrome());

        [Fact]
        public void IsPalindrome_OneLetterWord_ReturnsTrue()
        {
            var word = "x";
            Assert.True(word.IsPalindrome());
        }

        [Fact]
        public void IsPalindrome_PunctuationOnly_ReturnsFalse()
        {
            var word = "!.,,.!";
            Assert.False(word.IsPalindrome());
        }

        [Theory]
        [InlineData("11111")]
        [InlineData("22")]
        [InlineData("9999999999999999999")]
        public void IsPalindrome_SequenceOfRepeatedNumbers_ReturnsTrue(string sequence) => Assert.True(sequence.IsPalindrome());

        [Theory]
        [InlineData("865357943037344")]
        [InlineData("865368")]
        [InlineData("12201")]
        public void IsPalindrome_SequenceOfRandomNumbers_ReturnsFalse(string sequence) => Assert.False(sequence.IsPalindrome());

        [Theory]
        [InlineData("A man, a plan, a canal, Panama!")]
        [InlineData("Was it a car or a cat I saw?")]
        [InlineData("No 'x' in Nixon")]
        [InlineData("Live on time, emit no evil")]
        [InlineData("Madam, I'm Adam.")]
        [InlineData("Madam, in Eden I'm Adam.")]
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
        [InlineData("Nurses run")]
        [InlineData("'Nurses run', says sick Cissy as nurses run")]
        [InlineData("Did Hannah see bees? Hannah did...")]
        public void IsPalindrome_EnglishPalindromeMessage_ReturnsTrue(string message) => Assert.True(message.IsPalindrome());

        [Theory]
        [InlineData("Socorram-me, subi no ônibus em Marrocos")]
        [InlineData("In girum imus nocte et consumimur igni")]
        [InlineData("All'Unicef non feci nulla")]
        [InlineData("Mon nom")]
        [InlineData("¿Son mulas o cívicos alumnos?")]
        [InlineData("Eh ! ça va la vache")]
        [InlineData("Noël a trop par rapport à Léon")]
        [InlineData("La mariée ira mal")]
        [InlineData("Eine güldne, gute Tugend: Lüge nie!")]
        [InlineData("O vôo do ovo")]
        [InlineData("Oi, raro horário!")]
        [InlineData("Català a l'atac.")]
        [InlineData("Dábale arroz a la zorra el abad")]
        [InlineData("Isä, älä myy myymälääsi.")]
        [InlineData("Te pék, láttál képet?")]
        [InlineData("Ogni mare è ramingo")]
        [InlineData("Tien, Alícia sap mès sèm pas aicí la neit.")]
        [InlineData("Roma, lo còr nud d’un ròc, o l’amor.")]
        [InlineData("Luza Rocelina, a namorada do Manuel, leu na 'Moda da Romana': anil é cor azul")]
        public void IsPalindrome_NonEnglishPalindromeMessage_ReturnsTrue(string message) => Assert.True(message.IsPalindrome());

        [Theory]
        [InlineData("A palindrome is a word, number, sentence, or verse that reads the same backward or forward.")]
        [InlineData("this is not a palindrome")]
        public void IsPalindrome_NonPalindromeMessage_ReturnsFalse(string message) => Assert.False(message.IsPalindrome());
    }
}
