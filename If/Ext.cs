using System;

namespace If;

public static class Ext
{
    public static TOut If<TIn, TOut>(this TIn @in, bool @if, Func<TIn, TOut> then) where TIn: TOut => 
        @if ? then(@in) : @in;
    
    public static TOut If<TIn, TOut>(this TIn @in, bool @if, Func<TIn, TOut> then, Func<TIn, TOut> @else) where TIn: TOut => 
        @if ? then(@in) : @else(@in);
}