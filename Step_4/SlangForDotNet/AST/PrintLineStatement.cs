namespace SlangForDotNet.AST;


/// <summary>
///  Implementation of  PrintLine Statement
/// </summary>
public class PrintLineStatement : Stmt
{
    private Exp exp1;

    public PrintLineStatement(Exp e)
    {
        exp1 = e;
    }

    /// <summary>
    ///    Here we are calling Console.WriteLine to emit
    ///    an additional new line.
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