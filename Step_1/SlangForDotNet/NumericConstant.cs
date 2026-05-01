using System;

namespace SlangForDotNet;

public class NumericConstant : Exp
{
    private double _value;
    /// <summary>
    /// Construction does not do much , just keeps the value assigned to the private variable.
    /// </summary>
    /// <param name="value"></param>

    public NumericConstant(double value)
    {
        _value = value;
    }
    /// <summary>
    /// While evaluating a numeric constant , return the _value
    /// </summary>
    /// <param name="cont"></param>
    /// <returns></returns>
    public override double Evaluate(RUNTIME_CONTEXT cont)
    {
        return _value;
    }
}