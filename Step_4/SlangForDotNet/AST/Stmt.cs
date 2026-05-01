namespace SlangForDotNet.AST;

/// <summary>
/// Statement is what you Execute for it's Effect
/// </summary>
public abstract class Stmt
{
    public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT con);
}