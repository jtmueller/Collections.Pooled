``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |    Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |--------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   ListGetRange | 2.913 s | 0.0324 s | 0.0303 s |  1.00 | 843000.0000 | 830000.0000 | 714000.0000 |        2903063184 B |
| PooledGetRange | 2.341 s | 0.0041 s | 0.0034 s |  0.80 |           - |           - |           - |                   - |
