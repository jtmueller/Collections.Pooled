``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     Mean |     Error |    StdDev | Ratio |
|---------------------------- |---------:|----------:|----------:|------:|
|   ListIndexOf_ReferenceType | 100.0 us | 0.2321 us | 0.2171 us |  1.00 |
| PooledIndexOf_ReferenceType | 100.1 us | 0.2555 us | 0.2133 us |  1.00 |
