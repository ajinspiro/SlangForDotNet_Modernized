namespace SlangForDotNet.AST;

/// <summary>
/// Expression is what you evaluates for it's Value
/// </summary>
public abstract class Exp
{
    public abstract double Evaluate(RUNTIME_CONTEXT cont);
}
