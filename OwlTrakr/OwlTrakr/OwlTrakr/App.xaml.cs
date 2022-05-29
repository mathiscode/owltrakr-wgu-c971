using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using Plugin.LocalNotifications;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

namespace OwlTrakr
{
    public partial class App : Application
    {
        public App()
        {
            // For easy development and testing
            // File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "OwlTrakr.db"));

            MainPage = new NavigationPage(new TermList());
            InitializeComponent();
            Startup();
        }

        async private void Startup()
        {
            await Data.Connect();
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            ObservableCollection<Term> terms = await Data.FetchTerms();
            ObservableCollection<Course> courses = await Data.FetchCourses();
            ObservableCollection<Assessment> assessments = await Data.FetchAssessments();

            foreach (Term term in terms)
            {
                DateTime start = new DateTime(term.Start.Year, term.Start.Month, term.Start.Day);
                DateTime end = new DateTime(term.End.Year, term.End.Month, term.End.Day);

                if (term.NotificationsEnabled)
                {
                    if (start == today) CrossLocalNotifications.Current.Show("Term Starts Today", term.Title + " starts today!");
                    if (end == today) CrossLocalNotifications.Current.Show("Term Ends Today", term.Title + " is ending today!");
                }
            }

            foreach (Course course in courses)
            {
                DateTime start = new DateTime(course.Start.Year, course.Start.Month, course.Start.Day);
                DateTime end = new DateTime(course.End.Year, course.End.Month, course.End.Day);

                if (course.NotificationsEnabled)
                {
                    if (start == today) CrossLocalNotifications.Current.Show("Course Starts Today", course.Title + " starts today!");
                    if (end == today) CrossLocalNotifications.Current.Show("Course Ends Today", course.Title + " is ending today!");
                }
            }

            foreach (Assessment assessment in assessments)
            {
                DateTime start = new DateTime(assessment.Start.Year, assessment.Start.Month, assessment.Start.Day);
                DateTime end = new DateTime(assessment.End.Year, assessment.End.Month, assessment.End.Day);

                if (assessment.NotificationsEnabled)
                {
                    if (start == today) CrossLocalNotifications.Current.Show("Assessment Starts Today", assessment.Title + " starts today!");
                    if (end == today) CrossLocalNotifications.Current.Show("Assessment Ends Today", assessment.Title + " is ending today!");
                }
            }

            await TermList.Startup();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
