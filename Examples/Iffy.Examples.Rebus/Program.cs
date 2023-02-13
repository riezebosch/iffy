using Azure.Identity;
using Iffy;
using Rebus.Config;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddRebus(configure => configure
        .Transport(t => t.UseAzureServiceBus("", "", new DefaultAzureCredential())
            .AutomaticallyRenewPeekLock()
            .SetDuplicateDetectionHistoryTimeWindow(new TimeSpan(0, 2, 0))
            .If(builder.Environment.IsDevelopment()
                ? s => s.SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5))
                : s => s
                    .DoNotCreateQueues()
                    .DoNotCheckQueueConfiguration())
            .If(!builder.Environment.IsDevelopment(), then => then
                .DoNotCreateQueues()
                .DoNotCheckQueueConfiguration())
            .If(builder.Environment.IsDevelopment(), 
                then => then
                    .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)),
                @else => @else
                    .DoNotCreateQueues()
                    .DoNotCheckQueueConfiguration())
            .If(builder.Environment.IsDevelopment())
            .Then(settings => settings
                .SetAutoDeleteOnIdle(TimeSpan.FromMinutes(5)))
            .Else(settings => settings
                .DoNotCreateQueues()
                .DoNotCheckQueueConfiguration())));

var app = builder.Build();
app.Run();