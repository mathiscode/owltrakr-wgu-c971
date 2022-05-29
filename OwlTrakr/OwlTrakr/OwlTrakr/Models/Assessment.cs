using SQLite;
using System;

namespace OwlTrakr.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int TermId { get; set; }
        [Indexed]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool NotificationsEnabled { get; set; }

        public string DateRange
        {
            get { return Start.ToString("d") + " - " + End.ToString("d"); }
        }
    }
}
