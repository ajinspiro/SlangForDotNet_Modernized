using System;
using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

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

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        //
        // Emit LDC_R8 => Load Constant Real 8
        // IEEE 754 floating Point
        // 
        // cont.CodeOutput will return ILGenerator of the 
        // current method...
        cont.CodeOutput.Emit(OpCodes.Ldc_R8, info.dbl_val);
        return true;
    }
}