using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

class UnaryMinus : Exp
{
    /// <summary>
    ///  Plus has got a right expression (exp1 )
    ///  and a Associated type information
    /// </summary>
    private Exp exp1;
    TYPE_INFO _type;
    public UnaryMinus(Exp e1)
    {
        exp1 = e1;
    }

    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO eval_left = exp1.Evaluate(cont);
        if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC)
        {
            SYMBOL_INFO ret_val = new SYMBOL_INFO();
            ret_val.dbl_val = -eval_left.dbl_val;
            ret_val.Type = TYPE_INFO.TYPE_NUMERIC;
            ret_val.SymbolName = "";
            return ret_val;

        }
        else
        {
            throw new Exception("Type mismatch");
        }

    }


    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {
        TYPE_INFO eval_left = exp1.TypeCheck(cont);


        if (eval_left == TYPE_INFO.TYPE_NUMERIC)
        {
            _type = eval_left;
            return _type;
        }
        else
        {
            throw new Exception("Type mismatch failure");

        }
    }
    public override TYPE_INFO get_type()
    {
        return _type;
    }
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        // Compile the expression 
        exp1.Compile(cont);
        //
        // Negate the value on the top of the 
        // stack
        //
        cont.CodeOutput.Emit(OpCodes.Neg);
        return true;
    }
}