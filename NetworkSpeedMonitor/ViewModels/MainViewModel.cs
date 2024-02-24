using Microsoft.Office.Interop.Outlook;
using NetworkSpeedMonitor.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace NetworkSpeedMonitor.ViewModels;

public class MainViewModel : ViewModelBase
{
    private NetworkInterface _currentNetworkType;

    private string _downloadSpeed = "-";

    private string _uploadSpeed = "-";

    public NetworkInterface CurrentNetworkType
    {
        get { return _currentNetworkType; }
        set
        {
            this.RaiseAndSetIfChanged(ref _currentNetworkType, value);
        }
    }
    public string DownloadSpeed
    {
        get => _downloadSpeed;
        set
        {
            this.RaiseAndSetIfChanged(ref _downloadSpeed, value);
        }
    }

    public string UploadSpeed
    {
        get => _uploadSpeed;
        set
        {
            this.RaiseAndSetIfChanged(ref _uploadSpeed, value);
        }
    }

    public List<NetworkInterface> Networks { get; set; }

    public MainViewModel()
    {

        Networks = NetworkInterface.GetAllNetworkInterfaces()
            .Where(x => x.OperationalStatus == OperationalStatus.Up)
            .ToList();
        CurrentNetworkType = Networks.FirstOrDefault();

        temp();
    }

    public List<AppointmentCustomItem> temp()
    {
        var res = new List<AppointmentCustomItem>();
        DateTime startDate = new DateTime(2024, 02, 26, 1, 0, 0);
        DateTime endDate = new DateTime(2024, 02, 27, 0, 0, 0);

        Application outlookApp = new Outlook.Application();
        Outlook.NameSpace outlookNamespace = outlookApp.GetNamespace("MAPI");
        Outlook.MAPIFolder calendarFolder = outlookNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);
        var cntr1 = 0;
        var cntr2 = 0;
        foreach (object item in calendarFolder.Items)
        {
            cntr1++;
            if (item is Outlook.AppointmentItem)
            {
                cntr2++;
                Outlook.AppointmentItem appointment = (Outlook.AppointmentItem)item;

                //if (appointment.Start >= startDate && appointment.End <= endDate)
                {
                    var newAppointment = new AppointmentCustomItem();

                    newAppointment.StringDate = appointment.Start.ToString("HH:mm") + " - " + appointment.End.ToString("HH:mm");
                    newAppointment.Subject = appointment.Subject;
                    newAppointment.Body = appointment.Body;

                    res.Add(newAppointment);
                    //Console.WriteLine($"Subject: {appointment.Subject}, Start: {appointment.Start}, End: {appointment.End}");
                }
            }
        }
        return res;
    }
}
