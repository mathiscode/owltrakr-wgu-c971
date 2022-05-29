using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OwlTrakr.Models;

namespace OwlTrakr.ViewModels
{
    class TermViewViewModel : BaseViewModel
    {
        public Term _term;
        public int Id { get; set; }
        public string Title { get; set; }
        public string PageTitle { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string DateRange { get; set; }
        public ObservableCollection<Course> _courses;
        public static TermViewViewModel instance;

        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                SetValue(ref _courses, value);
                OnPropertyChanged();
            }
        }

        public static async Task<TermViewViewModel> Create(int termId)
        {
            Term term = await Data.FetchTerm(termId);
            ObservableCollection<Course> courses = await Data.FetchCourses(termId);
            instance = new TermViewViewModel(term, courses);
            return instance;
        }

        public TermViewViewModel(Term term, ObservableCollection<Course> courses)
        {
            Id = term.Id;
            Title = term.Title;
            Start = term.Start;
            End = term.End;
            DateRange = term.DateRange;
            PageTitle = "Term: " + term.Title;
            _term = term;
            _courses = courses;
        }

        async public void RefreshCourses()
        {
            Courses = await Data.FetchCourses(Id);
        }
    }
}
