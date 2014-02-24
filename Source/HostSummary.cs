using System.Collections.Generic;
using System;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class HostSummary
    {
        public string IpAddress { get; set; }
        public string NetBiosName { get; set; }
        public string DnsName { get; set; }
        public string MacAddress { get; set; }
        public string SystemType { get; set; }
        public string Os { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        private List<int> _ports = null;
        private int _numLow = 0;
        private int _numMed = 0;
        private int _numHigh = 0;
        private int _numCritical = 0;

        /// <summary>
        /// 
        /// </summary>
        public HostSummary()
        {
            IpAddress = string.Empty;
            NetBiosName = string.Empty;
            DnsName = string.Empty;
            MacAddress = string.Empty;
            SystemType = string.Empty;
            Os = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;

            _ports = new List<int>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public void AddPort(int port)
        {
            if (_ports.Contains(port) == false)
            {
                _ports.Add(port);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="severity"></param>
        public void AddIssue(string severity)
        {
            switch (severity.Trim())
            {
                case "0":
                    break;
                case "1":
                    _numLow++;
                    break;
                case "2":
                    _numMed++;
                    break;
                case "3":
                    _numHigh++;
                    break;
                case "4":
                    _numCritical++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumPorts
        {
            get
            {
                return _ports.Count;
            }
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumLow
        {
            get
            {
                return _numLow;
            }
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumMed
        {
            get
            {
                return _numMed;
            }
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumHigh
        {
            get
            {
                return _numHigh;
            }
            set
            {

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumCritical
        {
            get
            {
                return _numCritical;
            }
            set
            {

            }
        }
    }
}
