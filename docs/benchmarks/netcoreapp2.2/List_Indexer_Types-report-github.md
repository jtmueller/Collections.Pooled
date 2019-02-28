``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |      Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|           ListIndexer_Int | 16.712 us | 0.0555 us | 0.0519 us |  1.00 |           - |           - |           - |                   - |
|         PooledIndexer_Int | 16.693 us | 0.0470 us | 0.0440 us |  1.00 |           - |           - |           - |                   - |
|    PooledIndexer_Span_Int |  4.788 us | 0.0125 us | 0.0111 us |  0.29 |           - |           - |           - |                   - |
|        ListIndexer_String | 31.078 us | 0.1101 us | 0.1030 us |  1.86 |           - |           - |           - |                   - |
|      PooledIndexer_String | 31.071 us | 0.0732 us | 0.0649 us |  1.86 |           - |           - |           - |                   - |
| PooledIndexer_Span_String |  2.421 us | 0.0064 us | 0.0060 us |  0.14 |           - |           - |           - |                   - |
