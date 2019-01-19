``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   ListGetRange | 2,256.1 ms | 46.611 ms | 47.866 ms |  1.00 | 832000.0000 | 725000.0000 | 704000.0000 |        2903886416 B |
| PooledGetRange |   404.6 ms |  5.084 ms |  4.507 ms |  0.18 |           - |           - |           - |                   - |
