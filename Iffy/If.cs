namespace Iffy;

public class If<TIn>
{
    private readonly TIn _in;
    private readonly bool _if;

    public If(TIn @in, bool @if)
    {
        _in = @in;
        _if = @if;
    }

    public Then<TIn, TOut> Then<TOut>(Func<TIn, TOut> then) =>
        new(_in, _if, then);
    public Then<TIn> Then(Func<TIn, TIn> then) => 
        new(_in, _if, then);
}