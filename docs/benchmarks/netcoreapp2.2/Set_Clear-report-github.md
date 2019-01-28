``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  InvocationCount=1  
UnrollFactor=1  

```
|          Method | InitialSetSize |     Mean |    Error |    StdDev |   Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------- |--------------- |---------:|---------:|----------:|---------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   HashSet_Clear |         320000 | 54.98 us | 2.144 us |  5.869 us | 53.25 us |  1.00 |    0.00 |           - |           - |           - |                   - |
| PooledSet_Clear |         320000 | 72.89 us | 5.819 us | 16.222 us | 65.29 us |  1.34 |    0.31 |           - |           - |           - |                   - |
