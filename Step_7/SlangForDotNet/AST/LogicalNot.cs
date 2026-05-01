using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

/// <summary>
///     Logical !
/// </summary>
class LogicalNot : Exp
{

    private Exp ex1;
    TYPE_INFO _type;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="op"></param>
    /// <param name="e1"></param>

    public LogicalNot(Exp e1)
    {

        ex1 = e1;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO eval_left = ex1.Evaluate(cont);


        if (eval_left.Type == TYPE_INFO.TYPE_BOOL)
        {
            SYMBOL_INFO ret_val = new SYMBOL_INFO();
            ret_val.Type = TYPE_INFO.TYPE_BOOL;
            ret_val.SymbolName = "";
            ret_val.bol_val = !eval_left.bol_val;
            return ret_val;
        }
        else
        {
            return null;

        }


    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {
        TYPE_INFO eval_left = ex1.TypeCheck(cont);


        if (
            eval_left == TYPE_INFO.TYPE_BOOL)
        {
            _type = TYPE_INFO.TYPE_BOOL;
            return _type;
        }
        else
        {
            throw new Exception("Wrong Type in expression");

        }
    } 
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        ex1.Compile(cont);

        // Check whether top of the stack is 1 ( TRUE )
        // Check Whether the previous operation was successful
        // Functionally equivalent to Logical Not
        //
        // Case Top of Stack is 1 (TRUE )
        // ------------------------------
        // Top of Stack =>    [ 1 ]
        // LDC_I4 =>  [ 1 1 ] 
        // CEQ    =>  [ 1 ]
        // LDC_I4 =>  [ 1 0 ]
        // CEQ    =>  [ 0 ]
        //
        // Case Top of Stack is 0 (FALSE)
        // -----------------------------
        // Top of Stack =>    [ 0 ]
        // LDC_I4 =>  [ 0 1 ] 
        // CEQ    =>  [ 0 ]
        // LDC_I4 =>  [ 0 0 ]
        // CEQ    =>  [ 1 ]
        cont.CodeOutput.Emit(OpCodes.Ldc_I4, 1);
        cont.CodeOutput.Emit(OpCodes.Ceq);
        cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
        cont.CodeOutput.Emit(OpCodes.Ceq);

        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override TYPE_INFO get_type()
    {
        return _type;
    }


}