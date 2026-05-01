using System;
using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

/// <summary>
///    Node to represent Binary + 
/// </summary>

public class BinaryPlus : Exp
{
    /// <summary>
    ///  Plus has got a left expression (exp1 )
    ///  and a right expression...
    ///  and a Associated type information
    /// </summary>
    private Exp exp1, exp2;
    TYPE_INFO _type;

    public BinaryPlus(Exp e1, Exp e2)
    {
        exp1 = e1; exp2 = e2;
    }

    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO eval_left = exp1.Evaluate(cont);
        SYMBOL_INFO eval_right = exp2.Evaluate(cont);

        if (eval_left.Type == TYPE_INFO.TYPE_STRING &&
            eval_right.Type == TYPE_INFO.TYPE_STRING)
        {
            SYMBOL_INFO ret_val = new SYMBOL_INFO();
            ret_val.str_val = eval_left.str_val + eval_right.str_val;
            ret_val.Type = TYPE_INFO.TYPE_STRING;
            ret_val.SymbolName = "";
            return ret_val;
        }
        else if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
            eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
        {
            SYMBOL_INFO ret_val = new SYMBOL_INFO();
            ret_val.dbl_val = eval_left.dbl_val + eval_right.dbl_val;
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

        if (eval_left == eval_right && eval_left != TYPE_INFO.TYPE_BOOL)
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
        // Compile the Left Expression
        exp1.Compile(cont);
        //
        // Compile the Right Expression
        exp2.Compile(cont);
        //
        // Emit Add instruction
        //
        if (_type == TYPE_INFO.TYPE_NUMERIC)
        {
            cont.CodeOutput.Emit(OpCodes.Add);
        }
        else
        {
            // This is a string type..we need to call
            // Concat method..

            Type[] str2 = {
                          typeof(string),
                          typeof(string)
                      };

            cont.CodeOutput.Emit(OpCodes.Call,
                typeof(String).GetMethod("Concat", str2));

        }
        return true;
    }
}