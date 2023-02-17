namespace Iffy.Builder;

public class False<TIn, TOut> : Then<TIn, TOut>
{
    private readonly TIn _in;
    public False(TIn @in) => _in = @in;
    public TOut Else(Func<TIn, TOut> @else) => @else(_in);
}

public class False<TIn> : False<TIn, TIn>, Then<TIn>
{
    public False(TIn @in) : base(@in) {}
    public TIn Else() => Else(_ => _);
}