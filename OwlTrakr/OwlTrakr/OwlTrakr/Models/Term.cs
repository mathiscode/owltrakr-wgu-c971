using SQLite;
using System;

namespace OwlTrakr.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool NotificationsEnabled { get; set; }

        public string DateRange {
            get { return Start.ToString("d") + " - " + End.ToString("d"); }
        }
    }
}
