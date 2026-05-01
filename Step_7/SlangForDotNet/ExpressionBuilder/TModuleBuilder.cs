using System.Collections;
using SlangForDotNet.AST;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.ExpressionBuilder;

/// <summary>
///      A Builder for Creating a Module
/// </summary>
class TModuleBuilder : AbstractBuilder
{
    /// <summary>
    ///     Array of Procs 
    /// </summary>
    private ArrayList procs;
    /// <summary>
    ///    Array of Function Prototypes
    ///    not much use as of now...
    /// </summary>
    private ArrayList protos=null;

    /// <summary>
    ///     Ctor does not do much
    /// </summary>
    public TModuleBuilder()
    {
        procs = new ArrayList();
        protos = new ArrayList();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool IsFunction(string name)
    {
        foreach (FUNCTION_INFO fpinfo in protos)
        {
            if (fpinfo._name.Equals(name))
            {
                return true;
            }

        }

        return false;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ret_type"></param>
    /// <param name="type_infos"></param>
    public void AddFunctionProtoType(string name, TYPE_INFO ret_type,
        ArrayList type_infos)
    {
        FUNCTION_INFO info = new FUNCTION_INFO(name, ret_type, type_infos);
        protos.Add(info);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ret_type"></param>
    /// <param name="type_infos"></param>
    /// <returns></returns>
    public bool CheckFunctionProtoType(string name, TYPE_INFO ret_type,
        ArrayList type_infos)
    {
        foreach (FUNCTION_INFO fpinfo in protos)
        {
            if (fpinfo._name.Equals(name))
            {
                if (fpinfo._ret_value == ret_type)
                {
                    if (type_infos.Count == fpinfo._typeinfo.Count)
                    {
                        int i = 0;
                        for (i = 0; i < type_infos.Count; ++i)
                        {
                            TYPE_INFO a = (TYPE_INFO)type_infos[i];
                            TYPE_INFO b = (TYPE_INFO)type_infos[i];

                            if (a != b)
                                return false;

                        }

                        return true;

                    }


                }

            }

        }

        return false;

    }
    /// <summary>
    ///     Add Procedure
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool Add(Procedure p)
    {
        procs.Add(p);
        return true;
    }

    /// <summary>
    ///      Create Program 
    /// </summary>
    /// <returns></returns>
    public TModule GetProgram()
    {
        return new TModule(procs);
    }
 
    public Procedure GetProc(string name)
    {
        foreach (Procedure p in procs)
        {
            if (p.Name.Equals(name))
            {
                return p;
            }

        }

        return null;

    }

}