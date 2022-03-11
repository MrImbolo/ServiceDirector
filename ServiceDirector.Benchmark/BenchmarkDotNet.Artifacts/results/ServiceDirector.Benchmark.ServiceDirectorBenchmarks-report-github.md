``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1586 (21H2)
AMD Ryzen 7 5800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.101
  [Host]     : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT
  DefaultJob : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT


```
|                      Method |     Mean |    Error |   StdDev | Rank |  Gen 0 | Allocated |
|---------------------------- |---------:|---------:|---------:|-----:|-------:|----------:|
|    PlainServiceExecuteAsync | 35.45 ns | 0.343 ns | 0.321 ns |    1 | 0.0162 |     136 B |
| ServiceDirectorExecuteAsync | 70.47 ns | 0.456 ns | 0.381 ns |    2 | 0.0421 |     352 B |
