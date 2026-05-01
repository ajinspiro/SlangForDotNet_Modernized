using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;


/// <summary>
///    Node to store Variables
///    The data types supported are
///      NUMERIC
///      STRING
///      BOOLEAN
///    The node store only the variable name , the 
///    associated data will be found in the 
///    Symbol Table attached to the 
///      COMPILATION_CONTEXT 
///    
/// </summary>

public class Variable : Exp
{
    private string m_name;  // Var name
    TYPE_INFO _type;        // Type 
    /// <summary>
    ///     this Ctor just stores the variable name
    /// </summary>
    /// <param name="inf"></param>
    public Variable(SYMBOL_INFO inf)
    {
        m_name = inf.SymbolName;

    }
    /// <summary>
    ///     Creates a new symbol and puts into the symbol table
    ///     and stores the key ( variable name ) 
    /// </summary>
    /// <param name="st"></param>
    /// <param name="name"></param>
    /// <param name="_value"></param>
    public Variable(COMPILATION_CONTEXT st, string name, double _value)
    {
        SYMBOL_INFO s = new SYMBOL_INFO();
        s.SymbolName = name;
        s.Type = TYPE_INFO.TYPE_NUMERIC;
        s.dbl_val = _value;
        st.TABLE.Add(s);
        m_name = name;
    }
    /// <summary>
    ///     Creates a new symbol and puts into the symbol table
    ///     and stores the key ( variable name ) 
    /// </summary>
    /// <param name="st"></param>
    /// <param name="name"></param>
    /// <param name="_value"></param>
    public Variable(COMPILATION_CONTEXT st, string name, bool _value)
    {
        SYMBOL_INFO s = new SYMBOL_INFO();
        s.SymbolName = name;
        s.Type = TYPE_INFO.TYPE_BOOL;
        s.bol_val = _value;
        st.TABLE.Add(s);
        m_name = name;
    }
    /// <summary>
    ///     Creates a new symbol and puts into the symbol table
    ///     and stores the key ( variable name ) 
    /// </summary>
    /// <param name="st"></param>
    /// <param name="name"></param>
    /// <param name="_value"></param>
    public Variable(COMPILATION_CONTEXT st, string name, string _value)
    {
        SYMBOL_INFO s = new SYMBOL_INFO();
        s.SymbolName = name;
        s.Type = TYPE_INFO.TYPE_STRING;
        s.str_val = _value;
        st.TABLE.Add(s);
        m_name = name;
    }

    /// <summary>
    ///    Retrieves the name of the Variable ( method version )
    /// </summary>
    /// <returns></returns>

    public string GetName()
    {
        return m_name;
    }

    /// <summary>
    ///   Retrieves the name of the Variable ( property version )
    /// </summary>
    /// <returns></returns>
    public string Name
    {
        get
        {
            return m_name;
        }

        set
        {
            m_name = value;
        }
    }

    /// <summary>
    ///    To Evaluate a variable , we just need to do a lookup
    ///    in the Symbol table ( of RUNTIME_CONTEXT ) 
    /// </summary>
    /// <param name="st"></param>
    /// <param name="glb"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont)
    {

        if (cont.TABLE == null)
        {
            return null;
        }
        else
        {
            SYMBOL_INFO a = cont.TABLE.Get(m_name);
            return a;
        }

    }

    /// <summary>
    ///     Look it up in the Symbol Table and 
    ///     return the type
    /// </summary>
    /// <param name="local"></param>
    /// <param name="global"></param>
    /// <returns></returns>
    public override TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont)
    {

        if (cont.TABLE == null)
        {
            return TYPE_INFO.TYPE_ILLEGAL;
        }
        else
        {
            SYMBOL_INFO a = cont.TABLE.Get(m_name);
            if (a != null)
            {
                _type = a.Type;
                return _type;

            }


            return TYPE_INFO.TYPE_ILLEGAL;

        }

    }

    /// <summary>
    ///     this should only be called after the TypeCheck method
    ///     has been invoked on AST 
    /// </summary>
    /// <returns></returns>
    public override TYPE_INFO get_type()
    {
        return _type;
    }

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        //
        // Retrieve the Symbol information from the 
        // Symbol Table. Symbol name is the key here..
        //
        SYMBOL_INFO info = cont.TABLE.Get(m_name);
        //
        // Give the Position to retrieve the Local Variable
        // Builder.
        //
        LocalBuilder lb = cont.GetLocal(info.loc_position);
        //
        // LDLOC => Load Local... we need to give
        // a Local Builder as parameter
        //
        cont.CodeOutput.Emit(OpCodes.Ldloc, lb);
        return true;
    }
}