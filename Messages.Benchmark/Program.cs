﻿using Messages.Helpers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Perfolizer.Mathematics.OutlierDetection;

namespace Messages.Benchmark
{
    #pragma warning disable CA1822 // Mark members as static
    [MemoryDiagnoser]
    [MinColumn, MaxColumn]
    [MarkdownExporter, HtmlExporter, CsvExporter]
    public class MyBenchmark
    {
        [Benchmark]
        public void ShortPalindromeWord() => "racecar".IsPalindrome();

        [Benchmark]
        public void ShortPalindromeName() => "Anna".IsPalindrome();

        [Benchmark]
        public void SingleCharacter() => "x".IsPalindrome();

        [Benchmark]
        public void SequenceOfRepeatedNumbers() => "9999999999999999999".IsPalindrome();

        [Benchmark]
        public void SequenceOfRandomNumbers() => "865357943037344".IsPalindrome();

        [Benchmark]
        public void EnglishPalindromeMessage() => "Cigar? Toss it in a can. It is so tragic.".IsPalindrome();

        [Benchmark]
        public void NonEnglishPalindromeMessage() => "Socorram-me, subi no ônibus em Marrocos".IsPalindrome();

        [Benchmark]
        public void NonPalindromeMessage() => "this is not a palindrome".IsPalindrome();
    }
    #pragma warning restore CA1822 // Mark members as static

    class Program
    {
        static void Main()
        {
            var config = DefaultConfig.Instance.AddJob(Job.Default);
            BenchmarkRunner.Run<MyBenchmark>(config);
        }
    }
}
