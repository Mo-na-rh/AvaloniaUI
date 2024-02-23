using Avalonia.Controls;

namespace NetworkSpeedMonitor.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var workingArea = Screens.Primary?.WorkingArea;

        var x = (int)(workingArea?.Width - this.Width) - 40;
        var y = (int)(workingArea?.Height - this.Height) - 60;

        Position = new Avalonia.PixelPoint(x, y);
    }
}
