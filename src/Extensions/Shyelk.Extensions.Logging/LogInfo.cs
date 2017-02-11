using System;
namespace Shyelk.Extensions.Logging
{
    /// <summary>
    /// base Log Content
    /// </summary>
    public abstract class LogInfo
    {
        /// <summary>
        /// Log Message
        /// </summary>
        public string Message { get; set; }
        ///<summary>
        ///log occur function
        ///</summary>
        public string Function { get; set; }
        ///<summary>
        ///log occur Class
        ///</summary>
        public string Class { get; set; }
        ///<summary>
        ///log occur Namespace
        ///</summary>
        public string Namespace { get; set; }
        ///<summary>
        ///log occur Datetime Utc
        ///</summary>
        public DateTime OccurTimeUtc { get; set; }
        ///<summary>
        ///log occur Datetime of Server
        ///</summary>
        public DateTime OccurTimeLocation { get; set; }
        ///<summary>
        ///log Level
        ///</summary>
        public string Level { get; set; }
    }
}
