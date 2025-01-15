using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode;

public class day03
{
//Used to verify the correct result. It works but I want to solve all 50 parts in less than 1 second total.
    public static void solve_3_1_control()
    {
        string filePath = Path.Combine("..", "..", "..", "input_3");

        string inputStr = File.ReadAllText(filePath);
        string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        MatchCollection matches = Regex.Matches(inputStr, pattern);
        int output = 0;
        foreach (Match match in matches)
        {
            string x = match.Groups[1].Value;
            string y = match.Groups[2].Value;
            output += int.Parse(x) * int.Parse(y);
            Console.WriteLine(x + "," + y);
        }

        Console.WriteLine("" + output);
    }

//Rolling out the regex pushdownautomata
    static void solve_3_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_3");

        byte[] bytes = File.ReadAllBytes(filePath);
        char[] chars = Encoding.UTF8.GetChars(bytes);
        List<char> firstNumber = new List<char>();
        List<char> secondNumber = new List<char>();
        int num_one = 0;
        int num_two = 0;
        long outputsum = 0;
        {
            int pointerOnePosition = -1; //It has to count up inside the loop at least once, but I want to start it at 0.
            //int pointerTwoPosition = 0;

            while (pointerOnePosition < chars.Length - 4)
            {
                while (true)
                {
                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != 'm')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != 'u')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != 'l')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != '(')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] < '0' || chars[pointerOnePosition] > '9')
                    {
                        break;
                    }

                    firstNumber.Add(chars[pointerOnePosition]);
                    pointerOnePosition++;
                    //Reversing conditions to positive
                    //Second number OR comma
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ',')
                    {
                        if (chars[pointerOnePosition] == ',')
                        {
                            goto commareached;
                        }
                        else
                        {
                            firstNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    //third number or comma
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ',')
                    {
                        if (chars[pointerOnePosition] == ',')
                        {
                            goto commareached;
                        }
                        else
                        {
                            firstNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    //Back to neg condition
                    if (chars[pointerOnePosition] != ',')
                    {
                        break;
                    }

                    commareached:
                    pointerOnePosition++;
                    //First char of second number
                    if (chars[pointerOnePosition] < '0' || chars[pointerOnePosition] > '9')
                    {
                        break;
                    }

                    secondNumber.Add(chars[pointerOnePosition]);
                    pointerOnePosition++;
                    //Pos conditioning again
                    //second char
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ')')
                    {
                        if (chars[pointerOnePosition] == ')')
                        {
                            goto endparenreached;
                        }
                        else
                        {
                            secondNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    //third char
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ')')
                    {
                        if (chars[pointerOnePosition] == ')')
                        {
                            goto endparenreached;
                        }
                        else
                        {
                            secondNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    endparenreached:
                    if (chars[pointerOnePosition] != ')')
                    {
                        break;
                    }
                    else
                    {
                        //Add it, then break;
                        num_one = 0;
                        num_two = 0;
                        foreach (char ch in firstNumber)
                        {
                            num_one = num_one * 10 + (ch - '0');
                        }

                        foreach (char ch in secondNumber)
                        {
                            num_two = num_two * 10 + (ch - '0');
                        }

                        //Console.WriteLine(num_one +","+num_two);
                        outputsum += num_one * num_two;
                        break;
                    }
                }

                firstNumber.Clear();
                secondNumber.Clear();
            }

            //Console.WriteLine("Sum: "+outputsum);

            //Correct output is 165225049
        }
    }


    static void solve_3_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_3");

        byte[] bytes = File.ReadAllBytes(filePath);
        char[] chars = Encoding.UTF8.GetChars(bytes);

        List<char> firstNumber = new List<char>();
        List<char> secondNumber = new List<char>();
        int num_one = 0;
        int num_two = 0;

        bool enabled = true;

        long outputsum = 0;
        {
            int pointerOnePosition =
                -1; //It has to count up inside the loop at least once, but I want to start it at 0.
            int pointerTwoPosition = 0;

            while (pointerOnePosition < chars.Length - 7)
            {
                while (true && pointerOnePosition < chars.Length - 7)
                {
                    pointerOnePosition++;

                    /*
                    char cha = 'å';
                    try
                    {
                        cha = chars[pointerOnePosition];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.Write(cha.ToString());
                    }
                    */

                    if (chars[pointerOnePosition] == 'd')
                    {
                        if (chars[pointerOnePosition + 1] == 'o')
                        {
                            if (chars[pointerOnePosition + 2] == '(')
                            {
                                if (chars[pointerOnePosition + 3] == ')')
                                {
                                    enabled = true;
                                    pointerOnePosition += 3;
                                    break;
                                }
                            }
                            else
                            {
                                //DO_
                                if (chars[pointerOnePosition + 2] == 'n')
                                {
                                    if (chars[pointerOnePosition + 3] == '\'')
                                    {
                                        if (chars[pointerOnePosition + 4] == 't')
                                        {
                                            if (chars[pointerOnePosition + 5] == '(')
                                            {
                                                if (chars[pointerOnePosition + 6] == ')')
                                                {
                                                    enabled = false;
                                                    pointerOnePosition += 6;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Console.WriteLine("entering loop at index: "+pointerOnePosition);

                    //Console.WriteLine("got char: "+chars[pointerOnePosition]);
                    if (chars[pointerOnePosition] != 'm')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != 'u')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != 'l')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] != '(')
                    {
                        break;
                    }

                    pointerOnePosition++;
                    if (chars[pointerOnePosition] < '0' || chars[pointerOnePosition] > '9')
                    {
                        break;
                    }

                    firstNumber.Add(chars[pointerOnePosition]);
                    pointerOnePosition++;
                    //Reversing conditions to positive
                    //Second number OR comma
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ',')
                    {
                        if (chars[pointerOnePosition] == ',')
                        {
                            goto commareached;
                        }
                        else
                        {
                            firstNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    //third number or comma
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ',')
                    {
                        if (chars[pointerOnePosition] == ',')
                        {
                            goto commareached;
                        }
                        else
                        {
                            firstNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    //Back to neg condition
                    if (chars[pointerOnePosition] != ',')
                    {
                        break;
                    }

                    commareached:
                    pointerOnePosition++;
                    //First char of second number
                    if (chars[pointerOnePosition] < '0' || chars[pointerOnePosition] > '9')
                    {
                        break;
                    }

                    secondNumber.Add(chars[pointerOnePosition]);
                    pointerOnePosition++;
                    //Pos conditioning again
                    //second char
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ')')
                    {
                        if (chars[pointerOnePosition] == ')')
                        {
                            goto endparenreached;
                        }
                        else
                        {
                            secondNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    //third char
                    if ((chars[pointerOnePosition] >= '0' && chars[pointerOnePosition] <= '9') ||
                        chars[pointerOnePosition] == ')')
                    {
                        if (chars[pointerOnePosition] == ')')
                        {
                            goto endparenreached;
                        }
                        else
                        {
                            secondNumber.Add(chars[pointerOnePosition]);
                        }
                    }
                    else
                    {
                        break;
                    }

                    pointerOnePosition++;
                    endparenreached:
                    if (chars[pointerOnePosition] != ')')
                    {
                        break;
                    }
                    else
                    {
                        //Console.WriteLine("valid numbers!");
                        //Add it, then break
                        num_one = 0;
                        num_two = 0;
                        foreach (char ch in firstNumber)
                        {
                            num_one = num_one * 10 + (ch - '0');
                        }

                        foreach (char ch in secondNumber)
                        {
                            num_two = num_two * 10 + (ch - '0');
                        }

                        //Console.WriteLine(num_one +","+num_two);
                        if (enabled)
                        {
                            outputsum += num_one * num_two;
                        }

                        break;
                    }

                    Console.Write("This should never happen!");
                }

                pointerTwoPosition = pointerOnePosition;
                firstNumber.Clear();
                secondNumber.Clear();
            }

            //Console.WriteLine("Sum: "+outputsum);
            //Correct output is 165225049
        }
    }
}