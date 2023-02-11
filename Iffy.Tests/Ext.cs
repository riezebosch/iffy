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
        
        then.Received(received).Invoke(Arg.Any<IServiceCollection>());
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
        
        then.Received(a).Invoke(Arg.Any<IServiceCollection>());
        @else.Received(b).Invoke(Arg.Any<IServiceCollection>());
    }
}