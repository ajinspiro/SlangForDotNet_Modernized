namespace SlangForDotNet.AST;

/// <summary>
///    Compile the Variable Declaration statements
/// </summary>
public class VariableDeclStatement : Stmt
{
    SYMBOL_INFO m_inf = null;
    Variable var = null;
    public VariableDeclStatement(SYMBOL_INFO inf)
    {
        m_inf = inf;

    }
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {
        cont.TABLE.Add(m_inf);
        var = new Variable(m_inf);
        return null;
    }
}