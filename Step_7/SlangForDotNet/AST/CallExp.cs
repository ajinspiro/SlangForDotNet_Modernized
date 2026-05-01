using System.Collections;
using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

class CallExp : Exp
{
    /// <summary>
    ///    Procedure Object
    /// </summary>
    Procedure m_proc;
    /// <summary>
    ///   ArrayList of Actuals
    /// </summary>
    ArrayList m_actuals;
    /// <summary>
    ///    procedure name ...
    /// </summary>
    string _procname;
    /// <summary>
    ///    Is it  a Recursive Call ?
    /// </summary>
    bool _isrecurse;

    /// <summary>
    ///    Return type of the Function
    /// </summary>
    TYPE_INFO _type;
    /// <summary>
    ///    Ctor to be called when we make a ordinary
    ///    subroutine call
    /// </summary>
    /// <param name="proc"></param>
    /// <param name="actuals"></param>
    public CallExp(Procedure proc, ArrayList actuals)
    {
        m_proc = proc;
        m_actuals = actuals;
    }
    /// <summary>
    ///    Ctor to implement Recursive sub routine
    /// </summary>
    /// <param name="name"></param>
    /// <param name="recurse"></param>
    /// <param name="actuals"></param>
    public CallExp(string name, bool recurse, ArrayList actuals)
    {
        _procname = name;
        if (recurse)
            _isrecurse = true;

        m_actuals = actuals;
        //
        // For a recursive call Procedure Address will be null
        // During the interpretation time we will resolve the 
        // call by look up...
        //    m_proc = cont.GetProgram().Find(_procname);
        // This is a hack for implementing one pass compiler
        m_proc = null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {
        if (m_proc != null)
        {
            //
            // This is a Ordinary Function Call
            //
            //
            RUNTIME_CONTEXT ctx = new RUNTIME_CONTEXT(cont.GetProgram());

            ArrayList lst = new ArrayList();

            foreach (Exp ex in m_actuals)
            {
                lst.Add(ex.Evaluate(cont));
            }

            return m_proc.Execute(ctx, lst);

        }
        else
        {
            // Recursive function call...by the time we 
            // reach here..whole program has already been 
            // parsed. Lookup the Function name table and 
            // resolve the Address
            //
            //
            m_proc = cont.GetProgram().Find(_procname);
            RUNTIME_CONTEXT ctx = new RUNTIME_CONTEXT(cont.GetProgram());
            ArrayList lst = new ArrayList();

            foreach (Exp ex in m_actuals)
            {
                lst.Add(ex.Evaluate(cont));
            }

            return m_proc.Execute(ctx, lst);


        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {
        if (m_proc != null)
        {
            _type = m_proc.TypeCheck(cont);

        }

        return _type;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override TYPE_INFO get_type()
    {
        return _type;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {

        if (m_proc == null)
        {
            // if it is  a recursive call..
            // resolve the address...
            m_proc = cont.GetProgram().Find(_procname);
        }

        string name = m_proc.Name;


        TModule str = cont.GetProgram();
        MethodBuilder bld = str._get_entry_point(name);

        foreach (Exp ex in m_actuals)
        {
            ex.Compile(cont);
        }
        cont.CodeOutput.Emit(OpCodes.Call, bld);
        return true;
    }
}