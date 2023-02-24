using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormatCSV_PDF_Data
{
    public class Line
    {
        /// <summary>
        /// The lines
        /// </summary>
        /// <value></value>
        public string RawLine { get; set; } = "";

        /// <summary>
        /// The line id
        /// </summary>
        /// <value></value>
        public int Id { get; set; } = 0;

        public string[] Lines => ConvertToLines();

        public Line()
        {

        }

        public Line(string line, int id)
        {
            RawLine = line;
            Id = id;
        }

        /// <summary>
        /// Convert the line to an array of strings
        /// </summary>
        /// <returns></returns>
        public string[] ConvertToLines()
        {
            // var result = RawLine.Split(',');

            // Console.WriteLine(Id);
            // foreach (var x in result)
            // {
            //     Console.WriteLine(x);
            // }

            // return result;

            return ParseLine(RawLine).ToArray();
        }

        /// <summary>
        /// Parse csv line
        /// </summary>
        /// <param name="line">the line to parse</param>
        /// <returns>return the parsed string</returns>
        private List<string> ParseLine(string line)
        {

            bool literal = false;
            List<string> result = new List<string>();

            bool debug = Id == 1;
            Console.WriteLine(Id);

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    literal = !literal;
                }

                if (line[i] == ',' && !literal)
                {
                    result.Add(line.Substring(0, i));
                    line = line.Substring(i + 1);

                    if (debug)
                        Console.Write(" '" + line + "'");

                    i = 0;
                }
            }

            Console.WriteLine();
            return result;
        }

        public override string ToString()
        {

            string result = "";

            string[] data = ConvertToLines();

            for (int i = 0; i < data.Length; i++)
            {
                result = "\t" + i.ToString() + " " + data[i]; 
            }

            return result;
        }
    }
}