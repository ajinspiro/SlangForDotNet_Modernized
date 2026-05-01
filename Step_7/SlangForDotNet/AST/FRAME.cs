namespace SlangForDotNet.AST;

/// <summary>
///    Frame for recursive calls
/// </summary>
public class FRAME
{
    private SymbolTable tab;
    public FRAME()
    {
        tab = new SymbolTable();
    }

    public SymbolTable TABLE
    {
        get
        {
            return tab;

        }
    }
}