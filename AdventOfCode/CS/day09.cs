namespace AdventOfCode;

public class day09
{
    
    
    static void solve_9_1()
{
    string filePath = Path.Combine("..", "..", "..", "input_9");
    
    string input = File.ReadAllText(filePath);
    int bitsize = 0;
    for (int i = 0; i < input.Length; i++) // < input.Length
    {
        bitsize += input[i] - '0';
    }
    int[] completeArray = new int[bitsize];
    int id = 1;
    int count = 0;
    for (int i = 0; i < input.Length; i++) // < input.Length
    {
        int diff = input[i] - '0';
        if ((i & 1) == 0)
        {
            for (int j = 0; j < diff; j++)
            {
                completeArray[count] = id;
                count++;
            }
            id++;
        }
        else
        {
            count += diff;
        }
    }
    /*
    Console.WriteLine("fragmented array");
    for (int i = 0; i < completeArray.Length; i++)
    {
        Console.Write(" "+completeArray[i]+" ");
    }
    Console.WriteLine("");
    */
    
    //Scan from left to right, moving the numbers
    
    int pointerRightSide = bitsize-1;
    int pointerLeftSide = 0;
    int removed = 0;
    for (pointerLeftSide = 0; pointerLeftSide < bitsize; pointerLeftSide++)
    {
        if (completeArray[pointerLeftSide] < 1)
        {
            //Empty spot, move from the right
            while (completeArray[pointerRightSide] < 1)
            {
                pointerRightSide--;
            }
            //No need to set the new number to 0, we just discard it when we sum
            completeArray[pointerLeftSide] = completeArray[pointerRightSide]; 
            pointerRightSide--;
            removed++;
        }
    }
    
    
   
    
    
    /*
    Console.WriteLine("sorted array");
    for (int i = 0; i < completeArray.Length-removed; i++)
    {
        Console.Write(" "+completeArray[i]+" ");
    }
    */

    long sum = 0;
    for (long m = 0; m < bitsize - removed; m++)
    {
        sum += (completeArray[m]-1) * m;
    }
    Console.WriteLine("sum: "+sum);

}

static void solve_9_1_fast()
{
    string filePath = Path.Combine("..", "..", "..", "input_9");
    string input = File.ReadAllText(filePath);
    int bitsize = 0, skippedPos = 0;
    for (int i = 0; i < input.Length; i++)
    {
        int value = input[i] - '0';
        bitsize += value;
        if ((i & 1) != 0)
        {
            skippedPos += value;
        }
    }
    int[] completeArray = new int[bitsize];
    int id = 1, count = 0;
    for (int i = 0; i < input.Length; i++)
    {
        int diff = input[i] - '0';
        if ((i & 1) == 0)
        {
            for (int j = 0; j < diff; j++)
            {
                completeArray[count++] = id;
            }
            id++;
        }
        else
        {
            count += diff;
        }
    }
    long sum = 0;
    int rightPointer = bitsize - 1;
    for (int m = 0; m < bitsize - skippedPos; m++)
    {
        int currentElement = completeArray[m];
        
        if (currentElement > 0)
        {
            sum += (currentElement - 1) * m; // Sum for non-zero elements
        }
        else
        {
            // Find the next non-zero element in one pass from the right side
            while (completeArray[rightPointer] == 0 && rightPointer > m)
            {
                rightPointer--;
            }
            sum += (completeArray[rightPointer] - 1) * m;
            rightPointer--; // Move the pointer left after using it
        }
    }

    //Console.WriteLine("sum: " + sum);
}
    
}