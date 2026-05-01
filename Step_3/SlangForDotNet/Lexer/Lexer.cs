namespace SlangForDotNet.Lexer;

//////////////////////////////////////////////////////////
//
// A naive Lexical analyzer which looks for operators , Parenthesis
// and number. All numbers are treated as IEEE doubles. Only numbers
// without decimals can be entered. Feel free to modify the code
// to accomodate LONG and Double values
public class Lexer
{

    private string _exp;
    private int _index;
    private int _length_string;
    private double _curr_num;
    private ValueTable[] _val = null;
    private string last_str = string.Empty;

    public Lexer(string exp)
    {
        _exp = exp;
        _length_string = exp.Length;
        _index = 0;

        _val = new ValueTable[2];
        _val[0] = new ValueTable(TOKEN.TOK_PRINT, "PRINT");
        _val[1] = new ValueTable(TOKEN.TOK_PRINTLN, "PRINTLINE");
    }


    public double Number
    {
        get { return _curr_num; }
    }

    public double GetNumber()
    {
        return _curr_num;
    }

    public TOKEN GetToken()
    {
    re_start: /// Label
        TOKEN tok = TOKEN.ILLEGAL_TOKEN;

        //// Skipping white spaces
        while ((_index < _length_string)
            && (_exp[_index] == ' ' || _exp[_index] == '\t'))
        {
            _index++;
        }

        /// Enf Of Expression
        if (_index == _length_string)
        {
            return TOKEN.TOK_NULL;
        }



        switch (_exp[_index])
        {
            case '\r':
            case '\n':
                _index++;
                goto re_start;
            case '+':
                tok = TOKEN.TOK_PLUS;
                _index++;
                break;
            case '-':
                tok = TOKEN.TOK_SUB;
                _index++;
                break;
            case '/':
                tok = TOKEN.TOK_DIV;
                _index++;
                break;
            case '*':
                tok = TOKEN.TOK_MUL;
                _index++;
                break;
            case '(':
                tok = TOKEN.TOK_OPAREN;
                _index++;
                break;
            case ')':
                tok = TOKEN.TOK_CPAREN;
                _index++;
                break;
            case ';':
                tok = TOKEN.TOK_SEMI;
                _index++;
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
                    while ((_index < _length_string)
                        && (_exp[_index] == '0' ||
                        _exp[_index] == '1' ||
                        _exp[_index] == '2' ||
                        _exp[_index] == '3' ||
                        _exp[_index] == '4' ||
                        _exp[_index] == '5' ||
                        _exp[_index] == '6' ||
                        _exp[_index] == '7' ||
                        _exp[_index] == '8' ||
                        _exp[_index] == '9'))
                    {
                        str += Convert.ToString(_exp[_index]);
                        _index++;
                    }
                    _curr_num = Convert.ToDouble(str);
                    tok = TOKEN.TOK_DOUBLE;

                }
                break;
            default:
                {
                    if (char.IsLetter(_exp[_index]))
                    {

                        String tem = Convert.ToString(_exp[_index]);
                        _index++;
                        while (_index < _length_string && (char.IsLetterOrDigit(_exp[_index]) ||
                        _exp[_index] == '_'))
                        {
                            tem += _exp[_index];
                            _index++;
                        }

                        tem = tem.ToUpper();

                        for (int i = 0; i < this._val.Length; ++i)
                        {
                            if (_val[i].Value.CompareTo(tem) == 0)
                                return _val[i].tok;

                        }


                        this.last_str = tem;



                        return TOKEN.TOK_UNQUOTED_STRING;



                    }
                    else
                    {
                        Console.WriteLine("Error");
                        throw new Exception();
                    }

                }
        }
        return tok;
    }
}