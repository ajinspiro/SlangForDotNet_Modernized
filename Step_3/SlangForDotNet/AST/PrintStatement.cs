namespace SlangForDotNet.AST;

/// <summary>
/// Implementation of Print Statement
/// </summary>

public class PrintStatement : Stmt
{
    /// <summary>
    ///   At this point of time , Print will 
    ///   spit the value of an Expression on the screen.
    /// </summary>
    private Exp _ex;
    /// <summary>
    ///    Ctor just stores the expression passed as parameter
    /// </summary>
    /// <param name="ex"></param>
    public PrintStatement(Exp ex)
    {
        _ex = ex;
    }

    /// <summary>
    ///    Execute method Evaluates the expression and
    ///    spits the value to the console using 
    ///    Console.Write statement.
    /// </summary>
    /// <param name="con"></param>
    /// <returns></returns>
    public override bool Execute(RUNTIME_CONTEXT con)
    {
        double a = _ex.Evaluate(con);
        Console.Write(a.ToString());
        return true;
    }
}
