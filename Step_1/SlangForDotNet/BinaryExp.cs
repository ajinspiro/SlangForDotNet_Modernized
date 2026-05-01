using System;

namespace SlangForDotNet;

/// <summary>
/// This class supports Binary Operators like + , - , / , *
/// </summary>
public class BinaryExp : Exp
{
    private Exp _ex1, _ex2;
    private OPERATOR _op;

    public BinaryExp(Exp a, Exp b, OPERATOR op)
    {
        _ex1 = a;
        _ex2 = b;
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
                return _ex1.Evaluate(cont) + _ex2.Evaluate(cont);
            case OPERATOR.MINUS:
                return _ex1.Evaluate(cont) - _ex2.Evaluate(cont);
            case OPERATOR.DIV:
                return _ex1.Evaluate(cont) / _ex2.Evaluate(cont);
            case OPERATOR.MUL:
                return _ex1.Evaluate(cont) * _ex2.Evaluate(cont);
        }
        return double.NaN;
    }
}