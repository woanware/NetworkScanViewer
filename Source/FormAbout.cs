/* 
Copyright 2011 Mark Woan (markwoan@gmail.com)

This file is part of RegExtract.

RegExtract is free software: you can redistribute it and/or modify it under the terms of the 
GNU General Public License as published by the Free Software Foundation, either version 3 of 
the License, or (at your option) any later version.

RegExtract is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License for more details.

You should have received a copy of the GNU General Public License along with RegExtract. If not, 
see http://www.gnu.org/licenses/.
*/

using System.Windows.Forms;
using System.Drawing;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormAbout : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();

            lblApp.Text = Application.ProductName;
            lblVer.Text = "v" + Application.ProductVersion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkEmail_LinkClicked(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.RedirectStandardOutput = false;
           // process.StartInfo.FileName = "mailto:" + linkEmail.Text;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkWeb_LinkClicked(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.RedirectStandardOutput = false;
           // process.StartInfo.FileName = "http://" + linkEmail.Text;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
