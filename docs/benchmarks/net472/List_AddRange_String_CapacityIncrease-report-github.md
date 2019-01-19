``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                                         Method | LargeSets |     Mean |     Error |    StdDev |   Median | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------------------- |---------- |---------:|----------:|----------:|---------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|           **ListAddRange_String_CapacityIncrease** |     **False** | **2.919 ms** | **0.0367 ms** | **0.0343 ms** | **2.909 ms** |  **1.00** |    **0.00** |   **1742.1875** |    **996.0938** |    **996.0938** |           **7018496 B** |
|   PooledAddRange_String_CapacityIncrease_Array |     False | 4.117 ms | 0.0362 ms | 0.0321 ms | 4.101 ms |  1.41 |    0.02 |           - |           - |           - |                   - |
| PooledAddRange_Int_CapacityIncrease_Enumerable |     False | 2.285 ms | 0.0453 ms | 0.0757 ms | 2.235 ms |  0.79 |    0.03 |           - |           - |           - |                64 B |
|                                                |           |          |           |           |          |       |         |             |             |             |                     |
|           **ListAddRange_String_CapacityIncrease** |      **True** | **2.260 ms** | **0.0445 ms** | **0.0530 ms** | **2.278 ms** |  **1.00** |    **0.00** |    **734.3750** |    **316.4063** |    **273.4375** |           **6402616 B** |
|   PooledAddRange_String_CapacityIncrease_Array |      True | 1.480 ms | 0.0030 ms | 0.0028 ms | 1.480 ms |  0.65 |    0.02 |           - |           - |           - |                48 B |
| PooledAddRange_Int_CapacityIncrease_Enumerable |      True | 1.266 ms | 0.0028 ms | 0.0027 ms | 1.266 ms |  0.56 |    0.01 |           - |           - |           - |                48 B |
