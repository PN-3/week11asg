namespace Week11;

/***********************************************************
* CSC205 Assignment 11 
* Question 1 
***********************************************************/

using System;

class Program
{
    /*
     * CheckParenthesesA verifies if a string has properly balanced parentheses.
     * It pushes each '(' onto a stack, and pops when it sees ')'.
     * If the stack becomes empty before the end, it returns false early.
     * Returns true only if everything matches and nothing is left open.
     */
    static bool CheckParenthesesA(string str)
    {
        var stk = new Stack<char>();

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
                stk.Push(str[i]);
            else if (str[i] == ')' && !stk.IsEmpty())
                stk.Pop();

            if (stk.IsEmpty() && i < str.Length - 1)
                return false;
        }

        return stk.IsEmpty();
    }

    /*
     * CheckParenthesesB returns the index of the first unmatched parenthesis.
     * If there's an unmatched ')', it returns its index.
     * If an unmatched '(' remains at the end, it returns that index.
     * Returns -1 when all parentheses are correctly matched.
     */
    static int CheckParenthesesB(string str)
    {
        var stk = new Stack<int>();

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
                stk.Push(i);
            else if (str[i] == ')')
            {
                if (!stk.IsEmpty())
                    stk.Pop();
                else
                    return i;
            }
        }

        return stk.IsEmpty() ? -1 : stk.Pop();
    }

    static void Main(string[] args)
    {
        string[] testStrings = {
            "",               // Empty
            "()",             // Simple match
            "(())",           // Nested match
            "(()())",         // Complex match
            "(()",            // One missing )
            "())",            // Extra )
            ")(",             // Starts wrong
            "((())())",       // Multi-layered valid
            "(abc(def)ghi)",  // Characters included
            "(abc(def)ghi",   // One missing )
            "abc)def(",       // Wrong order
        };

        Console.WriteLine("=== Testing CheckParenthesesA ===\n");
        foreach (var test in testStrings)
        {
            Console.WriteLine($"Input: \"{test}\"");
            Console.WriteLine($"Balanced: {CheckParenthesesA(test)}\n");
        }

        Console.WriteLine("=== Testing CheckParenthesesB ===\n");
        foreach (var test in testStrings)
        {
            int result = CheckParenthesesB(test);
            Console.WriteLine($"Input: \"{test}\"");
            Console.WriteLine(result == -1
                ? "All parentheses matched.\n"
                : $"First unmatched at index: {result}\n");
        }
    }

    // Stack class – no changes needed
    public class Stack<TYPE>
    {
        TYPE[] values;
        int top = -1, capacity = 1024;
        public Stack() => values = new TYPE[capacity];
        public bool IsEmpty() => (top < 0);
        public bool Push(TYPE data)
        {
            if (top == capacity - 1)
                throw new Exception("Stack overflow!");
            values[++top] = data;
            return true;
        }
        public TYPE Pop()
        {
            if (top < 0)
                throw new Exception("Can't pop an empty stack");
            return values[top--];
        }
        public TYPE Peek()
        {
            if (top < 0)
            {
                Console.WriteLine("Peek - no item in an empty stack");
                return default(TYPE);
            }
            return values[top];
        }
        public void Clear() => top = -1;
        public void Display()
        {
            if (top < 0)
            {
                Console.WriteLine("Empty stack");
                return;
            }
            Console.Write("Stack content: \ntop ->");
            for (int i = top; i >= 0; i--)
                Console.Write(" " + values[i]);
            Console.WriteLine();
        }
    }
}
