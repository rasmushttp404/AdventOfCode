namespace AdventOfCode;

public class day08
{
    static void solve_8_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_8");
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        char[,] charMatrix = ConvertReaderToFixedCharMatrix(reader, 50, 50);
        int rows = 50, cols = 50;
        int sum = 0;
        // Collect character locations
        var occurrencesByChar = new Dictionary<char, List<(int x, int y)>>();

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                char ch = charMatrix[x, y];
                if (ch != '.')
                {
                    if (!occurrencesByChar.TryGetValue(ch, out var list))
                    {
                        list = new List<(int, int)>();
                        occurrencesByChar[ch] = list;
                    }

                    list.Add((x, y));
                }
            }
        }

        bool[,] visited = new bool[rows, cols];

        // Process each character and its positions
        foreach (var kvp in occurrencesByChar)
        {
            var locations = kvp.Value;
            int count = locations.Count;

            if (count < 2) continue; // Skip characters with fewer than 2 occurrences

            // Pre-calculate all deltas and check for new positions
            for (int i = 0; i < count; i++)
            {
                var (x1, y1) = locations[i];

                // Iterate over the rest of the locations to calculate deltas
                for (int j = i + 1; j < count; j++)
                {
                    var (x2, y2) = locations[j];
                    int deltaX = x2 - x1;
                    int deltaY = y2 - y1;

                    // Extrapolate new positions based on the delta vector
                    int newX1 = x1 - deltaX, newY1 = y1 - deltaY;
                    int newX2 = x2 + deltaX, newY2 = y2 + deltaY;

                    // Check new position (newX1, newY1)
                    if (newX1 >= 0 && newX1 < rows && newY1 >= 0 && newY1 < cols && !visited[newX1, newY1])
                    {
                        visited[newX1, newY1] = true;
                        sum++;
                    }

                    // Check new position (newX2, newY2)
                    if (newX2 >= 0 && newX2 < rows && newY2 >= 0 && newY2 < cols && !visited[newX2, newY2])
                    {
                        visited[newX2, newY2] = true;
                        sum++;
                    }
                }
            }
        }
        //381
        //Console.WriteLine("sum is: "+sum);
    }

//TODO: Optimize. It's very slow. Just check how many times each delta(pair) fits within a n^2 grid.
    static void solve_8_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_8");
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        int rows = 50, cols = 50;
        char[,] charMatrix = ConvertReaderToFixedCharMatrix(reader, rows, cols);

        int sum = 0;

        //Get all pairs
        var occurrencesByChar = new Dictionary<char, List<(int x, int y)>>();
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                char ch = charMatrix[x, y];
                if (ch >= 0x30)
                {
                    if (!occurrencesByChar.ContainsKey(ch))
                    {
                        occurrencesByChar[ch] = new List<(int, int)>();
                    }

                    occurrencesByChar[ch].Add((x, y));
                }
            }
        }

        var visitedPositions = new HashSet<(int, int)>(); // Track visited positions
        foreach (var kvp in occurrencesByChar)
        {
            var locations = kvp.Value;
            for (int i = 0; i < locations.Count; i++)
            {
                for (int j = i + 1; j < locations.Count; j++)
                {
                    var (x1, y1) = locations[i];
                    var (x2, y2) = locations[j];
                    int deltaX = x2 - x1;
                    int deltaY = y2 - y1;

                    // Include the original positions first
                    if (!visitedPositions.Contains((x1, y1)))
                    {
                        visitedPositions.Add((x1, y1));
                        //charMatrix[x1, y1] = '#';  // Mark as visited
                        sum++; // Increment sum for valid new position
                    }

                    if (!visitedPositions.Contains((x2, y2)))
                    {
                        visitedPositions.Add((x2, y2));
                        //charMatrix[x2, y2] = '#';  // Mark as visited
                        sum++; // Increment sum for valid new position
                    }

                    // Extrapolate (x1, y1) -> (x2, y2) in both directions until out of bounds
                    int newX1 = x1, newY1 = y1;
                    while (true)
                    {
                        newX1 -= deltaX;
                        newY1 -= deltaY;
                        // Check if new position is within bounds
                        if (newX1 < 0 || newX1 >= rows || newY1 < 0 || newY1 >= cols)
                            break; // Exit loop if out of bounds
                        if (!visitedPositions.Contains((newX1, newY1)))
                        {
                            visitedPositions.Add((newX1, newY1));
                            //charMatrix[newX1, newY1] = '#';  // Mark as visited
                            sum++; // Increment sum for valid new position
                        }
                    }

                    int newX2 = x2, newY2 = y2;
                    while (true)
                    {
                        newX2 += deltaX;
                        newY2 += deltaY;
                        // Check if new position is within bounds
                        if (newX2 < 0 || newX2 >= rows || newY2 < 0 || newY2 >= cols)
                            break; // Exit loop if out of bounds
                        if (!visitedPositions.Contains((newX2, newY2)))
                        {
                            visitedPositions.Add((newX2, newY2));
                            //charMatrix[newX2, newY2] = '#';  // Mark as visited
                            sum++; // Increment sum for valid new position
                        }
                    }
                }
            }
        }
        /*
        Console.WriteLine();
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Console.Write(charMatrix[x, y]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        */


        //1184
        //Console.WriteLine("sum is: "+sum);
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
    
    static (char[,], (int, int)) ConvertReaderToFixedCharWithCaret(TextReader reader, int rows, int cols)
    {
        char[,] charMatrix = new char[rows, cols];
        int currentRow = 0, currentCol = 0;
        (int, int) caretPosition = new ValueTuple<int, int>(0, 0);

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
                if (ch == '^')
                {
                    caretPosition = (currentRow, currentCol);
                }
                // Assign character to the matrix
                if (ch != '\n')
                {
                    charMatrix[currentRow, currentCol] = (char)ch;
                    currentCol++;
                }
                // Stop when the array is fully populated
                if (currentRow == rows)
                {
                    break;
                }
            }
        }
        return (charMatrix, caretPosition);
    }
    
}