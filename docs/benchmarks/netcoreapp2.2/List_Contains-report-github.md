``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|         Method |      N |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |
|--------------- |------- |-----------:|----------:|----------:|-----------:|------:|--------:|
|   **ListContains** |   **1000** |   **1.937 ms** | **0.0387 ms** | **0.0708 ms** |   **1.901 ms** |  **1.00** |    **0.00** |
| PooledContains |   1000 |   2.000 ms | 0.0380 ms | 0.0452 ms |   2.016 ms |  1.03 |    0.03 |
|                |        |            |           |           |            |       |         |
|   **ListContains** |  **10000** |  **19.221 ms** | **0.0249 ms** | **0.0208 ms** |  **19.228 ms** |  **1.00** |    **0.00** |
| PooledContains |  10000 |  19.152 ms | 0.3307 ms | 0.3093 ms |  19.223 ms |  1.00 |    0.02 |
|                |        |            |           |           |            |       |         |
|   **ListContains** | **100000** | **191.486 ms** | **0.6958 ms** | **0.6508 ms** | **191.246 ms** |  **1.00** |    **0.00** |
| PooledContains | 100000 | 189.324 ms | 3.7416 ms | 4.9950 ms | 191.497 ms |  0.99 |    0.02 |
