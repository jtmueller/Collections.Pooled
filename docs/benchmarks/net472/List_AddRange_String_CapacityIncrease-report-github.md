``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **2.924 ms** | **0.0279 ms** | **0.0261 ms** |  **1.00** |   **1734.3750** |    **996.0938** |    **996.0938** |           **7019661 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 3.735 ms | 0.0072 ms | 0.0063 ms |  1.28 |           - |           - |           - |                64 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 2.198 ms | 0.0073 ms | 0.0069 ms |  0.75 |           - |           - |           - |                64 B |
|                                                |           |          |           |           |       |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2.192 ms** | **0.0423 ms** | **0.0452 ms** |  **1.00** |    **726.5625** |    **285.1563** |    **277.3438** |           **6402488 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True | 1.500 ms | 0.0043 ms | 0.0041 ms |  0.68 |           - |           - |           - |                48 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True | 1.259 ms | 0.0035 ms | 0.0033 ms |  0.57 |           - |           - |           - |                48 B |
