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
                     ArrayList formals,
                     ArrayList stats,
                     SymbolTable locals,
                     TYPE_INFO type)
    {
        m_name = name;
        //
        // The value is only supplied for STEP 7
        m_formals = formals;
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
    ///     STEP 7 
    /// </summary>
    public ArrayList FORMALS
    {
        get
        {
            return m_formals;
        }

    }
    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public SYMBOL_INFO ReturnValue()
    {
        return return_value;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {

        return TYPE_INFO.TYPE_NUMERIC;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {

        if (m_formals != null)
        {


            int i = 0;

            foreach (SYMBOL_INFO b in m_formals)
            {

                System.Type type = (b.Type == TYPE_INFO.TYPE_BOOL) ?
                    typeof(bool) : (b.Type == TYPE_INFO.TYPE_NUMERIC) ?
                    typeof(double) : typeof(string);
                int s = cont.DeclareLocal(type);
                b.loc_position = s;
                cont.TABLE.Add(b);
                cont.CodeOutput.Emit(OpCodes.Ldarg, i);
                cont.CodeOutput.Emit(OpCodes.Stloc, cont.GetLocal(s));
                i++;
            }

        }


        foreach (Stmt e1 in m_statements)
        {
            e1.Compile(cont);
        }

        cont.CodeOutput.Emit(OpCodes.Ret);
        return true;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <param name="actuals"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont, ArrayList actuals)
    {
        ArrayList vars = new ArrayList();
        int i = 0;

        FRAME ft = new FRAME();

        if (m_formals != null && actuals != null)
        {

            i = 0;
            foreach (SYMBOL_INFO b in m_formals)
            {

                SYMBOL_INFO inf = actuals[i] as SYMBOL_INFO;
                inf.SymbolName = b.SymbolName;
                cont.TABLE.Add(inf);
                i++;
            }

        }

        foreach (Stmt e1 in m_statements)
        {
            return_value = e1.Execute(cont);

            if (return_value != null)
                return return_value;

        }

        return null;

    }
}