# Iffy

The one extension method you never knew you needed it that much: 

> iffy, if-as-a-method

Use conditions in fluent builder patterns, everywhere.

## if, then

```csharp
new ServiceCollection()
    .If(IsDevelopment(),  s => s.AddSingleton<...>())
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

## if().then().else()

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<...>()) 
    .Else(s => s.AddSingleton<...>())
    .BuildServiceProvider()
```

## any other builder

```csharp
new ServiceCollection()
    .AddRebus(configure => configure
        .Transport(t => t.UseAzureServiceBus("", "", new DefaultAzureCredential())
            .AutomaticallyRenewPeekLock()
            .SetDuplicateDetectionHistoryTimeWindow(new TimeSpan(0, 2, 0))
            .If(!IsDevelopment(), settings => settings
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
