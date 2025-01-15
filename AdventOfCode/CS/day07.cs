namespace AdventOfCode;

public class day07
{
    static void solve_7_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_7");

        string[] input = File.ReadAllLines(filePath);
        long keysums = 0;

        for (int i = 0; i < input.Length; i++) // < input.Length
        {
            string[] stringArray = input[i].Split(' ');

            long key = Convert.ToInt64(stringArray[0].Substring(0, stringArray[0].Length - 1));
            //Console.WriteLine("key: " + key);
            long[] intArray = new long[stringArray.Length];
            for (int j = 1; j < stringArray.Length; j++)
            {
                intArray[j] = long.Parse(stringArray[j]);
            }

            bool equal = EvalSimulateRecursion(intArray, key);
            if (equal)
            {
                keysums += key;
            }
        }

        //Console.WriteLine("Sums: " + keysums);
    }


    static void solve_7_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_7");

        string[] input = File.ReadAllLines(filePath);
        long keysums = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string[] stringArray = input[i].Split(' ');

            long key = Convert.ToInt64(stringArray[0].Substring(0, stringArray[0].Length - 1));
            long[] intArray = new long[stringArray.Length];
            for (int j = 1; j < stringArray.Length; j++)
            {
                intArray[j] = long.Parse(stringArray[j]);
            }

            if (EvalSimulateRecursionTwo(intArray, key))
            {
                keysums += key;
            }
        }

        //Console.WriteLine("Sums: " + keysums);
    }


    static bool EvalSimulateRecursion(long[] numbers, long key)
    {
        Stack<(int index, long currentResult)> stack = new Stack<(int, long)>();
        stack.Push((1, numbers[0])); // Initialize with the first number
        while (stack.Count > 0)
        {
            var (index, currentResult) = stack.Pop();
            //All numbers processed
            if (index == numbers.Length)
            {
                if (currentResult == key)
                {
                    return true;
                }

                continue;
            }

            // Push next states to the stack (Add and Multiply)
            if (currentResult > key)
            {
                continue;
            }

            stack.Push((index + 1, currentResult + numbers[index])); //+
            stack.Push((index + 1, currentResult * numbers[index])); // * operation
        }

        return false;
    }


//Should output: 328790210468594
    static bool EvalSimulateRecursionTwo(long[] numbers, long key)
    {
        Stack<(int index, long currentResult)> stack = new Stack<(int, long)>();
        stack.Push((1, numbers[0])); // Initialize with the first number

        //Just to beat bo
        long StringToInt64(string str)
        {
            long result = 0;
            foreach (char c in str)
            {
                result = result * 10 + (c - '0'); // Multiply by 10 and add the numeric value of the character
            }

            return result;
        }

        while (stack.Count > 0)
        {
            var (index, currentResult) = stack.Pop();
            if (index == numbers.Length)
            {
                if (currentResult == key)
                {
                    return true;
                }

                continue;
            }

            if (currentResult > key)
            {
                continue;
            }

            stack.Push((index + 1, currentResult * numbers[index])); // * operation
            stack.Push((index + 1, currentResult + numbers[index])); //+

            long digitsInB = 0;
            long tempB = numbers[index];
            while (tempB > 0)
            {
                digitsInB++;
                tempB /= 10;
            }

            //forConcat += numbers[index].ToString();
            //long asNum = StringToInt64(forConcat);
            //long asNum = ConcatenateNumbers(currentResult, numbers[index]);
            long asNum = currentResult * (long)Math.Pow(10, digitsInB) + numbers[index];
            stack.Push((index + 1, asNum)); //||
        }

        return false;
    }
}