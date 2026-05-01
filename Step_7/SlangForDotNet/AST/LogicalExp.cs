using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;
using SlangForDotNet.Lexer;

namespace SlangForDotNet.AST;

/// <summary>
///     Logical Expression...
/// </summary>
class LogicalExp : Exp
{
    /// <summary>
    ///    && ( AND ) , || ( OR )
    /// </summary>
    TOKEN m_op;
    /// <summary>
    ///   Operands
    /// </summary>
    private Exp ex1, ex2;
    /// <summary>
    ///     Type of the node...
    /// </summary>
    TYPE_INFO _type;
    public LogicalExp(TOKEN op, Exp e1, Exp e2)
    {
        m_op = op;
        ex1 = e1;
        ex2 = e2;
    }

    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO eval_left = ex1.Evaluate(cont);
        SYMBOL_INFO eval_right = ex2.Evaluate(cont);

        if (eval_left.Type == TYPE_INFO.TYPE_BOOL &&
            eval_right.Type == TYPE_INFO.TYPE_BOOL)
        {
            SYMBOL_INFO ret_val = new SYMBOL_INFO();
            ret_val.Type = TYPE_INFO.TYPE_BOOL;
            ret_val.SymbolName = "";

            if (m_op == TOKEN.TOK_AND)
                ret_val.bol_val = (eval_left.bol_val && eval_right.bol_val);
            else if (m_op == TOKEN.TOK_OR)
                ret_val.bol_val = (eval_left.bol_val || eval_right.bol_val);
            else
            {
                return null;

            }
            return ret_val;

        }

        return null;
    }

    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {
        TYPE_INFO eval_left = ex1.TypeCheck(cont);
        TYPE_INFO eval_right = ex2.TypeCheck(cont);

        // The Types should be Boolean...
        // Logical Operators only make sense
        // with Boolean Types

        if (eval_left == eval_right &&
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
        ex2.Compile(cont);
        if (m_op == TOKEN.TOK_AND)
            cont.CodeOutput.Emit(OpCodes.And);
        else if (m_op == TOKEN.TOK_OR)
            cont.CodeOutput.Emit(OpCodes.Or);
        return true;
    }

    public override TYPE_INFO get_type()
    {
        return _type;
    }
}