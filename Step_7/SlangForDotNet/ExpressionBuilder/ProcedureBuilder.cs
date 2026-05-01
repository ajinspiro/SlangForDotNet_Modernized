using System.Collections;
using SlangForDotNet.AST;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.ExpressionBuilder;

public class ProcedureBuilder : AbstractBuilder
{
    /// <summary>
    ///    Procedure name ..now it is hard coded
    ///    to MAIN
    /// </summary>
    private string proc_name = "";
    /// <summary>
    ///    Compilation context for type analysis
    /// </summary>
    COMPILATION_CONTEXT ctx = null;
    /// <summary>
    ///    We support Procedure arguments
    ///    in step 5
    /// </summary>
    ArrayList m_formals = new ArrayList();
    /// <summary>
    ///    Array of Statements
    /// </summary>
    ArrayList m_stmts = new ArrayList();
    /// <summary>
    ///    Return Type of the procedure
    /// </summary>
    TYPE_INFO inf = TYPE_INFO.TYPE_ILLEGAL;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="_ctx"></param>

    public ProcedureBuilder(string name, COMPILATION_CONTEXT _ctx)
    {
        ctx = _ctx;
        proc_name = name;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public bool AddLocal(SYMBOL_INFO info)
    {
        ctx.TABLE.Add(info);
        return true;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>

    public bool AddFormals(SYMBOL_INFO info)
    {
        m_formals.Add(info);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public TYPE_INFO TypeCheck(Exp e)
    {
        return e.TypeCheck(ctx);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="st"></param>
    public void AddStatement(Stmt st)
    {
        m_stmts.Add(st);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strname"></param>
    /// <returns></returns>
    public SYMBOL_INFO GetSymbol(string strname)
    {

        return ctx.TABLE.Get(strname);

    }

    /// <summary>
    ///   Check the function Prototype
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool CheckProto(string name)
    {
        return true;

    }
    /// <summary>
    /// 
    /// </summary>
    public TYPE_INFO TYPE
    {
        get
        {
            return inf;
        }

        set
        {
            inf = value;
        }
    }

    public SymbolTable TABLE
    {
        get
        {
            return ctx.TABLE;
        }
    }

    public COMPILATION_CONTEXT Context
    {
        get
        {
            return ctx;
        }
    }

    public string Name
    {
        get
        {
            return proc_name;
        }

        set
        {
            proc_name = value;

        }

    }

    public Procedure GetProcedure()
    {
        Procedure ret = new Procedure(proc_name, m_formals,
                m_stmts, ctx.TABLE, inf);

        return ret;
    }
}