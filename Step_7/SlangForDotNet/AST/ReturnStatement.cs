using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

public class ReturnStatement : Stmt
{
    private Exp m_e1;
    /// <summary>
    /// 
    /// </summary>
    private SYMBOL_INFO inf = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e1"></param>
    public ReturnStatement(Exp e1)
    {
        m_e1 = e1;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {
        inf = (m_e1 == null) ? null : m_e1.Evaluate(cont);
        return inf;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        if (m_e1 != null)
        {
            m_e1.Compile(cont);
        }
        cont.CodeOutput.Emit(OpCodes.Ret);
        return true;
    }

}