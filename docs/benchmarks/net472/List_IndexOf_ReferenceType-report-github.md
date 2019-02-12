``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                      Method |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   ListIndexOf_ReferenceType | 87.39 us | 0.4256 us | 0.3772 us |  1.00 |           - |           - |           - |                   - |
| PooledIndexOf_ReferenceType | 87.49 us | 0.6341 us | 0.5621 us |  1.00 |           - |           - |           - |                   - |
