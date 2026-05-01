using System.Collections;
using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

public class WhileStatement : Stmt
{
    private Exp cond;
    private ArrayList stmnts;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <param name="s"></param>
    public WhileStatement(Exp c, ArrayList s)
    {
        cond = c;
        stmnts = s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {

    Test:

        SYMBOL_INFO m_cond = cond.Evaluate(cont);


        if (m_cond == null || m_cond.Type != TYPE_INFO.TYPE_BOOL)
            return null;

        if (m_cond.bol_val != true)
            return null;

        SYMBOL_INFO tsp = null;
        foreach (Stmt rst in stmnts)
        {
            tsp = rst.Execute(cont);
            if (tsp != null)
            {
                return tsp;
            }
        }

        goto Test;
    }

    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        Label true_label, false_label;
        true_label = cont.CodeOutput.DefineLabel();
        false_label = cont.CodeOutput.DefineLabel();
        cont.CodeOutput.MarkLabel(true_label);
        cond.Compile(cont);
        cont.CodeOutput.Emit(OpCodes.Ldc_I4, 1);
        cont.CodeOutput.Emit(OpCodes.Ceq);
        cont.CodeOutput.Emit(OpCodes.Brfalse, false_label);

        foreach (Stmt rst in stmnts)
        {
            rst.Compile(cont);
        }

        cont.CodeOutput.Emit(OpCodes.Br, true_label);
        cont.CodeOutput.MarkLabel(false_label);
        return true;

    }

}