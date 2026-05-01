namespace SlangForDotNet.Lexer;

//////////////////////////////////////////////////////////
//
// A naive Lexical analyzer which looks for operators , Parenthesis
// and number. All numbers are treated as IEEE doubles. Only numbers
// without decimals can be entered. Feel free to modify the code
// to accomodate LONG and Double values
public class Lexer
{
    string IExpr; // Expression string
    int index; // index into a character
    int length; // Length of the string
    double number; // Last grabbed number from the stream
                   /////////////////////////////////////////////
                   //
                   // Ctor
                   //
                   //
    public Lexer(string Expr)
    {
        IExpr = Expr;
        length = IExpr.Length;
        index = 0;
    }
    /////////////////////////////////////////////////////
    // Grab the next token from the stream
    //
    //
    //
    //
    public TOKEN GetToken()
    {
        TOKEN tok = TOKEN.ILLEGAL_TOKEN;
        ////////////////////////////////////////////////////////////
        //
        // Skip the white space
        //
        while (index < length &&
        (IExpr[index] == ' ' || IExpr[index] == '\t'))
            index++;
        //////////////////////////////////////////////
        //
        // End of string ? return NULL;
        //
        if (index == length)
            return TOKEN.TOK_NULL;
        /////////////////////////////////////////////////
        //
        //
        //
        switch (IExpr[index])
        {
            case '+':
                tok = TOKEN.TOK_PLUS;
                index++;
                break;
            case '-':
                tok = TOKEN.TOK_SUB;
                index++;
                break;
            case '/':
                tok = TOKEN.TOK_DIV;
                index++;
                break;
            case '*':
                tok = TOKEN.TOK_MUL;
                index++;
                break;
            case '(':
                tok = TOKEN.TOK_OPAREN;
                index++;
                break;
            case ')':
                tok = TOKEN.TOK_CPAREN;
                index++;
                break;
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                {
                    string str = "";
                    while (index < length &&
                    (IExpr[index] == '0' ||
                    IExpr[index] == '1' ||
                    IExpr[index] == '2' ||
                    IExpr[index] == '3' ||
                    IExpr[index] == '4' ||
                    IExpr[index] == '5' ||
                    IExpr[index] == '6' ||
                    IExpr[index] == '7' ||
                    IExpr[index] == '8' ||
                    IExpr[index] == '9'))
                    {
                        str += Convert.ToString(IExpr[index]);
                        index++;
                    }
                    number = Convert.ToDouble(str);
                    tok = TOKEN.TOK_DOUBLE;
                }
                break;
            default:
                Console.WriteLine("Error While Analyzing Tokens");
                throw new Exception();
        }
        return tok;
    }
    public double GetNumber() { return number; }
}