``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------------- |----------:|----------:|----------:|------:|--------:|
|   ListIndexOf_ReferenceType | 101.73 us | 1.9420 us | 2.1585 us |  1.00 |    0.00 |
| PooledIndexOf_ReferenceType |  98.49 us | 0.8573 us | 0.8019 us |  0.97 |    0.02 |
