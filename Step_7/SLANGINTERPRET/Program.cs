
using System.Collections;
using SlangForDotNet.AST;
using SlangForDotNet.ExeGenerator;
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
    TModule p = null;
    p = pars.DoParse();

    if (p == null)
    {
        Console.WriteLine("Parse Process Failed");
        return;
    }
    //
    //  Now that Parse is Successul...
    //  Do a recursive interpretation...!
    //
    RUNTIME_CONTEXT f = new RUNTIME_CONTEXT(p);
    SYMBOL_INFO fp = p.Execute(f,null); 
    
}
