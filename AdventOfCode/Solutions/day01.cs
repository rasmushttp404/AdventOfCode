namespace AdventOfCode;

public class day01
{
    public static void solve_1_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_1");
        int[] first = new int[1000];
        int[] second = new int[1000];
        string content = File.ReadAllText(filePath);
        //string content = ReadFileBufferedTwo(filePath);
        //string content = UseStreamReaderReadBlockWithSpan(filePath);
        ReadOnlyMemory<char> memory = content.AsMemory();
        int inddd = 0;
        ReadOnlySpan<char> currSpan;


        static int SpanToInt(ReadOnlySpan<char> span) //Local func 
        {
            int y = 0;
            //y = y * 10 + (span[0] - -'0');
            y = y * 10 + (span[0] - '0');
            y = y * 10 + (span[1] - '0');
            y = y * 10 + (span[2] - '0');
            y = y * 10 + (span[3] - '0');
            y = y * 10 + (span[4] - '0');
            return y;
            /*
            int y = 0;
            for (int k = 0; k < span.Length; k++)
            {
                y = y * 10 + (span[k] - '0');
            }
            return y;
            */
        }

        /*
        //Final result should return 2192892
        for (int i = 0; i < 1000; i = i+2)
        {
            first[i] = int.Parse(memory.Slice(i*15, 5).Span);
            second[i] = int.Parse(memory.Slice(i*15+8, 5).Span);
            first[i+1] = int.Parse(memory.Slice(i*15+15, 5).Span);
            second[i+1] = int.Parse(memory.Slice(i*15+15+8, 5).Span);
        }
        */

        //for (int i = 0; i < 15000-15; i = i+15)
        for (int i = 0; i < 1000; i++)
        {
            currSpan = memory.Slice(i * 15, 5).Span;
            first[i] = SpanToInt(currSpan);
            currSpan = memory.Slice(i * 15 + 8, 5).Span;
            second[i] = SpanToInt(currSpan);
        }

        Array.Sort(first);
        Array.Sort(second);
        var sum = 0;
        for (int j = 0; j < 1000; j++)
        {
            sum += int.Abs(first[j] - second[j]);
        }
    }

    public static void solve_1_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_1");


        string content = File.ReadAllText(filePath);
        ReadOnlyMemory<char> memory = content.AsMemory();
        var multiset = new Dictionary<int, int>();

        static int SpanToInt(ReadOnlySpan<char> span)
        {
            return (span[0] - '0') * 10000 + (span[1] - '0') * 1000 + (span[2] - '0') * 100 + (span[3] - '0') * 10 + (span[4] - '0');
        }

        for (int i = 0; i < 1000; i++)
        {
            int key = SpanToInt(memory.Slice(i * 15 + 8, 5).Span);
            if (multiset.TryGetValue(key, out int count))
            {
                multiset[key] = count + 1;
            }
            else
            {
                multiset[key] = 1;
            }
        }

        int sum = 0;
        for (int i = 0; i < 1000; i++)
        {
            int key = SpanToInt(memory.Slice(i * 15, 5).Span);
            if (multiset.TryGetValue(key, out int count))
            {
                sum += key * count;
            }
        }
        //Console.WriteLine("To get answer " + sum);
    }

    //Old

    //Console.WriteLine("sofarsogood "+i);
    //Console.WriteLine("memory length is "+ content.Length);

    /*
    first[inddd] = int.Parse(memory.Slice(i, 5).Span);
    second[inddd] = int.Parse(memory.Slice(i+8, 5).Span);
    first[inddd+1] = int.Parse(memory.Slice(i+15, 5).Span);
    second[inddd+1] = int.Parse(memory.Slice(i+23, 5).Span);
    */
    //Console.WriteLine("First: " + int.Parse(memory.Slice(i+8, 5).Span));
    //Console.WriteLine("second: "+ spanToInt(memory.Slice(i+8, 5).Span));
}