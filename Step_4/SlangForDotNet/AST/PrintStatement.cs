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
}
