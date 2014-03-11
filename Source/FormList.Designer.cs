namespace woanware
{
    partial class FormList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormList));
            this.listResults = new BrightIdeasSoftware.FastObjectListView();
            this.colResultsType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsIpAddress = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsHostName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsPort = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsProtocol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsState = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsService = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsSeverity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsSynopsis = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsPluginId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsPluginName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.colResultsPluginFamily = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ctxMenuResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuResultsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsCopyIpAddress = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsCopyPort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsCopyService = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsCopyHostName = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsSepOne = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuResultsFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterType = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterHost = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterPort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterProtocol = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterService = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterSeverity = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterPluginId = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterPluginFamily = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterPluginName = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsFilterVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuResultsSepTwo = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuResultsIgnorePlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuResultsClearFilters = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResultsLastPage = new System.Windows.Forms.Button();
            this.btnResultsNextPage = new System.Windows.Forms.Button();
            this.btnResultsPreviousPage = new System.Windows.Forms.Button();
            this.btnResultsFirstPage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listResults)).BeginInit();
            this.ctxMenuResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // listResults
            // 
            this.listResults.AllColumns.Add(this.colResultsType);
            this.listResults.AllColumns.Add(this.colResultsIpAddress);
            this.listResults.AllColumns.Add(this.colResultsHostName);
            this.listResults.AllColumns.Add(this.colResultsPort);
            this.listResults.AllColumns.Add(this.colResultsProtocol);
            this.listResults.AllColumns.Add(this.colResultsState);
            this.listResults.AllColumns.Add(this.colResultsService);
            this.listResults.AllColumns.Add(this.colResultsSeverity);
            this.listResults.AllColumns.Add(this.colResultsSynopsis);
            this.listResults.AllColumns.Add(this.colResultsPluginId);
            this.listResults.AllColumns.Add(this.colResultsPluginName);
            this.listResults.AllColumns.Add(this.colResultsPluginFamily);
            this.listResults.AllowColumnReorder = true;
            this.listResults.AlternateRowBackColor = System.Drawing.Color.LightGray;
            this.listResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colResultsType,
            this.colResultsIpAddress,
            this.colResultsHostName,
            this.colResultsPort,
            this.colResultsProtocol,
            this.colResultsState,
            this.colResultsService,
            this.colResultsSeverity,
            this.colResultsSynopsis,
            this.colResultsPluginId,
            this.colResultsPluginName,
            this.colResultsPluginFamily});
            this.listResults.ContextMenuStrip = this.ctxMenuResults;
            this.listResults.FullRowSelect = true;
            this.listResults.HasCollapsibleGroups = false;
            this.listResults.HideSelection = false;
            this.listResults.Location = new System.Drawing.Point(0, 0);
            this.listResults.Name = "listResults";
            this.listResults.ShowGroups = false;
            this.listResults.Size = new System.Drawing.Size(678, 293);
            this.listResults.TabIndex = 1;
            this.listResults.UseAlternatingBackColors = true;
            this.listResults.UseCompatibleStateImageBehavior = false;
            this.listResults.UseExplorerTheme = true;
            this.listResults.View = System.Windows.Forms.View.Details;
            this.listResults.VirtualMode = true;
            this.listResults.SelectedIndexChanged += new System.EventHandler(this.listResults_SelectedIndexChanged);
            // 
            // colResultsType
            // 
            this.colResultsType.AspectName = "Type";
            this.colResultsType.CellPadding = null;
            this.colResultsType.IsEditable = false;
            this.colResultsType.Text = "Type";
            // 
            // colResultsIpAddress
            // 
            this.colResultsIpAddress.AspectName = "IpAddress";
            this.colResultsIpAddress.CellPadding = null;
            this.colResultsIpAddress.IsEditable = false;
            this.colResultsIpAddress.Text = "IP Address";
            this.colResultsIpAddress.Width = 100;
            // 
            // colResultsHostName
            // 
            this.colResultsHostName.AspectName = "HostName";
            this.colResultsHostName.CellPadding = null;
            this.colResultsHostName.Text = "Host Name";
            // 
            // colResultsPort
            // 
            this.colResultsPort.AspectName = "Port";
            this.colResultsPort.CellPadding = null;
            this.colResultsPort.IsEditable = false;
            this.colResultsPort.Text = "Port";
            // 
            // colResultsProtocol
            // 
            this.colResultsProtocol.AspectName = "Protocol";
            this.colResultsProtocol.CellPadding = null;
            this.colResultsProtocol.IsEditable = false;
            this.colResultsProtocol.Text = "Protocol";
            // 
            // colResultsState
            // 
            this.colResultsState.AspectName = "State";
            this.colResultsState.CellPadding = null;
            this.colResultsState.Text = "State";
            // 
            // colResultsService
            // 
            this.colResultsService.AspectName = "Service";
            this.colResultsService.CellPadding = null;
            this.colResultsService.IsEditable = false;
            this.colResultsService.Text = "Service";
            this.colResultsService.Width = 100;
            // 
            // colResultsSeverity
            // 
            this.colResultsSeverity.AspectName = "Severity";
            this.colResultsSeverity.CellPadding = null;
            this.colResultsSeverity.IsEditable = false;
            this.colResultsSeverity.Text = "Severity";
            // 
            // colResultsSynopsis
            // 
            this.colResultsSynopsis.AspectName = "Synopsis";
            this.colResultsSynopsis.CellPadding = null;
            this.colResultsSynopsis.FillsFreeSpace = true;
            this.colResultsSynopsis.IsEditable = false;
            this.colResultsSynopsis.Text = "Synopsis";
            this.colResultsSynopsis.Width = 100;
            // 
            // colResultsPluginId
            // 
            this.colResultsPluginId.AspectName = "PluginId";
            this.colResultsPluginId.CellPadding = null;
            this.colResultsPluginId.IsEditable = false;
            this.colResultsPluginId.Text = "Plugin ID";
            this.colResultsPluginId.Width = 100;
            // 
            // colResultsPluginName
            // 
            this.colResultsPluginName.AspectName = "PluginName";
            this.colResultsPluginName.CellPadding = null;
            this.colResultsPluginName.IsEditable = false;
            this.colResultsPluginName.Text = "Plugin Name";
            this.colResultsPluginName.Width = 100;
            // 
            // colResultsPluginFamily
            // 
            this.colResultsPluginFamily.AspectName = "PluginFamily";
            this.colResultsPluginFamily.CellPadding = null;
            this.colResultsPluginFamily.IsEditable = false;
            this.colResultsPluginFamily.Text = "Plugin Family";
            this.colResultsPluginFamily.Width = 100;
            // 
            // ctxMenuResults
            // 
            this.ctxMenuResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuResultsCopy,
            this.ctxMenuResultsSepOne,
            this.ctxMenuResultsFilter,
            this.ctxMenuResultsSepTwo,
            this.ctxMenuResultsIgnorePlugins,
            this.toolStripMenuItem2,
            this.ctxMenuResultsClearFilters});
            this.ctxMenuResults.Name = "ctxMenuResults";
            this.ctxMenuResults.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ctxMenuResults.Size = new System.Drawing.Size(153, 132);
            // 
            // ctxMenuResultsCopy
            // 
            this.ctxMenuResultsCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuResultsCopyIpAddress,
            this.ctxMenuResultsCopyPort,
            this.ctxMenuResultsCopyService,
            this.ctxMenuResultsCopyHostName});
            this.ctxMenuResultsCopy.Name = "ctxMenuResultsCopy";
            this.ctxMenuResultsCopy.Size = new System.Drawing.Size(152, 22);
            this.ctxMenuResultsCopy.Text = "Copy";
            // 
            // ctxMenuResultsCopyIpAddress
            // 
            this.ctxMenuResultsCopyIpAddress.Name = "ctxMenuResultsCopyIpAddress";
            this.ctxMenuResultsCopyIpAddress.Size = new System.Drawing.Size(134, 22);
            this.ctxMenuResultsCopyIpAddress.Text = "IP Address";
            this.ctxMenuResultsCopyIpAddress.Click += new System.EventHandler(this.ctxMenuResultsCopyIpAddress_Click);
            // 
            // ctxMenuResultsCopyPort
            // 
            this.ctxMenuResultsCopyPort.Name = "ctxMenuResultsCopyPort";
            this.ctxMenuResultsCopyPort.Size = new System.Drawing.Size(134, 22);
            this.ctxMenuResultsCopyPort.Text = "Port";
            this.ctxMenuResultsCopyPort.Click += new System.EventHandler(this.ctxMenuResultsCopyPort_Click);
            // 
            // ctxMenuResultsCopyService
            // 
            this.ctxMenuResultsCopyService.Name = "ctxMenuResultsCopyService";
            this.ctxMenuResultsCopyService.Size = new System.Drawing.Size(134, 22);
            this.ctxMenuResultsCopyService.Text = "Service";
            this.ctxMenuResultsCopyService.Click += new System.EventHandler(this.ctxMenuResultsCopyService_Click);
            // 
            // ctxMenuResultsCopyHostName
            // 
            this.ctxMenuResultsCopyHostName.Name = "ctxMenuResultsCopyHostName";
            this.ctxMenuResultsCopyHostName.Size = new System.Drawing.Size(134, 22);
            this.ctxMenuResultsCopyHostName.Text = "Host Name";
            this.ctxMenuResultsCopyHostName.Click += new System.EventHandler(this.ctxMenuResultsCopyHostName_Click);
            // 
            // ctxMenuResultsSepOne
            // 
            this.ctxMenuResultsSepOne.Name = "ctxMenuResultsSepOne";
            this.ctxMenuResultsSepOne.Size = new System.Drawing.Size(149, 6);
            // 
            // ctxMenuResultsFilter
            // 
            this.ctxMenuResultsFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuResultsFilterType,
            this.ctxMenuResultsFilterHost,
            this.ctxMenuResultsFilterPort,
            this.ctxMenuResultsFilterProtocol,
            this.ctxMenuResultsFilterService,
            this.ctxMenuResultsFilterSeverity,
            this.ctxMenuResultsFilterPluginId,
            this.ctxMenuResultsFilterPluginFamily,
            this.ctxMenuResultsFilterPluginName,
            this.ctxMenuResultsFilterProduct,
            this.ctxMenuResultsFilterVersion});
            this.ctxMenuResultsFilter.Name = "ctxMenuResultsFilter";
            this.ctxMenuResultsFilter.Size = new System.Drawing.Size(152, 22);
            this.ctxMenuResultsFilter.Text = "Filter";
            // 
            // ctxMenuResultsFilterType
            // 
            this.ctxMenuResultsFilterType.Name = "ctxMenuResultsFilterType";
            this.ctxMenuResultsFilterType.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterType.Text = "Type";
            this.ctxMenuResultsFilterType.Click += new System.EventHandler(this.ctxMenuResultsFilterType_Click);
            // 
            // ctxMenuResultsFilterHost
            // 
            this.ctxMenuResultsFilterHost.Name = "ctxMenuResultsFilterHost";
            this.ctxMenuResultsFilterHost.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterHost.Text = "Host";
            this.ctxMenuResultsFilterHost.Click += new System.EventHandler(this.ctxMenuResultsFilterHost_Click);
            // 
            // ctxMenuResultsFilterPort
            // 
            this.ctxMenuResultsFilterPort.Name = "ctxMenuResultsFilterPort";
            this.ctxMenuResultsFilterPort.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterPort.Text = "Port";
            this.ctxMenuResultsFilterPort.Click += new System.EventHandler(this.ctxMenuResultsFilterPort_Click);
            // 
            // ctxMenuResultsFilterProtocol
            // 
            this.ctxMenuResultsFilterProtocol.Name = "ctxMenuResultsFilterProtocol";
            this.ctxMenuResultsFilterProtocol.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterProtocol.Text = "Protocol";
            this.ctxMenuResultsFilterProtocol.Click += new System.EventHandler(this.ctxMenuResultsFilterProtocol_Click);
            // 
            // ctxMenuResultsFilterService
            // 
            this.ctxMenuResultsFilterService.Name = "ctxMenuResultsFilterService";
            this.ctxMenuResultsFilterService.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterService.Text = "Service";
            this.ctxMenuResultsFilterService.Click += new System.EventHandler(this.ctxMenuResultsFilterService_Click);
            // 
            // ctxMenuResultsFilterSeverity
            // 
            this.ctxMenuResultsFilterSeverity.Name = "ctxMenuResultsFilterSeverity";
            this.ctxMenuResultsFilterSeverity.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterSeverity.Text = "Severity";
            this.ctxMenuResultsFilterSeverity.Click += new System.EventHandler(this.ctxMenuResultsFilterSeverity_Click);
            // 
            // ctxMenuResultsFilterPluginId
            // 
            this.ctxMenuResultsFilterPluginId.Name = "ctxMenuResultsFilterPluginId";
            this.ctxMenuResultsFilterPluginId.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterPluginId.Text = "Plugin ID";
            this.ctxMenuResultsFilterPluginId.Click += new System.EventHandler(this.ctxMenuResultsFilterPluginId_Click);
            // 
            // ctxMenuResultsFilterPluginFamily
            // 
            this.ctxMenuResultsFilterPluginFamily.Name = "ctxMenuResultsFilterPluginFamily";
            this.ctxMenuResultsFilterPluginFamily.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterPluginFamily.Text = "Plugin Family";
            this.ctxMenuResultsFilterPluginFamily.Click += new System.EventHandler(this.ctxMenuResultsFilterPluginFamily_Click);
            // 
            // ctxMenuResultsFilterPluginName
            // 
            this.ctxMenuResultsFilterPluginName.Name = "ctxMenuResultsFilterPluginName";
            this.ctxMenuResultsFilterPluginName.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterPluginName.Text = "Plugin Name";
            this.ctxMenuResultsFilterPluginName.Click += new System.EventHandler(this.ctxMenuResultsFilterPluginName_Click);
            // 
            // ctxMenuResultsFilterProduct
            // 
            this.ctxMenuResultsFilterProduct.Name = "ctxMenuResultsFilterProduct";
            this.ctxMenuResultsFilterProduct.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterProduct.Text = "Product";
            this.ctxMenuResultsFilterProduct.Click += new System.EventHandler(this.ctxMenuResultsFilterProduct_Click);
            // 
            // ctxMenuResultsFilterVersion
            // 
            this.ctxMenuResultsFilterVersion.Name = "ctxMenuResultsFilterVersion";
            this.ctxMenuResultsFilterVersion.Size = new System.Drawing.Size(146, 22);
            this.ctxMenuResultsFilterVersion.Text = "Version";
            this.ctxMenuResultsFilterVersion.Click += new System.EventHandler(this.ctxMenuResultsFilterVersion_Click);
            // 
            // ctxMenuResultsSepTwo
            // 
            this.ctxMenuResultsSepTwo.Name = "ctxMenuResultsSepTwo";
            this.ctxMenuResultsSepTwo.Size = new System.Drawing.Size(149, 6);
            // 
            // ctxMenuResultsIgnorePlugins
            // 
            this.ctxMenuResultsIgnorePlugins.Name = "ctxMenuResultsIgnorePlugins";
            this.ctxMenuResultsIgnorePlugins.Size = new System.Drawing.Size(152, 22);
            this.ctxMenuResultsIgnorePlugins.Text = "Ignore Plugins";
            this.ctxMenuResultsIgnorePlugins.Click += new System.EventHandler(this.ctxMenuResultsIgnorePlugins_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // ctxMenuResultsClearFilters
            // 
            this.ctxMenuResultsClearFilters.Name = "ctxMenuResultsClearFilters";
            this.ctxMenuResultsClearFilters.Size = new System.Drawing.Size(152, 22);
            this.ctxMenuResultsClearFilters.Text = "Clear Filters";
            this.ctxMenuResultsClearFilters.Click += new System.EventHandler(this.ctxMenuResultsClearFilters_Click);
            // 
            // btnResultsLastPage
            // 
            this.btnResultsLastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultsLastPage.Image = ((System.Drawing.Image)(resources.GetObject("btnResultsLastPage.Image")));
            this.btnResultsLastPage.Location = new System.Drawing.Point(654, 297);
            this.btnResultsLastPage.Name = "btnResultsLastPage";
            this.btnResultsLastPage.Size = new System.Drawing.Size(25, 25);
            this.btnResultsLastPage.TabIndex = 13;
            this.btnResultsLastPage.UseVisualStyleBackColor = true;
            // 
            // btnResultsNextPage
            // 
            this.btnResultsNextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResultsNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnResultsNextPage.Image")));
            this.btnResultsNextPage.Location = new System.Drawing.Point(629, 297);
            this.btnResultsNextPage.Name = "btnResultsNextPage";
            this.btnResultsNextPage.Size = new System.Drawing.Size(25, 25);
            this.btnResultsNextPage.TabIndex = 12;
            this.btnResultsNextPage.UseVisualStyleBackColor = true;
            // 
            // btnResultsPreviousPage
            // 
            this.btnResultsPreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResultsPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("btnResultsPreviousPage.Image")));
            this.btnResultsPreviousPage.Location = new System.Drawing.Point(24, 297);
            this.btnResultsPreviousPage.Name = "btnResultsPreviousPage";
            this.btnResultsPreviousPage.Size = new System.Drawing.Size(25, 25);
            this.btnResultsPreviousPage.TabIndex = 11;
            this.btnResultsPreviousPage.UseVisualStyleBackColor = true;
            // 
            // btnResultsFirstPage
            // 
            this.btnResultsFirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResultsFirstPage.Image = ((System.Drawing.Image)(resources.GetObject("btnResultsFirstPage.Image")));
            this.btnResultsFirstPage.Location = new System.Drawing.Point(-1, 297);
            this.btnResultsFirstPage.Name = "btnResultsFirstPage";
            this.btnResultsFirstPage.Size = new System.Drawing.Size(25, 25);
            this.btnResultsFirstPage.TabIndex = 10;
            this.btnResultsFirstPage.UseVisualStyleBackColor = true;
            // 
            // FormList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 321);
            this.Controls.Add(this.btnResultsLastPage);
            this.Controls.Add(this.btnResultsNextPage);
            this.Controls.Add(this.btnResultsPreviousPage);
            this.Controls.Add(this.btnResultsFirstPage);
            this.Controls.Add(this.listResults);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormList";
            this.Text = "List";
            ((System.ComponentModel.ISupportInitialize)(this.listResults)).EndInit();
            this.ctxMenuResults.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView listResults;
        private BrightIdeasSoftware.OLVColumn colResultsType;
        private BrightIdeasSoftware.OLVColumn colResultsIpAddress;
        private BrightIdeasSoftware.OLVColumn colResultsHostName;
        private BrightIdeasSoftware.OLVColumn colResultsPort;
        private BrightIdeasSoftware.OLVColumn colResultsProtocol;
        private BrightIdeasSoftware.OLVColumn colResultsState;
        private BrightIdeasSoftware.OLVColumn colResultsService;
        private BrightIdeasSoftware.OLVColumn colResultsSeverity;
        private BrightIdeasSoftware.OLVColumn colResultsSynopsis;
        private BrightIdeasSoftware.OLVColumn colResultsPluginId;
        private BrightIdeasSoftware.OLVColumn colResultsPluginName;
        private BrightIdeasSoftware.OLVColumn colResultsPluginFamily;
        private System.Windows.Forms.Button btnResultsLastPage;
        private System.Windows.Forms.Button btnResultsNextPage;
        private System.Windows.Forms.Button btnResultsPreviousPage;
        private System.Windows.Forms.Button btnResultsFirstPage;
        private System.Windows.Forms.ContextMenuStrip ctxMenuResults;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsCopy;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsCopyIpAddress;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsCopyPort;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsCopyService;
        private System.Windows.Forms.ToolStripSeparator ctxMenuResultsSepOne;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilter;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterType;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterHost;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterPort;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterProtocol;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterService;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterSeverity;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterPluginId;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterPluginFamily;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterPluginName;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterProduct;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsFilterVersion;
        private System.Windows.Forms.ToolStripSeparator ctxMenuResultsSepTwo;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsIgnorePlugins;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsClearFilters;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuResultsCopyHostName;
    }
}