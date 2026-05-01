using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

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

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        if (!exp1.Compile(cont))
        {
            throw new Exception("Compilation in error string");
        }
        SYMBOL_INFO info = cont.TABLE.Get(variable.Name);
        LocalBuilder lb = cont.GetLocal(info.loc_position);
        cont.CodeOutput.Emit(OpCodes.Stloc, lb);
        return true;
    }
}