``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------- |----------:|----------:|----------:|------:|--------:|
|      ListContains_Int |  62.83 us | 0.7445 us | 0.6964 us |  1.00 |    0.00 |
|    PooledContains_Int |  20.99 us | 0.4188 us | 0.5445 us |  0.34 |    0.01 |
|   ListContains_String | 181.70 us | 3.6226 us | 3.2113 us |  2.89 |    0.07 |
| PooledContains_String | 107.74 us | 0.3071 us | 0.2565 us |  1.72 |    0.02 |
