namespace Iffy;

public class Else<TIn, TOut> : Then<TIn, TOut>
{
    private readonly TIn _in;
    public Else(TIn @in) => _in = @in;
    TOut Then<TIn, TOut>.Else(Func<TIn, TOut> @else) => @else(_in);
}

public class Else<TIn> : Then<TIn>
{
    private readonly TIn _in;
    public Else(TIn @in) => _in = @in;
    TIn Then<TIn, TIn>.Else(Func<TIn, TIn> @else) => @else(_in);
    TIn Then<TIn>.Else() =>  _in;
}