# Iffy

The one extension method you never knew you needed it that much.

## if, then

```csharp
new ServiceCollection()
    .If(IsDevelopment(),  s => s.AddSingleton<Ext>())
    .BuildServiceProvider()
```

## if, then, else

```csharp
new ServiceCollection()
    .If(IsDevelopment(),  
        s => s.AddSingleton<List<int>>(), 
        s => s.AddSingleton<Ext>())
    .BuildServiceProvider()
```
## if().then()

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<List<int>>()) 
    .Else()
    .BuildServiceProvider()
```

## if().then().else()

```csharp
new ServiceCollection()
    .If(IsDevelopment())
    .Then(s => s.AddSingleton<List<int>>()) 
    .Else(s => s.AddSingleton<Ext>())
    .BuildServiceProvider()
```




## with other builders

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
                settings => settings
                    .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)),
                settings => settings
                    .DoNotCreateQueues()
                    .DoNotCheckQueueConfiguration())
            .If(IsDevelopment())
            .Then(settings => settings
                .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)))
            .Else(settings => settings
                .DoNotCreateQueues()
                .DoNotCheckQueueConfiguration())));
```
