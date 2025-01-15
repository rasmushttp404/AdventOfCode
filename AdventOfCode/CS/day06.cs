using System.Collections;

namespace AdventOfCode;

public class day06
{
    static void solve_6_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_6");

        string input = File.ReadAllText(filePath);
        //Load world as 2D char array
        //Keep track of guard, he's an automaton moving by simple rules
        //Movement is relative, so "forward" is relative to the guards rotation
        //No bounds checking per se - the guard eventually leaves by going off the map, at which point the program is meant to terminate
        //We need to count distinct positions... Easy: Hashset
        HashSet<int> visitedLocations = new HashSet<int>();
        (int, int)[] directions = new (int, int)[]
        {
            (-1, 0), // North
            (0, 1), // East
            (1, 0), // South
            (0, -1) // West
        };
        StringReader reader = new StringReader(input);
        (char[,] charMatrix, (int, int) caretPosition) = ConvertReaderToFixedCharWithCaret(reader, 130, 130);
        int currentDirection = 0;
        int steps = 0;
        visitedLocations.Add((caretPosition.Item1 + caretPosition.Item2 * 130)); //2D -> 1D unique position
        while (true)
        {
            //If guard is about to leave the map, terminate
            if (caretPosition.Item1 == 0 && directions[currentDirection].Item1 == -1) //X Left overflow
            {
                break;
            }
            else if (caretPosition.Item1 == 129 && directions[currentDirection].Item1 == 1) //X Right overflow
            {
                break;
            }
            else if (caretPosition.Item2 == 0 && directions[currentDirection].Item2 == -1) //Y Up overflow
            {
                break;
            }
            else if (caretPosition.Item2 == 129 && directions[currentDirection].Item2 == 1) //Y Down overflow
            {
                break;
            }

            //If the next item is a '#', rotate right 90 degrees
            if (charMatrix[caretPosition.Item1 + directions[currentDirection].Item1,
                    caretPosition.Item2 + directions[currentDirection].Item2] == '#')
            {
                currentDirection = (currentDirection + 1) & 3;
            }

            caretPosition.Item1 += directions[currentDirection].Item1;
            caretPosition.Item2 += directions[currentDirection].Item2;
            visitedLocations.Add((caretPosition.Item1 + caretPosition.Item2 * 130));
        }
        //ENDOF while(true)

        //Console.WriteLine("Uniquely visited locations: "+ visitedLocations.Count);
    }


    static void solve_6_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_6");

        string input = File.ReadAllText(filePath);
        HashSet<int> visitedLocations = new HashSet<int>();
        List<int> visitedList = new List<int>(); //For storing visitedLocation while maintaining order
        (int, int) caretInitPosition = (0, 0);
        int loops = 0; //For final 6_2 output

        (int, int)[] directions = new (int, int)[]
        {
            (-1, 0),
            (0, 1),
            (1, 0),
            (0, -1)
        };

        StringReader reader = new StringReader(input);
        (char[,] charMatrix, (int, int) caretPosition) = ConvertReaderToFixedCharWithCaret(reader, 130, 130);
        caretInitPosition.Item1 = caretPosition.Item1;
        caretInitPosition.Item2 = caretPosition.Item2;
        int currentDirection = 0;
        visitedLocations.Add((caretPosition.Item1 + caretPosition.Item2 * 130)); //2D -> 1D unique position
        while (true)
        {
            if (caretPosition.Item1 == 0 && directions[currentDirection].Item1 == -1) //X Left overflow
            {
                break;
            }
            else if (caretPosition.Item1 == 129 && directions[currentDirection].Item1 == 1) //X Right overflow
            {
                break;
            }
            else if (caretPosition.Item2 == 0 && directions[currentDirection].Item2 == -1) //Y Up overflow
            {
                break;
            }
            else if (caretPosition.Item2 == 129 && directions[currentDirection].Item2 == 1) //Y Down overflow
            {
                break;
            }

            if (charMatrix[caretPosition.Item1 + directions[currentDirection].Item1,
                    caretPosition.Item2 + directions[currentDirection].Item2] == '#')
            {
                currentDirection = (currentDirection + 1) & 3;
            }

            //charMatrix[caretPosition.Item1, caretPosition.Item2] = 'x';
            caretPosition.Item1 += directions[currentDirection].Item1;
            caretPosition.Item2 += directions[currentDirection].Item2;
            int toAdd = caretPosition.Item1 + caretPosition.Item2 * 130;
            if (visitedLocations.Add(toAdd))
            {
                visitedList.Add(toAdd);
            }
            //visitedLocations.Add((caretPosition.Item1+caretPosition.Item2*130));
        }
        //ENDOF while(true)

        //C# doesn't include a deep clone function??

        /*
        static char[,] CloneCharMatrix(char[,] original)
        {
            int rows = original.GetLength(0);
            int cols = original.GetLength(1);
            char[,] clone = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    clone[i, j] = original[i, j];
                }
            }

            return clone;
        }
        */
        char[,] CloneCharMatrix(char[,] original)
        {
            int rows = original.GetLength(0);
            int cols = original.GetLength(1);
            char[,] copy = new char[rows, cols];
            Array.Copy(original, copy, original.Length); // Copies the contents of the original array into the new array
            return copy;
        }

        static BitArray ConvertToBitArray(char[,] grid)
        {
            // Total number of bits (for a 130x130 grid)
            int totalBits = 130 * 130;
            BitArray bitArray = new BitArray(totalBits);

            // Fill the bit array with values from the grid
            for (int i = 0; i < 130; i++)
            {
                for (int j = 0; j < 130; j++)
                {
                    int index = i * 130 + j;
                    // If the character is a wall ('#'), set the bit to 1, otherwise 0
                    bitArray[index] = grid[i, j] == '#';
                }
            }

            return bitArray;
        }

        //Now, to solve part 2, we have to reconstruct the map again, but with a wall added at any of the locations of visitedLocations!
        //I did not set up the solution to part 1 to account for how part 2 might look, so adding recursion would be awkward
        //In any case, I'm more concerned about execution speed than readability.
        //We start at 1, skipping the initial position of the guard.
        //The task in question explicitly asks for this
        char[,] copy = CloneCharMatrix(charMatrix);

        for (int locs = 1; locs < visitedList.Count; locs++)
        {
            int index = visitedList[locs];
            (int col, int row) = (index / 130, index - (index / 130) * 130);
            copy[row, col] = '#';
            index = visitedList[locs - 1];
            (col, row) = (index / 130, index - (index / 130) * 130);
            copy[row, col] = '.'; //Clear old location

            loops += doesLoopSteps(copy, caretInitPosition);
        }

        //Console.WriteLine("Loops: "+ loops);
    }

    static int ToIndex(int row, int col)
    {
        return row * 130 + col;
    }


    static int doesLoop(char[,] daySixInput, (int, int) caretPosition)
    {
        int stepsTaken = 0;
        int[] localCaretPosition = new[] { caretPosition.Item1, caretPosition.Item2 };
        int currentDirection = 0;
        HashSet<int> uniqueLocationsHash = new HashSet<int>();
        int[] directionsRow = { -1, 0, 1, 0 }; // North, East, South, West (row deltas)
        int[] directionsCol = { 0, 1, 0, -1 }; // North, East, South, West (col deltas)
        int hash = localCaretPosition[0] +
                   (localCaretPosition[1] * 131); //Current direction is 0 in all execution paths
        //uniqueLocationsHash.Add(hash);
        while (true)
        {
            int nextRow = localCaretPosition[0] + directionsRow[currentDirection];
            int nextCol = localCaretPosition[1] + directionsCol[currentDirection];
            if (nextRow < 0 || nextRow >= 130 || nextCol < 0 || nextCol >= 130)
            {
                return 0;
            }

            if (daySixInput[nextRow, nextCol] == '#')
            {
                currentDirection++;
                if (currentDirection > 3)
                {
                    currentDirection = 0;
                }

                nextRow = localCaretPosition[0] + directionsRow[currentDirection];
                nextCol = localCaretPosition[1] + directionsCol[currentDirection];
                // If there's still a wall after turning, turn again
                if (daySixInput[nextRow, nextCol] == '#')
                {
                    currentDirection++;
                    if (currentDirection > 3)
                    {
                        currentDirection = 0;
                    }
                }
            }

            localCaretPosition[0] += directionsRow[currentDirection];
            localCaretPosition[1] += directionsCol[currentDirection];
            hash = localCaretPosition[0] + localCaretPosition[1] * 131 + currentDirection * 17161;
            if (!uniqueLocationsHash.Add(hash))
            {
                return 1; // Loop detected
            }
        }
    }

    static int doesLoopSteps(char[,] daySixInput, (int, int) caretPosition)
    {
        int stepsTaken = 0;
        int[] localCaretPosition = new[] { caretPosition.Item1, caretPosition.Item2 };
        int currentDirection = 0;
        int[] directionsRow = { -1, 0, 1, 0 }; // North, East, South, West (row deltas)
        int[] directionsCol = { 0, 1, 0, -1 }; // North, East, South, West (col deltas)
        while (stepsTaken < 8000)
        {
            stepsTaken++;
            int nextRow = localCaretPosition[0] + directionsRow[currentDirection];
            int nextCol = localCaretPosition[1] + directionsCol[currentDirection];
            if (nextRow < 0 || nextRow >= 130 || nextCol < 0 || nextCol >= 130)
            {
                return 0;
            }

            if (daySixInput[nextRow, nextCol] == '#')
            {
                currentDirection = (currentDirection + 1) & 3; // Using bitwise AND to simulate modulo 4
                nextRow = localCaretPosition[0] + directionsRow[currentDirection];
                nextCol = localCaretPosition[1] + directionsCol[currentDirection];
                // If there's still a wall after turning, turn again
                if (daySixInput[nextRow, nextCol] == '#')
                {
                    currentDirection = (currentDirection + 1) & 3; // Using bitwise AND to simulate modulo 4
                }
            }

            localCaretPosition[0] += directionsRow[currentDirection];
            localCaretPosition[1] += directionsCol[currentDirection];
        }

        return 1;
    }

    static int DoesLoopBit(BitArray daySixInput, (int, int) caretPosition)
    {
        (int, int) localCaretPosition = caretPosition;
        int currentDirection = 0;
        HashSet<int> uniqueLocationsHash = new HashSet<int>();
        (int, int)[] directions = new (int, int)[]
        {
            (-1, 0), // North
            (0, 1), // East
            (1, 0), // South
            (0, -1) // West
        };
        // Create an initial hash based on position and direction
        int hash = localCaretPosition.Item1 + (localCaretPosition.Item2 * 131) + (currentDirection * 17161);
        uniqueLocationsHash.Add(hash);
        while (true)
        {
            // Check if the current position is about to exit the bounds
            if ((localCaretPosition.Item1 == 0 && directions[currentDirection].Item1 == -1) ||
                (localCaretPosition.Item1 == 129 && directions[currentDirection].Item1 == 1) ||
                (localCaretPosition.Item2 == 0 && directions[currentDirection].Item2 == -1) ||
                (localCaretPosition.Item2 == 129 && directions[currentDirection].Item2 == 1))
            {
                return 0;
            }

            if (daySixInput[
                    ((localCaretPosition.Item1 + directions[currentDirection].Item1) * 130) + localCaretPosition.Item2 +
                    directions[currentDirection].Item2])
            {
                currentDirection = (currentDirection + 1) % 4;
                if (daySixInput[
                        ((localCaretPosition.Item1 + directions[currentDirection].Item1) * 130) +
                        localCaretPosition.Item2 + directions[currentDirection].Item2])
                {
                    currentDirection = (currentDirection + 1) % 4;
                }
            }

            // Move the caret
            localCaretPosition.Item1 += directions[currentDirection].Item1;
            localCaretPosition.Item2 += directions[currentDirection].Item2;

            // Recompute the hash with the updated position and direction
            hash = localCaretPosition.Item1 + (localCaretPosition.Item2 * 131) + (currentDirection * 17161);

            // Add to the set and check if it's already visited (loop detected)
            if (!uniqueLocationsHash.Add(hash))
            {
                return 1; // Loop detected
            }
        }
    }

    static (char[,], (int row, int col)) ConvertReaderToFixedCharWithCaret(TextReader reader, int rows, int cols)
    {
        char[,] charMatrix = new char[rows, cols];
        int currentRow = 0, currentCol = 0;
        (int row, int col) caretPosition = (0, 0);

        int ch;
        while ((ch = reader.Read()) != -1)
        {
            if (ch == '\r')
            {
                reader.Peek(); // Throw away the \n (Windows line breaks)
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
                if (ch != '\n') // Avoid placing characters when there's a newline
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