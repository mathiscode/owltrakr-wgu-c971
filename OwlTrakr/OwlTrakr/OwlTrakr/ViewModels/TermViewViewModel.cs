using System;
using System.Collections.ObjectModel;
using OwlTrakr.Models;

namespace OwlTrakr.ViewModels
{
    class TermViewViewModel : BaseViewModel
    {
        public Term _term;
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string DateRange { get; set; }
        public ObservableCollection<Course> Courses { get; set; }

        public Term Term
        {
            get { return _term; }
            set
            {
                SetValue(ref _term, value);
                OnPropertyChanged();
            }
        }

        public TermViewViewModel(){}

        public TermViewViewModel(Term term)
        {
            Id = term.Id;
            Title = term.Title;
            Start = term.Start;
            End = term.End;
            DateRange = term.DateRange;
            _term = term;

            FetchCourses();
        }

        async public void FetchCourses()
        {
            Courses = await Data.FetchCourses(Id);
        }
    }
}
