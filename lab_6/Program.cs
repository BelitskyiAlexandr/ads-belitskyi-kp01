using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_6
{
    public class Stack<T>
    {
        private T[] items;
        private int count;
        const int n = 10;
        public Stack()
        {
            items = new T[n];
        }

        public bool IsEmpty
        {
            get { return count == 0; }
        }
        public int Count
        {
            get { return count; }
        }

        public void Push(T item)
        {
            if (count == items.Length)
                Resize(items.Length + 10);

            items[count++] = item;
        }
        public T Pop()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Stack is empty");
            T item = items[--count];
            items[count] = default(T);

            if (count > 0 && count < items.Length - 10)
                Resize(items.Length - 10);

            return item;
        }
        public T Peek()
        {
            return items[count - 1];
        }

        private void Resize(int max)
        {
            T[] tempItems = new T[max];
            for (int i = 0; i < count; i++)
                tempItems[i] = items[i];
            items = tempItems;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return this.items.Take(this.count).GetEnumerator();
        }
    }
    class PostfixNotationExpression
    {
        public PostfixNotationExpression()
        {
            operators = new List<string>(standart_operators);
        }
        private List<string> operators;
        private List<string> standart_operators =
            new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^" });
        private IEnumerable<string> Separate(string input)
        {
            int pos = 0;
            while (pos < input.Length)
            {
                string s = string.Empty + input[pos];
                if (!standart_operators.Contains(input[pos].ToString()))
                {
                    if (Char.IsDigit(input[pos]))
                        for (int i = pos + 1; i < input.Length &&
                            (Char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++)
                            s += input[i];
                    else if (Char.IsLetter(input[pos]))
                        for (int i = pos + 1; i < input.Length &&
                            (Char.IsLetter(input[i]) || Char.IsDigit(input[i])); i++)
                            s += input[i];
                }
                yield return s;
                pos += s.Length;
            }
        }
        private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }
        public string[] ConvertToPostfixNotation(string input)
        {
            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in Separate(input))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSeparated.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (GetPriority(c) > GetPriority(stack.Peek()))
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                                outputSeparated.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else
                        stack.Push(c);
                }
                else
                    outputSeparated.Add(c);
            }
            if (stack.Count > 0)
                foreach (string c in stack)
                    outputSeparated.Add(c);
            return outputSeparated.ToArray();
        }
        public decimal result(string input)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(ConvertToPostfixNotation(input));
            string str = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!operators.Contains(str))
                {
                    stack.Push(str);
                    str = queue.Dequeue();
                }
                else
                {
                    decimal summ = 0;
                    try
                    {
                        switch (str)
                        {
                            case "+":
                                {
                                    decimal a = Convert.ToDecimal(stack.Pop());
                                    decimal b = Convert.ToDecimal(stack.Pop());
                                    summ = a + b;
                                    break;
                                }
                            case "-":
                                {
                                    decimal a = Convert.ToDecimal(stack.Pop());
                                    decimal b = Convert.ToDecimal(stack.Pop());
                                    summ = b - a;
                                    break;
                                }
                            case "*":
                                {
                                    decimal a = Convert.ToDecimal(stack.Pop());
                                    decimal b = Convert.ToDecimal(stack.Pop());
                                    summ = b * a;
                                    break;
                                }
                            case "/":
                                {
                                    decimal a = Convert.ToDecimal(stack.Pop());
                                    decimal b = Convert.ToDecimal(stack.Pop());
                                    summ = b / a;
                                    break;
                                }
                            case "^":
                                {
                                    decimal a = Convert.ToDecimal(stack.Pop());
                                    decimal b = Convert.ToDecimal(stack.Pop());
                                    summ = Convert.ToDecimal(Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a)));
                                    break;
                                }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    stack.Push(summ.ToString());
                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;
                }
            }
            return Convert.ToDecimal(stack.Pop());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var RPN = new PostfixNotationExpression();
            Console.WriteLine("Enter expression: ");
            string expression = Console.ReadLine();
            int isOpen = 0, isClose = 0;
            for (int i = 0; i < expression.Length; i++)
            {
                char s = expression[i];
                if (char.IsLetter(s))
                {
                    Console.WriteLine("Error: example must not have letters");
                    Environment.Exit(0);
                }
                else if (!((s == '+') || (s == '-') || (s == '/') || (s == '*') || (s == '(') || (s == ')') || char.IsDigit(s)))
                {
                    Console.WriteLine("Error: example must have only standart operations: '+', '-', '/', '*' and '(', ')'");
                    Environment.Exit(0);
                }
                else if (s == '(')
                {
                    if (!((expression[i - 1] == '+') || (expression[i - 1] == '-') || (expression[i - 1] == '/') || (expression[i - 1] == '*')))
                    {
                        Console.WriteLine("Error: check operand before '('");
                        Environment.Exit(0);
                    }
                    isOpen++;
                }
                else if (s == ')')
                {
                    isClose++;
                }
                else if (int.TryParse(expression, out int num))
                {
                    Console.WriteLine("Error: example must have more than one number");
                    Environment.Exit(0);
                }
            }
            if (isClose != isOpen)
            {
                if (isClose > isOpen)
                {
                    Console.WriteLine("Error: you have ')' more than '(' ");
                    Environment.Exit(0);
                }
                else if (isClose < isOpen)
                {
                    Console.WriteLine("Error: you have '(' more than ')' ");
                    Environment.Exit(0);
                }
            }
            string[] RPNexpression = RPN.ConvertToPostfixNotation(expression);

            foreach (var item in RPNexpression)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Result: {0}", RPN.result(expression));
        }


    }
}
