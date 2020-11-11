Can be tested with:

```console
dotnet run -c RELEASE
```

Sample output:

``` ini
BenchmarkDotNet=v0.12.1, OS=debian 10 (container)
Intel Xeon Platinum 8275CL CPU 3.00GHz, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=5.0.100
  [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
  Job-KJMHHB : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

OutlierMode=DontRemove  
```

|                      Method |        Mean |     Error |    StdDev |         Min |         Max |
|---------------------------- |------------:|----------:|----------:|------------:|------------:|
|         ShortPalindromeWord |   244.52 ns |  4.777 ns |  5.686 ns |   240.24 ns |   267.88 ns |
|         ShortPalindromeName |   188.54 ns |  3.652 ns |  3.907 ns |   184.81 ns |   201.41 ns |
|             SingleCharacter |    62.78 ns |  1.200 ns |  1.179 ns |    59.91 ns |    65.09 ns |
|   SequenceOfRepeatedNumbers |   651.88 ns | 12.646 ns | 13.531 ns |   639.45 ns |   703.58 ns |
|     SequenceOfRandomNumbers |   203.00 ns |  0.619 ns |  0.579 ns |   201.78 ns |   203.67 ns |
|    EnglishPalindromeMessage | 1,020.91 ns |  4.946 ns |  4.627 ns | 1,013.96 ns | 1,033.01 ns |
| NonEnglishPalindromeMessage | 1,117.72 ns |  6.373 ns |  5.961 ns | 1,109.73 ns | 1,133.79 ns |
|        NonPalindromeMessage |   264.55 ns |  1.863 ns |  1.743 ns |   261.85 ns |   268.27 ns |

