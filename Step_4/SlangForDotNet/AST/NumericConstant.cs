using System;

namespace SlangForDotNet.AST;

public class NumericConstant : Exp
{
    /// <summary>
    ///    Info field
    /// </summary>
    private SYMBOL_INFO info;

    public NumericConstant(double pvalue)
    {
        info = new SYMBOL_INFO();
        info.SymbolName = null;
        info.dbl_val = pvalue;
        info.Type = TYPE_INFO.TYPE_NUMERIC;
    }

    /// <summary>
    ///    Evaluation of boolean will given the value
    /// </summary>
    /// <param name="local"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        return info;
    }

    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {
        return info.Type;
    }

    public override TYPE_INFO get_type()
    {
        return info.Type;
    }
}