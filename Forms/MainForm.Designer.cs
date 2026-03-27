namespace GooseInventorySystem;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        scanTextBox = new TextBox();
        btnClearScan = new Button();
        lblScanQuantity = new Label();
        numScanQuantity = new NumericUpDown();
        btnQuantityMinus = new Button();
        btnQuantityPlus = new Button();
        btnTagIn = new Button();
        btnTagOut = new Button();
        grid = new DataGridView();
        searchTextBox = new TextBox();
        searchResultsList = new ListBox();
        locationFilterCombo = new ComboBox();
        btnClearSearch = new Button();
        lblSearch = new Label();
        lblLocation = new Label();
        toolStrip = new ToolStrip();
        btnAddItem = new ToolStripButton();
        btnEdit = new ToolStripButton();
        btnDelete = new ToolStripButton();
        panelTop = new Panel();
        panelFilter = new Panel();
        panelMessageBar = new Panel();
        lblMessageBar = new Label();
        panelMainContent = new Panel();
        ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numScanQuantity).BeginInit();
        toolStrip.SuspendLayout();
        panelTop.SuspendLayout();
        panelFilter.SuspendLayout();
        panelMessageBar.SuspendLayout();
        panelMainContent.SuspendLayout();
        SuspendLayout();
        //
        // panelTop
        //
        panelTop.Controls.Add(scanTextBox);
        panelTop.Controls.Add(btnClearScan);
        panelTop.Controls.Add(lblScanQuantity);
        panelTop.Controls.Add(numScanQuantity);
        panelTop.Controls.Add(btnQuantityMinus);
        panelTop.Controls.Add(btnQuantityPlus);
        panelTop.Controls.Add(btnTagIn);
        panelTop.Controls.Add(btnTagOut);
        panelTop.Dock = DockStyle.Top;
        panelTop.Padding = new Padding(12, 12, 12, 8);
        panelTop.Size = new Size(1024, 280);
        //
        // scanTextBox
        //
        scanTextBox.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
        scanTextBox.Location = new Point(12, 12);
        scanTextBox.Name = "scanTextBox";
        scanTextBox.PlaceholderText = "Scan or type barcode here";
        scanTextBox.Size = new Size(600, 44);
        scanTextBox.TabIndex = 0;
        //
        // btnClearScan
        //
        btnClearScan.Font = new Font("Segoe UI", 11F);
        btnClearScan.Location = new Point(618, 12);
        btnClearScan.Name = "btnClearScan";
        btnClearScan.Size = new Size(90, 44);
        btnClearScan.TabIndex = 4;
        btnClearScan.Text = "Clear";
        btnClearScan.UseVisualStyleBackColor = true;
        btnClearScan.Click += BtnClearScan_Click;
        //
        // lblScanQuantity
        //
        lblScanQuantity.AutoSize = true;
        lblScanQuantity.Font = new Font("Segoe UI", 12F);
        lblScanQuantity.Location = new Point(12, 68);
        lblScanQuantity.Name = "lblScanQuantity";
        lblScanQuantity.Size = new Size(230, 28);
        lblScanQuantity.Text = "Quantity (resets to 1 after scan):";
        //
        // numScanQuantity
        //
        numScanQuantity.Font = new Font("Segoe UI", 14F);
        numScanQuantity.Location = new Point(12, 100);
        numScanQuantity.Maximum = 9999;
        numScanQuantity.Minimum = 1;
        numScanQuantity.Name = "numScanQuantity";
        numScanQuantity.Size = new Size(100, 38);
        numScanQuantity.TabIndex = 1;
        numScanQuantity.Value = 1;
        //
        // btnQuantityMinus
        //
        btnQuantityMinus.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        btnQuantityMinus.Location = new Point(118, 96);
        btnQuantityMinus.Name = "btnQuantityMinus";
        btnQuantityMinus.Size = new Size(56, 56);
        btnQuantityMinus.TabIndex = 5;
        btnQuantityMinus.Text = "−";
        btnQuantityMinus.UseVisualStyleBackColor = true;
        btnQuantityMinus.Click += BtnQuantityMinus_Click;
        //
        // btnQuantityPlus
        //
        btnQuantityPlus.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        btnQuantityPlus.Location = new Point(180, 96);
        btnQuantityPlus.Name = "btnQuantityPlus";
        btnQuantityPlus.Size = new Size(56, 56);
        btnQuantityPlus.TabIndex = 6;
        btnQuantityPlus.Text = "+";
        btnQuantityPlus.UseVisualStyleBackColor = true;
        btnQuantityPlus.Click += BtnQuantityPlus_Click;
        //
        // btnTagIn
        //
        btnTagIn.FlatStyle = FlatStyle.Flat;
        btnTagIn.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        btnTagIn.Location = new Point(12, 152);
        btnTagIn.Name = "btnTagIn";
        btnTagIn.Size = new Size(480, 112);
        btnTagIn.TabIndex = 2;
        btnTagIn.Text = "Tag In";
        btnTagIn.UseVisualStyleBackColor = true;
        //
        // btnTagOut
        //
        btnTagOut.FlatStyle = FlatStyle.Flat;
        btnTagOut.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        btnTagOut.Location = new Point(504, 152);
        btnTagOut.Name = "btnTagOut";
        btnTagOut.Size = new Size(480, 112);
        btnTagOut.TabIndex = 3;
        btnTagOut.Text = "Tag Out";
        btnTagOut.UseVisualStyleBackColor = true;
        //
        // panelFilter
        //
        panelFilter.Controls.Add(lblSearch);
        panelFilter.Controls.Add(searchTextBox);
        panelFilter.Controls.Add(searchResultsList);
        panelFilter.Controls.Add(lblLocation);
        panelFilter.Controls.Add(locationFilterCombo);
        panelFilter.Controls.Add(btnClearSearch);
        panelFilter.Dock = DockStyle.Top;
        panelFilter.Padding = new Padding(12, 8, 12, 8);
        panelFilter.Size = new Size(1024, 120);
        //
        // lblSearch
        //
        lblSearch.AutoSize = true;
        lblSearch.Location = new Point(12, 12);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(80, 20);
        lblSearch.Text = "Search items:";
        //
        // searchTextBox
        //
        searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        searchTextBox.Font = new Font("Segoe UI", 12F);
        searchTextBox.Location = new Point(12, 36);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Type to search name, barcode, company, or location...";
        searchTextBox.Size = new Size(500, 34);
        searchTextBox.TabIndex = 3;
        //
        // searchResultsList
        //
        searchResultsList.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        searchResultsList.Font = new Font("Segoe UI", 11F);
        searchResultsList.IntegralHeight = false;
        searchResultsList.ItemHeight = 20;
        searchResultsList.Location = new Point(12, 76);
        searchResultsList.Name = "searchResultsList";
        searchResultsList.Size = new Size(500, 36);
        searchResultsList.TabIndex = 4;
        searchResultsList.Visible = false;
        //
        // lblLocation
        //
        lblLocation.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblLocation.AutoSize = true;
        lblLocation.Location = new Point(530, 12);
        lblLocation.Name = "lblLocation";
        lblLocation.Size = new Size(100, 20);
        lblLocation.Text = "Filter by location:";
        //
        // locationFilterCombo
        //
        locationFilterCombo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        locationFilterCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        locationFilterCombo.Font = new Font("Segoe UI", 11F);
        locationFilterCombo.FormattingEnabled = true;
        locationFilterCombo.Location = new Point(530, 36);
        locationFilterCombo.Name = "locationFilterCombo";
        locationFilterCombo.Size = new Size(250, 33);
        locationFilterCombo.TabIndex = 5;
        //
        // btnClearSearch
        //
        btnClearSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnClearSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        btnClearSearch.Location = new Point(790, 34);
        btnClearSearch.Name = "btnClearSearch";
        btnClearSearch.Size = new Size(120, 44);
        btnClearSearch.TabIndex = 6;
        btnClearSearch.Text = "Clear search";
        btnClearSearch.UseVisualStyleBackColor = true;
        btnClearSearch.Click += BtnClearSearchFilter_Click;
        //
        // toolStrip
        //
        toolStrip.ImageScalingSize = new Size(24, 24);
        toolStrip.Items.AddRange(new ToolStripItem[] { btnAddItem, btnEdit, btnDelete });
        toolStrip.Dock = DockStyle.Top;
        toolStrip.Location = new Point(0, 260);
        toolStrip.Name = "toolStrip";
        toolStrip.Size = new Size(1024, 34);
        toolStrip.TabIndex = 6;
        toolStrip.Text = "toolStrip1";
        //
        // btnAddItem
        //
        btnAddItem.Name = "btnAddItem";
        btnAddItem.Size = new Size(85, 31);
        btnAddItem.Text = "Add item";
        //
        // btnEdit
        //
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(45, 31);
        btnEdit.Text = "Edit";
        //
        // btnDelete
        //
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(52, 31);
        btnDelete.Text = "Delete";
        //
        // grid
        //
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AutoGenerateColumns = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        grid.ColumnHeadersHeight = 44;
        grid.Dock = DockStyle.Fill;
        grid.Font = new Font("Segoe UI", 12F);
        grid.MultiSelect = false;
        grid.Name = "grid";
        grid.ReadOnly = true;
        grid.RowHeadersWidth = 51;
        grid.RowTemplate.Height = 44;
        grid.ScrollBars = ScrollBars.Both;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.Size = new Size(1024, 389);
        grid.TabIndex = 7;
        grid.CellDoubleClick += Grid_CellDoubleClick;
        //
        // panelMainContent (grid only)
        //
        panelMainContent.Controls.Add(grid);
        panelMainContent.Dock = DockStyle.Fill;
        panelMainContent.Name = "panelMainContent";
        panelMainContent.Size = new Size(1024, 387);
        //
        // panelMessageBar (under search, above table)
        //
        panelMessageBar.Controls.Add(lblMessageBar);
        panelMessageBar.Dock = DockStyle.Top;
        panelMessageBar.MinimumSize = new Size(0, 44);
        panelMessageBar.Padding = new Padding(12, 8, 12, 8);
        panelMessageBar.Size = new Size(1024, 44);
        //
        // lblMessageBar
        //
        lblMessageBar.AutoSize = true;
        lblMessageBar.Font = new Font("Segoe UI", 14F);
        lblMessageBar.ForeColor = Color.DarkRed;
        lblMessageBar.Location = new Point(12, 10);
        lblMessageBar.Name = "lblMessageBar";
        lblMessageBar.Size = new Size(0, 28);
        lblMessageBar.Text = "";
        //
        // MainForm
        //
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1024, 683);
        Controls.Add(panelMainContent);
        Controls.Add(toolStrip);
        Controls.Add(panelMessageBar);
        Controls.Add(panelFilter);
        Controls.Add(panelTop);
        MinimumSize = new Size(800, 500);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Goose Inventory System";
        Load += MainForm_Load;
        ((System.ComponentModel.ISupportInitialize)grid).EndInit();
        ((System.ComponentModel.ISupportInitialize)numScanQuantity).EndInit();
        toolStrip.ResumeLayout(false);
        toolStrip.PerformLayout();
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        panelFilter.ResumeLayout(false);
        panelFilter.PerformLayout();
        panelMessageBar.ResumeLayout(false);
        panelMessageBar.PerformLayout();
        panelMainContent.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private TextBox scanTextBox;
    private Button btnClearScan;
    private Label lblScanQuantity;
    private NumericUpDown numScanQuantity;
    private Button btnQuantityMinus;
    private Button btnQuantityPlus;
    private Button btnTagIn;
    private Button btnTagOut;
    private DataGridView grid;
    private TextBox searchTextBox;
    private ListBox searchResultsList;
    private ComboBox locationFilterCombo;
    private Button btnClearSearch;
    private Label lblSearch;
    private Label lblLocation;
    private ToolStrip toolStrip;
    private ToolStripButton btnAddItem;
    private ToolStripButton btnEdit;
    private ToolStripButton btnDelete;
    private Panel panelTop;
    private Panel panelFilter;
    private Panel panelMainContent;
    private Panel panelMessageBar;
    private Label lblMessageBar;
}
