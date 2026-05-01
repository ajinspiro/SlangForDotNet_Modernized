using SlangForDotNet.AST;

namespace SlangForDotNet.ExeGenerator;

/// <summary>
///    Abstract base class for Procedure
///    All the statements in a Program ( Compilation unit )
///    will be compiled into a PROC 
/// </summary>
public abstract class PROC
{
    public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT cont);
    public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);
}