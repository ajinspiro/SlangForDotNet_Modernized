namespace SlangForDotNet.AST;


/// <summary>
///  Implementation of  PrintLine Statement
/// </summary>
public class PrintLineStatement : Stmt
{
    private Exp _ex;

    public PrintLineStatement(Exp ex)
    {
        _ex = ex;
    }
    /// <summary>
    ///    Here we are calling Console.WriteLine to emit
    ///    an additional new line.
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public override bool Execute(RUNTIME_CONTEXT con)
    {
        double a = _ex.Evaluate(con);
        Console.WriteLine(a.ToString());
        return true;
    }
}