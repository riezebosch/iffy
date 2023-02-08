using Azure.Identity;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;

namespace If.Tests;

public class Ext
{
    [Fact]
    public void True() =>
        new ServiceCollection()
            .If(true,  s => s.AddSingleton<Ext>())
            .BuildServiceProvider()
            .GetService<Ext>()
            .Should()
            .NotBeNull();

    [Fact]
    public void False() =>
        new ServiceCollection()
            .If(false,  s => s.AddSingleton<Ext>())
            .BuildServiceProvider()
            .GetService<Ext>()
            .Should()
            .BeNull();
    
    [Fact]
    public void Else() =>
        new ServiceCollection()
            .If(false,  
                s => s.AddSingleton<List<int>>(), 
                s => s.AddSingleton<Ext>())
            .BuildServiceProvider()
            .GetService<Ext>()
            .Should()
            .NotBeNull();

    [Fact]
    public void Rebus() =>
        new ServiceCollection()
            .AddRebus(configure => configure
                .Transport(t => t.UseAzureServiceBus("", "", new DefaultAzureCredential())
                    .AutomaticallyRenewPeekLock()
                    .SetDuplicateDetectionHistoryTimeWindow(new TimeSpan(0, 2, 0))
                    .If(false, 
                        settings => settings
                            .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)),
                        settings => settings
                            .DoNotCreateQueues()
                            .DoNotCheckQueueConfiguration())));
}