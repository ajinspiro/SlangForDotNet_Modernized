using System;

namespace SlangForDotNet.AST;
/// <summary>
/// One can store the stack frame inside this class. A Context is necessary for Variable scope. RunTime context contains the Symbol Table during interpretation. 
/// </summary>
public class RUNTIME_CONTEXT
{
    /// <summary>
    ///    Symbol Table for this context
    /// </summary>
    private SymbolTable m_dt;

    /// <summary>
    ///    Create an instance of Symbol Table
    /// </summary>
    public RUNTIME_CONTEXT()
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