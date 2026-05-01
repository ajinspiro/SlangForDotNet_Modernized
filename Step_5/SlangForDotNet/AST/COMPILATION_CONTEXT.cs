using System;

namespace SlangForDotNet.AST;
/// <summary>
/// A Context is necessary for Variable scope
/// </summary>
public class COMPILATION_CONTEXT
{
    /// <summary>
    ///    Symbol Table for this context
    /// </summary>
    private SymbolTable m_dt;

    /// <summary>
    ///    Create an instance of Symbol Table
    /// </summary>
    public COMPILATION_CONTEXT()
    {
        m_dt = new SymbolTable();
    }

    /// <summary>
    ///    Property to retrieve Table
    /// </summary>
    public SymbolTable TABLE
    {

        get
        {
            return m_dt;
        }

        set
        {
            m_dt = value;
        }
    }



}
