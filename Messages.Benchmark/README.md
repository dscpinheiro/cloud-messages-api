Can be tested with:

```console
dotnet run -c RELEASE
```

Sample output:

``` ini

BenchmarkDotNet=v0.13.1, OS=debian 11 (container)
Intel Xeon Platinum 8124M CPU 3.00GHz, 1 CPU, 2 logical cores and 1 physical core
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


```
|                      Method |        Mean |     Error |    StdDev |         Min |         Max |
|---------------------------- |------------:|----------:|----------:|------------:|------------:|
|         ShortPalindromeWord |   239.99 ns |  1.495 ns |  1.248 ns |   238.29 ns |   242.77 ns |
|         ShortPalindromeName |   185.67 ns |  1.375 ns |  1.219 ns |   184.24 ns |   188.72 ns |
|             SingleCharacter |    60.44 ns |  1.248 ns |  2.084 ns |    51.78 ns |    62.58 ns |
|   SequenceOfRepeatedNumbers |   625.07 ns |  3.362 ns |  3.145 ns |   617.85 ns |   631.03 ns |
|     SequenceOfRandomNumbers |   165.63 ns |  3.102 ns |  2.750 ns |   161.91 ns |   172.43 ns |
|    EnglishPalindromeMessage | 1,004.62 ns | 12.230 ns | 11.440 ns |   971.81 ns | 1,018.35 ns |
| NonEnglishPalindromeMessage | 1,093.29 ns |  3.042 ns |  2.540 ns | 1,088.92 ns | 1,097.46 ns |
|        NonPalindromeMessage |   236.92 ns |  3.157 ns |  2.953 ns |   232.48 ns |   243.53 ns |
