using System.Collections;

namespace SlangForDotNet.SupportClasses;

public class CSyntaxErrorLog
{

    /// <summary>
    ///   instance variables
    /// </summary>
    static int ErrorCount = 0;
    static ArrayList lst = new ArrayList();
    /// <summary>
    ///    Ctor
    /// </summary>
    static CSyntaxErrorLog()
    {

    }


    public static void Cleanup()
    {
        lst.Clear();
        ErrorCount = 0;
    }
    /// <summary>
    ///    Add a Line from script
    /// </summary>
    /// <param name="str"></param>

    public static void AddLine(String str)
    {
        lst.Add(str.Substring(0));
        ErrorCount++;

    }

    /// <summary>
    ///    Get Logged data as a String 
    /// </summary>
    /// <returns></returns>
    public static String GetLog()
    {

        String str = "Syntax Error" + "\r\n";
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
}