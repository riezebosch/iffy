namespace Iffy;

public class Then<TIn, TOut>
{
    private readonly TIn _in;
    private readonly bool _if;
    private readonly Func<TIn, TOut> _then;

    public Then(TIn @in, bool @if, Func<TIn, TOut> then)
    {
        _in = @in;
        _if = @if;
        _then = then;
    }

    public TOut Else(Func<TIn, TOut> @else) => 
        _in.If(_if, _then, @else);
}

public class Then<TIn> : Then<TIn, TIn>
{
    public Then(TIn @in, bool @if, Func<TIn, TIn> then) : base(@in, @if, then)
    {
    }

    public TIn Else() =>
        Else(x => x);
}