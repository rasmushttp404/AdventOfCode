namespace AdventOfCode;

public class day05
{
    static void solve_5_1()
    {
        string filePath = Path.Combine("..", "..", "..", "input_5");

        string[] lines = File.ReadAllLines(filePath);
        HashSet<(int, int)> rulesList = new HashSet<(int, int)>();
        int numRules = 1176;
        int biggestRulePresumed = 99;
        int validPages = 0;
        int middlePageSum = 0;
        for (int i = 0; i < numRules; i++)
        {
            string line = lines[i];
            int num1 = (line[0] - '0') * 10 + (line[1] - '0');
            int num2 = (line[3] - '0') * 10 + (line[4] - '0');
            rulesList.Add((num1, num2));
        }

        List<List<int>> pagesList = new List<List<int>>();
        /*
        for (int pages = numRules + 1; pages < lines.Length; pages++)
        {
            pagesList.Add(lines[pages].Split(",").Select(int.Parse).ToList());
        }
        */
        for (int pages = numRules + 1; pages < lines.Length; pages++)
        {
            List<int> page = new List<int>();
            string line = lines[pages];
            int n = 0;
            while (n < line.Length)
            {
                int num = (line[n] - '0') * 10 + (line[n + 1] - '0');
                page.Add(num);
                n += 3;
            }

            pagesList.Add(page);
        }

        foreach (var page in pagesList)
        {
            bool isValid = ValidatePage(page, rulesList);
            if (isValid)
            {
                middlePageSum += page[page.Count / 2];
            }
        }

        //Validate the entire sequence based on the transitive closure
        bool ValidatePage(List<int> page, HashSet<(int, int)> rules)
        {
            Dictionary<int, int> elementIndices = new Dictionary<int, int>();
            for (int i = 0; i < page.Count; i++)
            {
                elementIndices[page[i]] = i;
            }

            foreach (var rule in rules)
            {
                int X = rule.Item1;
                int Y = rule.Item2;
                // Check if both X and Y are present in the sequence
                if (elementIndices.ContainsKey(X) && elementIndices.ContainsKey(Y))
                {
                    if (elementIndices[X] > elementIndices[Y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        //Console.WriteLine("Valid page middle sum: " + middlePageSum); //Correct answer is: 4872
    }

    static void solve_5_2()
    {
        string filePath = Path.Combine("..", "..", "..", "input_5");

        string[] lines = File.ReadAllLines(filePath);
        HashSet<(int, int)> rulesList = new HashSet<(int, int)>();
        int numRules = 1176;
        int biggestRulePresumed = 99;
        int validPages = 0;
        int middlePageSum = 0;
        int newlyOrderecPageMiddleSum = 0;
        for (int i = 0; i < numRules; i++)
        {
            string line = lines[i];
            int num1 = (line[0] - '0') * 10 + (line[1] - '0');
            int num2 = (line[3] - '0') * 10 + (line[4] - '0');
            rulesList.Add((num1, num2));
        }

        List<List<int>> pagesList = new List<List<int>>();
        for (int pages = numRules + 1; pages < lines.Length; pages++)
        {
            List<int> page = new List<int>();
            string line = lines[pages];
            int n = 0;
            while (n < line.Length)
            {
                int num = (line[n] - '0') * 10 + (line[n + 1] - '0');
                page.Add(num);
                n += 3;
            }

            pagesList.Add(page);
        }

        foreach (var page in pagesList)
        {
            bool isValid = ValidatePage(page, rulesList);
        }

        //Validate the entire sequence based on the transitive closure
        bool ValidatePage(List<int> page, HashSet<(int, int)> rules)
        {
            Dictionary<int, int> elementIndices = new Dictionary<int, int>();
            for (int i = 0; i < page.Count; i++)
            {
                elementIndices[page[i]] = i;
            }

            foreach (var rule in rules)
            {
                int X = rule.Item1;
                int Y = rule.Item2;
                // Check if both X and Y are present in the sequence
                if (elementIndices.ContainsKey(X) && elementIndices.ContainsKey(Y))
                {
                    if (elementIndices[X] > elementIndices[Y])
                    {
                        newlyOrderecPageMiddleSum += ReorderInvalidPageAndGetMiddleSum(page, rulesList);
                        return false;
                    }
                }
            }

            return true;
        }

        //.WriteLine("Valid page middle sum: " + newlyOrderecPageMiddleSum);
        //Correct answers are:
        //PART 1: 4872
        //PART 2: 5564
    }

//Topological ordering / Topological sort (Kahns algorithm) O(V-E)
//Called when a list has incorrect order (Day 5 part 2)
    static int ReorderInvalidPageAndGetMiddleSum(List<int> page, HashSet<(int, int)> rules)
    {
        Dictionary<int, HashSet<int>> adjacencyList = new Dictionary<int, HashSet<int>>();
        Dictionary<int, int> inDegree = new Dictionary<int, int>();
        foreach (var rule in rules)
        {
            int fromNode = rule.Item1;
            int toNode = rule.Item2;
            if (page.Contains(fromNode) && page.Contains(toNode))
            {
                if (!adjacencyList.ContainsKey(fromNode))
                {
                    adjacencyList[fromNode] = new HashSet<int>();
                }

                adjacencyList[fromNode].Add(toNode);
                if (!inDegree.ContainsKey(fromNode)) inDegree[fromNode] = 0;
                if (!inDegree.ContainsKey(toNode)) inDegree[toNode] = 0;
                inDegree[toNode]++;
            }
        }

        Queue<int> queue = new Queue<int>();
        List<int> sortedSequence = new List<int>();
        foreach (var node in page)
        {
            if (inDegree[node] == 0)
            {
                queue.Enqueue(node);
            }
        }

        //Kahn’s algorithm (topological sort)
        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            sortedSequence.Add(current);
            if (adjacencyList.ContainsKey(current))
            {
                foreach (var neighbor in adjacencyList[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return sortedSequence[sortedSequence.Count / 2];
    }
}