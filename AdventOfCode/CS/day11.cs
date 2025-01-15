namespace AdventOfCode;

public class day11
{


    public static void solve_11_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_11");
        string[] lines = File.ReadAllText(filePath).Split(" ");
        Dictionary<long, long> num_countnum = new Dictionary<long, long>();
        foreach (var line in lines)
        {
            long number = StringToInt64(line);
            num_countnum[number] = num_countnum.TryGetValue(number, out long count) ? count + 1 : 1;
        }
        int levelsToCompute = 25;
        Dictionary<long, long> nextDict = new Dictionary<long, long>();
        for (int x = 1; x <= levelsToCompute; x++)
        {
            nextDict.Clear();
            foreach (var entry in num_countnum)
            {
                long number = entry.Key;
                long count = entry.Value;
                if (number == 0)
                {
                    nextDict[1] = nextDict.TryGetValue(1, out _) ? nextDict[1] + count : count;
                }
                else
                {
                    int digitCount = (int)Math.Floor(Math.Log10(Math.Abs(number))) + 1;
                    if (digitCount % 2 == 0)
                    {
                        var newNumber = SplitNumber(number);
                        nextDict[newNumber.Item1] = nextDict.TryGetValue(newNumber.Item1, out _) ? nextDict[newNumber.Item1] + count : count;
                        nextDict[newNumber.Item2] = nextDict.TryGetValue(newNumber.Item2, out _) ? nextDict[newNumber.Item2] + count : count;
                    }
                    else
                    {
                        long newNumber = number * 2024;
                        nextDict[newNumber] = nextDict.TryGetValue(newNumber, out _) ? nextDict[newNumber] + count : count;
                    }
                }
            }
            var temp = num_countnum;
            num_countnum = nextDict;
            nextDict = temp;
            long totalStones = 0;
            foreach (var entry in num_countnum)
            {
                totalStones += entry.Value;
            }
            //Blink 25 = 235850
            //Blind 75 = 279903140844645 
            //Console.WriteLine("blink number " +x + " has "+ totalStones + " stones");
        }
    }
    public static void solve_11_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_11");
        string[] lines = File.ReadAllText(filePath).Split(" ");
        Dictionary<long, long> num_countnum = new Dictionary<long, long>();
        foreach (var line in lines)
        {
            long number = StringToInt64(line);
            num_countnum[number] = num_countnum.TryGetValue(number, out long count) ? count + 1 : 1;
        }
        int levelsToCompute = 75;
        Dictionary<long, long> nextDict = new Dictionary<long, long>();

        for (int x = 1; x <= levelsToCompute; x++)
        {
            nextDict.Clear();
            foreach (var entry in num_countnum)
            {
                long number = entry.Key;
                long count = entry.Value;
                if (number == 0)
                {
                    nextDict[1] = nextDict.TryGetValue(1, out _) ? nextDict[1] + count : count;
                }
                else
                {
                    int digitCount = (int)Math.Floor(Math.Log10(Math.Abs(number))) + 1;
                    if ((digitCount & 1) == 0)
                    {
                        var newNumber = SplitNumber(number);
                        nextDict[newNumber.Item1] = nextDict.TryGetValue(newNumber.Item1, out _) ? nextDict[newNumber.Item1] + count : count;
                        nextDict[newNumber.Item2] = nextDict.TryGetValue(newNumber.Item2, out _) ? nextDict[newNumber.Item2] + count : count;
                    }
                    else
                    {
                        long newNumber = (number << 3) * 253;
                        nextDict[newNumber] = nextDict.TryGetValue(newNumber, out _) ? nextDict[newNumber] + count : count;
                    }
                }
            }
            var temp = num_countnum;
            num_countnum = nextDict;
            nextDict = temp;

            long totalStones = 0;
            foreach (var entry in num_countnum)
            {
                totalStones += entry.Value;
            }
            //Blink 25 = 235850
            //Blind 75 = 279903140844645 
            //Console.WriteLine("blink number " +x + " has "+ totalStones + " stones");
        }
    }
    static long StringToInt64(string str)
    {
        long result = 0;
        foreach (char c in str)
        {
            result = result * 10 + (c - '0'); // Multiply by 10 and add the numeric value of the character
        }
        return result;
    }
    static (long firstHalf, long secondHalf) SplitNumber(long value)
    {
        int totalDigits = (int)Math.Floor(Math.Log10(value)) + 1; // Count the total digits
        int halfDigits = totalDigits / 2; // Calculate the midpoint of the digits

        long divisor = (long)Math.Pow(10, halfDigits); // Create a divisor (10^halfDigits)

        long secondHalf = value % divisor; // Get the lower half (remainder)
        long firstHalf = value / divisor;  // Get the upper half (quotient)

        return (firstHalf, secondHalf);
    }
    
    
    
    
    
    
    
    
    
}