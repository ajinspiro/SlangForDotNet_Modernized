using System;

namespace SlangForDotNet.AST;

class BinaryMinus : Exp
{
    private Exp exp1, exp2;
    TYPE_INFO _type;

    public BinaryMinus(Exp e1, Exp e2)
    {
        exp1 = e1; exp2 = e2;
    }

    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO eval_left = exp1.Evaluate(cont);
        SYMBOL_INFO eval_right = exp2.Evaluate(cont);

        if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
            eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
        {
            SYMBOL_INFO ret_val = new SYMBOL_INFO();
            ret_val.dbl_val = eval_left.dbl_val - eval_right.dbl_val;
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
        TYPE_INFO eval_right = exp2.TypeCheck(cont);

        if (eval_left == eval_right && eval_left == TYPE_INFO.TYPE_NUMERIC)
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
}