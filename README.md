# Goose Inventory System

Desktop inventory tool for Windows. It uses Windows Forms and stores data in a local SQLite database (`inventory.db`) next to the application.

## Requirements

- Windows
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (to build and run from source)

The project targets `net8.0-windows` and uses Windows Forms.

## Build and run

From the folder that contains `GooseInventorySystem.csproj`:

```bash
dotnet restore
dotnet build
dotnet run
```

## Publish (single-file executable)

The project is configured for single-file publish. Example:

```bash
dotnet publish -c Release -r win-x64
```

Output is under `bin\Release\net8.0-windows\win-x64\publish\`. Choose `--self-contained true` or `false` depending on whether the target machine has the .NET runtime installed.

## Data

- Database file: `inventory.db` in the application base directory (created automatically on first run).
- Each item has a unique barcode, name, optional company and location, and quantity.

## Features

- **Grid view** of all items with search (name, barcode, company, location) and optional location filter.
- **Tag In / Tag Out** with a scan field: Enter applies the current mode using the configured quantity. Unknown barcodes in Tag In open **Add new item** with that barcode.
- **Barcodes for mode**: scan `**IN**` or `**OUT**` to switch Tag In / Tag Out without using the buttons.
- **Keyboard**: Ctrl+- (minus on main keyboard or numpad) switches to Tag Out.
- **Add**, **Edit** (toolbar or double-click row), and **Delete** items.
- Add/Edit supports dropdown suggestions from existing companies, names, and locations.

## Project layout

| Path | Role |
|------|------|
| `Program.cs` | Application entry |
| `Forms/` | Main window and add/edit dialog |
| `Models/Item.cs` | Item entity |
| `Database/InventoryDbContext.cs` | SQLite access and schema |

## License

See [LICENSE](LICENSE) (GNU Affero General Public License v3).
