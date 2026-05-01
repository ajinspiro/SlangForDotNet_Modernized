using System.Collections;
using SlangForDotNet.AST;
using SlangForDotNet.Lexer;

namespace SlangForDotNet.RDParser;

public class RDParser : Lexer.Lexer
{
    TOKEN Current_Token;
    TOKEN Last_Token;

    public RDParser(String str)
        : base(str)
    {
    }

    public Exp CallExpr()
    {
        Current_Token = GetToken();
        return Expr();
    }

    /// <summary>
    ///    Here we remember the last token ..before we 
    ///    move on to the next token..
    /// </summary>
    /// <returns></returns>
    protected TOKEN GetNext()
    {
        Last_Token = Current_Token;
        Current_Token = GetToken();
        return Current_Token;
    }

    public Exp Expr()
    {
        TOKEN l_token;
        Exp RetValue = Term();
        while (Current_Token == TOKEN.TOK_PLUS || Current_Token == TOKEN.TOK_SUB)
        {
            l_token = Current_Token;
            Current_Token = GetToken();
            Exp e1 = Expr();
            RetValue = new BinaryExp(RetValue, e1,
                l_token == TOKEN.TOK_PLUS ? OPERATOR.PLUS : OPERATOR.MINUS);
        }
        return RetValue;
    }

    public Exp Term()
    {
        TOKEN l_token;
        Exp RetValue = Factor();

        while (Current_Token == TOKEN.TOK_MUL || Current_Token == TOKEN.TOK_DIV)
        {
            l_token = Current_Token;
            Current_Token = GetToken();


            Exp e1 = Term();
            RetValue = new BinaryExp(RetValue, e1,
                l_token == TOKEN.TOK_MUL ? OPERATOR.MUL : OPERATOR.DIV);

        }

        return RetValue;
    }

    public Exp Factor()
    {
        TOKEN l_token;
        Exp RetValue;
        if (Current_Token == TOKEN.TOK_DOUBLE)
        {

            RetValue = new NumericConstant(GetNumber());
            Current_Token = GetToken();

        }
        else if (Current_Token == TOKEN.TOK_OPAREN)
        {

            Current_Token = GetToken();

            RetValue = Expr();  // Recurse

            if (Current_Token != TOKEN.TOK_CPAREN)
            {
                Console.WriteLine("Missing Closing Parenthesis\n");
                throw new Exception();

            }
            Current_Token = GetToken();
        }

        else if (Current_Token == TOKEN.TOK_PLUS || Current_Token == TOKEN.TOK_SUB)
        {
            l_token = Current_Token;
            Current_Token = GetToken();
            RetValue = Factor();

            RetValue = new UnaryExp(RetValue,
                 l_token == TOKEN.TOK_PLUS ? OPERATOR.PLUS : OPERATOR.MINUS);
        }
        else
        {

            Console.WriteLine("Illegal Token");
            throw new Exception();
        }


        return RetValue;

    }

    /// <summary>
    ///   The new Parser entry point
    /// </summary>
    /// <returns></returns>
    public ArrayList Parse()
    {
        GetNext();  // Get the Next Token
        //
        // Parse all the statements
        //
        return StatementList();
    }
    /// <summary>
    ///  The Grammar is 
    ///  
    ///  <stmtlist> :=  { <statement> }+
    ///
    ///  {<statement> :=  <printstmt> | <printlinestmt>
    ///  <printstmt> :=  print   <expr >;
    ///
    /// <printlinestmt>:= printline <expr>;
    ///    
    /// <Expr>  ::=  <Term> | <Term> { + | - } <Expr>
    /// <Term> ::=  <Factor> | <Factor>  {*|/} <Term>
    /// <Factor>::=  <number> | ( <expr> ) | {+|-} <factor>
    ///       
    /// 
    /// </summary>
    /// <returns></returns>
    private ArrayList StatementList()
    {
        ArrayList arr = new ArrayList();
        while (Current_Token != TOKEN.TOK_NULL)
        {
            Stmt temp = Statement();
            if (temp != null)
            {
                arr.Add(temp);
            }
        }
        return arr;
    }

    /// <summary>
    ///    This Routine Queries Statement Type 
    ///    to take the appropriate Branch...
    ///    Currently , only Print and PrintLine statement
    ///    are supported..
    ///    if a line does not start with Print or PrintLine ..
    ///    an exception is thrown
    /// </summary>
    /// <returns></returns>
    private Stmt Statement()
    {
        Stmt retval;
        switch (Current_Token)
        {
            case TOKEN.TOK_PRINT:
                retval = ParsePrintStatement();
                GetNext();
                break;
            case TOKEN.TOK_PRINTLN:
                retval = ParsePrintLNStatement();
                GetNext();
                break;
            default:
                throw new Exception("Invalid statement");
        }
        return retval;
    }
    /// <summary>
    ///    Parse the Print Staement .. The grammar is 
    ///    PRINT <expr> ;
    ///    Once you are in this subroutine , we are expecting 
    ///    a valid expression ( which will be compiled ) and a
    ///    semi collon to terminate the line..
    ///    Once Parse Process is successful , we create a PrintStatement
    ///    Object..
    /// </summary>
    /// <returns></returns>
    private Stmt ParsePrintStatement()
    {
        GetNext();
        Exp a = Expr();

        if (Current_Token != TOKEN.TOK_SEMI)
        {
            throw new Exception("; is expected");
        }
        return new PrintStatement(a);
    }
    /// <summary>
    ///    Parse the PrintLine Staement .. The grammar is 
    ///    PRINTLINE <expr> ;
    ///    Once you are in this subroutine , we are expecting 
    ///    a valid expression ( which will be compiled ) and a
    ///    semi collon to terminate the line..
    ///    Once Parse Process is successful , we create a PrintLineStatement
    ///    Object..
    /// </summary>
    /// <returns></returns>
    private Stmt ParsePrintLNStatement()
    {
        GetNext();
        Exp a = Expr();

        if (Current_Token != TOKEN.TOK_SEMI)
        {
            throw new Exception("; is expected");
        }
        return new PrintLineStatement(a);
    }
}