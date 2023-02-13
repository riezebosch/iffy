using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Iffy.Benchmark;

[SimpleJob(launchCount: 1, warmupCount: 3, iterationCount: 5, invocationCount: 1000000)]
public class M
{
    public record Data(int Id);

    private readonly Data _data = new(2344);
    private readonly bool @if = true;

    [Benchmark]
    public Data None() =>
        new ServiceCollection()
            .AddSingleton(_data)
            .BuildServiceProvider()
            .GetRequiredService<Data>();
    
    [Benchmark]
    public Data Normal()
    {
        var services = new ServiceCollection();
        if (@if)
        {
            services.AddSingleton(_data);
        }
        
        return services
            .BuildServiceProvider()
            .GetRequiredService<Data>();
    }

    [Benchmark]
    public Data IfThen() =>
        new ServiceCollection()
            .If(@if, then => then.AddSingleton(_data))
            .BuildServiceProvider()
            .GetRequiredService<Data>();
    
    [Benchmark]
    public Data IfThenElse() =>
        new ServiceCollection()
            .If(@if, then => then.AddSingleton(_data), @else => @else)
            .BuildServiceProvider()
            .GetRequiredService<Data>();

    [Benchmark]
    public Data If() =>
        new ServiceCollection()
            .If(@if 
                ? s => s.AddSingleton(_data)
                : s => s.AddSingleton(_data))
            .BuildServiceProvider()
            .GetRequiredService<Data>();

    [Benchmark]
    public Data Builder() =>
        new ServiceCollection()
            .If(@if)
            .Then(s => s.AddSingleton(_data))
            .Else(_ => _)
            .BuildServiceProvider()
            .GetRequiredService<Data>();
}