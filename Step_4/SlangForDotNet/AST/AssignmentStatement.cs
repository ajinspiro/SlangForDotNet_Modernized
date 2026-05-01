namespace SlangForDotNet.AST;

/// <summary>
///   Assignment Statement
/// </summary>
public class AssignmentStatement : Stmt
{
    private Variable variable;
    private Exp exp1;

    public AssignmentStatement(Variable var, Exp e)
    {
        variable = var;
        exp1 = e;

    }

    public AssignmentStatement(SYMBOL_INFO var, Exp e)
    {
        variable = new Variable(var);
        exp1 = e;

    }
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {
        SYMBOL_INFO val = exp1.Evaluate(cont);
        cont.TABLE.Assign(variable, val);
        return null;
    }
}