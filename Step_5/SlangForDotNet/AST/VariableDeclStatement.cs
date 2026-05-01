using SlangForDotNet.ExeGenerator;

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

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        //
        // Retrieve the type from the SYMBOL_INFO
        //
        System.Type type = (m_inf.Type == TYPE_INFO.TYPE_BOOL) ?
            typeof(bool) : (m_inf.Type == TYPE_INFO.TYPE_NUMERIC) ?
            typeof(double) : typeof(string);
        //
        //  Get the offset of the variable
        //
        int s = cont.DeclareLocal(type);
        // Store the offset in the SYMBOL_INFO
        //
        m_inf.loc_position = s;
        //
        // Add the variable into Symbol Table..
        //
        cont.TABLE.Add(m_inf);

        return true;
    }
}