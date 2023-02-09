using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;

namespace Iffy.Tests;

public class Example
{
    [Fact]
    public void Rebus() =>
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

    private static bool IsDevelopment() => true;
}