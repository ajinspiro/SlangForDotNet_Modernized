using System.Collections;
using System.Reflection.Emit;
using SlangForDotNet.AST;

namespace SlangForDotNet.ExeGenerator;

/// <summary>
///     A CodeModule is a Compilation Unit ..
///     At this point of time ..it is just a bunch
///     of statements... 
/// </summary>
public class TModule : CompilationUnit
{
    /// <summary>
    ///    A Program is a collection of Procedures...
    ///    Now , we support only global function...
    /// </summary>
    private ArrayList m_procs = null;
    /// <summary>
    ///    List of Compiled Procedures....
    ///    At this point of time..only one procedure
    ///    will be there....
    /// </summary>
    private ArrayList compiled_procs = null;
    /// <summary>
    ///    class to generate IL executable... 
    /// </summary>

    private ExeGenerator _exe = null;

    /// <summary>
    ///    Ctor for the Program ...
    /// </summary>
    /// <param name="procedures"></param>

    public TModule(ArrayList procs)
    {
        m_procs = procs;

    }

    /// <summary>
    ///      
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool CreateExecutable(string name)
    {
        //
        // Create an instance of Exe Generator
        // ExeGenerator takes a TModule and 
        // exe name as the Parameter...
        _exe = new ExeGenerator(this, name);
        // Compile The module...
        Compile(null);
        // Save the Executable...
        _exe.Save();
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        compiled_procs = new ArrayList();
        foreach (Procedure p in m_procs)
        {
            DNET_EXECUTABLE_GENERATION_CONTEXT con = new DNET_EXECUTABLE_GENERATION_CONTEXT(this, p, _exe.type_bulder);
            compiled_procs.Add(con);
            p.Compile(con);

        }
        return true;

    }

    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont, ArrayList actuals)
    {
        Procedure p = Find("Main");

        if (p != null)
        {

            return p.Execute(cont, actuals);
        }

        return null;

    }

    public MethodBuilder _get_entry_point(string _funcname)
    {
        foreach (DNET_EXECUTABLE_GENERATION_CONTEXT u in compiled_procs)
        {
            if (u.MethodName.Equals(_funcname))
            {
                return u.MethodHandle;
            }

        }
        return null;
    }

    public Procedure Find(string str)
    {
        foreach (Procedure p in m_procs)
        {
            string pname = p.Name;

            if (pname.ToUpper().CompareTo(str.ToUpper()) == 0)
                return p;
        }
        return null;
    }
}