``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |---------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **2.966 ms** | **0.0206 ms** | **0.0193 ms** |  **1.00** |    **0.00** |   **1738.2813** |    **996.0938** |    **996.0938** |           **7019341 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 3.739 ms | 0.0066 ms | 0.0059 ms |  1.26 |    0.01 |           - |           - |           - |                64 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 2.215 ms | 0.0065 ms | 0.0058 ms |  0.75 |    0.01 |           - |           - |           - |                64 B |
|                                                |           |          |           |           |       |         |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2.175 ms** | **0.0432 ms** | **0.0561 ms** |  **1.00** |    **0.00** |    **761.7188** |    **320.3125** |    **289.0625** |           **6402552 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True | 1.488 ms | 0.0036 ms | 0.0032 ms |  0.68 |    0.02 |           - |           - |           - |                48 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True | 1.270 ms | 0.0044 ms | 0.0039 ms |  0.58 |    0.01 |           - |           - |           - |                48 B |
