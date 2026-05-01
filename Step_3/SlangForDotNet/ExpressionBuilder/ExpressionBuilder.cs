using SlangForDotNet.AST;

namespace SlangForDotNet.ExpressionBuilder;

public class ExpressionBuilder : AbstractBuilder
{
    public string _expr_string;
    public ExpressionBuilder(string expr)
    {
        _expr_string = expr;
    }
    public Exp GetExpression()
    {
        try
        {
            RDParser.RDParser p = new(_expr_string);
            return p.CallExpr();
        }
        catch (Exception)
        {
            return null;
        }
    }
}