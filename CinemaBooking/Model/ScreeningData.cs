using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBooking
{
    internal class ScreeningData
    {
        public int ScreeningID { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public int SalonID { get; set; }
        public string SeatsAvailable { get; set; }
    }
}