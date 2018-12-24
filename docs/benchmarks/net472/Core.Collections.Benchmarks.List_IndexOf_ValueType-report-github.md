``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                  Method |     Mean |     Error |    StdDev | Ratio |
|------------------------ |---------:|----------:|----------:|------:|
|   ListIndexOf_ValueType | 12.04 us | 0.0217 us | 0.0203 us |  1.00 |
| PooledIndexOf_ValueType | 12.04 us | 0.0238 us | 0.0222 us |  1.00 |
