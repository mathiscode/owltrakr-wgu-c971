using SQLite;
using System;

namespace OwlTrakr.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int TermId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public bool NotificationsEnabled { get; set; }
        public string Notes { get; set; }

        public string DateRange
        {
            get { return Start.ToString("d") + " - " + End.ToString("d"); }
        }
    }
}
