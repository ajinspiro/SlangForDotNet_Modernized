using System.Collections;
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
    private ArrayList protos = null;

    /// <summary>
    ///     Ctor does not do much
    /// </summary>
    public TModuleBuilder()
    {
        procs = new ArrayList();
        protos = null;
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

    ///
    ///
    ///
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