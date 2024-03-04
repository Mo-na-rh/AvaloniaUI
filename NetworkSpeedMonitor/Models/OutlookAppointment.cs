using System;

namespace NetworkSpeedMonitor.Models
{
    public class OutlookAppointment
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string? ZoomUrl { get; set; }
    }
}
