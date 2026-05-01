using System.Collections;
using System.Reflection.Emit;
using SlangForDotNet.AST;

namespace SlangForDotNet.ExeGenerator;

/// <summary>
///     A Procedure which returns an Exit Code...
///     It defaults to 0 in this step...!
/// </summary>
public class Procedure : PROC
{
    /// <summary>
    ///    Procedure name ..which defaults to Main 
    ///    in the type MainClass
    /// </summary>
    public string m_name;
    /// <summary>
    ///    Formal parameters...
    /// </summary>
    public ArrayList m_formals = null;
    /// <summary>
    ///     List of statements which comprises the Procedure
    /// </summary>
    public ArrayList m_statements = null;
    /// <summary>
    ///     Local variables
    /// </summary>
    public SymbolTable m_locals = null;
    /// <summary>
    ///        return_value.... a hard coded zero at this
    ///        point of time..
    /// </summary>
    public SYMBOL_INFO return_value = null;
    /// <summary>
    ///       TYPE_INFO => TYPE_NUMERIC
    /// </summary>
    public TYPE_INFO _type = TYPE_INFO.TYPE_ILLEGAL;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="formals"></param>
    /// <param name="stats"></param>
    /// <param name="locals"></param>
    /// <param name="type"></param>

    public Procedure(string name, 
                     ArrayList stats, 
                     SymbolTable locals, 
                     TYPE_INFO type)
    {
        m_name = name;
        m_formals = null;
        m_statements = stats;
        m_locals = locals;
        _type = type;
    }
    /// <summary>
    /// 
    /// </summary>
    public TYPE_INFO TYPE
    {

        get
        {
            return _type;

        }

    }
    /// <summary>
    ///     Null at this point of time...
    /// </summary>
    public ArrayList FORMALS
    {
        get
        {
            return m_formals;
        }

    }

    public string Name
    {
        set
        {

            Name = value;
        }

        get
        {
            return m_name;
        }

    }

    public SYMBOL_INFO ReturnValue()
    {
        return return_value;
    }

    public TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {

        return TYPE_INFO.TYPE_NUMERIC;
    }


    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
                

        foreach (Stmt e1 in m_statements)
        {
            e1.Compile(cont);
        }
       
        cont.CodeOutput.Emit(OpCodes.Ret);
        return true;

    }


    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {
                 
        foreach (Stmt stmt in m_statements)
                  stmt.Execute(cont);
        
        return null;

    }
}