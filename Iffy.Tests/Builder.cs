using Microsoft.Extensions.DependencyInjection;

namespace Iffy.Tests;

public class Builder
{
    [Fact]
    public void True()
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        var @else = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        
        new ServiceCollection()
            .If(true)
            .Then(then)
            .Else(@else)
            .BuildServiceProvider();
        
        then.Received().Invoke(Arg.Any<IServiceCollection>());
        @else.Received(0).Invoke(Arg.Any<IServiceCollection>());
    }

    [Fact]
    public void False()
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        var @else = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        
        new ServiceCollection()
            .If(false)
            .Then(then)
            .Else(@else)
            .BuildServiceProvider();
        
        then.Received(0).Invoke(Arg.Any<IServiceCollection>());
        @else.Received(1).Invoke(Arg.Any<IServiceCollection>());
    }

    [Fact]
    public void TrueThen()
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        
        new ServiceCollection()
            .AddSingleton<List<int>>()
            .If(true)
            .Then(then)
            .Else()
            .BuildServiceProvider();
        
        then.Received(1).Invoke(Arg.Any<IServiceCollection>());
    }
    
    [Fact]
    public void FalseThen()
    {
        var then = Substitute.For<Func<IServiceCollection, IServiceCollection>>();
        
        new ServiceCollection()
            .AddSingleton<List<int>>()
            .If(false)
            .Then(then)
            .Else()
            .BuildServiceProvider();
        
        then.Received(0).Invoke(Arg.Any<IServiceCollection>());
    }
}