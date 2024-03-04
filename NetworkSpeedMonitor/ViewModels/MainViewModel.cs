using Microsoft.Office.Interop.Outlook;
using NetworkSpeedMonitor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace NetworkSpeedMonitor.ViewModels;

public class MainViewModel : ViewModelBase
{

    public List<AppointmentCustomItem> Appointments { get; set; }

    public ObservableCollection<AppointmentCustomItem> Appointments1 { get; set; }

    public MainViewModel()
    {
        Appointments = new List<AppointmentCustomItem>();
        InitializeAppointments();

    }

    public void InitializeAppointments()
    {
        DateTime startDate = DateTime.Now.Date;
        DateTime endDate = DateTime.Now.Date.AddDays(1);

        Application outlookApp = new Outlook.Application();
        Outlook.Folder? calendarFolder = outlookApp.Session.GetDefaultFolder(
                                            Outlook.OlDefaultFolders.olFolderCalendar)
                                            as Outlook.Folder;

        Outlook.Items rangeAppts = GetAppointmentsInRange(calendarFolder, startDate, endDate);
        if (rangeAppts != null)
        {
            foreach (Outlook.AppointmentItem appointment in rangeAppts)
            {
                var newAppointment = new AppointmentCustomItem();

                newAppointment.DateOfEvent = appointment.Start.ToString("HH:mm") + " - " + appointment.End.ToString("HH:mm") + " (" + appointment.Start.ToString("MM/dd/yy") + ")";
                newAppointment.Subject = appointment.Subject;
                newAppointment.ZoomUrl = GetZoomMeetingUrlFromAppointment(appointment);
                newAppointment.Subject = appointment.Subject;
                newAppointment.Start = appointment.Start;
                newAppointment.End = appointment.End;
                Appointments.Add(newAppointment);
            }
        }

        Appointments = Appointments.OrderBy(t => t.Start).ToList();
        Appointments1 = new ObservableCollection<AppointmentCustomItem>(Appointments);
    }

    static string GetZoomMeetingUrlFromAppointment(Outlook.AppointmentItem appointment)
    {
        var res = string.Empty;
        try
        {
            string body = appointment?.Body;
            string zoomUrlIdentifier = "https://us06web.zoom.us";
            if (string.IsNullOrEmpty(body))
                return null;
            int zoomUrlIndex = body.IndexOf(zoomUrlIdentifier);

            if (zoomUrlIndex != -1)
            {
                int endIndex = body.IndexOf(" ", zoomUrlIndex);

                if (endIndex == -1)
                {
                    res = body.Substring(zoomUrlIndex);
                }
                else
                {
                    res = body.Substring(zoomUrlIndex, endIndex - zoomUrlIndex);
                }
            }
        }
        catch
        {
            var tst = 0;
        }

        if (string.IsNullOrEmpty(res))
        {
            string body = appointment?.Body;
            string zoomUrlIdentifier = "https://us04web.zoom.us";
            if (string.IsNullOrEmpty(body))
                return null;
            int zoomUrlIndex = body.IndexOf(zoomUrlIdentifier);

            if (zoomUrlIndex != -1)
            {
                int endIndex = body.IndexOf(" ", zoomUrlIndex);

                if (endIndex == -1)
                {
                    res = body.Substring(zoomUrlIndex);
                }
                else
                {
                    res = body.Substring(zoomUrlIndex, endIndex - zoomUrlIndex);
                }
            }
        }

        if (string.IsNullOrEmpty(res))
        {
            string body = appointment?.Body;
            string zoomUrlIdentifier = "https://call.vk.sminex.com";
            if (string.IsNullOrEmpty(body))
                return null;
            int zoomUrlIndex = body.IndexOf(zoomUrlIdentifier);

            if (zoomUrlIndex != -1)
            {
                int endIndex = body.IndexOf(" ", zoomUrlIndex);

                if (endIndex == -1)
                {
                    res = body.Substring(zoomUrlIndex);
                }
                else
                {
                    res = body.Substring(zoomUrlIndex, endIndex - zoomUrlIndex);
                }
            }
        }




        return res;

    }

    /// <summary>
    /// Get recurring appointments in date range.
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns>Outlook.Items</returns>
    private Outlook.Items GetAppointmentsInRange(
        Outlook.Folder folder, DateTime startTime, DateTime endTime)
    {
        string filter = "[Start] >= '"
            + startTime.ToString("g")
            + "' AND [End] <= '"
            + endTime.ToString("g") + "'";
        Debug.WriteLine(filter);
        try
        {
            Outlook.Items calItems = folder.Items;
            calItems.IncludeRecurrences = true;
            calItems.Sort("[Start]", Type.Missing);
            Outlook.Items restrictItems = calItems.Restrict(filter);
            if (restrictItems.Count > 0)
            {
                return restrictItems;
            }
            else
            {
                return null;
            }
        }
        catch { return null; }
    }
}
