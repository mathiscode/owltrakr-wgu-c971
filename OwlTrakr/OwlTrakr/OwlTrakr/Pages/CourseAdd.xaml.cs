using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseAdd : ContentPage
    {
        private Term _term;

        public CourseAdd(Term term)
        {
            _term = term;
            BindingContext = term;
            InitializeComponent();

            List<string> StatusOptions = new List<string>();
            StatusOptions.Add("In Progress");
            StatusOptions.Add("Completed");
            StatusOptions.Add("Dropped");
            StatusOptions.Add("Plan to Take");

            Picker status = (Picker)FindByName("NewCourse_Status");
            status.ItemsSource = StatusOptions;
            status.SelectedItem = "Plan to Take";
        }

        async public void SaveCourse_Clicked (object sender, EventArgs e)
        {
            Course course = new Course()
            {
                TermId = this._term.Id,
                Title = ((Entry)FindByName("NewCourse_Title")).Text,
                Start = ((DatePicker)FindByName("NewCourse_StartDate")).Date,
                End = ((DatePicker)FindByName("NewCourse_EndDate")).Date,
                Status = (string)((Picker)FindByName("NewCourse_Status")).SelectedItem,
                InstructorName = ((Entry)FindByName("NewCourse_InstructorName")).Text,
                InstructorEmail = ((Entry)FindByName("NewCourse_InstructorEmail")).Text,
                InstructorPhone = ((Entry)FindByName("NewCourse_InstructorPhone")).Text,
                NotificationsEnabled = ((Switch)FindByName("NewCourse_NotificationsEnabled")).IsToggled,
                Notes = ((Editor)FindByName("NewCourse_Notes")).Text
            };

            if (String.IsNullOrEmpty(course.Title))
            {
                await DisplayAlert("Error", "You must provide the course title", "OK");
                return;
            }

            if (String.IsNullOrEmpty(course.InstructorName) || String.IsNullOrEmpty(course.InstructorPhone) || String.IsNullOrEmpty(course.InstructorEmail))
            {
                await DisplayAlert("Error", "You must provide all instructor details", "OK");
                return;
            }

            await Data.NewCourse(course);
            TermViewViewModel.instance._courses.Add(course);
            await Navigation.PopAsync();
        }
    }
}