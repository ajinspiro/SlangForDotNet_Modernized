using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using SlangForDotNet.AST;

namespace SlangForDotNet.ExeGenerator;
/// <summary>
///      DNET_EXECUTABLE_GENERATION_CONTEXT is for generating 
///      CLR executable
/// </summary>
public class DNET_EXECUTABLE_GENERATION_CONTEXT
{
    /// <summary>
    ///     ILGenerator Object
    /// </summary>
    private ILGenerator ILout;
    /// <summary>
    ///    Auto (Local) Variable support
    ///    Stores the index return by DefineLocal
    ///    method of MethodBuilder
    /// </summary>
    private ArrayList variables = new ArrayList();
    /// <summary>
    ///    Symbol Table for storing Types and 
    ///    doing the type analysis
    /// </summary>
    SymbolTable m_tab = new SymbolTable();
    /// <summary>
    ///    CLR Reflection.Emit.MethodBuilder
    /// </summary>
    MethodBuilder _methinfo = null;
    /// <summary>
    ///     CLR Type Builder ( useful for creating
    ///     classes in the run time
    /// </summary>
    TypeBuilder _bld = null;

    /// <summary>
    ///    Procedure to compiled
    /// </summary>
    Procedure _proc = null;
    /// <summary>
    ///    Program to be compiled...
    ///    
    /// </summary>
    TModule _program;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prog"></param>
    /// <param name="p"></param>
    /// <param name="bld"></param>
    public DNET_EXECUTABLE_GENERATION_CONTEXT(TModule program,
                                              Procedure proc,
                                              TypeBuilder bld)
    {
        _proc = proc;
        _bld = bld;
        _program = program;



        System.Type[] s = new System.Type[_proc.FORMALS.Count];

        int i = 0;
        foreach (SYMBOL_INFO ts in _proc.FORMALS)
        {
            if (ts.Type == TYPE_INFO.TYPE_BOOL)
            {
                s[i] = typeof(bool);

            }
            else if (ts.Type == TYPE_INFO.TYPE_NUMERIC)
            {
                s[i] = typeof(double);
            }
            else
            {
                s[i] = typeof(string);

            }
            i = i + 1;

        }

        if (_proc.FORMALS.Count == 0)
        {

            s = null;
        }

        System.Type ret_type = null;

        if (_proc.TYPE == TYPE_INFO.TYPE_BOOL)
            ret_type = typeof(bool);
        else if (_proc.TYPE == TYPE_INFO.TYPE_STRING)
            ret_type = typeof(string);
        else
            ret_type = typeof(double);

        if (_proc.m_name.Equals("MAIN"))
        {
            //--------- Deleberately set to null
            // This is to ignore return value of main
            ret_type = null;
            _methinfo = _bld.DefineMethod(_proc.Name, MethodAttributes.Public | MethodAttributes.Static, ret_type, s);
        }
        else
        {
            _methinfo = _bld.DefineMethod(_proc.Name, MethodAttributes.Public | MethodAttributes.Static, ret_type, s);
        }
        ILout = _methinfo.GetILGenerator();

    }

    public string MethodName
    {
        get
        {
            return _proc.Name;
        }
    }

    public MethodBuilder MethodHandle
    {
        get
        {
            return _methinfo;
        }
    }

    public TypeBuilder TYPEBUILDER
    {
        get
        {
            return _bld;
        }
    }

    public SymbolTable TABLE
    {
        get
        {
            return m_tab;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public TModule GetProgram()
    {
        return _program;

    }
    /// <summary>
    /// 
    /// </summary>
    public ILGenerator CodeOutput
    {
        get
        {
            return ILout;
        }

    }

    /// <summary>
    ///      
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int DeclareLocal(System.Type type)
    {
        // It is possible to create Local ( auto )
        // Variables by Calling DeclareLocl method 
        // of ILGenerator... this returns an integer
        // We store this integer value in the variables
        // collection...
        LocalBuilder lb = ILout.DeclareLocal(type);
        // Now add the integer value associated with the 
        // variable to variables collection...
        return variables.Add(lb);
    }

    public LocalBuilder GetLocal(int s)
    {
        return variables[s] as LocalBuilder;
    }
}