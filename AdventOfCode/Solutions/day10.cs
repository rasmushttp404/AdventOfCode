namespace AdventOfCode;

public static class day10
{
    
    
    
    
    
    public static void solve_10_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_10");
        int inputSize = 52;
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        int[,] grid = ConvertReaderToFixedIntMatrix(reader, inputSize, inputSize);

        int totalReachableNines = 0;
        //printIntMatrix(grid); //Debug
        
        //Input: n^2 grid of char n in {'0'-'9'}
        //Output: Sum of paths from all zeros to all reachable nines
        //Reachability is the existence of a path such that all numbers are adjacent and strictly increasing by 1
        int[][] directions = new int[][]
        {
        new int[] { 0, 1 },  // Right
        new int[] { 1, 0 },  // Down
        new int[] { 0, -1 }, // Left
        new int[] { -1, 0 }  // Up
        };
        int totalPaths = 0;
        bool[,] visited = new bool[inputSize, inputSize];
        // Process each cell directly when we encounter a 0
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < inputSize; j++)
            {
                if (grid[i, j] == 0)
                {
                    Array.Clear(visited, 0, visited.Length);
                    int pathCount = 0;
                    var stack = new List<(int x, int y)>();
                    stack.Add((i, j));
                    visited[i, j] = true;
                    while (stack.Count > 0)
                    {
                        var (x, y) = stack[stack.Count - 1];
                        stack.RemoveAt(stack.Count - 1);
                        foreach (var dir in directions)
                        {
                            int newX = x + dir[0];
                            int newY = y + dir[1];
                            if (newX >= 0 && newX < inputSize && newY >= 0 && newY < inputSize)
                            {
                                if (!visited[newX, newY] && grid[newX, newY] == grid[x, y] + 1)
                                {
                                    visited[newX, newY] = true;
                                    if (grid[newX, newY] == 9)
                                    {
                                        pathCount++;
                                        
                                    }
                                    stack.Add((newX, newY));
                                }
                            }
                        }
                    }
                    totalPaths += pathCount;
                }
            }
        }
        //652
        //Console.WriteLine($"Total paths: {totalPaths}");
        //Console.WriteLine("Paths per `0`: " + string.Join(", ", pathCounts));
    }
    public static void solve_10_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_10");
        int inputSize = 52;
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        int[,] grid = ConvertReaderToFixedIntMatrix(reader, inputSize, inputSize);
        int[,] paddedGrid = padGrid(grid);
        int totalReachableNines = 0;
        
        List<(int, int)> allZeros = new List<(int, int)>();
        for (int x = 0; x < paddedGrid.GetLength(0); x++)
        {
            for (int y = 0; y < paddedGrid.GetLength(1); y++)
            {
                if (paddedGrid[x, y] == 0)
                {
                    allZeros.Add((x, y));
                }
            }
        }
        //1432
        //Console.WriteLine(CountAllPaths(paddedGrid, allZeros));
    }
    static int CountPaths(int[,] grid, int x, int y)
    {
        (int, int)[] directions = { (1, 0), (0, 1), (-1, 0), (0, -1) };
        if (grid[x, y] == 9)
        {
            return 1;
        }
        int paths = 0;
        foreach (var direction in directions)
        {
            int newX = x + direction.Item1;
            int newY = y + direction.Item2;
            if (grid[newX, newY] == grid[x, y] + 1)
            {
                paths += CountPaths(grid, newX, newY);
            }
        }
        return paths;
    }

    static int CountAllPaths(int[,] grid, List<(int, int)> startPositions)
    {
        int totalPaths = 0;
        foreach (var start in startPositions)
        {
            int startX = start.Item1;
            int startY = start.Item2;

            totalPaths += CountPaths(grid, startX, startY);
        }
        return totalPaths;
    }

    static int[,] padGrid(int[,] grid)
    {
        //Create a new grid with an additional border around the original grid
        int[,] newGrid = new int[grid.GetLength(0) + 2, grid.GetLength(1) + 2];
        for (int x = 0; x < newGrid.GetLength(0); x++)
        {
            newGrid[x, 0] = -1; //Left border
            newGrid[x, newGrid.GetLength(1) - 1] = -1; //Right border
        }
        for (int y = 0; y < newGrid.GetLength(1); y++)
        {
            newGrid[0, y] = -1; //Top border
            newGrid[newGrid.GetLength(0) - 1, y] = -1; //Bottom border
        }
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                newGrid[x + 1, y + 1] = grid[x, y];
            }
        }
        return newGrid;
    }
    static int[,] ConvertReaderToFixedIntMatrix(TextReader reader, int rows, int cols)
    {
        int[,] numMatrix = new int[rows, cols];
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
                    numMatrix[currentRow, currentCol] = ch-'0';
                    currentCol++;
                }
                if (currentRow == rows)
                {
                    break;
                }
            }
        }
        return numMatrix;
    }
    public static void printIntMatrix(int[,] grid)
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                Console.Write(grid[x, y]);
            }
            Console.WriteLine();
        }
    }
}

