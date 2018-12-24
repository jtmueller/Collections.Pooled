``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
|      ListIndexer_Int |  66.36 us | 0.2290 us | 0.2143 us |  1.00 |    0.00 |
|    PooledIndexer_Int | 156.14 us | 0.8102 us | 0.7182 us |  2.35 |    0.01 |
|   ListIndexer_String |  48.36 us | 0.0870 us | 0.0771 us |  0.73 |    0.00 |
| PooledIndexer_String | 156.28 us | 0.9881 us | 0.9242 us |  2.36 |    0.02 |
