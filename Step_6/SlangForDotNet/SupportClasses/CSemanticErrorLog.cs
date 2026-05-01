using System.Collections;

namespace SlangForDotNet.SupportClasses;

/// <summary>
///    Error for semntic erros
/// </summary>
/// 
public class CSemanticErrorLog
{
    /// <summary>
    ///     
    /// </summary>
    static int ErrorCount = 0;
    static ArrayList lst = new ArrayList();
    /// <summary>
    ///    Ctor
    /// </summary>
    static CSemanticErrorLog()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public static void Cleanup()
    {
        lst.Clear();
        ErrorCount = 0;
    }
    /// <summary>
    ///    Get Logged data as a String 
    /// </summary>
    /// <returns></returns>
    public static String GetLog()
    {


        String str = "Logged data by the user and processing status" + "\r\n";
        str += "--------------------------------------\r\n";

        int xt = lst.Count;

        if (xt == 0)
        {
            str += "NIL" + "\r\n";

        }
        else
        {

            for (int i = 0; i < xt; ++i)
            {
                str = str + lst[i].ToString() + "\r\n";
            }
        }
        str += "--------------------------------------\r\n";
        return str;
    }
    /// <summary>
    ///    Add a Line to Log
    /// </summary>
    /// <param name="str"></param>
    public static void AddLine(String str)
    {
        lst.Add(str.Substring(0));
        ErrorCount++;
    }
    /// <summary>
    ///   Add From a Script   
    /// </summary>
    /// <param name="str"></param>

    public static void AddFromUser(String str)
    {
        lst.Add(str.Substring(0));
        ErrorCount++;

    }
}