using System;

namespace SlangForDotNet.AST;

/// <summary>
///    Node for Boolean Constant ( TRUE, FALSE }
///    Value
/// </summary>
public class BooleanConstant : Exp
{
    private SYMBOL_INFO info;
    public BooleanConstant(bool pvalue)
    {
        info = new SYMBOL_INFO();
        info.SymbolName = null;
        info.bol_val = pvalue;
        info.Type = TYPE_INFO.TYPE_BOOL;
    }

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