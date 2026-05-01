using System.Collections;
using SlangForDotNet.AST;

namespace SlangForDotNet.ExeGenerator;

/// <summary>
///    A bunch of statement is called a Compilation
///    unit at this point of time... STEP 5
///    In future , a Collection of Procedures will be
///    called a Compilation unit
///    
///    Added in the STEP 5
/// </summary>
public abstract class CompilationUnit
{
    //public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT cont);
    //Extended with Formal Parameter list given to Main
    //Addition in STEP 7
    public abstract SYMBOL_INFO Execute(RUNTIME_CONTEXT cont, ArrayList actuals);

    public abstract bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont);
}