using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

/// <summary>
///   To Store Literal string enclosed in quotes  
/// </summary>
public class StringLiteral : Exp
{
    /// <summary>
    ///  info field
    /// </summary>
    private SYMBOL_INFO info;

    public StringLiteral(string pvalue)
    {
        info = new SYMBOL_INFO();
        info.SymbolName = null;
        info.str_val = pvalue;
        info.Type = TYPE_INFO.TYPE_STRING;
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
        //
        // For string emit 
        //     LDSTR => Load String 
        //
        cont.CodeOutput.Emit(OpCodes.Ldstr, info.str_val);
        return true;
    }
}