namespace SlangForDotNet.SupportClasses;

/// <summary>
///    class for Exception Handling 
/// </summary>
public class CParserException : Exception
{
    private int ErrorCode;
    private String ErrorString;
    private int Lexical_Offset;
    /// <summary>
    ///   Ctor
    /// </summary>
    /// <param name="pErrorCode"></param>
    /// <param name="pErrorString"></param>
    /// <param name="pLexical_Offset"></param>

    public CParserException(int pErrorCode,
        String pErrorString,
        int pLexical_Offset)
    {
        ErrorCode = pErrorCode;
        ErrorString = pErrorString;
        Lexical_Offset = pLexical_Offset;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetErrorCode()
    {
        return ErrorCode;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public String GetErrorString()
    {
        return ErrorString;
    }
    /// <summary>
    ///   
    /// </summary>
    /// <returns></returns>

    public int GetLexicalOffset()
    {
        return Lexical_Offset;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lex"></param>

    public void SetLexicalOffset(int lex)
    {
        Lexical_Offset = lex;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pStr"></param>

    public void SetErrorString(String pStr)
    {
        ErrorString = pStr;
    }


}