``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |---------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **2.897 ms** | **0.0246 ms** | **0.0230 ms** |  **1.00** |    **0.00** |   **1746.0938** |    **996.0938** |    **996.0938** |           **7018479 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 3.736 ms | 0.0147 ms | 0.0138 ms |  1.29 |    0.01 |           - |           - |           - |                64 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 2.215 ms | 0.0061 ms | 0.0057 ms |  0.76 |    0.01 |           - |           - |           - |                64 B |
|                                                |           |          |           |           |       |         |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2.157 ms** | **0.0451 ms** | **0.0632 ms** |  **1.00** |    **0.00** |    **734.3750** |    **316.4063** |    **277.3438** |           **6402392 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True | 1.501 ms | 0.0050 ms | 0.0047 ms |  0.70 |    0.02 |           - |           - |           - |                64 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True | 1.262 ms | 0.0057 ms | 0.0053 ms |  0.58 |    0.02 |           - |           - |           - |                64 B |
