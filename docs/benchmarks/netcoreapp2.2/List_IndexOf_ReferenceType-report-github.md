``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                      Method |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   ListIndexOf_ReferenceType | 93.64 us | 0.6780 us | 0.6010 us |  1.00 |           - |           - |           - |                   - |
| PooledIndexOf_ReferenceType | 93.49 us | 0.8147 us | 0.7620 us |  1.00 |           - |           - |           - |                   - |
