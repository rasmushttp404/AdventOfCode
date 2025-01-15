namespace AdventOfCode;

public class day13
{
    

    public static void solve_13_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_13");
        //string filePath = Path.Combine("..", "..", "..", "input_13_small");
        int vals = 6;
        int blocks = 320;
        //int blocks = 4; //For small text input only
        ReadOnlySpan<Char> input = File.ReadAllText(filePath).AsSpan();
        int inputIntsAmt = vals * blocks;
        int[] allNums = ExtractNumbers(input, inputIntsAmt);
        int tokensNeeded = 0;
        
        //Console.WriteLine("nums size: " + allNums.Length);
        
        for (int i = 0; i < blocks; i++)
        {
            var resultsCurrBlock = SolveLinearEquationsCramer(allNums[i*6+0], allNums[i*6+1], allNums[i*6+2], allNums[i*6+3], allNums[i*6+4], allNums[i*6+5]);
            tokensNeeded += 3 * resultsCurrBlock.Item1;
            tokensNeeded += resultsCurrBlock.Item2;
            //Console.WriteLine("res: "+resultsCurrBlock);
        }
        //29711
        //Console.WriteLine("tokens required: "+tokensNeeded);
        

    }
    public static void solve_13_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_13");
        //string filePath = Path.Combine("..", "..", "..", "input_13_small");
        int vals = 6;
        int blocks = 320;
        //int blocks = 4; //For small text input only
        ReadOnlySpan<Char> input = File.ReadAllText(filePath).AsSpan();
        int inputIntsAmt = vals * blocks;
        int[] allNums = ExtractNumbers(input, inputIntsAmt);
        long tokensNeeded = 0;
        
        //Console.WriteLine("nums size: " + allNums.Length);
        
        for (int i = 0; i < blocks; i++)
        {
            var resultsCurrBlock = SolveLinearEquationsCramerLong(allNums[i*6+0], allNums[i*6+1], allNums[i*6+2], allNums[i*6+3], allNums[i*6+4], allNums[i*6+5]);
            tokensNeeded += 3 * resultsCurrBlock.Item1;
            tokensNeeded += resultsCurrBlock.Item2;
            //Console.WriteLine("res: "+resultsCurrBlock);
        }
        //94955433618919
        //Console.WriteLine("tokens required: "+tokensNeeded);
    }
    
    
    static int StringToInt(string str)
    {
        int y = 0;
        for (int k = 0; k < str.Length; k++)
        {
            y = y * 10 + (str[k] - '0');
        }
        return y;
    }
    
    public static int[] ExtractNumbers(ReadOnlySpan<char> span, int numbersAmount)
    {
        int[] numbers = new int[numbersAmount];
        int currentNumber = 0;
        bool inNumber = false;
        int numberIndex = 0;
        for (int i = 0; i < span.Length; i++)
        {
            char c = span[i];
            if (char.IsDigit(c))
            {
                if (!inNumber)
                {
                    inNumber = true;
                    currentNumber = 0;
                }
                currentNumber = currentNumber * 10 + (c - '0');
            }
            else
            {
                if (inNumber)
                {
                    if (numberIndex < numbersAmount)
                    {
                        numbers[numberIndex++] = currentNumber;
                    }
                    inNumber = false;
                }
            }
        }
        if (inNumber && numberIndex < numbersAmount)
        {
            numbers[numberIndex++] = currentNumber;
        }
        return numbers;
    }

    public static (int x, int y) SolveLinearEquationsCramer(int a1, int a2, int b1, int b2, int c1, int c2)
    {
        int detA = a1 * b2 - a2 * b1;
        int detA_x = c1 * b2 - c2 * b1;
        int detA_y = a1 * c2 - a2 * c1;
        if (detA_x % detA != 0 || detA_y % detA != 0)
        {
            return (0, 0);
        }
        int x = detA_x / detA;
        int y = detA_y / detA;

        if (x > 100 || y > 100)
        {
            return (0, 0);
        }
        return (x, y);
    }
    public static (long x, long y) SolveLinearEquationsCramerLong(long a1, long a2, long b1, long b2, long c1, long c2)
    {
        c1 += 10000000000000;
        c2 += 10000000000000;
        //Console.WriteLine("input numbers are: " + a1 + ", " + b1 + ", " + c1 + ", " + a2 + ", " + b2 + ", " + c2);
        long detA = a1 * b2 - a2 * b1;
        long detA_x = c1 * b2 - c2 * b1;
        long detA_y = a1 * c2 - a2 * c1;
        if (detA_x % detA != 0 || detA_y % detA != 0)
        {
            return (0, 0);
        }
        long x = detA_x / detA;
        long y = detA_y / detA;
        return (x, y);
    }
    
    
}