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


        public string[] Lines => ConvertToLines();

        public Line()
        {

        }

        public Line(string line)
        {
            RawLine = line;
        }

        public string[] ConvertToLines()
        {
            return RawLine.Split(',');
        }
    }
}