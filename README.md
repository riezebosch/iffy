# Iffy

[![nuget](https://img.shields.io/nuget/v/Iffy.svg)](https://www.nuget.org/packages/Iffy/)

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

> ⚠️ You _must_ close the builder with an `.Else([action])`, otherwise it will not be executed!

or:

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<...>()) 
    .Else(_ => _)
    .BuildServiceProvider()
```

for when the `.Else()` method is not available (due to different input/output types).

## if().then().else()

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<...>()) 
    .Else(s => s.AddSingleton<...>())
    .BuildServiceProvider()
```

> ⚠️ You _must_ close the builder with an `.Else([action])`, otherwise it will not be executed!

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
