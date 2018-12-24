``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      ListIndexer_Int |  17.69 us | 0.1200 us | 0.1002 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|    PooledIndexer_Int | 162.42 us | 0.6372 us | 0.5960 us |  9.18 |    0.05 |           - |           - |           - |                   - |
|   ListIndexer_String |  31.34 us | 0.3377 us | 0.2994 us |  1.77 |    0.02 |           - |           - |           - |                   - |
| PooledIndexer_String | 165.53 us | 1.3346 us | 1.2484 us |  9.36 |    0.11 |           - |           - |           - |                   - |
