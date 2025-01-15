// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode;


//solve_1_1();
//solve_1_2();
//solve_2_1();
//solve_2_2();
//solve_2_2_better();
//solve_2_2_bo();
//solve_3_1();
//solve_3_2();
//solve_4_1();
//solve_4_2();
//solve_4_2_robust();
//solve_4_1_alt();
//solve_5_1();
//solve_5_2();
//solve_6_1();
//solve_6_2();
//solve_7_1();
//solve_7_2();
//solve_3_1_control();
//solve_9_1();


//Stopwatch granularity is garbage
Stopwatch sw = Stopwatch.StartNew();
int iterations = 10000;
for (int k = 0; k < iterations; k++)
{
    day13.solve_13_1();
}
sw.Stop();
double totalTimeMilliseconds = sw.Elapsed.TotalMilliseconds;
double averageTimeMilliseconds = totalTimeMilliseconds / iterations;
Console.WriteLine($"Total Time: {totalTimeMilliseconds:F6} ms");
Console.WriteLine($"Average Time Per Iteration: {averageTimeMilliseconds:F9} ms");

/*
Time so far
TASK	TIME PER TASK		ITERATIONS	
1_1:	0.164776070 ms		10000
1_2:	0.143750710 ms		10000
2_1:	0.296751810 ms		10000
2_2:	0.341370210 ms		10000
3_1:	0.060835404 ms		100000
3_2:	0.063033042 ms		100000
4_1:	0.366270460 ms		10000
4_2:	0.249363960 ms		10000
5_1:	1.346352920 ms		10000
5_2:	3.248737500 ms		10000
6_1:	0.266881710 ms		10000
6_2:	68.146106000 ms		100
7_1:	3.338661600 ms		1000
7_2:	104.072909000 ms	100
8_1:	0.090068620 ms		10000
8_2:	0.205227170 ms		10000
9_1:	0.466468660 ms		10000
9_2:    
10_1:	0.236015200 ms		10000
10_2:	0.089563260 ms		10000
11_1:	0.216205450 ms		10000
11_2:	8.474134500 ms		1000
12_1:	0.677880960 ms		10000
12_2:	
13_1:	0.109765970 ms		10000
13_2:	0.115267250 ms		10000
*/



//Unused, I was just trying different things because I thought the overhead of the stopwatch class was time wasted on reading the text file
static string[] ReadFileMemoryMapped(string filePath)
{
    using var mmf = MemoryMappedFile.CreateFromFile(filePath, FileMode.Open, null, 0, MemoryMappedFileAccess.Read);
    using var stream = mmf.CreateViewStream(0, 0, MemoryMappedFileAccess.Read);
    using var reader = new StreamReader(stream);
    string content = reader.ReadToEnd();
    return content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
}
static string[] ReadFileBuffered(string filePath)
{
    using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, FileOptions.SequentialScan);
    using var reader = new StreamReader(fileStream);
    string content = reader.ReadToEnd();
    return content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
}
static string UseStreamReaderReadBlockWithSpan(string path)
{
    using var streamReader = new StreamReader(path);
    var buffer = new char[4096].AsSpan();
    int numberRead;
    var stringBuilder = new StringBuilder();
    while ((numberRead = streamReader.ReadBlock(buffer)) > 0)
    {
        stringBuilder.Append(buffer[..numberRead]);
    }
    return stringBuilder.ToString();
}













 



