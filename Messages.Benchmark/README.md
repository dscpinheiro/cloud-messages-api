Can be tested with:

```console
dotnet run -c RELEASE
```

Sample output:

``` ini
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-8700 CPU 3.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  DefaultJob : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
```

|                      Method |        Mean |     Error |    StdDev |         Min |         Max |
|---------------------------- |------------:|----------:|----------:|------------:|------------:|
|         ShortPalindromeWord |   217.83 ns |  0.447 ns |  0.373 ns |   217.17 ns |   218.49 ns |
|         ShortPalindromeName |   160.66 ns |  0.685 ns |  0.572 ns |   159.42 ns |   161.48 ns |
|             SingleCharacter |    31.95 ns |  0.980 ns |  0.917 ns |    31.26 ns |    34.41 ns |
|   SequenceOfRepeatedNumbers |   604.97 ns |  0.828 ns |  0.734 ns |   603.75 ns |   606.09 ns |
|     SequenceOfRandomNumbers |   154.44 ns |  2.979 ns |  3.060 ns |   151.69 ns |   161.46 ns |
|    EnglishPalindromeMessage |   967.92 ns |  1.960 ns |  1.737 ns |   965.22 ns |   971.33 ns |
| NonEnglishPalindromeMessage | 1,074.07 ns | 14.342 ns | 13.416 ns | 1,061.41 ns | 1,101.51 ns |
|        NonPalindromeMessage |   195.86 ns |  3.789 ns |  4.364 ns |   190.83 ns |   203.59 ns |
