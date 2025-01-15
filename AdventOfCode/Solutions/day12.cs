namespace AdventOfCode;

public class day12
{


    public static void solve_12_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_12");
        int inputWidth = 140;
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        char[,] grid = ConvertReaderToFixedCharMatrix(reader, inputWidth, inputWidth);
        (int x, int y)[] Directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };
        bool[,] visited = new bool[inputWidth, inputWidth];
        List<string> results = new List<string>();
        Dictionary<char, int> regionCounts = new Dictionary<char, int>();
        int output = 0;
        int totalRegion = 0; //For debug. Should be 140^2=19600
        void DFS(int x, int y, char regionChar)
        {
            Stack<(int x, int y)> stack = new Stack<(int, int)>();
            stack.Push((x, y));
            visited[x, y] = true;
            int perimeter = 0;
            int count = 0;
            while (stack.Count > 0)
            {
                var (currX, currY) = stack.Pop();
                count++;
                
                foreach (var direction in Directions)
                {
                    int dx = direction.x;
                    int dy = direction.y;
                    int newX = currX + dx;
                    int newY = currY + dy;
                    if (newX < 0 || newX >= inputWidth || newY < 0 || newY >= inputWidth || grid[newX, newY] != regionChar)
                    {
                        perimeter++;
                    }
                    else if (!visited[newX, newY])
                    {
                        visited[newX, newY] = true;
                        stack.Push((newX, newY));
                    }
                }
            }
            output += perimeter * count;
            regionCounts[regionChar] = count;
        }
        for (int i = 0; i < inputWidth; i++)
        {
            for (int j = 0; j < inputWidth; j++)
            {
                if (!visited[i, j])
                {
                    char regionChar = grid[i, j];
                    DFS(i, j, regionChar);
                }
            }
        }
        foreach (string res in results)
        {
           Console.WriteLine(res);
        }
        //1421958
        //Console.WriteLine("output: "+output);
        //Console.WriteLine("totalRegion : "+totalRegion);
    }


    public static void solve_12_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_12_small");
        int inputWidth = 5;
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        char[,] grid = ConvertReaderToFixedCharMatrix(reader, inputWidth, inputWidth);
        (int x, int y)[] Directions = { (-1, 0), (1, 0), (0, -1), (0, 1) };
        int discountCost = 0;
        bool[,] visited = new bool[inputWidth, inputWidth];
        List<string> results = new List<string>();
        Dictionary<char, int> regionCounts = new Dictionary<char, int>();
        int output = 0;
        int totalSides = 0;
        int totalLetters = 0;
        int totalRegion = 0; // For debug. Should be 140^2 = 19600
        void DFS(int x, int y, char regionChar)
        {
        Stack<(int x, int y)> stack = new Stack<(int, int)>();
        stack.Push((x, y));
        visited[x, y] = true;
        int sides = 0; // To count the sides (not perimeter)
        int count = 0; // Count of cells in this region
        int totalCounts = 0;
        
        while (stack.Count > 0)
        {
            var (currX, currY) = stack.Pop();
            count++;
            foreach (var direction in Directions)
            {
                int dx = direction.x;
                int dy = direction.y;
                int newX = currX + dx;
                int newY = currY + dy;
                if (newX < 0 || newX >= inputWidth || newY < 0 || newY >= inputWidth || grid[newX, newY] != regionChar)
                {
                    sides++;
                }
                else if (!visited[newX, newY])
                {
                    visited[newX, newY] = true;
                    stack.Push((newX, newY));
                }
            }
        }
        output += sides;
        regionCounts[regionChar] = count; 
    }
    for (int i = 0; i < inputWidth; i++)
    {
        for (int j = 0; j < inputWidth; j++)
        {
            if (!visited[i, j])
            {
                char regionChar = grid[i, j];
                DFS(i, j, regionChar);
            }
        }
    }
    foreach (var entry in regionCounts)
    {
        Console.WriteLine($"Region {entry.Key}: {entry.Value} cells");
    }
        //2032096 is too high
        //Console.WriteLine("Discount Costs are " + discountCost);
    }
    static char[,] ConvertReaderToFixedCharMatrix(TextReader reader, int rows, int cols)
    {
        char[,] charMatrix = new char[rows, cols];
        int currentRow = 0, currentCol = 0;
        int ch;
        while ((ch = reader.Read()) != -1)
        {
            if (ch == '\r')
            {
                reader.Peek(); //Throw away the \n. Blame Windows file formatting
                currentRow++;
                currentCol = 0;
            }
            else
            {
                if (ch != '\n')
                {
                    charMatrix[currentRow, currentCol] = (char)ch;
                    currentCol++;
                }
                if (currentRow == rows)
                {
                    break;
                }
            }
        }
        return charMatrix;
    }
}