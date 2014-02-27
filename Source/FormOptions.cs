using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormOptions : Form
    {
        #region Member Variables
        public Settings Settings { get; private set; }
        public IgnorePlugins IgnorePlugins { get; private set; }
        public bool NeedsReload { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="ignorePlugins"></param>
        public FormOptions(Settings settings, 
                           IgnorePlugins ignorePlugins)
        {
            InitializeComponent();

            Settings = settings;
            IgnorePlugins = ignorePlugins;

            chkAlwaysOnTop.Checked = Settings.AlwaysOnTop;
            chkColourSevere.Checked = Settings.ColourSevereItems;
            chkRemoveNewLinesOnExport.Checked = Settings.RemoveNewLinesOnExport;
            chkMoveFocusToList.Checked = Settings.MoveFocusToList;

            UserInterface.LocateAndSelectComboBoxValue(Settings.NumResultsPerPage.ToString(), cboNumResultsPerPage);

            listPlugins.SetObjects(ignorePlugins.Plugins);

            if (ignorePlugins.Plugins.Count == 0)
            {
                btnRemovePlugin.Enabled = false;
                olvcPluginId.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                olvcPluginName.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            else
            {
                listPlugins.SelectedObject = ignorePlugins.Plugins[0];
                olvcPluginId.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                olvcPluginName.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemovePlugin_Click(object sender, EventArgs e)
        {
            foreach (Plugin plugin in listPlugins.SelectedObjects)
            {
                listPlugins.RemoveObject(plugin);
            }

            this.NeedsReload = true;

            if (listPlugins.Items.Count == 0)
            {
                btnRemovePlugin.Enabled = false;
            }
            else
            {
                btnRemovePlugin.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Settings.ColourSevereItems = chkColourSevere.Checked;
            Settings.AlwaysOnTop = chkAlwaysOnTop.Checked;
            Settings.RemoveNewLinesOnExport = chkRemoveNewLinesOnExport.Checked;
            Settings.MoveFocusToList = chkMoveFocusToList.Checked;

            if (cboNumResultsPerPage.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The no. results per page value must be selected", MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                Settings.NumResultsPerPage = int.Parse(cboNumResultsPerPage.Items[cboNumResultsPerPage.SelectedIndex].ToString());
            }

            IgnorePlugins = new IgnorePlugins();
            foreach (Plugin plugin in listPlugins.Objects)
            {
                Plugin temp = new Plugin();
                temp.PluginId = plugin.PluginId;
                temp.PluginName = plugin.PluginName;
                IgnorePlugins.Plugins.Add(temp);
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
