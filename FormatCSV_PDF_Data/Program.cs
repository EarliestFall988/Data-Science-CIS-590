
using System.IO;
using System;
using System.Collections.Generic;

using FormatCSV_PDF_Data;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Start");

string? path = System.Environment.GetEnvironmentVariable("CSV_PATH", EnvironmentVariableTarget.User);

if (path == null)
    throw new NullReferenceException("cannot find the csv path");

if (path == string.Empty)
    throw new ArgumentException("csv path is empty");

if (!File.Exists(path))
    throw new FileNotFoundException("csv file not found");

string lines = File.ReadAllText(path);


bool literal = true;
List<Line> result = new List<Line>();

for (int i = 0; i < lines.Length; i++)
{
    // Console.WriteLine("i = " + i.ToString() + " " + lines[i].ToString());
    if (lines[i] == '"')
    {
        //Console.WriteLine("found a quote");
        literal = !literal;
    }
    else if (lines[i] == '\n' && literal)
    {

        //Console.WriteLine("found a newline");
        string newline = lines.Substring(0, i).Trim();
        //Console.WriteLine(newline);

        result.Add(new Line(newline, i));
        lines = lines.Substring(i);
        i = 0;
    }
    else if (lines[i] == ',' && literal)
    {
        //Console.WriteLine("found a comma");
        string newline = lines.Substring(0, i).Trim();
        //Console.WriteLine(newline);

        result.Add(new Line(newline, i));
        lines = lines.Substring(i);
        i = 0;
    }
    else if (i == lines.Length - 1)
    {
        //Console.WriteLine("found the end of the file");
        string newline = lines.Substring(0, i).Trim();
        //Console.WriteLine(newline);

        result.Add(new Line(newline, i));
        lines = lines.Substring(i);
        i = 0;
    }
}

int k = 0;
foreach (var x in result)
{
    Console.WriteLine("\t" + (k++).ToString());
    Console.WriteLine(x.RawLine);
}



Console.WriteLine("end");