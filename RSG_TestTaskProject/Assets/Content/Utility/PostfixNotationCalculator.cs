using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility.Calculator
{
    public class PostfixNotationCalculator
    {
        private readonly Stack<OperandToken> _operandTokensStack;
        public PostfixNotationCalculator()
        {
            _operandTokensStack = new Stack<OperandToken>();
        }
        public float Process(string expression)
        {
            var parser = new PostfixTokenizer();
            return Calculate(parser.Parse(expression)).Value;
        }
        public bool ProcessLogic(string expression)
        {
            var parser = new PostfixTokenizer();
            return Convert.ToBoolean(Calculate(parser.Parse(expression)).Value);
        }
        public float ProcessInfix(string expression)
        {
            var parser = new InfixToPostfixTokenizer();
            return Calculate(parser.Parse(expression)).Value;
        }
        public bool ProcessLogicInfix(string expression)
        {
            var parser = new InfixToPostfixTokenizer();
            return Convert.ToBoolean(Calculate(parser.Parse(expression)).Value);
        }
        private OperandToken Calculate(IEnumerable<IToken> tokens)
        {
            Reset();
            foreach (var token in tokens)
            {
                ProcessToken(token);
            }
            return GetResult();
        }

        private void Reset()
        {
            _operandTokensStack.Clear();
        }

        private void ProcessToken(IToken token)
        {
            switch (token)
            {
                case OperandToken operandToken:
                    StoreOperand(operandToken);
                    break;
                case OperatorToken operatorToken:
                    ApplyOperator(operatorToken);
                    break;
                default:
                    var exMessage = $"An unknown token type: {token.GetType()}.";
                    throw new Exception(exMessage);
            }
        }

        private void StoreOperand(OperandToken operandToken)
        {
            _operandTokensStack.Push(operandToken);
        }

        private void ApplyOperator(OperatorToken operatorToken)
        {
            switch (operatorToken.OperatorType)
            {
                case OperatorType.Addition:
                    ApplyAdditionOperator();
                    break;
                case OperatorType.Subtraction:
                    ApplySubtractionOperator();
                    break;
                case OperatorType.Multiplication:
                    ApplyMultiplicationOperator();
                    break;
                case OperatorType.Division:
                    ApplyDivisionOperator();
                    break;
                case OperatorType.LogicAND:
                    ApplyLosicANDOperator();
                    break;
                case OperatorType.LogicOR:
                    ApplyLosicOROperator();
                    break;
                case OperatorType.LogicNOT:
                    ApplyLosicNOTOperator();
                    break;
                default:
                    var exMessage = $"An unknown operator type: {operatorToken.OperatorType}.";
                    throw new Exception(exMessage);
            }
        }

        private void ApplyAdditionOperator()
        {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value + operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private void ApplySubtractionOperator()
        {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value - operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private void ApplyMultiplicationOperator()
        {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value * operands.Item2.Value);
            _operandTokensStack.Push(result);
        }

        private void ApplyDivisionOperator()
        {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(operands.Item1.Value / operands.Item2.Value);
            _operandTokensStack.Push(result);
        }
        private void ApplyLosicANDOperator()
        {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(Convert.ToSingle(Convert.ToBoolean(operands.Item1.Value) & Convert.ToBoolean(operands.Item2.Value)));
            _operandTokensStack.Push(result);
        }
        private void ApplyLosicOROperator()
        {
            var operands = GetBinaryOperatorArguments();
            var result = new OperandToken(Convert.ToSingle(Convert.ToBoolean(operands.Item1.Value) | Convert.ToBoolean(operands.Item2.Value)));
            _operandTokensStack.Push(result);
        }
        private void ApplyLosicNOTOperator()
        {
            var operand = GetUnaryOperatorArguments();
            var result = new OperandToken(1 - operand.Value); // numeric 1-value equal to bolean !value
            _operandTokensStack.Push(result);
        }
        private OperandToken GetUnaryOperatorArguments()
        {
            if (_operandTokensStack.Count < 1)
            {
                var exMessage = "Not enough arguments for applying a unary operator.";
                throw new Exception(exMessage);
            }

            var left = _operandTokensStack.Pop();

            return left;
        }
        private Tuple<OperandToken, OperandToken> GetBinaryOperatorArguments()
        {
            if (_operandTokensStack.Count < 2)
            {
                var exMessage = "Not enough arguments for applying a binary operator.";
                throw new Exception(exMessage);
            }

            var right = _operandTokensStack.Pop();
            var left = _operandTokensStack.Pop();

            return Tuple.Create(left, right);
        }

        private OperandToken GetResult()
        {
            if (_operandTokensStack.Count == 0)
            {
                var exMessage = "The expression is invalid." +
                    " Check, please, that the expression is not empty.";
                throw new Exception(exMessage);
            }

            if (_operandTokensStack.Count != 1)
            {
                var exMessage = "The expression is invalid." +
                    " Check, please, that you're providing the full expression and" +
                    " the tokens have a correct order.";
                throw new Exception(exMessage);
            }

            return _operandTokensStack.Pop();
        }
        abstract class BaseTokenizer
        {
            protected readonly StringBuilder _valueTokenBuilder;
            protected readonly List<IToken> _infixNotationTokens;
            public BaseTokenizer()
            {
                _valueTokenBuilder = new StringBuilder();
                _infixNotationTokens = new List<IToken>();
            }
            protected static bool IsOperatorCharacter(char c) => c switch
            {
                var x when new char[] { '+', '-', '*', '/', '&', '|', '!', '(', ')' }.Contains(x) => true,
                _ => false
            };

            protected static bool IsSpacingCharacter(char c)
            {
                return c switch
                {
                    ' ' => true,
                    _ => false,
                };
            }

            protected static IToken CreateOperandToken(string raw)
            {
                if (float.TryParse(
                    raw,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out float result))
                {
                    return new OperandToken(result);
                }

                throw new Exception($"The operand {raw} has an invalid format.");
            }

            protected static OperatorToken CreateOperatorToken(char c)
            {
                return c switch
                {
                    '(' => new OperatorToken(OperatorType.LeftParenthesis),
                    ')' => new OperatorToken(OperatorType.RightParenthesis),
                    '+' => new OperatorToken(OperatorType.Addition),
                    '-' => new OperatorToken(OperatorType.Subtraction),
                    '*' => new OperatorToken(OperatorType.Multiplication),
                    '/' => new OperatorToken(OperatorType.Division),
                    '&' => new OperatorToken(OperatorType.LogicAND),
                    '|' => new OperatorToken(OperatorType.LogicOR),
                    '!' => new OperatorToken(OperatorType.LogicNOT),
                    _ => throw new Exception($"There's no a suitable operator for the char {c}"),
                };
            }
            public IEnumerable<IToken> Parse(string expression)
            {
                Reset();
                foreach (char next in expression)
                {
                    FeedCharacter(next);
                }
                return GetResult();
            }
            private void Reset()
            {
                _valueTokenBuilder.Clear();
                _infixNotationTokens.Clear();
            }
            private void FeedCharacter(char next)
            {
                if (IsSpacingCharacter(next))
                {
                    if (_valueTokenBuilder.Length > 0)
                    {
                        var token = CreateOperandToken(_valueTokenBuilder.ToString());
                        _valueTokenBuilder.Clear();
                        _infixNotationTokens.Add(token);
                    }
                }
                else if (IsOperatorCharacter(next))
                {
                    if (_valueTokenBuilder.Length > 0)
                    {
                        var token = CreateOperandToken(_valueTokenBuilder.ToString());
                        _valueTokenBuilder.Clear();
                        _infixNotationTokens.Add(token);
                    }

                    var operatorToken = CreateOperatorToken(next);
                    _infixNotationTokens.Add(operatorToken);
                }
                else
                {
                    _valueTokenBuilder.Append(next);
                }
            }
            protected abstract IEnumerable<IToken> GetResult();
        }
        class PostfixTokenizer : BaseTokenizer
        {
            protected override IEnumerable<IToken> GetResult()
            {
                if (_valueTokenBuilder.Length > 0)
                {
                    var token = CreateOperandToken(_valueTokenBuilder.ToString());
                    _valueTokenBuilder.Clear();
                    _infixNotationTokens.Add(token);
                }

                return _infixNotationTokens.ToList();
            }
        }

        class InfixToPostfixTokenizer : BaseTokenizer
        {
            protected override IEnumerable<IToken> GetResult()
            {
                if (_valueTokenBuilder.Length > 0)
                {
                    var token = CreateOperandToken(_valueTokenBuilder.ToString());
                    _valueTokenBuilder.Clear();
                    _infixNotationTokens.Add(token);
                }

                return ToPostfix(_infixNotationTokens);
            }

            private IEnumerable<IToken> ToPostfix(List<IToken> infixNotationTokens)
            {
                var operatorStack = new Stack<OperatorToken>();
                List<IToken> postfixTokens = new List<IToken>();

                foreach (var token in infixNotationTokens)
                {
                    if (token is OperandToken)
                    {
                        postfixTokens.Add(token);
                    }
                    else if (token is OperatorToken opToken)
                    {
                        if (opToken.OperatorType == OperatorType.LeftParenthesis)
                        {
                            operatorStack.Push(opToken);
                        }
                        else if (opToken.OperatorType == OperatorType.RightParenthesis)
                        {
                            while (operatorStack.Count > 0 && operatorStack.Peek().OperatorType != OperatorType.LeftParenthesis)
                            {
                                postfixTokens.Add(operatorStack.Pop());
                            }
                            if (operatorStack.Count == 0 || operatorStack.Peek().OperatorType != OperatorType.LeftParenthesis)
                            {
                                throw new ArgumentException("Неправильно розставлені дужки");
                            }
                            operatorStack.Pop(); // Видаляємо відкриваючу дужку зі стеку
                        }
                        else
                        {
                            while (operatorStack.Count > 0 && operatorStack.Peek().OperatorType != OperatorType.LeftParenthesis &&
                                   GetPrecedence(operatorStack.Peek()) >= GetPrecedence(opToken))
                            {
                                postfixTokens.Add(operatorStack.Pop());
                            }
                            operatorStack.Push(opToken);
                        }
                    }
                }

                while (operatorStack.Count > 0)
                {
                    if (operatorStack.Peek().OperatorType == OperatorType.LeftParenthesis ||
                        operatorStack.Peek().OperatorType == OperatorType.RightParenthesis)
                    {
                        throw new ArgumentException("Неправильно розставлені дужки");
                    }
                    postfixTokens.Add(operatorStack.Pop());
                }

                return postfixTokens;

            }
            public int GetPrecedence(OperatorToken token)
            {
                var op = token.OperatorType;
                if (op == OperatorType.Multiplication || op == OperatorType.Division || op == OperatorType.LogicNOT)
                    return 3;
                else if (op == OperatorType.Addition || op == OperatorType.Subtraction || op == OperatorType.LogicAND)
                    return 2;
                else if (op == OperatorType.LogicOR)
                    return 1;
                else
                    return 0;
            }
        }


        interface IToken { }

        class OperandToken : IToken
        {
            public float Value { get; }

            public OperandToken(float value)
            {
                Value = value;
            }
        }

        enum OperatorType
        {
            Addition,
            Subtraction,
            Multiplication,
            Division,
            LogicAND,
            LogicOR,
            LogicNOT,
            LeftParenthesis,
            RightParenthesis
        }

        class OperatorToken : IToken
        {
            public OperatorType OperatorType { get; }

            public OperatorToken(OperatorType operatorType)
            {
                OperatorType = operatorType;
            }
        }
    }
}