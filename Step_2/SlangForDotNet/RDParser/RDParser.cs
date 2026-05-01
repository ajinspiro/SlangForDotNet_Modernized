using SlangForDotNet.AST;
using SlangForDotNet.Lexer;

namespace SlangForDotNet.RDParser;

public class RDParser : Lexer.Lexer
{
    TOKEN Current_Token;

    public RDParser(string str)
        : base(str)
    {

    }

    public Exp CallExpr()
    {
        Current_Token = GetToken();
        return Expr();
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
        Exp RetValue = null;
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
}