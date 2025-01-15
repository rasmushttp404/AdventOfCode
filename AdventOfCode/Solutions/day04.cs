namespace AdventOfCode;

public class day04
{
    static void solve_4_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_4");

        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        char[,] charMatrix = ConvertReaderToFixedCharMatrix(reader, 140, 140);
        int xmasesFound = 0;

        //Direction vectors
        int[] dx = { 0, 1, 0, -1, -1, 1, 1, -1 };
        int[] dy = { -1, 0, 1, 0, -1, -1, 1, 1 };

        char[] target = { 'X', 'M', 'A', 'S' };

        for (int i = 0; i < 140; i++)
        {
            for (int j = 0; j < 140; j++)
            {
                if (charMatrix[i, j] == 'X')
                {
                    for (int dir = 0; dir < 8; dir++)
                    {
                        int nx = i + dx[dir] * 3;
                        int ny = j + dy[dir] * 3;
                        if (nx < 0 || nx >= 140 || ny < 0 || ny >= 140) continue;

                        if (charMatrix[i + dx[dir], j + dy[dir]] == target[1] &&
                            charMatrix[i + 2 * dx[dir], j + 2 * dy[dir]] == target[2] &&
                            charMatrix[i + 3 * dx[dir], j + 3 * dy[dir]] == target[3])
                        {
                            xmasesFound++;
                        }
                    }
                }
            }
        }

        //Console.WriteLine("Found XMASes: " + xmasesFound);
    }


    static void solve_4_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_4");

        int x_mas = 0;
        string input = File.ReadAllText(filePath);
        StringReader reader = new StringReader(input);
        char[,] charMatrix = ConvertReaderToFixedCharMatrix(reader, 140, 140);

        for (int i = 1; i < 139; i++)
        {
            for (int j = 1; j < 139; j++)
            {
                if (charMatrix[i, j] == 'A')
                {
                    //Top left is M, top right is M
                    if (charMatrix[i - 1, j - 1] == 'M' && charMatrix[i + 1, j - 1] == 'M' &&
                        charMatrix[i - 1, j + 1] == 'S' && charMatrix[i + 1, j + 1] == 'S')
                    {
                        x_mas++;
                    }
                    //Top right is M, bottom right is M
                    else if (charMatrix[i + 1, j - 1] == 'M' && charMatrix[i + 1, j + 1] == 'M' &&
                             charMatrix[i - 1, j - 1] == 'S' && charMatrix[i - 1, j + 1] == 'S')
                    {
                        x_mas++;
                    }
                    //Bottom right is M, bottom left is M
                    else if (charMatrix[i + 1, j + 1] == 'M' && charMatrix[i - 1, j + 1] == 'M' &&
                             charMatrix[i - 1, j - 1] == 'S' && charMatrix[i + 1, j - 1] == 'S')
                    {
                        x_mas++;
                    }
                    //Bottom left is M, top left is M
                    else if (charMatrix[i - 1, j + 1] == 'M' && charMatrix[i - 1, j - 1] == 'M' &&
                             charMatrix[i + 1, j + 1] == 'S' && charMatrix[i + 1, j - 1] == 'S')
                    {
                        x_mas++;
                    }
                }
            }
        }
        //1854
        //Console.WriteLine("Found patterns: " + x_mas);
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