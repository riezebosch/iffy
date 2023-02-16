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
        _if ? new Value<TIn, TOut>(then(_in)) 
            : new Else<TIn, TOut>(_in);

    public Then<TIn> Then(Func<TIn, TIn> then) =>
        _if ? new Value<TIn>(then(_in))
            : new Else<TIn>(_in);
}