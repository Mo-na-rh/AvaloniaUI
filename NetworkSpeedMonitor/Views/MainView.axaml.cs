using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Diagnostics;

namespace NetworkSpeedMonitor.Views;

public partial class MainView : UserControl
{
    // private DataGrid myDataGrid1;


    private DateTime lastClickTime = DateTime.MinValue;
    private const int doubleClickMilliseconds = 300; // Adjust this value as needed

    public MainView()
    {
        InitializeComponent();
    }


    private void DataGrid_CellDoubleClick(object sender, PointerPressedEventArgs e)
    {
        var test = "12";
        if (e.ClickCount == 2)
        {
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now - lastClickTime;
            if (elapsed.TotalMilliseconds < doubleClickMilliseconds)
            {
                // Double-click logic
                HandleCellDoubleClick(e);
            }

            lastClickTime = now;
        }
    }

    private void HandleCellDoubleClick(PointerPressedEventArgs e)
    {
        // Implement your logic for double-clicking a cell
        if (e.Source is DataGridCell cell)
        {
            if (cell != null)
            {
                var path = cell.Content as string;
                Process.Start(path);
            }
            // Access the content of the cell, e.g., cell.Content
            // Perform actions based on the cell content or row data
        }
    }
}
