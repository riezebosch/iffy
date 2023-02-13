namespace Iffy;

public static class Ext
{
    public static If<TIn> If<TIn>(this TIn @in, bool @if) => 
        new (@in, @if);
    public static T If<T>(this T @in, Action<T> with)
    {
        with(@in);
        return @in;
    }
    public static TOut If<TIn, TOut>(this TIn @in, bool @if, Func<TIn, TOut> then) where TIn: TOut => 
        @if ? then(@in) : @in;
    public static TOut If<TIn, TOut>(this TIn @in, bool @if, Func<TIn, TOut> then, Func<TIn, TOut> @else) => 
        @if ? then(@in) : @else(@in);
}