using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace NetworkSpeedMonitor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private NetworkInterface _currentNetworkType;

    public NetworkInterface CurrentNetworkType
    {
        get { return _currentNetworkType; }
        set
        {
            this.RaiseAndSetIfChanged(ref _currentNetworkType, value);
        }
    }

    public List<NetworkInterface> Networks { get; set; }

    public MainViewModel()
    {

        Networks = NetworkInterface.GetAllNetworkInterfaces()
            .Where(x => x.OperationalStatus == OperationalStatus.Up)
            .ToList();
        CurrentNetworkType = Networks.FirstOrDefault();
    }
}
