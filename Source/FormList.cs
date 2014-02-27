using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace woanware
{
    /// <summary>
    /// Floating window containing the listview of results
    /// </summary>
    public partial class FormList : DockContent
    {
        #region Events
        public event ResultSelected ResultSelectedEvent;
        public delegate void ResultSelected(Result result);
        #endregion

        #region Member Variables
        private bool isLoading = false;
        private FormMain formMain;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormList()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets a reference to the main form so that we can its properties  
        /// and methods now that we have implemented the floating docks etc
        /// </summary>
        /// <param name="formMain"></param>
        public void SetFormMain(FormMain formMain)
        {
            this.formMain = formMain;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetFocusToList()
        {
            this.listResults.Select();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Result GetSelectedResult()
        {
            if (listResults.SelectedObject == null)
            {
                return null;
            }

            if (isLoading == true)
            {
                return null;
            }

            return (Result)listResults.SelectedObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Result GetFirstSelectedResult()
        {
            if (listResults.SelectedObjects == null)
            {
                return null;
            }

            return (Result)listResults.SelectedObjects[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSelectedIpAddresses()
        {
            List<IpAddress> list = new List<IpAddress>();

            for (int index = 0; index < listResults.SelectedObjects.Count; index++)
            {
                Result result = (Result)listResults.SelectedObjects[index];

                IpAddress ipAddress = new IpAddress();
                ipAddress.Text = result.IpAddress;

                var temp = from l in list where l.Text == result.IpAddress select l;
                if (temp.Count() == 0)
                {
                    list.Add(ipAddress);
                }
            }

            list.Sort();
            return string.Join(Environment.NewLine, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSelectedHostNames()
        {
            List<string> list = new List<string>();

            for (int index = 0; index < listResults.SelectedObjects.Count; index++)
            {
                Result result = (Result)listResults.SelectedObjects[index];

                if (list.Contains(result.HostName) == false)
                {
                    list.Add(result.HostName);
                }
            }

            list.Sort();
            return string.Join(Environment.NewLine, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSelectedPorts()
        {
            List<int> list = new List<int>();

            for (int index = 0; index < listResults.SelectedObjects.Count; index++)
            {
                Result result = (Result)listResults.SelectedObjects[index];

                if (list.Contains(result.Port) == false)
                {
                    list.Add(result.Port);
                }
            }

            list.Sort(); 
            return string.Join(Environment.NewLine, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetSelectedServices()
        {
            List<string> list = new List<string>();

            for (int index = 0; index < listResults.SelectedObjects.Count; index++)
            {
                Result result = (Result)listResults.SelectedObjects[index];

                if (list.Contains(result.Service) == false)
                {
                    list.Add(result.Service);
                }
            }

            list.Sort();
            return string.Join(Environment.NewLine, list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        public void SetResults(List<Result> results)
        {
            listResults.ClearObjects();
            listResults.SetObjects(results);
        }
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int ListSelectedCount
        {
            get
            {
                if (listResults.SelectedObjects == null)
                {
                    return 0;
                }

                return listResults.SelectedObjects.Count;
            }
        }
        #endregion

        #region Listview Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = true;
            if (this.ListSelectedCount == 0)
            {
                enabled = false;
            }

            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            OnResultSelected(result);

            ctxMenuResultsCopyIpAddress.Enabled = enabled;
            ctxMenuResultsCopyPort.Enabled = enabled;
            ctxMenuResultsCopyService.Enabled = enabled;

            ctxMenuResultsFilterHost.Enabled = enabled;
            ctxMenuResultsFilterPluginFamily.Enabled = enabled;
            ctxMenuResultsFilterPluginId.Enabled = enabled;
            ctxMenuResultsFilterPluginName.Enabled = enabled;
            ctxMenuResultsFilterPort.Enabled = enabled;
            ctxMenuResultsFilterProduct.Enabled = enabled;
            ctxMenuResultsFilterProtocol.Enabled = enabled;
            ctxMenuResultsFilterService.Enabled = enabled;
            ctxMenuResultsFilterSeverity.Enabled = enabled;
            ctxMenuResultsFilterType.Enabled = enabled;
            ctxMenuResultsFilterVersion.Enabled = enabled;
        }
        #endregion

        #region Results Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterType_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterType, result.Type);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterHost_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterIp, result.IpAddress);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterPort_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterPort, result.Port.ToString());
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterProtocol_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterProtocol, result.Protocol);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterService_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterService, result.Service);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterSeverity_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterSeverity, result.IpAddress);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterPluginId_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterPluginId, result.PluginId);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterPluginFamily_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterPluginFamilyName, result.PluginFamily);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterPluginName_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterPluginName, result.PluginName);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterProduct_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterProduct, result.Product);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsFilterVersion_Click(object sender, EventArgs e)
        {
            Result result = this.GetSelectedResult();
            if (result == null)
            {
                return;
            }

            this.formMain.SetFilterComboBoxItem(this.formMain.cboFilterVersion, result.Version);
            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsClearFilters_Click(object sender, EventArgs e)
        {
            this.formMain.ClearFilters();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResults_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.ListSelectedCount == 0)
            {
                ctxMenuResultsCopyIpAddress.Enabled = false;
                ctxMenuResultsCopyPort.Enabled = false;
                ctxMenuResultsCopyService.Enabled = false;

                ctxMenuResultsCopyIpAddress.Text = "IP Address";
                ctxMenuResultsCopyPort.Text = "Port";
                ctxMenuResultsCopyService.Text = "Service";

                ctxMenuResultsClearFilters.Enabled = true;
                ctxMenuResultsFilterHost.Enabled = false;
                ctxMenuResultsFilterPluginFamily.Enabled = false;
                ctxMenuResultsFilterPluginId.Enabled = false;
                ctxMenuResultsFilterPort.Enabled = false;
                ctxMenuResultsFilterProduct.Enabled = false;
                ctxMenuResultsFilterService.Enabled = false;
                ctxMenuResultsFilterSeverity.Enabled = false;
                ctxMenuResultsFilterType.Enabled = false;
                ctxMenuResultsFilterVersion.Enabled = false;

                ctxMenuResultsIgnorePlugin.Enabled = false;
            }
            else if (this.ListSelectedCount == 1)
            {
                ctxMenuResultsCopyIpAddress.Enabled = true;
                ctxMenuResultsCopyPort.Enabled = true;
                ctxMenuResultsCopyService.Enabled = true;

                ctxMenuResultsCopyIpAddress.Text = "IP Address";
                ctxMenuResultsCopyPort.Text = "Port";
                ctxMenuResultsCopyService.Text = "Service";

                ctxMenuResultsClearFilters.Enabled = true;
                ctxMenuResultsFilterHost.Enabled = true;
                ctxMenuResultsFilterPluginFamily.Enabled = true;
                ctxMenuResultsFilterPluginId.Enabled = true;
                ctxMenuResultsFilterPort.Enabled = true;
                ctxMenuResultsFilterProduct.Enabled = true;
                ctxMenuResultsFilterService.Enabled = true;
                ctxMenuResultsFilterSeverity.Enabled = true;
                ctxMenuResultsFilterType.Enabled = true;
                ctxMenuResultsFilterVersion.Enabled = true;

                Result result = this.GetFirstSelectedResult();
                if (result.PluginId.Length > 0 & result.PluginName.Length > 0)
                {
                    ctxMenuResultsIgnorePlugin.Enabled = true;
                }
                else
                {
                    ctxMenuResultsIgnorePlugin.Enabled = false;
                }
            }
            else
            {
                ctxMenuResultsCopyIpAddress.Enabled = true;
                ctxMenuResultsCopyPort.Enabled = true;
                ctxMenuResultsCopyService.Enabled = true;

                ctxMenuResultsCopyIpAddress.Text = "IP Addresses";
                ctxMenuResultsCopyPort.Text = "Ports";
                ctxMenuResultsCopyService.Text = "Services";

                ctxMenuResultsClearFilters.Enabled = true;
                ctxMenuResultsFilterHost.Enabled = false;
                ctxMenuResultsFilterPluginFamily.Enabled = false;
                ctxMenuResultsFilterPluginId.Enabled = false;
                ctxMenuResultsFilterPort.Enabled = false;
                ctxMenuResultsFilterProduct.Enabled = false;
                ctxMenuResultsFilterService.Enabled = false;
                ctxMenuResultsFilterSeverity.Enabled = false;
                ctxMenuResultsFilterType.Enabled = false;
                ctxMenuResultsFilterVersion.Enabled = false;

                ctxMenuResultsIgnorePlugin.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsIgnorePlugin_Click(object sender, EventArgs e)
        {
            if (this.ListSelectedCount == 0)
            {
                return;
            }

            Result result = this.GetFirstSelectedResult();
            if (result == null)
            {
                return;
            }

            Plugin plugin = new Plugin();
            plugin.PluginId = result.PluginId;
            plugin.PluginName = result.PluginName;

            this.formMain.IgnoredPlugins.Plugins.Add(plugin);

            this.formMain.LoadResults(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsCopyIpAddress_Click(object sender, EventArgs e)
        {
            if (this.ListSelectedCount == 0)
            {
                return;
            }

            Clipboard.SetText(this.GetSelectedIpAddresses());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsCopyPort_Click(object sender, EventArgs e)
        {
            if (this.ListSelectedCount == 0)
            {
                return;
            }

            Clipboard.SetText(this.GetSelectedPorts());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsCopyService_Click(object sender, EventArgs e)
        {
            if (this.ListSelectedCount == 0)
            {
                return;
            }

            Clipboard.SetText(this.GetSelectedServices());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctxMenuResultsCopyHostName_Click(object sender, EventArgs e)
        {
            if (this.ListSelectedCount == 0)
            {
                return;
            }

            Clipboard.SetText(this.GetSelectedHostNames());
        }
        #endregion

        #region Event Handler Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void OnResultSelected(Result result)
        {
            var handler = ResultSelectedEvent;
            if (handler != null)
            {
                handler(result);
            }
        }
        #endregion 
    }
}
