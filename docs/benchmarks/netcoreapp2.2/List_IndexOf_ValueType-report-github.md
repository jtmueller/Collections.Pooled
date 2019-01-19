``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                  Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------ |---------:|----------:|----------:|------:|--------:|
|   ListIndexOf_ValueType | 13.07 us | 0.1347 us | 0.1195 us |  1.00 |    0.00 |
| PooledIndexOf_ValueType | 12.57 us | 0.2455 us | 0.3277 us |  0.96 |    0.02 |
