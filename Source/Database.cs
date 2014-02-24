using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Microsoft.Isam.Esent;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    internal class Database
    {
        public string OutputPath {get;private set;}
        private Connection _connection = null;
        private Table _table = null;
        private Transaction _transaction = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputPath"></param>
        public Database(string outputPath)
        {
            OutputPath = outputPath;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="directory"></param>
        ///// <param name="path"></param>
        //public void Delete(string directory, string path)
        //{
        //    File.Delete(path);
        //    File.Delete(System.IO.Path.Combine(directory, String.Format(CultureInfo.InvariantCulture, "{0}.chk", "edb")));
        //    foreach (string file in Directory.GetFiles(directory, String.Format(CultureInfo.InvariantCulture, "{0}*.log", "edb")))
        //    {
        //        File.Delete(file);
        //    }

        //    foreach (string file in Directory.GetFiles(directory, String.Format(CultureInfo.InvariantCulture, "{0}*.jrs", "edb")))
        //    {
        //        File.Delete(file);
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public string CreateDatabase()
        {
            try
            {
                if (File.Exists(OutputPath + @"\data.edb") == false)
                {
                    _connection = Esent.CreateDatabase(OutputPath + @"\data.edb");

                    _table = _connection.CreateTable("Data");

                    _connection.UsingLazyTransaction(() =>
                    {
                        _table.CreateColumn(new ColumnDefinition("Id", ColumnType.Int32));           // Filterable
                        _table.CreateColumn(new ColumnDefinition("Type", ColumnType.Text));          // Filterable
                        _table.CreateColumn(new ColumnDefinition("HostName", ColumnType.Text));      // Filterable
                        _table.CreateColumn(new ColumnDefinition("IpAddress", ColumnType.Text));     // Filterable
                        _table.CreateColumn(new ColumnDefinition("MacAddress", ColumnType.Text));    // Filterable
                        _table.CreateColumn(new ColumnDefinition("Service", ColumnType.Text));       // Filterable
                        _table.CreateColumn(new ColumnDefinition("Protocol", ColumnType.Text));      // Filterable
                        _table.CreateColumn(new ColumnDefinition("Port", ColumnType.Text));          // Filterable
                        _table.CreateColumn(new ColumnDefinition("Text", ColumnType.Text));          // Filterable
                        _table.CreateColumn(new ColumnDefinition("PluginId", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("PluginName", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("PluginFamily", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Severity", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Solution", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("RiskFactor", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Description", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("PluginPublicationDate", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("VulnPublicationDate", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("PatchPublicationDate", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Synopsis", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("PluginOutput", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("PluginVersion", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("SeeAlso", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("CvssVector", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("CvssBaseScore", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("CvssTemporalScore", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Cve", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Bid", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("Xref", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("State", ColumnType.Text));         // Filterable
                        _table.CreateColumn(new ColumnDefinition("Product", ColumnType.Text));       // Filterable
                        _table.CreateColumn(new ColumnDefinition("Version", ColumnType.Text));       // Filterable
                        _table.CreateColumn(new ColumnDefinition("ExtraInfo", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("ExploitabilityEase", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("ExploitAvailable", ColumnType.Text)); // Filterable
                        _table.CreateColumn(new ColumnDefinition("ExploitFrameworkCanvas", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("ExploitFrameworkMetasploit", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("ExploitFrameworkCore", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("MetasploitName", ColumnType.Text));
                        _table.CreateColumn(new ColumnDefinition("CanvasPackage", ColumnType.Text));
                    });

                    _transaction = _connection.BeginTransaction();
                }
                else
                {
                    _connection = Esent.OpenDatabase(OutputPath + @"\data.edb");
                    _table = _connection.OpenTable("Data");
                    _transaction = _connection.BeginTransaction();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public string AddResult(Result result)
        {
            StringBuilder text = new StringBuilder();
            if (result.Type.ToUpper() == "NMAP")
            {
                this.AddOutputLine(text, "Product", result.Product);
                this.AddOutputLine(text, "Version", result.Version);
                this.AddOutputLine(text, "Extra Info", result.ExtraInfo);
                this.AddOutputLine(text, "State", result.State);
            }
            else
            {
                this.AddOutputLine(text, "Description", result.Description);
                this.AddOutputLine(text, "Synopsis", result.Synopsis);
                this.AddOutputLine(text, "Plugin Version", result.PluginVersion);
                this.AddOutputLine(text, "Risk Factor", result.RiskFactor);
                this.AddOutputLine(text, "Vuln Publication Date", result.VulnPublicationDate);
                this.AddOutputLine(text, "Plugin Publication Date", result.PluginPublicationDate);
                this.AddOutputLine(text, "Patch Publication Date", result.PatchPublicationDate);
                this.AddOutputLine(text, "CVSS Vector", result.CvssVector);
                this.AddOutputLine(text, "CVSS Base Score", result.CvssBaseScore);
                this.AddOutputLine(text, "CVSS Temporal Score", result.CvssTemporalScore);
                this.AddOutputLine(text, "CVE", result.Cve);
                this.AddOutputLine(text, "BID", result.Bid);
                this.AddOutputLine(text, "XREF", result.Xref);
                this.AddOutputLine(text, "See Also", result.SeeAlso);
                this.AddOutputLine(text, "Plugin Output", result.PluginOutput);
                this.AddOutputLine(text, "Exploitability Ease", result.ExploitabilityEase);
                this.AddOutputLine(text, "Exploit Available", result.ExploitAvailable);
                this.AddOutputLine(text, "Exploit Framework (Canvas)", result.ExploitFrameworkCanvas);
                this.AddOutputLine(text, "Exploit Framework (Metasploit)", result.ExploitFrameworkMetasploit);
                this.AddOutputLine(text, "Exploit Framework (Core)", result.ExploitFrameworkCore);
                this.AddOutputLine(text, "Metasploit Name", result.MetasploitName);
                this.AddOutputLine(text, "Canvas Package", result.CanvasPackage);
            }

            try
            {
                _table.NewRecord()
                   .SetColumn("Id", result.Id)
                   .SetColumn("Type", result.Type)
                   .SetColumn("HostName", result.HostName)
                   .SetColumn("IpAddress", result.IpAddress)
                   .SetColumn("MacAddress", result.MacAddress)
                   .SetColumn("Service", result.Service)
                   .SetColumn("Protocol", result.Protocol)
                   .SetColumn("Port", result.Port)
                   .SetColumn("PluginId", result.PluginId)
                   .SetColumn("PluginName", result.PluginName)
                   .SetColumn("PluginFamily", result.PluginFamily)
                   .SetColumn("Severity", result.Severity)
                   .SetColumn("Solution", result.Solution)
                   .SetColumn("RiskFactor", result.RiskFactor)
                   .SetColumn("Description", result.Description)
                   .SetColumn("PluginPublicationDate", result.PluginPublicationDate)
                   .SetColumn("VulnPublicationDate", result.VulnPublicationDate)
                   .SetColumn("PatchPublicationDate", result.PatchPublicationDate)
                   .SetColumn("Synopsis", result.Synopsis)
                   .SetColumn("PluginOutput", result.PluginOutput)
                   .SetColumn("PluginVersion", result.PluginVersion)
                   .SetColumn("SeeAlso", result.SeeAlso)
                   .SetColumn("CvssVector", result.CvssVector)
                   .SetColumn("CvssBaseScore", result.CvssBaseScore)
                   .SetColumn("CvssTemporalScore", result.CvssTemporalScore)
                   .SetColumn("Cve", result.Cve)
                   .SetColumn("Bid", result.Bid)
                   .SetColumn("Xref", result.Xref)
                   .SetColumn("State", result.State)
                   .SetColumn("Product", result.Product)
                   .SetColumn("Version", result.Version)
                   .SetColumn("ExtraInfo", result.ExtraInfo)
                   .SetColumn("ExploitabilityEase", result.ExploitabilityEase)
                   .SetColumn("ExploitAvailable", result.ExploitAvailable)
                   .SetColumn("ExploitFrameworkCanvas", result.ExploitFrameworkCanvas)
                   .SetColumn("ExploitFrameworkMetasploit", result.ExploitFrameworkMetasploit)
                   .SetColumn("ExploitFrameworkCore", result.ExploitFrameworkCore)
                   .SetColumn("MetasploitName", result.MetasploitName)
                   .SetColumn("CanvasPackage", result.CanvasPackage)
                   .SetColumn("Text", text.ToString())
                   .Save();

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void AddOutputLine(StringBuilder output,
                                   string name,
                                   string value)
        {
            if (value.Length > 0)
            {
                if (name == "Plugin Output")
                {
                    output.AppendFormat("{0}:\r\n\r\n{1}" + Environment.NewLine + Environment.NewLine, name, value.Trim());
                }
                else if (name == "Description")
                {
                    output.AppendFormat("{0}: {1}" + Environment.NewLine + Environment.NewLine, name, value.Trim());
                }
                else if (name == "Synopsis")
                {
                    output.AppendFormat("{0}: {1}" + Environment.NewLine + Environment.NewLine, name, value.Trim());
                }
                else
                {
                    output.AppendFormat("{0}: {1}" + Environment.NewLine, name, value.Trim());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberRecords()
        {
            using (Connection connection = Esent.OpenDatabase(OutputPath + @"\data.edb"))
            using (Table table = connection.OpenTable("Data"))
            {
                return (from data in table select data).Count();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _table.Dispose();
            _connection.Dispose();
        }
    }
}
