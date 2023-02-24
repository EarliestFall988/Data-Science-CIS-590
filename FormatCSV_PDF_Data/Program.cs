
using System.IO;
using System;
using System.Collections.Generic;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Start");


string? path = System.Environment.GetEnvironmentVariable("CSV_PATH", EnvironmentVariableTarget.User);

if (path == null)
    throw new NullReferenceException("cannot find the csv path");

if (path == string.Empty)
    throw new ArgumentException("csv path is empty");

if(!File.Exists(path))
    throw new FileNotFoundException("csv file not found");

string[] lines = File.ReadAllLines(path);

Console.WriteLine("end");