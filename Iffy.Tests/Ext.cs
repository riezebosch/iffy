using Microsoft.Extensions.DependencyInjection;

namespace Iffy.Tests;

public class Ext
{
    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void IfThen(bool @if, int received)
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();

        new ServiceCollection()
            .If(@if, then)
            .BuildServiceProvider();
        
        then.Received(received)(Arg.Any<IServiceCollection>());
    }
    
    [Theory]
    [InlineData(true, 1, 0)]
    [InlineData(false, 0, 1)]

    public void IfThenElse(bool @if, int a, int b)
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        var @else = Substitute.For<Func<IServiceCollection, IServiceCollection>>();

        new ServiceCollection()
            .If(@if, then, @else)
            .BuildServiceProvider();
        
        then.Received(a)(Arg.Any<IServiceCollection>());
        @else.Received(b)(Arg.Any<IServiceCollection>());
    }
    
    [Theory]
    [InlineData(true, 1, 0)]
    [InlineData(false, 0, 1)]
    public void If(bool @if, int a, int b)
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        var @else = Substitute.For<Func<IServiceCollection, IServiceCollection>>();

        new ServiceCollection()
            .If(@if ? s => then(s) : s => @else(s))
            .If(DateTime.Today.DayOfWeek == DayOfWeek.Monday
                ? s => s.AddSingleton<Ext>()
                : s => s.AddSingleton<Ext>())
            .BuildServiceProvider();
        
        then.Received(a)(Arg.Any<IServiceCollection>());
        @else.Received(b)(Arg.Any<IServiceCollection>());
    }
}