namespace AdventOfCode;

public class day02
{
    static void solve_2_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_2");


        string[] reports = new string[1000];
        using (StreamReader sr = File.OpenText(filePath))
        {
            string s = String.Empty;
            int n = 0;
            while ((s = sr.ReadLine()) != null)
            {
                reports[n] = s;
                n++;
            }
        }


        int safeLevelsTotal = 0;

        static int StringToInt(string str)
        {
            int y = 0;
            for (int k = 0; k < str.Length; k++)
            {
                y = y * 10 + (str[k] - '0');
            }

            return y;
        }

        for (int i = 0; i < reports.Length; i++)
        {
            string[] levels = reports[i].Split(' ');
            int[] realLevels = new int[levels.Length];
            for (int j = 0; j < levels.Length; j++)
            {
                realLevels[j] = StringToInt(levels[j]);
            }

            bool allGoingUp = true;
            bool allGoingDown = true;
            bool levelIsSafe = true;

            for (int j = 0; j < realLevels.Length - 1; j++)
            {
                if (realLevels[j] < realLevels[j + 1])
                {
                    allGoingUp = false;
                    break;
                }
            }

            for (int j = 0; j < realLevels.Length - 1; j++)
            {
                if (realLevels[j] > realLevels[j + 1])
                {
                    allGoingDown = false;
                    break;
                }
            }

            if (allGoingUp || allGoingDown)
            {
                for (int j = 0; j < realLevels.Length - 1; j++)
                {
                    if ((realLevels[j] - realLevels[j + 1]) > 3)
                    {
                        levelIsSafe = false;
                        break;
                    }
                    else if ((realLevels[j] - realLevels[j + 1]) < -3)
                    {
                        levelIsSafe = false;
                        break;
                    }
                    else if ((realLevels[j] - realLevels[j + 1]) == 0)
                    {
                        levelIsSafe = false;
                        break;
                    }
                }

                if (levelIsSafe)
                {
                    safeLevelsTotal++;
                }
            }
        }

        //Console.WriteLine("Safe levels: "+safeLevelsTotal);
    }

    //Looks like a rolled-out DPA
    static void solve_2_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_2");

        string[] reports = new string[1000];
        using (StreamReader sr = File.OpenText(filePath))
        {
            string s = String.Empty;
            int n = 0;
            while ((s = sr.ReadLine()) != null)
            {
                reports[n] = s;
                n++;
            }
        }

        int safeLevelsTotal = 0;

        static int StringToInt(string str)
        {
            int y = 0;
            for (int k = 0; k < str.Length; k++)
            {
                y = y * 10 + (str[k] - '0');
            }

            return y;
        }

        for (int i = 0; i < reports.Length; i++)
        {
            string[] levels = reports[i].Split(' ');
            int[] realLevels = new int[levels.Length];
            for (int j = 0; j < levels.Length; j++)
            {
                realLevels[j] = StringToInt(levels[j]);
            }

            //bool allGoingUp = true;
            //bool allGoingDown = true;
            //bool levelIsSafe = true;


            int increases = 0;
            int decreases = 0;
            for (int j = 0; j < realLevels.Length - 1; j++)
            {
                if (realLevels[j] < realLevels[j + 1])
                {
                    increases++;
                }
                else if (realLevels[j] > realLevels[j + 1])
                {
                    decreases++;
                }
            }

            if (decreases > increases)
            {
                //Continue as DECREASE TYPE
                //Now, you can check for all errors at once
                //If there's no errors, pass it.
                //On error, enter another section of code which makes a new array without the faulty index
                //The new section must be perfect
                for (int k = 0; k < realLevels.Length - 1; k++)
                {
                    //Check all, should be TRUE
                    if (realLevels[k] > realLevels[k + 1] && realLevels[k] - realLevels[k + 1] <= 3)
                    {
                        //Still perfect
                        if (k == realLevels.Length - 2)
                        {
                            safeLevelsTotal++;
                            break;
                        }
                    }
                    else
                    {
                        //At least one flaw.
                        int[] dampenedLevel;
                        if (realLevels[k] < realLevels[k + 1] || realLevels[k] - realLevels[k + 1] > 3)
                        {
                            if (k + 2 >= realLevels.Length)
                            {
                                //at the end, check from right-to-left instead
                                dampenedLevel = RemoveAt(realLevels, k + 1);
                            }
                            else
                            {
                                if (realLevels[k + 1] > realLevels[k + 2])
                                {
                                    dampenedLevel = RemoveAt(realLevels, k);
                                }
                                else
                                {
                                    dampenedLevel = RemoveAt(realLevels, k + 1);
                                }
                            }

                            dampenedLevel = RemoveAt(realLevels, k + 1);
                        }
                        else
                        {
                            //Decrease, but too big
                            if (k + 2 >= realLevels.Length)
                            {
                                if (realLevels[k] - realLevels[k + 1] > 3)
                                {
                                    if (realLevels[k + 1] - realLevels[k + 2] < 4)
                                    {
                                        dampenedLevel = RemoveAt(realLevels, k);
                                    }
                                    else
                                    {
                                        dampenedLevel = RemoveAt(realLevels, k + 1);
                                    }
                                }
                            }
                            else
                            {
                                dampenedLevel = RemoveAt(realLevels, k);
                            }

                            dampenedLevel = RemoveAt(realLevels, k + 1);
                        }

                        safeLevelsTotal += isPerfect(dampenedLevel, true);
                        break;
                    }
                }
            }
            else
            {
                //Continue as INCREASE TYPE
                //Symmetric to above code, with a reverse of ordering
                for (int k = 0; k < realLevels.Length - 1; k++)
                {
                    //Check all, should be TRUE
                    if ((realLevels[k] < realLevels[k + 1]) && (realLevels[k] - realLevels[k + 1]) >= -3)
                    {
                        //Console.WriteLine("k, k+1 is " +realLevels[k] + ", " + realLevels[k + 1]);
                        //Console.WriteLine("differene is: " + (realLevels[k] - realLevels[k + 1]));
                        //Still perfect
                        if ((k == realLevels.Length - 2)) //Off-by-one because array?
                        {
                            for (int l = 0; l < realLevels.Length; l++)
                            {
                                //Console.Write(realLevels[l] + " ");
                            }

                            //Console.WriteLine("");
                            safeLevelsTotal++;
                            break;
                        }
                    }
                    else
                    {
                        //At least one flaw.

                        //int[] dampenedLevel = new int[realLevels.Length-1];

                        //Has two cases!
                        int[] dampenedLevel;
                        if (realLevels[k] < realLevels[k + 1] || realLevels[k] - realLevels[k + 1] < -3)
                        {
                            //Consider 45 50 53 54, 45 should be removed.
                            //Consider 45 50 46 47, 50 should be removed

                            //Consider 45 46 50, 50 should be removed
                            //Consider 45 46 50, 46 should be removed
                            if (k + 2 >= realLevels.Length)
                            {
                                //at the end, check from right-to-left instead
                                dampenedLevel = RemoveAt(realLevels, k + 1);
                            }
                            else
                            {
                                if (realLevels[k + 1] < realLevels[k + 2])
                                {
                                    dampenedLevel = RemoveAt(realLevels, k + 1);
                                }
                                else
                                {
                                    dampenedLevel = RemoveAt(realLevels, k + 1);
                                }
                            }
                        }
                        else
                        {
                            //Increase but too big
                            if (k + 2 >= realLevels.Length)
                            {
                                if (realLevels[k] - realLevels[k + 1] < -3)
                                {
                                    if (realLevels[k + 1] - realLevels[k + 2] < -3)
                                    {
                                        dampenedLevel = RemoveAt(realLevels, k + 1);
                                    }
                                    else
                                    {
                                        dampenedLevel = RemoveAt(realLevels, k);
                                    }
                                }
                            }
                            else
                            {
                                dampenedLevel = RemoveAt(realLevels, k + 1);
                            }

                            dampenedLevel = RemoveAt(realLevels, k + 1);
                        }

                        safeLevelsTotal += isPerfect(dampenedLevel, true);
                        break;
                    }
                }
            }
        }

        //Console.WriteLine("Safe levels: "+safeLevelsTotal);
    }

    static int[] RemoveAt(int[] array, int index)
    {
        /*
        Console.WriteLine("Complete input array:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + ",");
        }
        Console.WriteLine("");
        */

        int[] result = new int[array.Length - 1];
        int currIndex = 0;

        /*
        for (int i = 0; i < array.Length; i++)
        {
            if (i != index)
            {
                //Console.WriteLine("curr index:" +currIndex);
                //Console.WriteLine("result len" + result.Length);
                result[currIndex] = array[i];
            }
            else
            {
                currIndex++;
            }

        }
        */
        List<int> list = array.ToList();
        list.RemoveAt(index);
        int[] output = list.ToArray();

        /*
        Console.WriteLine("Complete output array:");
        for (int i = 0; i < output.Length; i++)
        {
            Console.Write(output[i]+",");
        }
        Console.WriteLine("");
        */

        return output;
    }

//Returns 1 if perfect, 0 otherwise
    static int isPerfect(int[] level, bool increasing)
    {
        if (increasing)
        {
            for (int i = 0; i < level.Length - 1; i++)
            {
                if (level[i] > level[(i + 1)])
                {
                    return 0;
                }
            }
        }
        else
        {
            for (int i = 0; i < level.Length - 1; i++)
            {
                if (level[i] < level[i + 1])
                {
                    return 0;
                }
            }
        }

        for (int i = 0; i < level.Length - 1; i++)
        {
            if (level[i] == level[i + 1])
            {
                return 0;
            }
            else if (level[i] - level[i + 1] > 3)
            {
                return 0;
            }
            else if (level[i] - level[i + 1] < -3)
            {
                return 0;
            }
        }

        return 1; //Lets keep at 0 for now to test
    }


    static void solve_2_2_better()
    {
        string filePath = Path.Combine("..", "..", "..", "input_2");

        string[] reports = new string[1000];
        using (StreamReader sr = File.OpenText(filePath))
        {
            string s = String.Empty;
            int n = 0;
            while ((s = sr.ReadLine()) != null)
            {
                reports[n] = s;
                n++;
            }
        }

        int safeLevelsTotal = 0;

        static int StringToInt(string str)
        {
            int y = 0;
            for (int k = 0; k < str.Length; k++)
            {
                y = y * 10 + (str[k] - '0');
            }

            return y;
        }

        for (int i = 0; i < reports.Length; i++)
        {
            string[] levels = reports[i].Split(' ');
            int[] realLevels = new int[levels.Length];
            for (int j = 0; j < levels.Length; j++)
            {
                realLevels[j] = StringToInt(levels[j]);
            }

            bool IsValid(int[] levels)
            {
                bool isIncreasing = true;
                bool isDecreasing = true;

                for (int i = 0; i < levels.Length - 1; i++)
                {
                    int diff = Math.Abs(levels[i] - levels[i + 1]);
                    if (diff > 3 || levels[i] == levels[i + 1]) return false;
                    if (levels[i] < levels[i + 1]) isDecreasing = false;
                    if (levels[i] > levels[i + 1]) isIncreasing = false;
                }

                return isIncreasing || isDecreasing;
            }

            int[] RemoveAt(int[] array, int index)
            {
                int[] result = new int[array.Length - 1];
                for (int i = 0, j = 0; i < array.Length; i++)
                {
                    if (i == index) continue;
                    result[j++] = array[i];
                }

                return result;
            }

            if (IsValid(realLevels))
            {
                safeLevelsTotal++;
            }
            else
            {
                bool foundValidAfterRemoval = false;
                for (int j = 0; j < realLevels.Length; j++)
                {
                    int[] dampenedLevel = RemoveAt(realLevels, j);
                    if (IsValid(dampenedLevel))
                    {
                        safeLevelsTotal++;
                        foundValidAfterRemoval = true;
                        break;
                    }
                }

                if (foundValidAfterRemoval)
                {
                    continue;
                }
            }
        }

        //Console.WriteLine("Safe levels: "+safeLevelsTotal);
    }
}