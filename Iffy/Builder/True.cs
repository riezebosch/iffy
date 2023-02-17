namespace Iffy.Builder;

public class True<TIn, TOut> : Then<TIn, TOut>
{
    private readonly TOut _out;
    public True(TOut @out) => _out = @out;
    public TOut Else(Func<TIn, TOut> @else) => _out;
}

public class True<TIn> : True<TIn, TIn>, Then<TIn>
{
    public True(TIn @in) : base(@in) {}
    public TIn Else() => Else(_ => _);
}