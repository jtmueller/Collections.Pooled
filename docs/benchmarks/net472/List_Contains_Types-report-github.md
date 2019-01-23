``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      ListContains_Int |  57.50 us | 0.1796 us | 0.1592 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|    PooledContains_Int |  19.28 us | 0.0498 us | 0.0441 us |  0.34 |    0.00 |           - |           - |           - |                   - |
|   ListContains_String | 165.89 us | 1.1573 us | 1.0825 us |  2.89 |    0.02 |           - |           - |           - |                   - |
| PooledContains_String | 105.99 us | 0.1682 us | 0.1574 us |  1.84 |    0.01 |           - |           - |           - |                   - |
