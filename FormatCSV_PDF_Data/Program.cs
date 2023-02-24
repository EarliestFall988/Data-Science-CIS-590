
using System.Diagnostics;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

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
List<string> result = new List<string>();
List<DayOfMonth> daysOfMonth = new List<DayOfMonth>();
int someMonth = 1;
int someYear = 2021;

for (int i = 0; i < lines.Length; i++)
{
    // Console.WriteLine("i = " + i.ToString() + " " + lines[i].ToString());
    if (lines[i] == '"')
    {
        //Console.WriteLine("found a quote");
        literal = !literal;

        // lines = lines.Remove(i, 1);
    }
    else if (lines[i] == '\n' && literal)
    {
        // lines = lines.Remove(i, 1);
        //Console.WriteLine("found a newline");
        string newline = lines.Substring(0, i).Trim().Trim('\"').Trim(',');
        //Console.WriteLine(newline);
        newline = newline.Replace("\"\"", "\"");
        newline = newline.Replace("\n", " ");

        result.Add(newline);
        lines = lines.Substring(i);


        i = 0;
    }
    else if (lines[i] == ',' && literal)
    {

        string newline = lines.Substring(0, i).Trim().Trim(',').Trim('\"');

        newline = newline.Replace("\"\"", "\"");
        newline = newline.Replace("\n", " ");

        result.Add(newline);
        lines = lines.Substring(i);

        i = 0;
    }
    else if (i == lines.Length - 1)
    {
        //Console.WriteLine("found the end of the file");
        string newline = lines.Substring(0, i).Trim();
        //Console.WriteLine(newline);

        result.Add(newline);
        lines = lines.Substring(i);
        i = 0;
    }
}

for (int i = 0; i < result.Count; i++)
{
    string events = result[i];

    if (events.Length == 0)
    {
        result.RemoveAt(i);
        i--;
    }
    else
    {

        if (events.Length > 0 && Int32.TryParse(events.Trim().Split(" ")[0], out var day))
        {
            //Console.WriteLine(day);
        }
        else if (i > 0)
        {
            // Console.WriteLine(events);
            string actualDay = result[i - 1];

            string correction = actualDay + " " + events;
            //Console.WriteLine(correction);

            result.RemoveAt(i - 1);
            result.RemoveAt(i - 1);

            if (i < result.Count - 2)
            {
                result.Insert(i - 1, correction);
            }
            else
            {
                result.Add(actualDay + " " + events);
            }
        }

        List<Event> eventList = new List<Event>();
        string someDay = events.Split(" ")[0];

        int theDay = 0;

        if (Int32.TryParse(someDay, out var d))
        {
            theDay = d;
        }


        string timeRegex = @"([0-2]?[0-9]:[0-9][0-9](pm|am) - [0-2]?[0-9]:[0-9][0-9](pm|am))";

        Regex r = new Regex(timeRegex, RegexOptions.IgnoreCase);

        MatchCollection mc = r.Matches(events);

        var moreData = r.Split(events);

        Console.WriteLine(string.Join("|", moreData));

        int iterator = 4;
        Console.WriteLine("mc count = " + mc.Count.ToString());
        foreach (Match m in mc)
        {
            string[] times = m.Value.Split(" - ");

            string start = times[0];
            string end = times[1];

            string[] startTimes = start.Split(":");
            string[] endTimes = end.Split(":");

            int startHour = 0;
            int startMinute = 0;
            int endHour = 0;
            int endMinute = 0;

            if (Int32.TryParse(startTimes[0], out var sh))
            {
                startHour = sh;
            }

            if (Int32.TryParse(startTimes[1].Substring(0, 2), out var sm))
            {
                startMinute = sm;
            }

            if (Int32.TryParse(endTimes[0], out var eh))
            {
                endHour = eh;
            }

            if (Int32.TryParse(endTimes[1].Substring(0, 2), out var em))
            {
                endMinute = em;
            }

            string startAMPM = startTimes[1].Substring(2, 2);
            string endAMPM = endTimes[1].Substring(2, 2);

            if (startAMPM == "pm" && startHour != 12)
            {
                startHour += 12;
            }

            if (endAMPM == "pm" && endHour != 12)
            {
                endHour += 12;
            }

            if (startAMPM == "am" && startHour == 12)
            {
                startHour = 0;
            }

            if (endAMPM == "am" && endHour == 12)
            {
                endHour = 0;
            }

            var data = moreData[iterator].Trim();

            var sp = data.Split(":");

            string title = sp[0];

            string location = "";
            if (sp.Length > 1)
                location = sp[1];

            string headCount = "";
            if (sp.Length > 2)
                headCount = sp[2];

            Console.WriteLine("Title: " + title);
            Console.WriteLine("Location: " + location);
            Console.WriteLine("Head Count: " + headCount);

            Event newEvent = new Event()
            {
                StartTime = new TimeSpan(startHour, startMinute, 0),
                EndTime = new TimeSpan(endHour, endMinute, 0),
                description = title,
                HeadCount = headCount,
                Name = location,
            };

            eventList.Add(newEvent);

            iterator += 4;
        }

        DayOfMonth newDay = new DayOfMonth()
        {
            Day = theDay,
            Month = someMonth,
            Year = someYear,
            Events = eventList,
        };

        daysOfMonth.Add(newDay);
    }
}

foreach (var x in result)
{
    // Console.WriteLine("\t" + (k++).ToString());
    if (x.Length > 0)
    {
        Console.WriteLine();
        Console.WriteLine("\'" + x + "\'");
    }
}


foreach (var x in daysOfMonth)
{
    Console.WriteLine("\n");
    Console.WriteLine(x.Day.ToString() + " " + x.Month.ToString() + " " + x.Year.ToString());
    // Console.WriteLine("\tEventCount: " + x.EventCount.ToString());
    foreach (var y in x.GetEvents)
    {
        Console.WriteLine();
        Console.WriteLine("\tEvent: " + y.id);
        Console.WriteLine("\tTime: " + y.StartTime.ToString() + " " + y.EndTime.ToString());
        Console.WriteLine("\tDesc: " + y.description);
        Console.WriteLine("\tName: " + y.Name);
        Console.WriteLine("\tHeadCount: " + y.HeadCount);
    }
}


Console.WriteLine("end");