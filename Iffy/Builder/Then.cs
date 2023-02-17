namespace Iffy.Builder;

public interface Then<out TIn, TOut>
{
    TOut Else(Func<TIn, TOut> @else);
}

public interface Then<TIn> : Then<TIn, TIn>
{
    TIn Else();
}