``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|              Method |  Job | Runtime | InitialSetSize |       Mean |      Error |     StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |----- |-------- |--------------- |-----------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|     **HashSet_Hashset** |  **Clr** |     **Clr** |          **32000** |   **545.3 us** |   **7.179 us** |   **6.715 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **48 B** |
| PooledSet_PooledSet |  Clr |     Clr |          32000 |   569.8 us |   8.481 us |   7.518 us |  1.04 |    0.02 |     - |     - |     - |      48 B |
|        HashSet_Enum |  Clr |     Clr |          32000 | 1,243.3 us |  17.605 us |  16.468 us |  2.28 |    0.03 |     - |     - |     - |      48 B |
|      PooledSet_Enum |  Clr |     Clr |          32000 |   922.9 us |  10.178 us |   9.520 us |  1.69 |    0.03 |     - |     - |     - |      48 B |
|       HashSet_Array |  Clr |     Clr |          32000 |   889.0 us |  16.783 us |  15.699 us |  1.63 |    0.03 |     - |     - |     - |      40 B |
|     PooledSet_Array |  Clr |     Clr |          32000 |   681.7 us |  11.184 us |  10.462 us |  1.25 |    0.02 |     - |     - |     - |         - |
|                     |      |         |                |            |            |            |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |          32000 |   564.7 us |  10.895 us |  10.192 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |          32000 |   547.5 us |  10.580 us |  10.391 us |  0.97 |    0.03 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |          32000 |   952.1 us |   8.687 us |   8.126 us |  1.69 |    0.03 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |          32000 |   912.7 us |  17.005 us |  15.906 us |  1.62 |    0.04 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |          32000 |   899.2 us |  15.069 us |  14.095 us |  1.59 |    0.04 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |          32000 |   693.4 us |   9.629 us |   9.007 us |  1.23 |    0.03 |     - |     - |     - |         - |
|                     |      |         |                |            |            |            |       |         |       |       |       |           |
|     **HashSet_Hashset** |  **Clr** |     **Clr** |         **320000** |   **810.2 us** |  **15.408 us** |  **15.132 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |      **48 B** |
| PooledSet_PooledSet |  Clr |     Clr |         320000 |   822.7 us |  10.780 us |  10.083 us |  1.02 |    0.02 |     - |     - |     - |      48 B |
|        HashSet_Enum |  Clr |     Clr |         320000 | 9,135.3 us | 103.006 us |  91.312 us | 11.29 |    0.24 |     - |     - |     - |     128 B |
|      PooledSet_Enum |  Clr |     Clr |         320000 | 9,396.9 us |  97.053 us |  90.783 us | 11.61 |    0.25 |     - |     - |     - |     128 B |
|       HashSet_Array |  Clr |     Clr |         320000 | 8,927.8 us | 158.157 us | 140.202 us | 11.04 |    0.25 |     - |     - |     - |     128 B |
|     PooledSet_Array |  Clr |     Clr |         320000 | 7,085.6 us | 136.501 us | 127.684 us |  8.75 |    0.26 |     - |     - |     - |         - |
|                     |      |         |                |            |            |            |       |         |       |       |       |           |
|     HashSet_Hashset | Core |    Core |         320000 |   845.5 us |  15.629 us |  14.619 us |  1.00 |    0.00 |     - |     - |     - |      40 B |
| PooledSet_PooledSet | Core |    Core |         320000 |   826.0 us |  16.329 us |  15.274 us |  0.98 |    0.03 |     - |     - |     - |      40 B |
|        HashSet_Enum | Core |    Core |         320000 | 9,339.5 us | 174.160 us | 154.389 us | 11.06 |    0.25 |     - |     - |     - |      40 B |
|      PooledSet_Enum | Core |    Core |         320000 | 9,219.3 us | 108.280 us | 101.285 us | 10.91 |    0.18 |     - |     - |     - |      40 B |
|       HashSet_Array | Core |    Core |         320000 | 8,964.6 us | 117.590 us | 109.994 us | 10.61 |    0.19 |     - |     - |     - |      32 B |
|     PooledSet_Array | Core |    Core |         320000 | 6,688.9 us |  76.772 us |  71.813 us |  7.91 |    0.16 |     - |     - |     - |         - |
