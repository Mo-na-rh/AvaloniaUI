using System;

namespace NetworkSpeedMonitor.Models
{
    public class AppointmentCustomItem
    {
        public string DateOfEvent { get; set; }

        public string Subject { get; set; }

        public string? ZoomUrl { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
