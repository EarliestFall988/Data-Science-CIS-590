using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormatCSV_PDF_Data
{
    public class DayOfMonth
    {
        public int Day { get; set; } = 0;
        public int Month { get; set; } = 0;
        public int Year { get; set; } = 2012;

        /// <summary>
        /// The events for the day
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <returns></returns>
        public List<Event> Events { private get; init; } = new List<Event>();

        /// <summary>
        /// The total events for the day
        /// </summary>
        public int EventCount => Events.Count;

        /// <summary>
        /// The events
        /// </summary>
        public IEnumerable<Event> GetEvents => Events;

        public DayOfMonth()
        {

        }
    }
}