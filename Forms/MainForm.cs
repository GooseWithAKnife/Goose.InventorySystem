using System.Windows.Forms;
using GooseInventorySystem.Database;
using GooseInventorySystem.Models;

namespace GooseInventorySystem;

public partial class MainForm : Form
{
    private readonly InventoryDbContext _db;
    private List<Item> _allItems = new();
    private System.Windows.Forms.Timer? _searchDebounce;
    private const int SearchDebounceMs = 150;
    private bool _tagInMode = true; // true = Tag In, false = Tag Out

    public MainForm()
    {
        _db = new InventoryDbContext();
        InitializeComponent();
        SetupGridColumns();
        SetupSearchDebounce();
    }

    private void SetupGridColumns()
    {
        grid.Columns.Clear();
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", DataPropertyName = "Id", Visible = false });
        grid.Columns["Id"].Visible = false;
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Barcode", DataPropertyName = "Barcode", HeaderText = "Barcode", FillWeight = 12, MinimumWidth = 80 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Company", DataPropertyName = "Company", HeaderText = "Company", FillWeight = 22 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", DataPropertyName = "Name", HeaderText = "Name", FillWeight = 40 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Location", DataPropertyName = "Location", HeaderText = "Location", FillWeight = 18 });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", DataPropertyName = "Quantity", HeaderText = "Qty", FillWeight = 8, MinimumWidth = 50 });
    }

    private void SetupSearchDebounce()
    {
        _searchDebounce = new System.Windows.Forms.Timer { Interval = SearchDebounceMs };
        _searchDebounce.Tick += (_, _) =>
        {
            _searchDebounce.Stop();
            ApplySearchAndFilters();
        };
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        LoadFormIcon();
        LoadData();
        UpdateModeButtons();
        scanTextBox.Focus();
        btnTagIn.Click += BtnTagIn_Click;
        btnTagOut.Click += BtnTagOut_Click;
        scanTextBox.KeyDown += ScanTextBox_KeyDown;
        searchTextBox.TextChanged += SearchTextBox_TextChanged;
        locationFilterCombo.SelectedIndexChanged += LocationFilterCombo_SelectedIndexChanged;
        searchResultsList.SelectedIndexChanged += SearchResultsList_SelectedIndexChanged;
        searchResultsList.DoubleClick += SearchResultsList_DoubleClick;
        btnAddItem.Click += BtnAddItem_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        Shown += MainForm_Shown;
    }

    private void LoadFormIcon()
    {
        var baseDir = AppContext.BaseDirectory;
        var icoPath = Path.Combine(baseDir, "icon.ico");
        if (File.Exists(icoPath))
        {
            try { Icon = new Icon(icoPath); } catch { }
        }
        else
        {
            try { Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath); } catch { }
        }
    }

    private void BtnClearSearchFilter_Click(object? sender, EventArgs e)
    {
        searchTextBox.Clear();
        ApplySearchAndFilters();
        searchTextBox.Focus();
    }

    private void BtnQuantityMinus_Click(object? sender, EventArgs e)
    {
        if (numScanQuantity.Value > numScanQuantity.Minimum)
            numScanQuantity.Value--;
    }

    private void BtnQuantityPlus_Click(object? sender, EventArgs e)
    {
        if (numScanQuantity.Value < numScanQuantity.Maximum)
            numScanQuantity.Value++;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        scanTextBox.Focus();
    }

    private void BtnClearScan_Click(object? sender, EventArgs e)
    {
        scanTextBox.Clear();
        scanTextBox.Focus();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == (Keys.Control | Keys.OemMinus) || keyData == (Keys.Control | Keys.Subtract))
        {
            _tagInMode = false;
            UpdateModeButtons();
            return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void UpdateModeButtons()
    {
        if (_tagInMode)
        {
            btnTagIn.BackColor = Color.FromArgb(0, 128, 0);
            btnTagIn.ForeColor = Color.White;
            btnTagIn.FlatAppearance.BorderSize = 4;
            btnTagOut.BackColor = Color.FromArgb(220, 220, 220);
            btnTagOut.ForeColor = Color.Gray;
            btnTagOut.FlatAppearance.BorderSize = 1;
        }
        else
        {
            btnTagOut.BackColor = Color.FromArgb(200, 80, 60);
            btnTagOut.ForeColor = Color.White;
            btnTagOut.FlatAppearance.BorderSize = 4;
            btnTagIn.BackColor = Color.FromArgb(220, 220, 220);
            btnTagIn.ForeColor = Color.Gray;
            btnTagIn.FlatAppearance.BorderSize = 1;
        }
    }

    private void LoadData()
    {
        _allItems = _db.GetAllItems();
        RefreshLocationFilterCombo();
        ApplySearchAndFilters();
    }

    private void RefreshLocationFilterCombo()
    {
        var locations = _db.GetDistinctLocations();
        var current = locationFilterCombo.SelectedItem?.ToString();
        locationFilterCombo.Items.Clear();
        locationFilterCombo.Items.Add("(All locations)");
        foreach (var loc in locations)
            locationFilterCombo.Items.Add(loc);
        locationFilterCombo.SelectedIndex = 0;
        if (!string.IsNullOrEmpty(current))
        {
            var idx = locationFilterCombo.Items.IndexOf(current);
            if (idx >= 0) locationFilterCombo.SelectedIndex = idx;
        }
    }

    private void ApplySearchAndFilters()
    {
        var search = searchTextBox.Text.Trim();
        var locationFilter = locationFilterCombo.SelectedIndex <= 0 || locationFilterCombo.SelectedItem?.ToString() == "(All locations)"
            ? null
            : locationFilterCombo.SelectedItem?.ToString();

        var filtered = _allItems.AsEnumerable();
        if (!string.IsNullOrEmpty(search))
        {
            var s = search.ToLowerInvariant();
            filtered = filtered.Where(i =>
                (i.Name?.Contains(s, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (i.Barcode?.Contains(s, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (i.Company?.Contains(s, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (i.Location?.Contains(s, StringComparison.OrdinalIgnoreCase) ?? false));
        }
        if (!string.IsNullOrEmpty(locationFilter))
            filtered = filtered.Where(i => string.Equals(i.Location, locationFilter, StringComparison.OrdinalIgnoreCase));

        var list = filtered.ToList();
        var displayList = list.Take(50).ToList();
        searchResultsList.Items.Clear();
        foreach (var item in displayList)
            searchResultsList.Items.Add(FormatItemForSearch(item));
        searchResultsList.Visible = displayList.Count > 0 && !string.IsNullOrEmpty(search);
        searchResultsList.Tag = displayList;

        grid.DataSource = null;
        grid.DataSource = list;
        if (grid.Columns.Contains("Id"))
            grid.Columns["Id"].Visible = false;
    }

    private static string FormatItemForSearch(Item item)
    {
        var loc = string.IsNullOrWhiteSpace(item.Location) ? "" : $" @ {item.Location}";
        return $"{item.Name} ({item.Barcode}) — Qty: {item.Quantity}{loc}";
    }

    private void SearchTextBox_TextChanged(object? sender, EventArgs e)
    {
        _searchDebounce?.Stop();
        _searchDebounce?.Start();
    }

    private void LocationFilterCombo_SelectedIndexChanged(object? sender, EventArgs e) => ApplySearchAndFilters();

    private void SearchResultsList_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (searchResultsList.Tag is not List<Item> list || searchResultsList.SelectedIndex < 0) return;
        if (searchResultsList.SelectedIndex >= list.Count) return;
        var item = list[searchResultsList.SelectedIndex];
        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.DataBoundItem is Item bound && bound.Id == item.Id)
            {
                row.Selected = true;
                grid.FirstDisplayedScrollingRowIndex = row.Index;
                break;
            }
        }
    }

    private void SearchResultsList_DoubleClick(object? sender, EventArgs e) => SearchResultsList_SelectedIndexChanged(sender, e);

    private void ScanTextBox_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            PerformCurrentModeScan();
        }
    }

    private void BtnTagIn_Click(object? sender, EventArgs e)
    {
        _tagInMode = true;
        UpdateModeButtons();
        ClearMessageBar();
        scanTextBox.Clear();
        scanTextBox.Focus();
    }

    private void BtnTagOut_Click(object? sender, EventArgs e)
    {
        _tagInMode = false;
        UpdateModeButtons();
        ClearMessageBar();
        scanTextBox.Clear();
        scanTextBox.Focus();
    }

    private void PerformCurrentModeScan()
    {
        var barcode = scanTextBox.Text.Trim();
        if (string.Equals(barcode, "**OUT**", StringComparison.OrdinalIgnoreCase))
        {
            _tagInMode = false;
            UpdateModeButtons();
            ClearMessageBar();
            scanTextBox.Clear();
            ResetScanQuantity();
            scanTextBox.Focus();
            return;
        }
        if (string.Equals(barcode, "**IN**", StringComparison.OrdinalIgnoreCase))
        {
            _tagInMode = true;
            UpdateModeButtons();
            ClearMessageBar();
            scanTextBox.Clear();
            ResetScanQuantity();
            scanTextBox.Focus();
            return;
        }
        var amount = (int)numScanQuantity.Value;
        if (_tagInMode)
            PerformTagIn(amount);
        else
            PerformTagOut(amount);
    }

    private void ResetScanQuantity()
    {
        numScanQuantity.Value = 1;
    }

    private void SetMessageBar(string message)
    {
        lblMessageBar.Text = message;
        lblMessageBar.ForeColor = Color.DarkRed;
    }

    private void ClearMessageBar()
    {
        lblMessageBar.Text = "";
    }

    private void SelectItemInGrid(int itemId)
    {
        foreach (DataGridViewRow row in grid.Rows)
        {
            if (row.DataBoundItem is Item bound && bound.Id == itemId)
            {
                row.Selected = true;
                if (row.Cells.Count > 0)
                    grid.CurrentCell = row.Cells[1];
                grid.FirstDisplayedScrollingRowIndex = Math.Max(0, row.Index - 2);
                break;
            }
        }
    }

    private void PerformTagIn(int amount)
    {
        var barcode = scanTextBox.Text.Trim();
        if (string.IsNullOrEmpty(barcode)) return;
        var item = _db.GetByBarcode(barcode);
        if (item != null)
        {
            _db.IncrementQuantity(item.Id, amount);
            ClearMessageBar();
            scanTextBox.Clear();
            ResetScanQuantity();
            LoadData();
            SelectItemInGrid(item.Id);
            return;
        }
        using var form = new AddEditItemForm(_db, barcode, amount);
        if (form.ShowDialog() == DialogResult.OK)
        {
            ClearMessageBar();
            scanTextBox.Clear();
            ResetScanQuantity();
            LoadData();
            var added = _db.GetByBarcode(barcode);
            if (added != null)
                SelectItemInGrid(added.Id);
        }
        else
        {
            scanTextBox.Clear();
            scanTextBox.Focus();
        }
    }

    private void PerformTagOut(int amount)
    {
        var barcode = scanTextBox.Text.Trim();
        if (string.IsNullOrEmpty(barcode))
        {
            MessageBox.Show("Enter or scan a barcode first.", "Goose Inventory System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        var item = _db.GetByBarcode(barcode);
        if (item == null)
        {
            SetMessageBar($"Item not found in inventory: {barcode}");
            scanTextBox.Clear();
            scanTextBox.Focus();
            return;
        }
        if (item.Quantity <= 0)
        {
            var nameOrBarcode = !string.IsNullOrWhiteSpace(item.Name) ? item.Name : item.Barcode;
            SetMessageBar($"Quantity is 0 for item {nameOrBarcode}.");
            scanTextBox.Clear();
            scanTextBox.Focus();
            return;
        }
        var actual = Math.Min(amount, item.Quantity);
        _db.IncrementQuantity(item.Id, -actual);
        ClearMessageBar();
        scanTextBox.Clear();
        ResetScanQuantity();
        LoadData();
        SelectItemInGrid(item.Id);
    }

    private void BtnAddItem_Click(object? sender, EventArgs e)
    {
        using var form = new AddEditItemForm(_db);
        if (form.ShowDialog() == DialogResult.OK)
            LoadData();
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (grid.CurrentRow?.DataBoundItem is not Item item) return;
        using var form = new AddEditItemForm(_db, item);
        if (form.ShowDialog() == DialogResult.OK)
            LoadData();
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (grid.CurrentRow?.DataBoundItem is not Item item) return;
        if (MessageBox.Show($"Delete '{item.Name}'?", "Goose Inventory System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        _db.Delete(item.Id);
        LoadData();
    }

    private void Grid_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || grid.Rows[e.RowIndex].DataBoundItem is not Item item) return;
        using var form = new AddEditItemForm(_db, item);
        if (form.ShowDialog() == DialogResult.OK)
            LoadData();
    }
}
