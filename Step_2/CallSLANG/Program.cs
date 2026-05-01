
using SlangForDotNet.AST;
using SlangForDotNet.ExpressionBuilder;

ExpressionBuilder b = new ExpressionBuilder("2*(5+(3-4+5))");
Exp e = b.GetExpression();
Console.WriteLine(e.Evaluate(null));