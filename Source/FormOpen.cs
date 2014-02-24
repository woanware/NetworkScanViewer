using System;
using System.Windows.Forms;
using System.IO;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormOpen : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormOpen()
        {
            InitializeComponent();
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Select the input folder...";
            if (System.IO.Directory.Exists(txtInputFolder.Text) == true)
            {
                folderBrowserDialog.SelectedPath = txtInputFolder.Text;
            }

            if (folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            txtInputFolder.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = "Select the output folder...";
            if (System.IO.Directory.Exists(txtOutputFolder.Text) == true)
            {
                folderBrowserDialog.SelectedPath = txtOutputFolder.Text;
            }

            if (folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            txtOutputFolder.Text = folderBrowserDialog.SelectedPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtInputFolder.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The input directory must be selected", MessageBoxIcon.Exclamation);
                txtInputFolder.Select();
                return;
            }

            if (System.IO.Directory.Exists(txtInputFolder.Text) == false)
            {
                UserInterface.DisplayMessageBox(this, "The input directory does not exist", MessageBoxIcon.Exclamation);
                txtInputFolder.Select();
                return;
            }

            if (txtOutputFolder.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The output directory must be selected", MessageBoxIcon.Exclamation);
                txtOutputFolder.Select();
                return;
            }

            if (System.IO.Directory.Exists(txtOutputFolder.Text) == false)
            {
                UserInterface.DisplayMessageBox(this, "The output directory does not exist", MessageBoxIcon.Exclamation);
                txtOutputFolder.Select();
                return;
            }

            if (chkDeleteExistingDatabase.Checked == true)
            {   
                if (System.IO.File.Exists(System.IO.Path.Combine(txtOutputFolder.Text, "data.edb")) == true)
                {
                    string errors = string.Empty;
                    string ret = IO.DeleteFiles(txtOutputFolder.Text, "*.log");
                    if (ret.Length > 0)
                    {
                        errors += ret + Environment.NewLine;
                    }

                    ret = IO.DeleteFiles(txtOutputFolder.Text, "*.jrs");
                    if (ret.Length > 0)
                    {
                        errors += ret + Environment.NewLine;
                    }

                    ret = IO.DeleteFiles(txtOutputFolder.Text, "*.edb");
                    if (ret.Length > 0)
                    {
                        errors += ret + Environment.NewLine;
                    }

                    ret = IO.DeleteFiles(txtOutputFolder.Text, "*.chk");
                    if (ret.Length > 0)
                    {
                        errors += ret + Environment.NewLine;
                    }

                    if (errors.Length > 0)
                    {
                        UserInterface.DisplayErrorMessageBox(this, "An error occurred whilst deleting the existing database: " + errors.Trim());
                    }
                }
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

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public string InputFolder
        {
            get
            {
                return txtInputFolder.Text;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string OutputFolder
        {
            get
            {
                return txtOutputFolder.Text;
            }
        }
        #endregion
    }
}
