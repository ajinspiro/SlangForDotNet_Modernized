using System;

namespace SlangForDotNet.AST;

/// <summary>
/// This class supports Unary Operators + and - 
/// </summary>
public class UnaryExp : Exp
{
    private Exp _ex1;
    private OPERATOR _op;

    public UnaryExp(Exp a, OPERATOR op)
    {
        _ex1 = a;
        _op = op;
    }

    /// <summary>
    /// While evaluating a numeric constant , return the _value
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override double Evaluate(RUNTIME_CONTEXT cont)
    {
        switch (_op)
        {
            case OPERATOR.PLUS:
                return _ex1.Evaluate(cont);
            case OPERATOR.MINUS:
                return -_ex1.Evaluate(cont);
        }
        return double.NaN;
    }
}