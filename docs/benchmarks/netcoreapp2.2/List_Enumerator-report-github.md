``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                     Method |      N |         Mean |        Error |       StdDev |       Median | Ratio | RatioSD |
|--------------------------- |------- |-------------:|-------------:|-------------:|-------------:|------:|--------:|
|          **ListEnumerate_Int** |   **1000** |   **2,185.7 ns** |     **5.803 ns** |     **5.428 ns** |   **2,184.7 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |   1000 |   2,243.7 ns |    42.075 ns |    46.766 ns |   2,255.6 ns |  1.03 |    0.02 |
|    PooledEnumerateSpan_Int |   1000 |     539.5 ns |     1.368 ns |     1.212 ns |     539.3 ns |  0.25 |    0.00 |
|       ListEnumerate_String |   1000 |   3,719.9 ns |    36.316 ns |    32.194 ns |   3,722.9 ns |  1.70 |    0.01 |
|     PooledEnumerate_String |   1000 |   3,304.3 ns |     8.404 ns |     7.017 ns |   3,302.3 ns |  1.51 |    0.01 |
| PooledEnumerateSpan_String |   1000 |     344.1 ns |     6.843 ns |    13.508 ns |     347.4 ns |  0.16 |    0.01 |
|                            |        |              |              |              |              |       |         |
|          **ListEnumerate_Int** |  **10000** |  **21,833.4 ns** |    **73.906 ns** |    **69.132 ns** |  **21,812.5 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int |  10000 |  21,735.7 ns |   427.777 ns |   653.260 ns |  21,822.7 ns |  1.00 |    0.03 |
|    PooledEnumerateSpan_Int |  10000 |   5,762.0 ns |   110.314 ns |   122.613 ns |   5,808.6 ns |  0.26 |    0.01 |
|       ListEnumerate_String |  10000 |  34,790.4 ns |   775.748 ns | 1,087.490 ns |  34,164.1 ns |  1.60 |    0.06 |
|     PooledEnumerate_String |  10000 |  34,832.0 ns |   694.675 ns | 1,121.771 ns |  35,263.2 ns |  1.59 |    0.05 |
| PooledEnumerateSpan_String |  10000 |   4,698.5 ns |    93.346 ns |   249.160 ns |   4,676.7 ns |  0.21 |    0.01 |
|                            |        |              |              |              |              |       |         |
|          **ListEnumerate_Int** | **100000** | **217,971.1 ns** |   **516.965 ns** |   **431.689 ns** | **217,810.8 ns** |  **1.00** |    **0.00** |
|        PooledEnumerate_Int | 100000 | 207,785.1 ns | 1,111.238 ns |   927.934 ns | 207,837.0 ns |  0.95 |    0.00 |
|    PooledEnumerateSpan_Int | 100000 |  55,692.7 ns | 1,105.971 ns | 1,398.699 ns |  55,767.3 ns |  0.26 |    0.01 |
|       ListEnumerate_String | 100000 | 345,619.0 ns | 7,801.411 ns | 8,347.422 ns | 341,729.0 ns |  1.59 |    0.04 |
|     PooledEnumerate_String | 100000 | 353,924.4 ns | 2,335.070 ns | 2,184.226 ns | 354,905.8 ns |  1.62 |    0.01 |
| PooledEnumerateSpan_String | 100000 |  54,075.3 ns |   596.547 ns |   528.823 ns |  53,844.9 ns |  0.25 |    0.00 |
