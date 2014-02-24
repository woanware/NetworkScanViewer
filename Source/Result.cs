using System;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public enum ResultType
    {
        Nessus = 0,
        Nmap = 1
    }

    /// <summary>
    /// 
    /// </summary>
    public class Result 
    {
        #region Generic Member Variables
        public string Type { get; set; }
        public int Id { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string Service { get; set; }
        public string Protocol { get; set; }
        public int Port { get; set; }
        public string Text { get; set; }
        #endregion

        #region Nessus Member Variables
        public string PluginId { get; set; }
        public string PluginName { get; set; }
        public string PluginFamily { get; set; }
        public string Severity { get; set; }
        public string Solution { get; set; }
        public string RiskFactor { get; set; }
        public string Description { get; set; }
        public string PluginPublicationDate { get; set; }
        public string VulnPublicationDate { get; set; }
        public string PatchPublicationDate { get; set; }
        public string Synopsis { get; set; }
        public string PluginOutput { get; set; }
        public string PluginVersion { get; set; }
        public string SeeAlso { get; set; }
        public string CvssBaseScore { get; set; }
        public string CvssVector { get; set; }
        public string CvssTemporalScore { get; set; }
        public string Cve { get; set; }
        public string Bid { get; set; }
        public string Xref { get; set; }

        public string ExploitabilityEase { get; set; }
        public string ExploitAvailable { get; set; }
        public string ExploitFrameworkCanvas { get; set; }
        public string ExploitFrameworkMetasploit { get; set; }
        public string ExploitFrameworkCore { get; set; }
        public string MetasploitName { get; set; }
        public string CanvasPackage { get; set; }

        #endregion

        #region Nmap Member Variables
        public string State { get; set; }
        public string Product { get; set; }
        public string Version { get; set; }
        public string ExtraInfo { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public Result()
        {
            Type = string.Empty;

            HostName = string.Empty;
            IpAddress = string.Empty;
            MacAddress = string.Empty;
            Service = string.Empty;
            Protocol = string.Empty;
            Port = 0;

            PluginId = string.Empty;
            PluginName = string.Empty;
            PluginFamily = string.Empty;
            Severity = string.Empty;
            Solution = string.Empty;
            RiskFactor = string.Empty;
            Description = string.Empty;
            PluginPublicationDate = string.Empty;
            VulnPublicationDate = string.Empty;
            PatchPublicationDate = string.Empty;
            Synopsis = string.Empty;
            PluginOutput = string.Empty;
            PluginVersion = string.Empty;
            SeeAlso = string.Empty;
            CvssBaseScore = string.Empty;
            CvssVector = string.Empty;
            CvssTemporalScore = string.Empty;
            Cve = string.Empty;
            Bid = string.Empty;
            Xref = string.Empty;
            ExploitabilityEase = string.Empty;
            ExploitAvailable = string.Empty;
            ExploitFrameworkCanvas = string.Empty;
            ExploitFrameworkMetasploit = string.Empty;
            ExploitFrameworkCore = string.Empty;
            MetasploitName= string.Empty;
            CanvasPackage= string.Empty;

            State = string.Empty;
            Product = string.Empty;
            Version = string.Empty;
            ExtraInfo = string.Empty;
        }
        #endregion

        //#region IComparable Interface
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="other"></param>
        ///// <returns></returns>
        //public int CompareTo(Result other)
        //{
        //    if (IpCompare.AreEqual(this.IpAddress, other.IpAddress) == true)
        //    {
        //        return 0;
        //    }

        //    if (IpCompare.IsGreater(this.IpAddress, other.IpAddress) == true)
        //    {
        //        return 1;
        //    }

        //    if (IpCompare.IsLess(this.IpAddress, other.IpAddress) == true)
        //    {
        //        return -1;
        //    }

        //    return 0;
        //}
        //#endregion

        //#region Properties
        ///// <summary>
        ///// 
        ///// </summary>
        //public string Text
        //{
        //    get
        //    {
        //        return Description + Environment.NewLine + 
        //               Severity + Environment.NewLine + 
        //               Synopsis + Environment.NewLine + 
        //               PluginOutput + Environment.NewLine + 
        //               SeeAlso + Environment.NewLine + 
        //               Cve + Environment.NewLine + 
        //               Bid + Environment.NewLine + 
        //               Xref + Environment.NewLine +
        //               Version + Environment.NewLine +
        //               Product + Environment.NewLine + 
        //               ExtraInfo + Environment.NewLine;
        //    }
        //}
        //#endregion
    }
}
