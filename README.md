# Iffy

[![nuget](https://img.shields.io/nuget/v/Iffy.svg)](https://www.nuget.org/packages/Iffy/)
[![stryker](https://img.shields.io/endpoint?style=flat&url=https%3A%2F%2Fbadge-api.stryker-mutator.io%2Fgithub.com%2Friezebosch%2Fiffy%2Fmain&label=stryker)](https://dashboard.stryker-mutator.io/reports/github.com/riezebosch/iffy/main)

The one extension method you never knew you needed it that much: 

> iffy, if-as-a-method.
> Use conditions in fluent builder patterns, everywhere.

Three flavours to construct your if-method, using:

1. [arguments](#if-then): `.If(condition, then, [else])`
2. a [builder](#ifthen): `.If(condition).Then(action).Else([action])`
3. or an [expression](#if): `.If(condition ? action : action)` 

## if, then

```csharp
new ServiceCollection()
    .If(IsDevelopment(),  then => then.AddSingleton<...>())
    .BuildServiceProvider()
```

## if, then, else

```csharp
new ServiceCollection()
    .If(IsDevelopment(),  
        then => then.AddSingleton<...>(), 
        @else => @else.AddSingleton<...>())
    .BuildServiceProvider()
```

## if().then()

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<...>()) 
    .Else()
    .BuildServiceProvider()
```

or:

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<...>()) 
    .Else(_ => _)
    .BuildServiceProvider()
```

**Update:** the `.Then(action)` is from now on still executed even when you omit the `.Else([action])`. For sure your method chain then stops there.

## if().then().else()

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<...>()) 
    .Else(s => s.AddSingleton<...>())
    .BuildServiceProvider()
```

## if ?:

```csharp
new ServiceCollection()
    .If(IsDevelopment() 
        ? s => s.AddSingleton<...>()
        : s => s.AddSingleton<...>())
    .BuildServiceProvider()
```

This one is a bit of a hack since it just accepts and executes whatever expression you provide. 
But it looks so pretty 🥰.

## and with any other builder

```csharp
new ServiceCollection()
    .AddRebus(configure => configure
        .Transport(t => t.UseAzureServiceBus("", "", new DefaultAzureCredential())
            .AutomaticallyRenewPeekLock()
            .SetDuplicateDetectionHistoryTimeWindow(new TimeSpan(0, 2, 0))
            .If(IsDevelopment()
                ? s => s.SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5))
                : s => s.DoNotCreateQueues()
                    .DoNotCheckQueueConfiguration())
            .If(!IsDevelopment(), then => then
                .DoNotCreateQueues()
                .DoNotCheckQueueConfiguration())
            .If(IsDevelopment(),
                then => then
                    .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)),
                @else => @else
                    .DoNotCreateQueues()
                    .DoNotCheckQueueConfiguration())
            .If(IsDevelopment())
            .Then(settings => settings
                .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)))
            .Else(settings => settings
                .DoNotCreateQueues()
                .DoNotCheckQueueConfiguration())));
```

## benchmark

``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.1 (22C65) [Darwin 22.2.0]
Apple M1, 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.405
  [Host]     : .NET 6.0.13 (6.0.1322.58009), Arm64 RyuJIT AdvSIMD
  Job-DYHKDS : .NET 6.0.13 (6.0.1322.58009), Arm64 RyuJIT AdvSIMD

InvocationCount=1000000  IterationCount=5  LaunchCount=1  
WarmupCount=3  

```
|     Method |     Mean |    Error |    StdDev |
|----------- |---------:|---------:|----------:|
|       None | 3.713 μs | 4.506 μs | 1.1703 μs |
|     Normal | 3.855 μs | 3.679 μs | 0.9555 μs |
|     IfThen | 4.003 μs | 5.244 μs | 1.3618 μs |
| IfThenElse | 3.958 μs | 4.304 μs | 1.1178 μs |
|         If | 3.970 μs | 4.232 μs | 1.0991 μs |
|    Builder | 3.751 μs | 4.092 μs | 1.0628 μs |

See for yourselves in [Iffy.Benchmark](Iffy.Benchmark).