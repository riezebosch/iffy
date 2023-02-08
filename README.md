# IF

## if/then

```csharp
new ServiceCollection()
    .If(true,  s => s.AddSingleton<Ext>())
    .BuildServiceProvider()
```

## if/then/else

```csharp
new ServiceCollection()
    .If(false,  
        s => s.AddSingleton<List<int>>(), 
        s => s.AddSingleton<Ext>())
    .BuildServiceProvider()
```

