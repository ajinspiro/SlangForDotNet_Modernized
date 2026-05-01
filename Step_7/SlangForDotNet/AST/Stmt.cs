using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

/// <summary>
/// Statement is what you Execute for it's Effect
/// </summary>
public abstract class Stmt
{
    public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT con);
    //
    // Added in the Step 5 for .net IL compilation
    //
    public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);
}