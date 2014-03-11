namespace woanware
{
    partial class FormOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.chkMoveFocusToList = new System.Windows.Forms.CheckBox();
            this.chkRemoveNewLinesOnExport = new System.Windows.Forms.CheckBox();
            this.lblNumResultsPerPage = new System.Windows.Forms.Label();
            this.cboNumResultsPerPage = new System.Windows.Forms.ComboBox();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.chkColourSevere = new System.Windows.Forms.CheckBox();
            this.tabIgnoredPlugins = new System.Windows.Forms.TabPage();
            this.listPlugins = new BrightIdeasSoftware.ObjectListView();
            this.olvcPluginId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPluginName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnRemovePlugin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabIgnoredPlugins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listPlugins)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabIgnoredPlugins);
            this.tabControl.Location = new System.Drawing.Point(8, 8);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(547, 330);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.chkMoveFocusToList);
            this.tabGeneral.Controls.Add(this.chkRemoveNewLinesOnExport);
            this.tabGeneral.Controls.Add(this.lblNumResultsPerPage);
            this.tabGeneral.Controls.Add(this.cboNumResultsPerPage);
            this.tabGeneral.Controls.Add(this.chkAlwaysOnTop);
            this.tabGeneral.Controls.Add(this.chkColourSevere);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(539, 302);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chkMoveFocusToList
            // 
            this.chkMoveFocusToList.AutoSize = true;
            this.chkMoveFocusToList.Location = new System.Drawing.Point(4, 110);
            this.chkMoveFocusToList.Name = "chkMoveFocusToList";
            this.chkMoveFocusToList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkMoveFocusToList.Size = new System.Drawing.Size(216, 19);
            this.chkMoveFocusToList.TabIndex = 5;
            this.chkMoveFocusToList.Text = "Move focus to list after filter change";
            this.chkMoveFocusToList.UseVisualStyleBackColor = true;
            // 
            // chkRemoveNewLinesOnExport
            // 
            this.chkRemoveNewLinesOnExport.AutoSize = true;
            this.chkRemoveNewLinesOnExport.Location = new System.Drawing.Point(46, 82);
            this.chkRemoveNewLinesOnExport.Name = "chkRemoveNewLinesOnExport";
            this.chkRemoveNewLinesOnExport.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkRemoveNewLinesOnExport.Size = new System.Drawing.Size(174, 19);
            this.chkRemoveNewLinesOnExport.TabIndex = 4;
            this.chkRemoveNewLinesOnExport.Text = "Remove new lines on export";
            this.chkRemoveNewLinesOnExport.UseVisualStyleBackColor = true;
            // 
            // lblNumResultsPerPage
            // 
            this.lblNumResultsPerPage.AutoSize = true;
            this.lblNumResultsPerPage.Location = new System.Drawing.Point(86, 57);
            this.lblNumResultsPerPage.Name = "lblNumResultsPerPage";
            this.lblNumResultsPerPage.Size = new System.Drawing.Size(115, 15);
            this.lblNumResultsPerPage.TabIndex = 3;
            this.lblNumResultsPerPage.Text = "No. Results Per Page";
            // 
            // cboNumResultsPerPage
            // 
            this.cboNumResultsPerPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNumResultsPerPage.FormattingEnabled = true;
            this.cboNumResultsPerPage.Items.AddRange(new object[] {
            "1000",
            "2000",
            "3000",
            "4000",
            "5000",
            "10000",
            "20000"});
            this.cboNumResultsPerPage.Location = new System.Drawing.Point(206, 53);
            this.cboNumResultsPerPage.Name = "cboNumResultsPerPage";
            this.cboNumResultsPerPage.Size = new System.Drawing.Size(77, 23);
            this.cboNumResultsPerPage.TabIndex = 2;
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(119, 29);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(101, 19);
            this.chkAlwaysOnTop.TabIndex = 1;
            this.chkAlwaysOnTop.Text = "Always on top";
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // chkColourSevere
            // 
            this.chkColourSevere.AutoSize = true;
            this.chkColourSevere.Location = new System.Drawing.Point(21, 6);
            this.chkColourSevere.Name = "chkColourSevere";
            this.chkColourSevere.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkColourSevere.Size = new System.Drawing.Size(199, 19);
            this.chkColourSevere.TabIndex = 0;
            this.chkColourSevere.Text = "Colour severe items  e.g. Risk > 1";
            this.chkColourSevere.UseVisualStyleBackColor = true;
            // 
            // tabIgnoredPlugins
            // 
            this.tabIgnoredPlugins.Controls.Add(this.listPlugins);
            this.tabIgnoredPlugins.Controls.Add(this.btnRemovePlugin);
            this.tabIgnoredPlugins.Location = new System.Drawing.Point(4, 24);
            this.tabIgnoredPlugins.Name = "tabIgnoredPlugins";
            this.tabIgnoredPlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabIgnoredPlugins.Size = new System.Drawing.Size(539, 296);
            this.tabIgnoredPlugins.TabIndex = 1;
            this.tabIgnoredPlugins.Text = "Ignored Plugins";
            this.tabIgnoredPlugins.UseVisualStyleBackColor = true;
            // 
            // listPlugins
            // 
            this.listPlugins.AllColumns.Add(this.olvcPluginId);
            this.listPlugins.AllColumns.Add(this.olvcPluginName);
            this.listPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcPluginId,
            this.olvcPluginName});
            this.listPlugins.Location = new System.Drawing.Point(6, 6);
            this.listPlugins.Name = "listPlugins";
            this.listPlugins.Size = new System.Drawing.Size(526, 252);
            this.listPlugins.TabIndex = 2;
            this.listPlugins.UseCompatibleStateImageBehavior = false;
            this.listPlugins.View = System.Windows.Forms.View.Details;
            // 
            // olvcPluginId
            // 
            this.olvcPluginId.AspectName = "PluginId";
            this.olvcPluginId.CellPadding = null;
            this.olvcPluginId.Text = "ID";
            // 
            // olvcPluginName
            // 
            this.olvcPluginName.AspectName = "PluginName";
            this.olvcPluginName.CellPadding = null;
            this.olvcPluginName.Text = "Name";
            // 
            // btnRemovePlugin
            // 
            this.btnRemovePlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemovePlugin.Location = new System.Drawing.Point(5, 263);
            this.btnRemovePlugin.Name = "btnRemovePlugin";
            this.btnRemovePlugin.Size = new System.Drawing.Size(87, 27);
            this.btnRemovePlugin.TabIndex = 1;
            this.btnRemovePlugin.Text = "Remove";
            this.btnRemovePlugin.UseVisualStyleBackColor = true;
            this.btnRemovePlugin.Click += new System.EventHandler(this.btnRemovePlugin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(478, 341);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(399, 341);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(562, 372);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(578, 409);
            this.Name = "FormOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabIgnoredPlugins.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listPlugins)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabIgnoredPlugins;
        private System.Windows.Forms.CheckBox chkColourSevere;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnRemovePlugin;
        private System.Windows.Forms.Label lblNumResultsPerPage;
        private System.Windows.Forms.ComboBox cboNumResultsPerPage;
        private System.Windows.Forms.CheckBox chkRemoveNewLinesOnExport;
        private BrightIdeasSoftware.ObjectListView listPlugins;
        private BrightIdeasSoftware.OLVColumn olvcPluginId;
        private BrightIdeasSoftware.OLVColumn olvcPluginName;
        private System.Windows.Forms.CheckBox chkMoveFocusToList;
    }
}