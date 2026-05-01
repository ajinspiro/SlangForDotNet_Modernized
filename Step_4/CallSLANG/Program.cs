
using System.Collections;
using SlangForDotNet.AST;
using SlangForDotNet.RDParser;



if (args == null ||
     args.Length != 1)
{
    Console.WriteLine("CallSlang <scriptname>\n");
    return;
}
TestFileScript(args[0]);



/// <summary>
///    Driver routine to call the program script
/// </summary>
static void TestFileScript(string filename)
{

    if (filename == null)
        return;


    // -------------- Read the contents from the file

    StreamReader sr = new StreamReader(filename);
    string programs2 = sr.ReadToEnd();


    //---------------- Creates the Parser Object
    // With Program text as argument 
    RDParser pars = null;
    pars = new RDParser(programs2);

    // Create a Compilation Context 
    //
    //
    COMPILATION_CONTEXT ctx = new COMPILATION_CONTEXT();

    //
    // Call the top level Parsing Routine with 
    // Compilation Context as the Argument
    //
    ArrayList stmts = pars.Parse(ctx);

    //
    // if we have reached here , the parse process 
    // is successful... Create a Run time context and 
    // Call Execute statements of each statement...
    //

    RUNTIME_CONTEXT f = new RUNTIME_CONTEXT();
    foreach (Object obj in stmts)
    {
        Stmt? s = obj as Stmt;
        s?.Execute(f);
    }

}
