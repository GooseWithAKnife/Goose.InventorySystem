using GooseInventorySystem.Database;
using GooseInventorySystem.Models;

namespace GooseInventorySystem;

public partial class AddEditItemForm : Form
{
    private readonly InventoryDbContext _db;
    private readonly Item? _existing;

    public AddEditItemForm(InventoryDbContext db, string? barcode = null, int initialQuantity = 1)
    {
        _db = db;
        _existing = null;
        InitializeComponent();
        LoadLocations();
        LoadItemNames();
        LoadCompanies();
        if (!string.IsNullOrWhiteSpace(barcode))
            txtBarcode.Text = barcode.Trim();
        numQuantity.Value = Math.Max(1, Math.Min(9999, initialQuantity));
        if (_existing == null)
            Text = string.IsNullOrEmpty(txtBarcode.Text) ? "Add item" : "Add new item";
        Shown += AddEditItemForm_Shown;
    }

    public AddEditItemForm(InventoryDbContext db, Item item)
    {
        _db = db;
        _existing = item;
        InitializeComponent();
        LoadLocations();
        LoadItemNames();
        LoadCompanies();
        txtBarcode.Text = item.Barcode;
        cmbCompany.Text = item.Company ?? "";
        cmbName.Text = item.Name;
        cmbLocation.Text = item.Location ?? "";
        numQuantity.Value = Math.Max(0, item.Quantity);
        txtBarcode.ReadOnly = true;
        Text = "Edit item";
    }

    private void AddEditItemForm_Shown(object? sender, EventArgs e)
    {
        if (_existing == null)
            cmbCompany.Focus();
    }

    private void LoadLocations()
    {
        var locations = _db.GetDistinctLocations();
        cmbLocation.Items.Clear();
        foreach (var loc in locations)
            cmbLocation.Items.Add(loc);
    }

    private void LoadItemNames()
    {
        var names = _db.GetDistinctItemNames();
        cmbName.Items.Clear();
        foreach (var n in names)
            cmbName.Items.Add(n);
    }

    private void LoadCompanies()
    {
        var companies = _db.GetDistinctCompanies();
        cmbCompany.Items.Clear();
        foreach (var c in companies)
            cmbCompany.Items.Add(c);
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        var barcode = txtBarcode.Text.Trim();
        var name = cmbName.Text.Trim();
        if (string.IsNullOrEmpty(barcode))
        {
            MessageBox.Show("Barcode is required.", "Goose Inventory System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtBarcode.Focus();
            return;
        }
        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Name is required.", "Goose Inventory System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cmbName.Focus();
            return;
        }
        var company = string.IsNullOrWhiteSpace(cmbCompany.Text) ? null : cmbCompany.Text.Trim();
        var location = string.IsNullOrWhiteSpace(cmbLocation.Text) ? null : cmbLocation.Text.Trim();
        var quantity = (int)numQuantity.Value;

        if (_existing != null)
        {
            _existing.Name = name;
            _existing.Company = company;
            _existing.Location = location;
            _existing.Quantity = quantity;
            _db.Update(_existing);
        }
        else
        {
            var existingByBarcode = _db.GetByBarcode(barcode);
            if (existingByBarcode != null)
            {
                MessageBox.Show("An item with this barcode already exists.", "Goose Inventory System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _db.Insert(new Item { Barcode = barcode, Name = name, Company = company, Location = location, Quantity = quantity });
        }
        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
