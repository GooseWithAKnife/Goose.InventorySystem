namespace GooseInventorySystem;

partial class AddEditItemForm
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
        lblBarcode = new Label();
        txtBarcode = new TextBox();
        lblCompany = new Label();
        cmbCompany = new ComboBox();
        lblName = new Label();
        cmbName = new ComboBox();
        lblLocation = new Label();
        cmbLocation = new ComboBox();
        lblQuantity = new Label();
        numQuantity = new NumericUpDown();
        btnSave = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        SuspendLayout();
        //
        // lblBarcode
        //
        lblBarcode.AutoSize = true;
        lblBarcode.Location = new Point(20, 24);
        lblBarcode.Name = "lblBarcode";
        lblBarcode.Size = new Size(62, 20);
        lblBarcode.Text = "Barcode:";
        //
        // txtBarcode
        //
        txtBarcode.Location = new Point(20, 48);
        txtBarcode.Name = "txtBarcode";
        txtBarcode.Size = new Size(400, 27);
        txtBarcode.TabIndex = 0;
        //
        // lblCompany
        //
        lblCompany.AutoSize = true;
        lblCompany.Location = new Point(20, 88);
        lblCompany.Name = "lblCompany";
        lblCompany.Size = new Size(72, 20);
        lblCompany.Text = "Company:";
        //
        // cmbCompany
        //
        cmbCompany.DropDownStyle = ComboBoxStyle.DropDown;
        cmbCompany.FormattingEnabled = true;
        cmbCompany.Location = new Point(20, 112);
        cmbCompany.Name = "cmbCompany";
        cmbCompany.Size = new Size(400, 28);
        cmbCompany.TabIndex = 1;
        //
        // lblName
        //
        lblName.AutoSize = true;
        lblName.Location = new Point(20, 152);
        lblName.Name = "lblName";
        lblName.Size = new Size(52, 20);
        lblName.Text = "Name:";
        //
        // cmbName
        //
        cmbName.DropDownStyle = ComboBoxStyle.DropDown;
        cmbName.FormattingEnabled = true;
        cmbName.Location = new Point(20, 176);
        cmbName.Name = "cmbName";
        cmbName.Size = new Size(400, 28);
        cmbName.TabIndex = 2;
        //
        // lblLocation
        //
        lblLocation.AutoSize = true;
        lblLocation.Location = new Point(20, 216);
        lblLocation.Name = "lblLocation";
        lblLocation.Size = new Size(69, 20);
        lblLocation.Text = "Location:";
        //
        // cmbLocation
        //
        cmbLocation.DropDownStyle = ComboBoxStyle.DropDown;
        cmbLocation.FormattingEnabled = true;
        cmbLocation.Location = new Point(20, 240);
        cmbLocation.Name = "cmbLocation";
        cmbLocation.Size = new Size(400, 28);
        cmbLocation.TabIndex = 3;
        //
        // lblQuantity
        //
        lblQuantity.AutoSize = true;
        lblQuantity.Location = new Point(20, 280);
        lblQuantity.Name = "lblQuantity";
        lblQuantity.Size = new Size(72, 20);
        lblQuantity.Text = "Quantity:";
        //
        // numQuantity
        //
        numQuantity.Location = new Point(20, 304);
        numQuantity.Maximum = 99999;
        numQuantity.Minimum = 0;
        numQuantity.Name = "numQuantity";
        numQuantity.Size = new Size(120, 27);
        numQuantity.TabIndex = 4;
        numQuantity.Value = 1;
        //
        // btnSave
        //
        btnSave.Location = new Point(20, 354);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(120, 44);
        btnSave.TabIndex = 5;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;
        //
        // btnCancel
        //
        btnCancel.Location = new Point(150, 354);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(120, 44);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = true;
        btnCancel.Click += BtnCancel_Click;
        //
        // AddEditItemForm
        //
        AcceptButton = btnSave;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancel;
        ClientSize = new Size(444, 414);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(numQuantity);
        Controls.Add(lblQuantity);
        Controls.Add(cmbLocation);
        Controls.Add(lblLocation);
        Controls.Add(cmbName);
        Controls.Add(lblName);
        Controls.Add(cmbCompany);
        Controls.Add(lblCompany);
        Controls.Add(txtBarcode);
        Controls.Add(lblBarcode);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AddEditItemForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Add / Edit Item";
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblBarcode;
    private TextBox txtBarcode;
    private Label lblName;
    private ComboBox cmbName;
    private Label lblCompany;
    private ComboBox cmbCompany;
    private Label lblLocation;
    private ComboBox cmbLocation;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Button btnSave;
    private Button btnCancel;
}
