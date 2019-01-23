``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                     Method |      N |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------------- |------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   **DictContainsKey_Int_True** |   **1000** |   **8.265 us** | **0.0577 us** | **0.0540 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True |   1000 |   6.755 us | 0.0208 us | 0.0184 us |  0.82 |           - |           - |           - |                   - |
|                            |        |            |           |           |       |             |             |             |                     |
|   **DictContainsKey_Int_True** |  **10000** |  **82.459 us** | **0.7782 us** | **0.6899 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True |  10000 |  67.109 us | 0.3839 us | 0.3591 us |  0.81 |           - |           - |           - |                   - |
|                            |        |            |           |           |       |             |             |             |                     |
|   **DictContainsKey_Int_True** | **100000** | **821.935 us** | **1.5723 us** | **1.4707 us** |  **1.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledContainsKey_Int_True | 100000 | 670.315 us | 2.8438 us | 2.6601 us |  0.82 |           - |           - |           - |                   - |
