``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|            Method |     N |         Mean |       Error |      StdDev | Ratio | RatioSD |
|------------------ |------ |-------------:|------------:|------------:|------:|--------:|
|      **ListSort_Int** |  **1000** |     **20.68 us** |   **0.1775 us** |   **0.1660 us** |  **1.00** |    **0.00** |
|    PooledSort_Int |  1000 |     20.49 us |   0.1553 us |   0.1453 us |  0.99 |    0.01 |
|   ListSort_String |  1000 |  1,127.05 us |   6.0430 us |   5.6526 us | 54.50 |    0.50 |
| PooledSort_String |  1000 |  1,157.96 us |   7.7138 us |   6.8381 us | 56.06 |    0.56 |
|                   |       |              |             |             |       |         |
|      **ListSort_Int** | **10000** |    **551.05 us** |   **5.2268 us** |   **4.8892 us** |  **1.00** |    **0.00** |
|    PooledSort_Int | 10000 |    542.76 us |   9.1593 us |   8.1194 us |  0.98 |    0.02 |
|   ListSort_String | 10000 | 15,220.18 us | 112.1413 us | 104.8970 us | 27.62 |    0.29 |
| PooledSort_String | 10000 | 15,388.65 us | 124.1878 us | 116.1654 us | 27.93 |    0.25 |
