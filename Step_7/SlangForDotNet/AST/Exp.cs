using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

/// <summary>
///     In  this Step , we add two more methods to the Exp class
///     TypeCheck => To do Type analysis
///     get_type  => Type of this node
/// </summary>
public abstract class Exp
{
    public abstract SYMBOL_INFO Evaluate(RUNTIME_CONTEXT cont);
    public abstract TYPE_INFO TypeCheck(COMPILATION_CONTEXT cont);
    public abstract TYPE_INFO get_type();
    /// <summary>
    ///   Added in the STEP 5 for .NET IL code generation
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);
}