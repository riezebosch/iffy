namespace Iffy.Builder;

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
        _if ? new True<TIn, TOut>(then(_in)) 
            : new False<TIn, TOut>(_in);

    public Then<TIn> Then(Func<TIn, TIn> then) =>
        _if ? new True<TIn>(then(_in))
            : new False<TIn>(_in);
}