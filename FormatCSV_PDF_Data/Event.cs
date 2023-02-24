using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormatCSV_PDF_Data
{
    /// <summary>
    /// Manage data for a given event
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The name of the event
        /// </summary>
        /// <value></value>
        public string Name { get; init; } = "";

        /// <summary>
        /// Event id
        /// </summary>
        /// <returns></returns>
        public string id { get; init; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The description of the event
        /// </summary>
        /// <value></value>
        public string description { get; init; } = "";

        /// <summary>
        /// The start time of the event
        /// </summary>
        /// <value></value>
        public TimeSpan StartTime { get; init; } = TimeSpan.MinValue;

        /// <summary>
        /// The end time of the event
        /// </summary>
        /// <value></value>
        public TimeSpan EndTime { get; init; } = TimeSpan.MinValue;


        /// <summary>
        /// The head count of the event
        /// </summary>
        /// <value></value>
        public string HeadCount { get; init; } = "";
    }
}