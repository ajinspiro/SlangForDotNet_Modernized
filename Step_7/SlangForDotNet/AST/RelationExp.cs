using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;
using SlangForDotNet.Lexer;

namespace SlangForDotNet.AST;

public class RelationExp : Exp
{
    /// <summary>
    ///   Which Operator
    /// </summary>
    RELATION_OPERATOR m_op;
    /// <summary>
    ///     Left and Right Expression
    /// </summary>
    private Exp ex1, ex2;
    /// <summary>
    ///   Type of this node
    /// </summary>
    TYPE_INFO _type;
    ///
    /// Operand Types .. if operands are string
    /// we need to generate call to String.Compare 
    /// method...
    /// 
    TYPE_INFO _optype;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="op"></param>
    /// <param name="e1"></param>
    /// <param name="e2"></param>
    public RelationExp(RELATION_OPERATOR op, Exp e1, Exp e2)
    {
        m_op = op;
        ex1 = e1;
        ex2 = e2;

    }
    /// <summary>
    ///    The logic of this method is obvious...
    ///    Evaluate the Left and Right Expression...
    ///    Query the Type of the expressions and perform
    ///    appropriate action
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO eval_left = ex1.Evaluate(cont);
        SYMBOL_INFO eval_right = ex2.Evaluate(cont);

        SYMBOL_INFO ret_val = new SYMBOL_INFO();
        if (eval_left.Type == TYPE_INFO.TYPE_NUMERIC &&
            eval_right.Type == TYPE_INFO.TYPE_NUMERIC)
        {

            ret_val.Type = TYPE_INFO.TYPE_BOOL;
            ret_val.SymbolName = "";

            if (m_op == RELATION_OPERATOR.TOK_EQ)
                ret_val.bol_val = eval_left.dbl_val == eval_right.dbl_val;
            else if (m_op == RELATION_OPERATOR.TOK_NEQ)
                ret_val.bol_val = eval_left.dbl_val != eval_right.dbl_val;
            else if (m_op == RELATION_OPERATOR.TOK_GT)
                ret_val.bol_val = eval_left.dbl_val > eval_right.dbl_val;
            else if (m_op == RELATION_OPERATOR.TOK_GTE)
                ret_val.bol_val = eval_left.dbl_val >= eval_right.dbl_val;
            else if (m_op == RELATION_OPERATOR.TOK_LTE)
                ret_val.bol_val = eval_left.dbl_val <= eval_right.dbl_val;
            else if (m_op == RELATION_OPERATOR.TOK_LT)
                ret_val.bol_val = eval_left.dbl_val < eval_right.dbl_val;


            return ret_val;

        }
        else if (eval_left.Type == TYPE_INFO.TYPE_STRING &&
            eval_right.Type == TYPE_INFO.TYPE_STRING)
        {

            ret_val.Type = TYPE_INFO.TYPE_BOOL;
            ret_val.SymbolName = "";

            if (m_op == RELATION_OPERATOR.TOK_EQ)
            {
                ret_val.bol_val = (String.Compare(
                       eval_left.str_val,
                       eval_right.str_val) == 0) ? true : false;

            }
            else if (m_op == RELATION_OPERATOR.TOK_NEQ)
            {
                ret_val.bol_val = String.Compare(
                      eval_left.str_val,
                      eval_right.str_val) != 0;

            }
            else
            {
                ret_val.bol_val = false;

            }


            return ret_val;

        }
        if (eval_left.Type == TYPE_INFO.TYPE_BOOL &&
            eval_right.Type == TYPE_INFO.TYPE_BOOL)
        {

            ret_val.Type = TYPE_INFO.TYPE_BOOL;
            ret_val.SymbolName = "";

            if (m_op == RELATION_OPERATOR.TOK_EQ)
                ret_val.bol_val = eval_left.bol_val == eval_right.bol_val;
            else if (m_op == RELATION_OPERATOR.TOK_NEQ)
                ret_val.bol_val = eval_left.bol_val != eval_right.bol_val;
            else
            {
                ret_val.bol_val = false;

            }
            return ret_val;

        }
        return null;
    }
    /// <summary>
    ///     Recursively check the type and bubble up the type
    ///     information to the top...
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {
        TYPE_INFO eval_left = ex1.TypeCheck(cont);
        TYPE_INFO eval_right = ex2.TypeCheck(cont);

        if (eval_left != eval_right)
        {
            throw new Exception("Wrong Type in expression");
        }

        if (eval_left == TYPE_INFO.TYPE_STRING &&
             (!(m_op == RELATION_OPERATOR.TOK_EQ ||
               m_op == RELATION_OPERATOR.TOK_NEQ)))
        {
            throw new Exception("Only == amd != supported for string type ");
        }

        if (eval_left == TYPE_INFO.TYPE_BOOL &&
            (!(m_op == RELATION_OPERATOR.TOK_EQ ||
              m_op == RELATION_OPERATOR.TOK_NEQ)))
        {
            throw new Exception("Only == amd != supported for boolean type ");
        }
        // store the operand type as well
        _optype = eval_left;
        _type = TYPE_INFO.TYPE_BOOL;
        return _type;



    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    private bool CompileStringRelOp(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        //
        // Compile the Left Expression
        ex1.Compile(cont);
        //
        // Compile the Right Expression
        ex2.Compile(cont);

        // This is a string type..we need to call
        // Compare method..

        Type[] str2 = {
                              typeof(string),
                              typeof(string)
                   };

        cont.CodeOutput.Emit(OpCodes.Call,
        typeof(String).GetMethod("Compare", str2));

        if (m_op == RELATION_OPERATOR.TOK_EQ)
        {
            cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
            cont.CodeOutput.Emit(OpCodes.Ceq);
        }
        else
        {
            //
            // This logic is bit convoluted...
            // String.Compare will give 0 , 1 or -1
            // First we will check whether the stack value
            // is zero..
            // This will put 1 on stack ..if value was zero
            // after string.Compare
            // Once again check against zero ...it is equivalent
            // to negation

            cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
            cont.CodeOutput.Emit(OpCodes.Ceq);
            cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
            cont.CodeOutput.Emit(OpCodes.Ceq);
        }
        return true;
    }


    /// <summary>
    ///      Compile the Relational Expression...
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        if (_optype == TYPE_INFO.TYPE_STRING)
        {
            return CompileStringRelOp(cont);
        }

        //
        // Compile the Left Expression
        ex1.Compile(cont);
        //
        // Compile the Right Expression
        ex2.Compile(cont);


        if (m_op == RELATION_OPERATOR.TOK_EQ)
            cont.CodeOutput.Emit(OpCodes.Ceq);
        else if (m_op == RELATION_OPERATOR.TOK_GT)
            cont.CodeOutput.Emit(OpCodes.Cgt);
        else if (m_op == RELATION_OPERATOR.TOK_LT)
            cont.CodeOutput.Emit(OpCodes.Clt);
        else if (m_op == RELATION_OPERATOR.TOK_NEQ)
        {
            // There is no IL instruction for !=
            // We check for the equivality of the 
            // top two values on the stack ...
            // This will put 0 ( FALSE ) or 1 (TRUE)
            // on the top of stack...
            // Load zero and check once again
            // Check == once again...

            cont.CodeOutput.Emit(OpCodes.Ceq);
            cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
            cont.CodeOutput.Emit(OpCodes.Ceq);

        }
        else if (m_op == RELATION_OPERATOR.TOK_GTE)
        {

            // There is no IL instruction for >=
            // We check for the <  of the 
            // top two values on the stack ...
            // This will put 0 ( FALSE ) or 1 (TRUE)
            // on the top of stack...
            // Load Zero and 
            // Check == once again...

            cont.CodeOutput.Emit(OpCodes.Clt);
            cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
            cont.CodeOutput.Emit(OpCodes.Ceq);

        }
        else if (m_op == RELATION_OPERATOR.TOK_LTE)
        {
            // There is no IL instruction for <=
            // We check for the >  of the 
            // top two values on the stack ...
            // This will put 0 ( FALSE ) or 1 (TRUE)
            // on the top of stack...
            // Load Zero and 
            // Check == once again...

            cont.CodeOutput.Emit(OpCodes.Cgt);
            cont.CodeOutput.Emit(OpCodes.Ldc_I4, 0);
            cont.CodeOutput.Emit(OpCodes.Ceq);

        }
        return true;

    }

    public override TYPE_INFO get_type()
    {
        return _type;
    }
}