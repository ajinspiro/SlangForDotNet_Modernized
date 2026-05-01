using System;
using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

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

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        //
        // Retrieve the IL Code generator and Emit 
        //    LDC_I4 => Load Constant Integer 4
        // We are planning to use a 32 bit long for Boolean 
        // True or False
        cont.CodeOutput.Emit(OpCodes.Ldc_I4, (info.bol_val) ? 1 : 0);
        return true;
    }
}