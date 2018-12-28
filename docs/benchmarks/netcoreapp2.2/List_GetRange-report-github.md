``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |       Mean |     Error |   StdDev |     Median | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |-----------:|----------:|---------:|-----------:|------:|------------:|------------:|------------:|--------------------:|
|   ListGetRange | 2,197.9 ms | 39.033 ms | 36.51 ms | 2,194.3 ms |  1.00 | 840000.0000 | 737000.0000 | 712000.0000 |        2903467728 B |
| PooledGetRange |   373.0 ms |  7.422 ms | 16.14 ms |   363.9 ms |  0.17 |           - |           - |           - |                   - |
