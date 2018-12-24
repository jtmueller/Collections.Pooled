``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |     N |         Mean |      Error |     StdDev | Ratio | RatioSD |
|-------------------------- |------ |-------------:|-----------:|-----------:|------:|--------:|
|      **ListBinarySearch_Int** |  **1000** |     **47.63 us** |  **0.1697 us** |  **0.1587 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int |  1000 |     49.05 us |  0.1451 us |  0.1358 us |  1.03 |    0.00 |
|   ListBinarySearch_String |  1000 |    822.36 us |  2.4955 us |  2.3342 us | 17.26 |    0.07 |
| PooledBinarySearch_String |  1000 |    836.14 us |  1.3876 us |  1.2980 us | 17.55 |    0.06 |
|                           |       |              |            |            |       |         |
|      **ListBinarySearch_Int** | **10000** |    **621.32 us** |  **2.0636 us** |  **1.9303 us** |  **1.00** |    **0.00** |
|    PooledBinarySearch_Int | 10000 |    632.04 us |  1.7453 us |  1.6325 us |  1.02 |    0.00 |
|   ListBinarySearch_String | 10000 | 11,683.84 us | 33.0237 us | 30.8904 us | 18.81 |    0.08 |
| PooledBinarySearch_String | 10000 | 11,846.67 us | 67.0003 us | 59.3940 us | 19.07 |    0.12 |
