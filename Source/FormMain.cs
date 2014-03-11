using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Isam.Esent;
using WeifenLuo.WinFormsUI.Docking;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMain : Form
    {
        #region Enums
        /// <summary>
        /// 
        /// </summary>
        private enum FileFormat
        {
            Csv = 0,
            Xml = 1
        }
        #endregion

        #region Member Variables
        private Settings _settings = null;
        public IgnorePlugins IgnoredPlugins { get; set; }
        private FileFinder _fileFinder = null;
        private Parser _networkScanParser = null;
        private bool _isLoading = false;
        private int _currentResultsPage = 1;
        private int _totalResultsPages = 0;
        private HourGlass _hourGlass;
        private FormList formList;
        private FormDescription formDescription;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            _fileFinder = new FileFinder();
            _fileFinder.CompleteEvent += new woanware.Events.DefaultEvent(OnFileFinder_CompleteEvent);
            _fileFinder.UpdateEvent += new woanware.Events.MessageEvent(OnFileFinder_UpdateEvent);

            _networkScanParser = new Parser();
            _networkScanParser.UpdateEvent += new woanware.Events.MessageEvent(OnNetworkScanParser_UpdateEvent);
            _networkScanParser.CompleteEvent += new woanware.Events.DefaultEvent(OnNetworkScanParser_CompleteEvent);

            SetPagingButtonStatus();

            this.formList = new FormList();
            this.formList.ResultSelectedEvent += formList_ResultSelectedEvent;
            this.formList.BackColor = this.BackColor;
            this.formList.Show(dockPanel, DockState.DockRight);
            this.formList.SetFormMain(this);

            DockPane dp = dockPanel.DockPaneFactory.CreateDockPane(formList, DockState.Document, true);

            this.formDescription = new FormDescription();
            this.formDescription.BackColor = this.BackColor;
            this.formDescription.Show(dockPanel, DockState.DockRight);
        }
        #endregion

        #region Child Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void formList_ResultSelectedEvent(Result result)
        {
            if (result != null)
            {
                this.formDescription.SetData(result.Text);
            }
        }
        #endregion

        #region Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsOptions_Click(object sender, EventArgs e)
        {
            using (FormOptions formOptions = new FormOptions(_settings, IgnoredPlugins))
            {
                if (formOptions.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                _settings = formOptions.Settings;
                IgnoredPlugins = formOptions.IgnorePlugins;

                if (formOptions.NeedsReload == true)
                {
                    LoadResults(1);
                }

                

                base.TopMost = _settings.AlwaysOnTop;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            using (FormOpen formOpen = new FormOpen())
            {
                if (formOpen.ShowDialog(this) == DialogResult.Cancel)
                {
                    return;
                }

                _hourGlass = new HourGlass(this);

                _networkScanParser.Clear();
                _networkScanParser.OutputPath = formOpen.OutputFolder;
                _networkScanParser.InputPath = formOpen.InputFolder;
                _fileFinder.Start(formOpen.InputFolder);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            using (FormAbout formAbout = new FormAbout())
            {
                formAbout.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpHelp_Click(object sender, EventArgs e)
        {
            Misc.ShellExecuteFile(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Help.pdf"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveResultsCsv_Click(object sender, EventArgs e)
        {
            SaveResults(FileFormat.Csv);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveResultsXml_Click(object sender, EventArgs e)
        {
            SaveResults(FileFormat.Xml);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileSaveHostSummaryCsv_Click(object sender, EventArgs e)
        {
            SaveHostSummarys(FileFormat.Csv);
        }
        #endregion

        #region Network Scan Parser Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void OnNetworkScanParser_UpdateEvent(string text)
        {
            MethodInvoker methodInvoker = delegate
            {
                txtLog.AppendText(text);
                txtLog.AppendText(Environment.NewLine);
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnNetworkScanParser_CompleteEvent()
        {
            MethodInvoker methodInvoker = delegate
            {
                _isLoading = true;

                _networkScanParser.IpAddresses.Sort();
                _networkScanParser.HostNames.Sort();
                _networkScanParser.Ports.Sort();
                _networkScanParser.Severities.Sort();
                _networkScanParser.States.Sort();
                _networkScanParser.Protocols.Sort();
                _networkScanParser.Services.Sort();
                _networkScanParser.PluginFamilys.Sort();
                _networkScanParser.PluginIds.Sort();
                _networkScanParser.PluginNames.Sort();
                _networkScanParser.Products.Sort();
                _networkScanParser.Versions.Sort();

                LoadFilterComboBox(cboFilterIp, _networkScanParser.IpAddresses);
                LoadFilterComboBox(cboFilterHostName, _networkScanParser.HostNames);
                LoadFilterComboBox(cboFilterPort, _networkScanParser.Ports);
                LoadFilterComboBox(cboFilterState, _networkScanParser.States);
                LoadFilterComboBox(cboFilterProtocol, _networkScanParser.Protocols);
                LoadFilterComboBox(cboFilterService, _networkScanParser.Services);
                LoadFilterComboBox(cboFilterPluginFamilyName, _networkScanParser.PluginFamilys);
                LoadFilterComboBox(cboFilterPluginId, _networkScanParser.PluginIds);
                LoadFilterComboBox(cboFilterPluginName, _networkScanParser.PluginNames);
                LoadFilterComboBox(cboFilterProduct, _networkScanParser.Products);
                LoadFilterComboBox(cboFilterVersion, _networkScanParser.Versions);
                LoadFilterComboBox(cboFilterSeverity, _networkScanParser.Severities);
                LoadFilterComboBox(cboFilterExploitAvailable, _networkScanParser.ExploitAvailable);

                cboFilterType.Items.Clear();
                cboFilterType.Items.Add(string.Empty);
                cboFilterType.Items.Add("Nessus");
                cboFilterType.Items.Add("Nmap");
                cboFilterType.SelectedIndex = 0;

                LoadResults(1);
                LoadHostSummary();

                if (_hourGlass != null)
                {
                    _hourGlass.Dispose();
                }

                _isLoading = false;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void OnNetworkScanParser_ResultEvent(Result result)
        {
            

            //UpdateStatusBar("Identified: " + result.IpAddress + ":" + result.Port);
        }
        #endregion

        #region ComboBox Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="data"></param>
        private void LoadFilterComboBox(ComboBox comboBox, List<IpAddress> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);

                for (int index = 0; index < data.Count; index++)
                {
                    comboBox.Items.Add(data[index].Text);
                }

                comboBox.SelectedIndex = 0;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="data"></param>
        private void LoadFilterComboBox(ToolStripComboBox comboBox, List<IpAddress> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);

                for (int index = 0; index < data.Count; index++)
                {
                    comboBox.Items.Add(data[index].Text);
                }

                comboBox.SelectedIndex = 0;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="data"></param>
        private void LoadFilterComboBox(ComboBox comboBox, List<string> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);

                for (int index = 0; index < data.Count; index++)
                {
                    comboBox.Items.Add(data[index]);
                }

                comboBox.SelectedIndex = 0;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="data"></param>
        private void LoadFilterComboBox(ToolStripComboBox comboBox, List<string> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);

                for (int index = 0; index < data.Count; index++)
                {
                    comboBox.Items.Add(data[index]);
                }

                comboBox.SelectedIndex = 0;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="data"></param>
        private void LoadFilterComboBox(ComboBox comboBox, List<int> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);

                for (int index = 0; index < data.Count; index++)
                {
                    comboBox.Items.Add(data[index]);
                }

                comboBox.SelectedIndex = 0;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="data"></param>
        private void LoadFilterComboBox(ToolStripComboBox comboBox, List<int> data)
        {
            MethodInvoker methodInvoker = delegate
            {
                comboBox.Items.Clear();
                comboBox.Items.Add(string.Empty);

                for (int index = 0; index < data.Count; index++)
                {
                    comboBox.Items.Add(data[index]);
                }

                comboBox.SelectedIndex = 0;
            };

            if (this.InvokeRequired == true)
            {
                BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="text"></param>
        public void SetFilterComboBoxItem(ComboBox comboBox, string text)
        {
            for (int index = 0; index < comboBox.Items.Count; index++)
            {
                if (comboBox.Items[index].ToString() == text)
                {
                    comboBox.SelectedIndex = index;
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="text"></param>
        public void SetFilterComboBoxItem(ToolStripComboBox comboBox, string text)
        {
            for (int index = 0; index < comboBox.Items.Count; index++)
            {
                if (comboBox.Items[index].ToString() == text)
                {
                    comboBox.SelectedIndex = index;
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearFilters()
        {
            cboFilterIp.SelectedIndex = 0;
            cboFilterPluginFamilyName.SelectedIndex = 0;
            cboFilterPluginId.SelectedIndex = 0;
            cboFilterPluginName.SelectedIndex = 0;
            cboFilterService.SelectedIndex = 0;
            cboFilterPort.SelectedIndex = 0;
            cboFilterProduct.SelectedIndex = 0;
            cboFilterProtocol.SelectedIndex = 0;
            cboFilterService.SelectedIndex = 0;
            cboFilterType.SelectedIndex = 0;
            cboFilterVersion.SelectedIndex = 0;
        }
        #endregion

        #region File Finder Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void OnFileFinder_UpdateEvent(string path)
        {
            _networkScanParser.AddFile(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileFinder_CompleteEvent()
        {
            _networkScanParser.Parse();
        }
        #endregion

        #region ComboBox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoading == true)
            {
                return;
            }

            if (_settings.MoveFocusToList == true)
            {
                this.formList.SetFocusToList();
            }

            LoadResults(1);
        }
        #endregion

        #region Misc Methods
        /// <summary>
        /// 
        /// </summary>
        private void LoadHostSummary()
        {
            MethodInvoker methodInvoker = delegate
            {
                using (new HourGlass(this))
                {
                    listHostSummary.ClearObjects();
                    listHostSummary.SetObjects(_networkScanParser.HostSummaries);
                }
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadResults(int page)
        {
            MethodInvoker methodInvoker = delegate
            {
                _currentResultsPage = page;

                using (new HourGlass(this))
                using (Connection connection = Esent.OpenDatabase(_networkScanParser.OutputPath + @"\data.edb"))
                using (Table table = connection.OpenTable("Data"))
                {
                    var query = GetQuery(table);

                    if (_currentResultsPage == 1)
                    {
                        int temp = (from q in query select q).Count();
                        _totalResultsPages = (int)Math.Ceiling((decimal)temp / (decimal)(_settings.NumResultsPerPage));
                    }

                    query = query.Skip((_currentResultsPage - 1) * _settings.NumResultsPerPage).Take(_settings.NumResultsPerPage);

                    List<Result> results = new List<Result>();
                    foreach (Record record in query)
                    {
                        Result result = new Result();
                        result.Id = int.Parse(record["Id"].ToString());

                        result.Type = record["Type"].ToString();
                        result.HostName = record["HostName"].ToString();
                        result.IpAddress = record["IpAddress"].ToString();
                        result.MacAddress = record["MacAddress"].ToString();
                        result.Service = record["Service"].ToString();
                        result.Protocol = record["Protocol"].ToString();

                        int port = 0;
                        if (int.TryParse(record["Port"].ToString(), out port) == true)
                        {
                            result.Port = port;
                        }

                        result.Text = record["Text"].ToString();
                        result.PluginId = record["PluginId"].ToString();
                        result.PluginName = record["PluginName"].ToString();
                        result.PluginFamily = record["PluginFamily"].ToString();
                        result.Severity = record["Severity"].ToString();
                        result.Solution = record["Solution"].ToString();
                        result.RiskFactor = record["RiskFactor"].ToString();
                        result.Description = record["Description"].ToString();
                        result.PluginPublicationDate = record["PluginPublicationDate"].ToString();
                        result.VulnPublicationDate = record["VulnPublicationDate"].ToString();
                        result.PatchPublicationDate = record["PatchPublicationDate"].ToString();
                        result.Synopsis = record["Synopsis"].ToString();
                        result.PluginOutput = record["PluginOutput"].ToString();
                        result.PluginVersion = record["PluginVersion"].ToString();
                        result.SeeAlso = record["SeeAlso"].ToString();
                        result.CvssBaseScore = record["CvssBaseScore"].ToString();
                        result.CvssVector = record["CvssVector"].ToString();
                        result.Cve = record["Cve"].ToString();
                        result.Bid = record["Bid"].ToString();
                        result.Xref = record["Xref"].ToString();
                        result.State = record["State"].ToString();
                        result.Product = record["Product"].ToString();
                        result.Version = record["Version"].ToString();
                        result.ExtraInfo = record["ExtraInfo"].ToString();

                        results.Add(result);
                    }

                    this.formList.SetResults(results);
                 
                    UpdateStatusBar("Loaded " + query.Count() + " results");
                    SetFormFilterText();
                    SetPagingButtonStatus();
                }
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private IEnumerable<Record> GetQuery(Table table)
        {
            var query = from data in table select data;

            if (cboFilterType.SelectedIndex != -1 & cboFilterType.SelectedIndex != 0)
            {
                string value = cboFilterType.Items[cboFilterType.SelectedIndex].ToString();
                query = query.Where(record => record["Type"].ToString() == value);
            }

            if (cboFilterIp.SelectedIndex != -1 & cboFilterIp.SelectedIndex != 0)
            {
                string value = cboFilterIp.Items[cboFilterIp.SelectedIndex].ToString();
                query = query.Where(result => result["IpAddress"].ToString() == value);
            }

            if (cboFilterHostName.SelectedIndex != -1 & cboFilterHostName.SelectedIndex != 0)
            {
                string value = cboFilterHostName.Items[cboFilterHostName.SelectedIndex].ToString();
                query = query.Where(result => result["HostName"].ToString() == value);
            }

            if (cboFilterPort.SelectedIndex != -1 & cboFilterPort.SelectedIndex != 0)
            {
                string value = cboFilterPort.Items[cboFilterPort.SelectedIndex].ToString();
                query = query.Where(result => result["Port"].ToString() == value);
            }

            if (cboFilterSeverity.SelectedIndex != -1 & cboFilterSeverity.SelectedIndex != 0)
            {
                string value = cboFilterSeverity.Items[cboFilterSeverity.SelectedIndex].ToString();
                query = query.Where(result => result["Severity"].ToString() == value);
            }

            if (cboFilterService.SelectedIndex != -1 & cboFilterService.SelectedIndex != 0)
            {
                string value = cboFilterService.Items[cboFilterService.SelectedIndex].ToString();
                query = query.Where(result => result["Service"].ToString() == value);
            }

            if (cboFilterState.SelectedIndex != -1 & cboFilterState.SelectedIndex != 0)
            {
                string value = cboFilterState.Items[cboFilterState.SelectedIndex].ToString();
                query = query.Where(result => result["State"].ToString() == value);
            }

            if (cboFilterProtocol.SelectedIndex != -1 & cboFilterProtocol.SelectedIndex != 0)
            {
                string value = cboFilterProtocol.Items[cboFilterProtocol.SelectedIndex].ToString();
                query = query.Where(result => result["Protocol"].ToString() == value);
            }

            if (cboFilterPluginId.SelectedIndex != -1 & cboFilterPluginId.SelectedIndex != 0)
            {
                query = query.Where(result => result["PluginId"].ToString() == cboFilterPluginId.Items[cboFilterPluginId.SelectedIndex].ToString());
            }

            if (cboFilterPluginFamilyName.SelectedIndex != -1 & cboFilterPluginFamilyName.SelectedIndex != 0)
            {
                query = query.Where(result => result["PluginFamily"].ToString() == cboFilterPluginFamilyName.Items[cboFilterPluginFamilyName.SelectedIndex].ToString());
            }

            if (cboFilterPluginName.SelectedIndex != -1 & cboFilterPluginName.SelectedIndex != 0)
            {
                query = query.Where(result => result["PluginName"].ToString() == cboFilterPluginName.Items[cboFilterPluginName.SelectedIndex].ToString());
            }

            if (cboFilterProduct.SelectedIndex != -1 & cboFilterProduct.SelectedIndex != 0)
            {
                query = query.Where(result => result["Product"].ToString() == cboFilterProduct.Items[cboFilterProduct.SelectedIndex].ToString());
            }

            if (cboFilterVersion.SelectedIndex != -1 & cboFilterVersion.SelectedIndex != 0)
            {
                query = query.Where(result => result["Version"].ToString() == cboFilterVersion.Items[cboFilterVersion.SelectedIndex].ToString());
            }

            if (cboFilterExploitAvailable.SelectedIndex != -1 & cboFilterExploitAvailable.SelectedIndex != 0)
            {
                query = query.Where(result => result["ExploitAvailable"].ToString() == cboFilterExploitAvailable.Items[cboFilterExploitAvailable.SelectedIndex].ToString());
            }

            if (txtFilterSearch.Text.Length > 0)
            {
                query = query.Where(result => result["Text"].ToString().IndexOf(txtFilterSearch.Text, StringComparison.InvariantCultureIgnoreCase) > -1);
            }

            if (IgnoredPlugins.Plugins.Count > 0)
            {
                query = from q in query where !(from p in IgnoredPlugins.Plugins select p.PluginId).Contains(q["PluginId"]) select q;
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetFormFilterText()
        {
            MethodInvoker methodInvoker = delegate
            {
                if (cboFilterIp.SelectedIndex != -1 & cboFilterIp.SelectedIndex != 0)
                {
                    this.Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterPluginFamilyName.SelectedIndex != -1 & cboFilterPluginFamilyName.SelectedIndex != 0)
                {
                    this.Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterPluginId.SelectedIndex != -1 & cboFilterPluginId.SelectedIndex != 0)
                {
                    this.Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterPluginName.SelectedIndex != -1 & cboFilterPluginName.SelectedIndex != 0)
                {
                    this.Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterPort.SelectedIndex != -1 & cboFilterPort.SelectedIndex != 0)
                {
                    this.Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterProduct.SelectedIndex != -1 & cboFilterProduct.SelectedIndex != 0)
                {
                    Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterProtocol.SelectedIndex != -1 & cboFilterProtocol.SelectedIndex != 0)
                {
                    Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterService.SelectedIndex != -1 & cboFilterService.SelectedIndex != 0)
                {
                    Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterType.SelectedIndex != -1 & cboFilterType.SelectedIndex != 0)
                {
                    Text = "NetworkScanViewer - FILTERED";
                }
                else if (cboFilterVersion.SelectedIndex != -1 & cboFilterVersion.SelectedIndex != 0)
                {
                    this.Text = "NetworkScanViewer - FILTERED";
                }
                else
                {
                    this.Text = "NetworkScanViewer";
                }
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
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
        #endregion

        #region Summary Context Menu  Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSummaryCopyIp_Click(object sender, EventArgs e)
        {
            if (listHostSummary.SelectedObjects == null)
            {
                return;
            }

            List<string> list = new List<string>();

            for (int index = 0; index < listHostSummary.SelectedObjects.Count; index++)
            {
                HostSummary hostSummary = (HostSummary)listHostSummary.SelectedObjects[index];

                list.Add(hostSummary.IpAddress);
            }

            list.Sort();

            string temp = string.Empty;
            for (int index = 0; index < list.Count; index++)
            {
                if (index + 1 != list.Count)
                {
                    temp += list[index] + Environment.NewLine;
                }
                else
                {
                    temp += list[index];
                }
            }

            if (temp.Length > 0)
            {
                Clipboard.SetText(temp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSummaryCopyNetbios_Click(object sender, EventArgs e)
        {
            if (listHostSummary.SelectedObjects == null)
            {
                return;
            }

            List<string> list = new List<string>();

            for (int index = 0; index < listHostSummary.SelectedObjects.Count; index++)
            {
                HostSummary hostSummary = (HostSummary)listHostSummary.SelectedObjects[index];

                list.Add(hostSummary.NetBiosName);
            }

            list.Sort();

            string temp = string.Empty;
            for (int index = 0; index < list.Count; index++)
            {
                if (index + 1 != list.Count)
                {
                    temp += list[index] + Environment.NewLine;
                }
                else
                {
                    temp += list[index];
                }
            }

            if (temp.Length > 0)
            {
                Clipboard.SetText(temp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSummaryCopyDns_Click(object sender, EventArgs e)
        {
            if (listHostSummary.SelectedObjects == null)
            {
                return;
            }

            List<string> list = new List<string>();

            for (int index = 0; index < listHostSummary.SelectedObjects.Count; index++)
            {
                HostSummary hostSummary = (HostSummary)listHostSummary.SelectedObjects[index];

                list.Add(hostSummary.DnsName);
            }

            list.Sort();

            string temp = string.Empty;
            for (int index = 0; index < list.Count; index++)
            {
                if (index + 1 != list.Count)
                {
                    temp += list[index] + Environment.NewLine;
                }
                else
                {
                    temp += list[index];
                }
            }

            if (temp.Length > 0)
            {
                Clipboard.SetText(temp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSummaryCopyMac_Click(object sender, EventArgs e)
        {
            if (listHostSummary.SelectedObjects == null)
            {
                return;
            }

            List<string> list = new List<string>();

            for (int index = 0; index < listHostSummary.SelectedObjects.Count; index++)
            {
                HostSummary hostSummary = (HostSummary)listHostSummary.SelectedObjects[index];

                list.Add(hostSummary.MacAddress);
            }

            list.Sort();

            string temp = string.Empty;
            for (int index = 0; index < list.Count; index++)
            {
                if (index + 1 != list.Count)
                {
                    temp += list[index] + Environment.NewLine;
                }
                else
                {
                    temp += list[index];
                }
            }

            if (temp.Length > 0)
            {
                Clipboard.SetText(temp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuSummaryCopyOs_Click(object sender, EventArgs e)
        {
            if (listHostSummary.SelectedObjects == null)
            {
                return;
            }

            List<string> list = new List<string>();

            for (int index = 0; index < listHostSummary.SelectedObjects.Count; index++)
            {
                HostSummary hostSummary = (HostSummary)listHostSummary.SelectedObjects[index];

                list.Add(hostSummary.Os);
            }

            list.Sort();

            string temp = string.Empty;
            for (int index = 0; index < list.Count; index++)
            {
                if (index + 1 != list.Count)
                {
                    temp += list[index] + Environment.NewLine;
                }
                else
                {
                    temp += list[index];
                }
            }

            if (temp.Length > 0)
            {
                Clipboard.SetText(temp);
            }
        }
        #endregion

        #region Export Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileFormat"></param>
        private void SaveResults(FileFormat fileFormat)
        {
            if (fileFormat == FileFormat.Xml)
            {
                saveFileDialog.FileName = "Results.xml";
                saveFileDialog.Filter = "XML File (*.xml)|*.xml";
            }
            else if (fileFormat == FileFormat.Csv)
            {
                saveFileDialog.FileName = "Results.csv";
                saveFileDialog.Filter = "CSV File (*.csv)|*.csv";
            }

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Length == 0)
            {
                return;
            }

            if (fileFormat == FileFormat.Xml)
            {
                XmlResultExport xmlExport = new XmlResultExport();
                foreach (Result result in this.formList.GetAllResults())
                {
                    result.PluginOutput = result.PluginOutput.Replace(Environment.NewLine, " ");

                    xmlExport.Results.Add(result);
                }

                string ret = xmlExport.Save(saveFileDialog.FileName);

                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, ret);
                }
                else
                {
                    statusLabel.Text = "Results successfully exported to XML";
                }
            }
            else if (fileFormat == FileFormat.Csv)
            {
                this.GenerateResultsCsv(saveFileDialog.FileName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        private void GenerateResultsCsv(string fileName)
        {
            if (_networkScanParser == null)
            {
                return;
            }

            string ret = IO.WriteTextToFile(string.Empty, fileName, false);
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst clearing the existing CSV file: " + ret);
                return;
            }

            using (new HourGlass(this))
            using (Connection connection = Esent.OpenDatabase(_networkScanParser.OutputPath + @"\data.edb"))
            using (Table table = connection.OpenTable("Data"))
            {
                var query = GetQuery(table);

                StringBuilder output = new StringBuilder();

                output.AppendLine("TYPE,HOST_NAME,IP_ADDRESS,MAC_ADDRESS,PORT,STATE,PROTOCOL,SERVICE,DESCRIPTION,SYNOPSIS,SOLUTION,SEVERITY,SEE_ALSO,PLUGIN_ID,PLUGIN_NAME,PLUGIN_FAMILY,PLUGIN_VERSION,PLUGIN_PUBLICATION_DATE,VULN_PUBLICATION_DATE,PATCH_PUBLICATION_DATE,RISK_FACTOR,CVSS_BASE_SCORE,CVSS_VECTOR,CVE,BID,XREF,PRODUCT,VERSION,EXTRA_INFO,PLUGIN_OUTPUT");

                foreach (Record record in query)
                {
                    output.Append(@"""");
                    output.Append(FormatCsvText(record["Type"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["HostName"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["IpAddress"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["MacAddress"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Port"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["State"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Protocol"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Service"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Description"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Synopsis"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Solution"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Severity"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["SeeAlso"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PluginId"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PluginName"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PluginFamily"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PluginVersion"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PluginPublicationDate"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["VulnPublicationDate"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PatchPublicationDate"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["RiskFactor"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["CvssBaseScore"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["CvssVector"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Cve"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Bid"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Xref"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Product"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["Version"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["ExtraInfo"].ToString(), _settings.RemoveNewLinesOnExport));
                    output.Append(@""",""");
                    output.Append(FormatCsvText(record["PluginOutput"].ToString(), _settings.RemoveNewLinesOnExport));

                    output.Append(@"""");
                    output.Append(Environment.NewLine);

                    if (output.Length >= 1000)
                    {
                        string ret2 = IO.WriteTextToFile(output.ToString(), fileName, true);
                        if (ret2.Length > 0)
                        {
                            UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst writing the export CSV: " + ret2);
                            return;
                        }

                        output = new StringBuilder();
                    }
                }

                ret = IO.WriteTextToFile(output.ToString(), fileName, true);
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst writing the export CSV: " + ret);
                    return;
                }

                statusLabel.Text = "Results successfully exported to CSV";

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileFormat"></param>
        private void SaveHostSummarys(FileFormat fileFormat)
        {
            if (fileFormat == FileFormat.Xml)
            {
                saveFileDialog.FileName = "HostSummaries.xml";
                saveFileDialog.Filter = "XML File (*.xml)|*.xml";
            }
            else if (fileFormat == FileFormat.Csv)
            {
                saveFileDialog.FileName = "HostSummaries.csv";
                saveFileDialog.Filter = "CSV File (*.csv)|*.csv";
            }

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (saveFileDialog.FileName.Length == 0)
            {
                return;
            }

            if (fileFormat == FileFormat.Xml)
            {
                //XmlHostSummaryExport xmlExport = new XmlHostSummaryExport();
                //foreach (HostSummary hostSummary in listHostSummary.Objects)
                //{
                //    xmlExport.HostSummaries.Add(hostSummary);
                //}

                //string ret = xmlExport.Save(saveFileDialog.FileName);

                //if (ret.Length > 0)
                //{
                //    UserInterface.DisplayErrorMessageBox(this, ret);
                //}
                //else
                //{
                //    statusLabel.Text = "Results successfully exported to XML";
                //}
            }
            else if (fileFormat == FileFormat.Csv)
            {
                string temp = this.GenerateHostSummaryCsv();
                if (temp.Length > 0)
                {
                    string ret = IO.WriteTextToFile(temp, saveFileDialog.FileName, false);
                    if (ret.Length > 0)
                    {
                        UserInterface.DisplayErrorMessageBox(this, ret);
                    }
                    else
                    {
                        statusLabel.Text = "Host summaries successfully exported to CSV";
                    }
                }
                else
                {
                    UserInterface.DisplayErrorMessageBox(this, "No host summaries to export");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string GenerateHostSummaryCsv()
        {
            if (_networkScanParser == null)
            {
                return string.Empty;
            }

            StringBuilder output = new StringBuilder();

            output.AppendLine("IP_ADDRESS,NETBIOS_NAME,DNS_NAME,MAC_ADDRESS,OS,PORTS,NUM_CRITICAL,NUM_HIGH,NUM_MED,NUM_LOW,START_TIME,END_TIME");

            foreach (HostSummary hostSummary in listHostSummary.Objects)
            {
                StringBuilder pluginOutput = new StringBuilder();

                output.Append(@"""");
                output.Append(FormatCsvText(hostSummary.IpAddress, _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.NetBiosName, _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.DnsName, _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.MacAddress, _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.Os, _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.NumPorts.ToString(), _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.NumCritical.ToString(), _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.NumHigh.ToString(), _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.NumMed.ToString(), _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.NumLow.ToString(), _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.StartTime, _settings.RemoveNewLinesOnExport));
                output.Append(@""",""");
                output.Append(FormatCsvText(hostSummary.EndTime, _settings.RemoveNewLinesOnExport));

                output.Append(@"""");
                output.Append(Environment.NewLine);
            }

            return output.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string FormatCsvText(string text, bool removeNewLines)
        {
            if (removeNewLines == true)
            {
                text = text.Replace("\"", "\"\"");
                text = text.Replace(Environment.NewLine, string.Empty);
                text = text.Replace("\n", string.Empty);
                text = text.Replace("\r", string.Empty);
            }
            else
            {
                text = text.Replace("\"", "\"\"");
                text = text.Replace(Environment.NewLine, "\n");
                text = text.Replace("\n", "\r");
            }
            
            return text;
        }
        #endregion

        #region Toolbar Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBtnSearch_Click(object sender, EventArgs e)
        {
            LoadResults(1);
        }
        #endregion
         
        #region Textbox Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilterSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadResults(1);
            }
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            _settings = new Settings();
            if (_settings.FileExists == true)
            {
                string ret = _settings.Load();
                if (ret.Length > 0)
                {
                    UserInterface.DisplayErrorMessageBox(this, ret);
                }
                else
                {
                    this.WindowState = _settings.FormState;

                    if (_settings.FormState != FormWindowState.Maximized)
                    {
                        this.Location = _settings.FormLocation;
                        this.Size = _settings.FormSize;
                    }

                    base.TopMost = _settings.AlwaysOnTop;
                }
            }

            IgnoredPlugins = new IgnorePlugins();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.FormLocation = base.Location;
            this._settings.FormSize = base.Size;
            this._settings.FormState = base.WindowState;
            string ret = this._settings.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, ret);
            }

            ret = IgnoredPlugins.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, ret);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        private void UpdateStatusBar(string text)
        {
            MethodInvoker methodInvoker = delegate
            {
                statusLabel.Text = text;
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultsFirstPage_Click(object sender, EventArgs e)
        {
            LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultsPreviousPage_Click(object sender, EventArgs e)
        {
            LoadResults(_currentResultsPage - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultsLastPage_Click(object sender, EventArgs e)
        {
            LoadResults(_totalResultsPages);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResultsNextPage_Click(object sender, EventArgs e)
        {
            LoadResults(_currentResultsPage + 1);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void SetPagingButtonStatus()
        {
            //if (_totalResultsPages == 0)
            //{
            //    btnResultsFirstPage.Enabled = false;
            //    btnResultsLastPage.Enabled = false;
            //    btnResultsNextPage.Enabled = false;
            //    btnResultsPreviousPage.Enabled = false;
            //}
            //else
            //{
            //    btnResultsFirstPage.Enabled = (_currentResultsPage != 1);
            //    btnResultsLastPage.Enabled = (_currentResultsPage < _totalResultsPages);
            //    btnResultsNextPage.Enabled = (_currentResultsPage < _totalResultsPages);
            //    btnResultsPreviousPage.Enabled = (_currentResultsPage != 1);
            //}
        }
    }
}
