using Microsoft.Extensions.DependencyInjection;

namespace Iffy.Tests;

public class Builder
{
    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void IfThen(bool @if, int received)
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        
        new ServiceCollection()
            .AddSingleton<List<int>>()
            .If(@if)
            .Then(then)
            .Else()
            .BuildServiceProvider();
        
        then.Received(received)(Arg.Any<IServiceCollection>());
    }

    [Theory]
    [InlineData(true, 1, 0)]
    [InlineData(false, 0, 1)]

    public void IfThenElse(bool @if, int then_received, int else_received)
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        var @else = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        
        new ServiceCollection()
            .If(@if)
            .Then(then)
            .Else(@else)
            .BuildServiceProvider();
        
        then.Received(then_received)(Arg.Any<IServiceCollection>());
        @else.Received(else_received)(Arg.Any<IServiceCollection>());
    }
}