using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

/// <summary>
/// Implementation of Print Statement
/// </summary>

public class PrintStatement : Stmt
{
    private Exp exp1;

    public PrintStatement(Exp e)
    {
        exp1 = e;
    }

    /// <summary>
    ///    Execute method Evaluates the expression and
    ///    spits the value to the console using 
    ///    Console.Write statement.
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO val = exp1.Evaluate(cont);
        Console.WriteLine((val.Type == TYPE_INFO.TYPE_NUMERIC) ? val.dbl_val.ToString() :
            (val.Type == TYPE_INFO.TYPE_STRING) ? val.str_val : val.bol_val ? "TRUE" : "FALSE");
        return null;

    }

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        //
        //  Compile the Expression
        //  The Output will be on the top of stack
        exp1.Compile(cont);
        //
        // Generate Code to Call Console.Write
        //
        System.Type typ = typeof(System.Console);
        Type[] Parameters = new Type[1];

        TYPE_INFO tdata = exp1.get_type();

        if (tdata == TYPE_INFO.TYPE_STRING)
            Parameters[0] = typeof(string);
        else if (tdata == TYPE_INFO.TYPE_NUMERIC)
            Parameters[0] = typeof(double);
        else
            Parameters[0] = typeof(bool);
        cont.CodeOutput.Emit(OpCodes.Call, typ.GetMethod("Write", Parameters));
        return true;
    }
}
