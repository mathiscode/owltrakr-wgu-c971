using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OwlTrakr.Models;

namespace OwlTrakr.ViewModels
{
    class AssessmentListViewModel : BaseViewModel
    {
        public ObservableCollection<Assessment> _assessments;
        public Term _term { get; set; }
        public Course _course { get; set; }
        public static AssessmentListViewModel instance;
        public string Detail { get; set; }
        public string PageTitle { get; set; }
        public string PageSubtitle { get; set; }

        public static async Task<AssessmentListViewModel> Create(Term term, Course course)
        {
            ObservableCollection<Assessment> assessments = await Data.FetchAssessments(term, course);
            return new AssessmentListViewModel(term, course, assessments);
        }

        public AssessmentListViewModel(Term term, Course course, ObservableCollection<Assessment> assessments)
        {
            _term = term;
            _course = course;
            Assessments = assessments;
            instance = this;
            PageTitle = "Assessments: " + term.Title;
            PageSubtitle = "For Course: " + course.Title;
        }

        async public void RefreshAssessments()
        {
            Assessments = await Data.FetchAssessments(_term, _course);
        }

        public ObservableCollection<Assessment> Assessments
        {
            get { return _assessments; }
            set
            {
                SetValue(ref _assessments, value);
                OnPropertyChanged();
            }
        }
    }
}
