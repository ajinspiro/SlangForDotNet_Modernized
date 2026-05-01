
using System.Collections;
using SlangForDotNet.AST;
using SlangForDotNet.RDParser;

TestFirstScript();
TestSecondScript();
TestThirdScript();

static void TestFirstScript()
{
    string a = "PRINTLINE 2*10;" + "\r\n" + "PRINTLINE 10;\r\n PRINT 2*10;\r\n";
    RDParser p = new RDParser(a);
    ArrayList arr = p.Parse();
    foreach (object obj in arr)
    {
        Stmt? s = obj as Stmt;
        s?.Execute(null);
    }
}

static void TestSecondScript()
{
    string a = "PRINTLINE (2);";
    RDParser p = new RDParser(a);
    ArrayList arr = p.Parse();
    foreach (object obj in arr)
    {
        Stmt? s = obj as Stmt;
        s?.Execute(null);
    }
}

static void TestThirdScript()
{
    string a = "PRINTLINE 2*(5+(3-4+5));";
    RDParser p = new RDParser(a);
    ArrayList arr = p.Parse();
    foreach (object obj in arr)
    {
        Stmt? s = obj as Stmt;
        s?.Execute(null);
    }
}