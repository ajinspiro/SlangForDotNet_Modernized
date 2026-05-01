using System.Collections;
using System.Reflection.Emit;
using SlangForDotNet.ExeGenerator;

namespace SlangForDotNet.AST;

public class IfStatement : Stmt
{
    /// <summary>
    ///    cond expression
    ///    the type ought to be boolean
    /// </summary>
    private Exp cond;
    /// <summary>
    ///    List of statements to be 
    ///    executed if cond is true
    /// </summary>
    private ArrayList stmnts;
    /// <summary>
    ///   List of statements to be 
    ///   executed if cond is false
    /// </summary>
    private ArrayList else_part;

    /// <summary>
    ///   IF <Bexpr> Then
    ///      <statementlist>
    ///   ELSE
    ///      <statementlist>
    ///   ENDIF
    /// </summary>
    /// <param name="c"></param>
    /// <param name="s"></param>
    /// <param name="e"></param>
    public IfStatement(Exp c, ArrayList s, ArrayList e)
    {
        cond = c;
        stmnts = s;
        else_part = e;
    }
    /// <summary>
    ///    Interpret the if statement
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override SYMBOL_INFO Execute(RUNTIME_CONTEXT cont)
    {
        //
        //  Evaluate the Condition
        //
        SYMBOL_INFO m_cond = cond.Evaluate(cont);

        //
        // if cond is not boolean..or evaluation failed
        //
        if (m_cond == null ||
            m_cond.Type != TYPE_INFO.TYPE_BOOL)
            return null;
        if (m_cond.bol_val == true)
        {
            //
            // if cond is true
            foreach (Stmt rst in stmnts)
                rst.Execute(cont);
        }
        else if (else_part != null)
        {
            // if cond is false and there is 
            // else statement ..!
            foreach (Stmt rst in else_part)
                rst.Execute(cont);
        }
        return null;
    }
 
    public override bool Compile(DNET_EXECUTABLE_GENERATION_CONTEXT cont)
    {
        Label true_label, false_label;
        // 
        // Generate Label for True
        true_label = cont.CodeOutput.DefineLabel();
        // Generate Label for False
        false_label = cont.CodeOutput.DefineLabel();
        //
        // Compile the expression 
        //
        cond.Compile(cont);
        //
        // Check whether the top of the stack contain
        // 1 ( TRUE)
        cont.CodeOutput.Emit(OpCodes.Ldc_I4, 1);
        cont.CodeOutput.Emit(OpCodes.Ceq);
        //
        //  if False , jump to false_label ...
        //  ie to else part
        cont.CodeOutput.Emit(OpCodes.Brfalse, false_label);

        foreach (Stmt rst in stmnts)
        {
            rst.Compile(cont);
        }
        // Once we have reached here...go
        // to True label...
        cont.CodeOutput.Emit(OpCodes.Br, true_label);
        //
        // Place a Label here...if the condition evaluates
        // to false , jump to this place..
        cont.CodeOutput.MarkLabel(false_label);

        if (else_part != null)
        {
            foreach (Stmt rst in else_part)
            {
                rst.Compile(cont);

            }
        }
        //
        // Place a label here...to mark the end of the
        // IF statement
        cont.CodeOutput.MarkLabel(true_label);
        return true;
    }
}