using System;
using System.Collections.Generic;
using System.Linq;
using RevnCompiler.Lexers;

namespace RevnCompiler.Parsers.Asts
{

    internal abstract class Ast
    {
        internal abstract string GenerateIl();
    }

    internal abstract class ExpressionAst : Ast { }

    internal class BinopAst : ExpressionAst
    {
        private readonly Ast _leftHandSide;
        private readonly Ast _rightHandSide;
        private readonly Token _operand;

        public BinopAst(Ast leftHandSide, Ast rightHandSide, Token operand)
        {
            _leftHandSide = leftHandSide;
            _rightHandSide = rightHandSide;
            _operand = operand;
        }

        internal override string GenerateIl()
        {
            string operandIl;
            switch (_operand.Value)
            {
                case "+":
                    operandIl = "add";
                    break;
                case "-":
                    operandIl = "sub";
                    break;
                case "/":
                    operandIl = "div";
                    break;
                case "*":
                    operandIl = "mul";
                    break;
                default: throw new Exception();
            }

            return _leftHandSide.GenerateIl()
                   + _rightHandSide.GenerateIl()
                   + operandIl + "\n";
        }
    }

    internal abstract class LiteralAst : ExpressionAst
    {
        protected object Value;

        internal LiteralAst(object value)
        {
            Value = value;
        }
    }

    internal class NumeralAst : LiteralAst
    {
        public NumeralAst(object value) : base(value) { }

        internal override string GenerateIl()
        {
            return $"ldc.r8 {Value}\n";
        }
    }

    internal class StringAst : LiteralAst
    {
        public StringAst(object value) : base(value) { }

        internal override string GenerateIl()
        {
            return $"ldstr \"{Value}\"\n";
        }
    }

    internal class FunctionAst : Ast
    {
        private readonly string _name;
        private readonly List<ExpressionAst> _expressionAsts;
        private bool IsEntryPoint => _name == "Main";

        public FunctionAst(string name, List<ExpressionAst> expressionAsts)
        {
            _name = name;
            _expressionAsts = expressionAsts;
        }

        internal override string GenerateIl()
        {
            string expressionIls = _expressionAsts.Aggregate(string.Empty, (prev, ast) => prev + ast.GenerateIl());
            return $".method public hidebysig static void\n{_name}() cil managed\n{{\n{( IsEntryPoint ? ".entrypoint" : "" )}\n{expressionIls}\nret}}\n";
        }
    }

}