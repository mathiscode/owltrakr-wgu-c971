using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseView : ContentPage
    {
        public Term _term { get; set; }
        public Course _course { get; set; }
        public string PageTitle { get; set; }

        public CourseView(Term term, Course course)
        {
            _term = term;
            _course = course;
            BindingContext = this;
            PageTitle = "Course: " + course.Title;
            InitializeComponent();

            List<string> StatusOptions = new List<string>();
            StatusOptions.Add("In Progress");
            StatusOptions.Add("Completed");
            StatusOptions.Add("Dropped");
            StatusOptions.Add("Plan to Take");

            Picker status = (Picker)FindByName("EditCourse_Status");
            status.ItemsSource = StatusOptions;
            status.SelectedItem = course.Status;
        }

        async private void SaveCourse_Clicked(object sender, EventArgs e)
        {
            Picker status = (Picker)FindByName("EditCourse_Status");
            _course.Status = (string)status.SelectedItem;

            if (String.IsNullOrEmpty(_course.Title))
            {
                await DisplayAlert("Error", "You must provide the course title", "OK");
                return;
            }

            if (String.IsNullOrEmpty(_course.InstructorName) || String.IsNullOrEmpty(_course.InstructorPhone) || String.IsNullOrEmpty(_course.InstructorEmail))
            {
                await DisplayAlert("Error", "You must provide all instructor details", "OK");
                return;
            }

            await Data.UpdateCourse(_course);
            TermViewViewModel.instance.RefreshCourses();
            await Navigation.PopAsync();
        }

        private void ViewAssessments_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.AssessmentList(_term, _course));
        }

        private void ShareNotes_Clicked(object sender, EventArgs e)
        {
            string notes = ((Editor)FindByName("EditCourse_Notes")).Text;
            Share.RequestAsync(new ShareTextRequest
            {
                Text = _term.Title + ", " + _course.Title + " Notes: " + notes,
                Title = "Share Course Notes"
            });
        }

        async private void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            await Data.DeleteCourse(_course);
            TermViewViewModel.instance.RefreshCourses();
            await Navigation.PopAsync();
        }
    }
}