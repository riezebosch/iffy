namespace Iffy;

public class Value<TIn> : Then<TIn>
{
    private readonly TIn _in;
    public Value(TIn @in) => _in = @in;
    public TIn Else(Func<TIn, TIn> @else) => _in;
    public TIn Else() => _in;
}

public class Value<TIn, TOut> : Then<TIn, TOut>
{
    private readonly TOut _out;
    public Value(TOut @out) => _out = @out;
    public TOut Else(Func<TIn, TOut> @else) => _out;
}