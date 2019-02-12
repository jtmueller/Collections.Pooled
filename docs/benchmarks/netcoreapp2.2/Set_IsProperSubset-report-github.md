``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                             Method | InitialSetSize |              Mean |             Error |            StdDev |            Median |         Ratio |    RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|----------------------------------- |--------------- |------------------:|------------------:|------------------:|------------------:|--------------:|-----------:|------------:|------------:|------------:|--------------------:|
|     **HashSet_IsProperSubset_Hashset** |         **320000** |          **10.53 ns** |         **0.0353 ns** |         **0.0313 ns** |          **10.53 ns** |          **1.00** |       **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsProperSubset_PooledSet |         320000 |          10.49 ns |         0.0287 ns |         0.0269 ns |          10.49 ns |          1.00 |       0.00 |           - |           - |           - |                   - |
|        HashSet_IsProperSubset_Enum |         320000 |  10,302,490.33 ns |    65,364.2611 ns |    57,943.7149 ns |  10,282,716.56 ns |    977,989.49 |   6,249.87 |           - |           - |           - |             12096 B |
|      PooledSet_IsProperSubset_Enum |         320000 |  10,362,975.49 ns |    52,148.0365 ns |    48,779.3057 ns |  10,357,969.45 ns |    983,432.41 |   5,728.11 |           - |           - |           - |             12056 B |
|       HashSet_IsProperSubset_Array |         320000 |  10,138,869.24 ns |    63,515.0129 ns |    59,411.9824 ns |  10,116,113.51 ns |    963,055.20 |   5,570.26 |           - |           - |           - |             12088 B |
|     PooledSet_IsProperSubset_Array |         320000 |   8,470,736.59 ns |    52,301.7625 ns |    48,923.1011 ns |   8,465,437.42 ns |    804,571.23 |   5,174.23 |           - |           - |           - |             12016 B |
|                                    |                |                   |                   |                   |                   |               |            |             |             |             |                     |
|     **HashSet_IsProperSubset_Hashset** |        **8000000** |          **10.42 ns** |         **0.2697 ns** |         **0.6196 ns** |          **10.08 ns** |          **1.00** |       **0.00** |           **-** |           **-** |           **-** |                   **-** |
| PooledSet_IsProperSubset_PooledSet |        8000000 |          11.35 ns |         0.0741 ns |         0.0657 ns |          11.34 ns |          1.00 |       0.03 |           - |           - |           - |                   - |
|        HashSet_IsProperSubset_Enum |        8000000 | 301,340,146.67 ns | 1,367,174.0698 ns | 1,278,855.4732 ns | 301,195,000.00 ns | 26,480,183.46 | 750,218.48 |           - |           - |           - |             12608 B |
|      PooledSet_IsProperSubset_Enum |        8000000 | 289,020,057.14 ns | 2,217,993.0298 ns | 1,966,193.0489 ns | 288,480,900.00 ns | 25,420,547.52 | 845,938.85 |           - |           - |           - |             12568 B |
|       HashSet_IsProperSubset_Array |        8000000 | 302,621,913.33 ns | 2,196,476.6777 ns | 2,054,585.6471 ns | 302,132,500.00 ns | 26,593,825.54 | 804,637.85 |           - |           - |           - |             12600 B |
|     PooledSet_IsProperSubset_Array |        8000000 | 264,808,973.33 ns | 1,704,340.6200 ns | 1,594,241.2734 ns | 265,112,200.00 ns | 23,270,685.22 | 691,605.55 |           - |           - |           - |             12528 B |
