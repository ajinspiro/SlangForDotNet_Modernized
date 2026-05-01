namespace SlangForDotNet.AST;

/// <summary>
///    Symbol Table entry for variable
///    using Attributes , one can optimize the
///    storage by simulating C/C++ union.
/// </summary>
public class SYMBOL_INFO
{
    public string? SymbolName;   // Symbol Name
    public TYPE_INFO Type;      // Data type
    public string? str_val;      // memory to hold string 
    public double dbl_val;      // memory to hold double
    public bool bol_val;      // memory to hold boolean

    //
    // Added in STEP 5 to store offset 
    // in the TypeBuilder.BuildLocal table
    // Only used by the compiler..interpreter
    // just ignores it..!
    public int loc_position = 0;
}