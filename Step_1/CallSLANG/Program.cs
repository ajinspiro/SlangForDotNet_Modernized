// Abstract Syntax Tree (AST) for 5*10
using SlangForDotNet;

Exp e = new BinaryExp(new NumericConstant(5),
                           new NumericConstant(10),
                           OPERATOR.MUL);

//
// Evaluate the Expression
//
//
Console.WriteLine(e.Evaluate(null));

// AST for  -(10 + (30 + 50 ) )

e = new UnaryExp(
             new BinaryExp(new NumericConstant(10),
                 new BinaryExp(new NumericConstant(30),
                               new NumericConstant(50),
                      OPERATOR.PLUS),
             OPERATOR.PLUS),
         OPERATOR.MINUS);

//
// Evaluate the Expression
//
Console.WriteLine(e.Evaluate(null));
