using Microsoft.Extensions.DependencyInjection;

namespace Iffy.Tests;

public class Ext
{
    [Fact]
    public void True() =>
        new ServiceCollection()
            .If(true, s => s.AddSingleton<Ext>())
            .BuildServiceProvider()
            .GetService<Ext>()
            .Should()
            .NotBeNull();

    [Fact]
    public void False() =>
        new ServiceCollection()
            .If(false, s => s.AddSingleton<Ext>())
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
}