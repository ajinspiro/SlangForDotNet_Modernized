using SlangForDotNet.ExeGenerator;
using SlangForDotNet.RDParser;

if (args == null ||
     args.Length != 1)
{
    Console.WriteLine("SLANGCOMPILE <scriptname>\n");
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
    sr.Close();
    sr.Dispose();

    //---------------- Creates the Parser Object
    // With Program text as argument 
    RDParser pars = null;
    pars = new RDParser(programs2);
    TModule p = null;
    p = pars.DoParse();

    //
    //  Now that Parse is Successul...
    //  Create an Executable...!
    //
    if (p.CreateExecutable("First.exe"))
    {
        Console.WriteLine("Creation of Executable is successul");
        return;
    }



}