using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class Parser
    {
        #region Public Events
        public delegate void ErrorEventHandler(string error);
        public event ErrorEventHandler ErrorEvent;
        public delegate void UpdateEventHandler(string text);
        public event UpdateEventHandler UpdateEvent;
        public delegate void ResultEventHandler(Result result);
        public event ResultEventHandler ResultEvent;
        public delegate void CompleteEventHandler();
        public event CompleteEventHandler CompleteEvent;
        #endregion

        #region Delegates
        /// <summary>
        /// 
        /// </summary>
        private delegate void StartDelegate();
        #endregion

        #region Enums
        /// <summary>
        /// Used to determine which data store is used
        /// </summary>
        private enum ItemType
        {
            IpAddress = 0,
            HostName = 1,
            Protocol = 2,
            Port = 3,
            State = 4,
            Service = 5,
            PluginId = 6,
            PluginName = 7,
            PluginFamily = 8,
            Product = 9,
            Severities = 10,
            Versions = 11,
            ExploitAvailable = 12
        }
        #endregion

        #region Member Variables
        private Database _database = null;
        private int _counter = 0;
        public string OutputPath {get;set;}
        public string InputPath { get; set; }
        private List<string> _files = null;
        private string _error = string.Empty;
        public int TotalNumberResults { get; private set; }

        public List<HostSummary> HostSummaries { get; set; }

        public List<IpAddress> IpAddresses { get; set; }
        public List<string> HostNames { get; set; }
        public List<string> Protocols { get; set; }
        public List<int> Ports { get; set; }
        public List<string> States { get; set; }
        public List<string> Services { get; set; }
        
        public List<string> Severities { get; set; }
        public List<int> PluginIds { get; set; }
        public List<string> PluginNames { get; set; }
        public List<string> PluginFamilys { get; set; }
        public List<string> Synopsises { get; set; }
        public List<string> Products { get; set; }
        public List<string> Versions { get; set; }
        public List<string> ExploitAvailable { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public Parser()
        {
            OutputPath = string.Empty;
            this.Clear();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void AddFile(string path)
        {
            _files.Add(path);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Parse()
        {
            StartDelegate startDelegate = new StartDelegate(DoParse);
            startDelegate.BeginInvoke(null, null);  
        }

        /// <summary>
        /// 
        /// </summary>
        private void DoParse()
        {
            _database = new Database(OutputPath);

            string ret = _database.CreateDatabase();
            if (ret.Length > 0)
            {
                OnError("An error occurred whilst creating the database: " + ret);
                return;
            }

            foreach (string file in _files)
            {
                if (System.IO.Path.GetExtension(file) == ".nessus")
                {
                    UpdateEvent("* Processing file: " + file);
                    ProcessNessus(file);
                }
                else if (System.IO.Path.GetExtension(file) == ".xml")
                {
                    UpdateEvent("* Processing file: " + file);
                    ProcessNmap(file);
                }
            }

            this.OnComplete();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        private void ProcessNmap(string filePath)
        {
            try
            {
                XDocument xml = XDocument.Load(filePath);
                var hosts = from h in xml.Descendants("host") select h;
                foreach (var host in hosts)
                {
                    string ipAddress = string.Empty;
                    string macAddress = string.Empty;
                    string hostName = string.Empty;

                    var ipNode = from n in host.Elements("address") where n.Attribute("addrtype").Value == "ipv4" select n;
                    if (ipNode.Count() == 1)
                    {
                        ipAddress = ipNode.First().Attribute("addr").Value;
                    }

                    var macNode = from n in host.Elements("address") where n.Attribute("addrtype").Value == "mac" select n;
                    if (macNode.Count() == 1)
                    {
                        macAddress = macNode.First().Attribute("addr").Value;
                    }

                    var hostNames = from h in host.Elements("hostnames").Elements("hostname") where h.Attribute("type").Value == "user" select h;
                    if (hostNames.Any() == true)
                    {
                        hostName = hostNames.First().Attribute("name").Value;
                    }

                    var ports = from h in host.Descendants("port") select h;
                    foreach (var port in ports)
                    {
                        Result result = new Result();
                        result.Type = "Nmap";
                        result.IpAddress = ipAddress;
                        result.MacAddress = macAddress;
                        result.HostName = hostName;

                        result.Protocol = Xml.GetAttributeValueAsString(port, "protocol");
                        result.Port = Xml.GetAttributeValueAsInt(port, "portid");

                        var state = from n in port.Elements("state") select n;
                        if (state.Count() == 1)
                        {
                            result.State = Xml.GetAttributeValueAsString(state.First(), "state");
                        }

                        var service = from n in port.Elements("service") select n;
                        if (service.Count() == 1)
                        {
                            result.Service = Xml.GetAttributeValueAsString(service.First(), "name");
                            result.Product = Xml.GetAttributeValueAsString(service.First(), "product");
                            result.Version = Xml.GetAttributeValueAsString(service.First(), "version");
                            result.ExtraInfo = Xml.GetAttributeValueAsString(service.First(), "extrainfo");
                        }

                        // Generic
                        this.AddItem(ItemType.IpAddress, result.IpAddress);
                        this.AddItem(ItemType.HostName, result.HostName);
                        this.AddItem(ItemType.Port, result.Port);
                        this.AddItem(ItemType.Protocol, result.Protocol);
                        this.AddItem(ItemType.Service, result.Service);

                        // Nmap specific
                        this.AddItem(ItemType.State, result.State);
                        this.AddItem(ItemType.Product, result.Product);
                        this.AddItem(ItemType.Versions, result.Version);

                        OnResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateEvent("* Error: " + ex.Message);
                _error = ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        private void ProcessNessus(string filePath)
        {
            try
            {
                XDocument xml = XDocument.Load(filePath);
                var header = from h in xml.Descendants("NessusClientData_v2") select h;
                if (header.Any() == false)
                {
                    UpdateEvent("* Incorrect nessus results file version. Should be \"NessusClientData_v2\": " + filePath);
                    return;
                }

                var hosts = from h in xml.Descendants("ReportHost") select h;
                foreach (var host in hosts)
                {
                    HostSummary hostSummary = new HostSummary();

                    hostSummary.IpAddress = Xml.GetAttributeValueAsString(host, "name");

                    var hostProperties = from h in host.Element("HostProperties").Descendants("tag") select h;
                    foreach (var hostProperty in hostProperties)
                    {
                        string attribute = Xml.GetAttributeValueAsString(hostProperty, "name").ToLower();
                        switch (attribute)
                        {
                            case "host_end":
                                hostSummary.EndTime = hostProperty.Value;
                                break;
                            case "operating-system":
                                hostSummary.Os = hostProperty.Value;
                                break;
                            case "mac-address":
                                hostSummary.MacAddress = hostProperty.Value;
                                break;
                            case "system-type":
                                hostSummary.SystemType = hostProperty.Value;
                                break;
                            case "netbios-name":
                                hostSummary.NetBiosName = hostProperty.Value;
                                break;
                            case "host-fqdn":
                                hostSummary.DnsName = hostProperty.Value;
                                break;
                            case "host_start":
                                hostSummary.StartTime = hostProperty.Value;
                                break;
                        }
                    }

                    HostSummaries.Add(hostSummary);

                    var items = from i in host.Descendants("ReportItem") select i;
                    foreach (var item in items)
                    {
                        Result result = new Result();
                        result.Type = "Nessus";

                        result.IpAddress = hostSummary.IpAddress;
                        result.HostName = hostSummary.DnsName;

                        result.Port = Xml.GetAttributeValueAsInt(item, "port");
                        hostSummary.AddPort(result.Port);
                        result.Service = Xml.GetAttributeValueAsString(item, "svc_name");
                        result.Protocol = Xml.GetAttributeValueAsString(item, "protocol");
                        result.Severity = Xml.GetAttributeValueAsString(item, "severity");
                        hostSummary.AddIssue(result.Severity);
                        result.PluginId = Xml.GetAttributeValueAsString(item, "pluginID");
                        result.PluginName = Xml.GetAttributeValueAsString(item, "pluginName");
                        result.PluginFamily = Xml.GetAttributeValueAsString(item, "pluginFamily");

                        result.Solution = this.AddMultilineString(result.Solution, Xml.GetElementValueAsString(item, "solution"));
                        result.SeeAlso = this.AddMultilineString(result.Solution, Xml.GetElementValueAsString(item, "see_also"));
                        result.Synopsis = this.AddMultilineString(result.Synopsis, Xml.GetElementValueAsString(item, "synopsis"));
                        result.RiskFactor = this.AddMultilineString(result.RiskFactor, Xml.GetElementValueAsString(item, "risk_factor"));
                        result.Description = this.AddMultilineString(result.Description, Xml.GetElementValueAsString(item, "description"));
                        result.PluginPublicationDate = this.AddMultilineString(result.PluginPublicationDate, Xml.GetElementValueAsString(item, "plugin_publication_date"));
                        result.VulnPublicationDate = this.AddMultilineString(result.VulnPublicationDate, Xml.GetElementValueAsString(item, "vuln_publication_date"));
                        result.PatchPublicationDate = this.AddMultilineString(result.PatchPublicationDate, Xml.GetElementValueAsString(item, "patch_publication_date"));
                        result.PluginOutput = this.AddMultilineString(result.PluginOutput, Xml.GetElementValueAsString(item, "plugin_output"));
                        result.PluginVersion = this.AddMultilineString(result.PluginVersion, Xml.GetElementValueAsString(item, "plugin_version"));
                        result.CvssBaseScore = this.AddMultilineString(result.CvssBaseScore, Xml.GetElementValueAsString(item, "cvss_base_score"));
                        result.CvssVector = this.AddMultilineString(result.CvssVector, Xml.GetElementValueAsString(item, "cvss_vector"));
                        result.CvssTemporalScore = this.AddMultilineString(result.CvssVector, Xml.GetElementValueAsString(item, "cvss_temporal_score"));
                        result.Cve = this.AddMultilineString(result.Cve, Xml.GetElementValueAsString(item, "cve"));
                        result.Bid = this.AddMultilineString(result.Bid, Xml.GetElementValueAsString(item, "bid"));
                        result.Xref = this.AddMultilineString(result.Xref, Xml.GetElementValueAsString(item, "xref"));
                        result.ExploitabilityEase = this.AddMultilineString(result.ExploitabilityEase, Xml.GetElementValueAsString(item, "exploitability_ease"));
                        result.ExploitAvailable = this.AddMultilineString(result.ExploitAvailable, Xml.GetElementValueAsString(item, "exploit_available"));
                        result.ExploitFrameworkCanvas = this.AddMultilineString(result.ExploitFrameworkCanvas, Xml.GetElementValueAsString(item, "exploit_framework_canvas"));
                        result.ExploitFrameworkMetasploit = this.AddMultilineString(result.ExploitFrameworkMetasploit, Xml.GetElementValueAsString(item, "exploit_framework_metasploit"));
                        result.ExploitFrameworkCore = this.AddMultilineString(result.ExploitFrameworkCore, Xml.GetElementValueAsString(item, "exploit_framework_core"));
                        result.MetasploitName = this.AddMultilineString(result.MetasploitName, Xml.GetElementValueAsString(item, "metasploit_name"));
                        result.CanvasPackage = this.AddMultilineString(result.CanvasPackage, Xml.GetElementValueAsString(item, "canvas_package"));

                        // Generic
                        this.AddItem(ItemType.IpAddress, result.IpAddress);
                        this.AddItem(ItemType.HostName, result.HostName);
                        this.AddItem(ItemType.Port, result.Port);
                        this.AddItem(ItemType.Protocol, result.Protocol);
                        this.AddItem(ItemType.Service, result.Service);

                        // Nessus Specific
                        this.AddItem(ItemType.Severities, result.Severity);
                        this.AddItem(ItemType.PluginFamily, result.PluginFamily);
                        this.AddItem(ItemType.PluginId, result.PluginId);
                        this.AddItem(ItemType.PluginName, result.PluginName);
                        this.AddItem(ItemType.ExploitAvailable, result.ExploitAvailable);

                        OnResult(result);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateEvent("* Error: " + ex.Message);

                _error = ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        private string AddMultilineString(string property, string value)
        {
            if (property.Trim().Length > 0)
            {
                return property + Environment.NewLine + value;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="value"></param>
        private void AddItem(ItemType itemType, object value)
        {
            if (value.ToString().Length == 0)
            {
                return;    
            }

            switch (itemType)
            {
                case ItemType.IpAddress:
                    var ipAddresses = from ipAddress in IpAddresses where ipAddress.Text.ToUpper() == ((string)value).ToUpper() select ipAddress;
                    if (ipAddresses.Count() == 0)
                    {
                        IpAddress ipAddress = new IpAddress();
                        ipAddress.Text = (string)value;
                        IpAddresses.Add(ipAddress);
                    }
                    break;
                case ItemType.HostName:
                    var hosts = from host in HostNames where host.ToUpper() == ((string)value).ToUpper() select host;
                    if (hosts.Count() == 0)
                    {
                        HostNames.Add((string)value);
                    }
                    break;
                case ItemType.Port:
                    var ports = from port in Ports where port == (int)value select port;
                    if (ports.Count() == 0)
                    {
                        Ports.Add((int)value);
                    }
                    break;
                case ItemType.Protocol:
                    var protocols = from protocol in Protocols where protocol.ToUpper() == ((string)value).ToUpper() select protocol;
                    if (protocols.Count() == 0)
                    {
                        Protocols.Add(((string)value).ToUpper());
                    }
                    break;
                case ItemType.Service:
                    var services = from service in Services where service.ToUpper() == ((string)value).ToUpper() select service;
                    if (services.Count() == 0)
                    {
                        Services.Add((string)value);
                    }
                    break;
                case ItemType.State:
                    var states = from state in States where state.ToUpper() == ((string)value).ToUpper() select state;
                    if (states.Count() == 0)
                    {
                        States.Add((string)value);
                    }
                    break;
                case ItemType.Severities:
                    var severities = from severity in Severities where severity.ToUpper() == ((string)value).ToUpper() select severity;
                    if (severities.Count() == 0)
                    {
                        Severities.Add((string)value);
                    }
                    break;
                case ItemType.PluginFamily:
                    var pluginFamilies = from pluginFamily in PluginFamilys where pluginFamily.ToUpper() == ((string)value).ToUpper() select pluginFamily;
                    if (pluginFamilies.Count() == 0)
                    {
                        PluginFamilys.Add((string)value);
                    }
                    break;
                case ItemType.PluginId:
                    var pluginIds = from pluginId in PluginIds where pluginId.ToString().ToUpper() == ((string)value).ToUpper() select pluginId;
                    if (pluginIds.Count() == 0)
                    {
                        PluginIds.Add(int.Parse(value.ToString()));
                    }
                    break;
                case ItemType.PluginName:
                    var pluginNames = from pluginName in PluginNames where pluginName.ToUpper() == ((string)value).ToUpper() select pluginName;
                    if (pluginNames.Count() == 0)
                    {
                        PluginNames.Add((string)value);
                    }
                    break;
                case ItemType.Product:
                    var products = from product in Products where product.ToUpper() == ((string)value).ToUpper() select product;
                    if (products.Count() == 0)
                    {
                        Products.Add((string)value);
                    }
                    break;
                case ItemType.Versions:
                    var versions = from version in Versions where version.ToUpper() == ((string)value).ToUpper() select version;
                    if (versions.Count() == 0)
                    {
                        Versions.Add((string)value);
                    }
                    break;
                case ItemType.ExploitAvailable:
                    var exploitAvailable = from e in ExploitAvailable where e.ToUpper() == ((string)value).ToUpper() select e;
                    if (exploitAvailable.Count() == 0)
                    {
                        ExploitAvailable.Add((string)value);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _counter = 0;
            _files = new List<string>();

            HostSummaries = new List<HostSummary>();

            IpAddresses = new List<IpAddress>();
            HostNames = new List<string>();
            Protocols = new List<string>();
            Ports = new List<int>();
            States = new List<string>();
            Services = new List<string>();

            Severities = new List<string>();
            PluginIds = new List<int>();
            PluginNames = new List<string>();
            PluginFamilys = new List<string>();
            Synopsises = new List<string>();
            Products = new List<string>();
            Versions = new List<string>();
            ExploitAvailable = new List<string>();
        }

        #region Event Handler Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void OnResult(Result result)
        {
            _counter++;

            result.Id = _counter;

            if (_database != null)
            {
                _database.AddResult(result);
            }

            if (ResultEvent != null)
            {
                ResultEvent(result);
            }
        }

        /// 
        /// </summary>
        /// <param name="result"></param>
        private void OnUpdate(string text)
        {
            if (UpdateEvent != null)
            {
                UpdateEvent(text);
            }
        }

        /// 
        /// </summary>
        /// <param name="error"></param>
        private void OnError(string error)
        {
            if (ErrorEvent != null)
            {
                ErrorEvent(error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            if (_database != null)
            {
                _database.Commit();
            }

            TotalNumberResults = _database.GetTotalNumberRecords();

            if (CompleteEvent != null)
            {
                CompleteEvent();
            }
        }
        #endregion
    }
}
