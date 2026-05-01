using System.Collections;

namespace SlangForDotNet.AST;

class FUNCTION_INFO
{
    public TYPE_INFO _ret_value;
    public string _name;
    public ArrayList _typeinfo;

    public FUNCTION_INFO(string name, TYPE_INFO ret_value,
        ArrayList formals)
    {

        _ret_value = ret_value;
        _typeinfo = formals;
        _name = name;
    }
}