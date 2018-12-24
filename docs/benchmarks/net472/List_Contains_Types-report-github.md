``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------- |----------:|----------:|----------:|------:|--------:|
|      ListContains_Int |  57.31 us | 0.1084 us | 0.1014 us |  1.00 |    0.00 |
|    PooledContains_Int |  19.27 us | 0.0600 us | 0.0561 us |  0.34 |    0.00 |
|   ListContains_String | 165.10 us | 1.3683 us | 1.2799 us |  2.88 |    0.02 |
| PooledContains_String | 105.87 us | 0.3314 us | 0.3100 us |  1.85 |    0.01 |
