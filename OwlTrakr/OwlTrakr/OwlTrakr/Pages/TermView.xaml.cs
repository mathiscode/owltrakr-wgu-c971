using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermView : ContentPage
    {
        private ObservableCollection<Course> Courses { get; set; }

        public TermView(Term term)
        {
            InitializeComponent();
            Startup(term);
        }

        async private void Startup(Term term)
        {

            BindingContext = await TermViewViewModel.Create(term.Id);
            ((Switch)EditTerm_NotificationsEnabled).IsToggled = term.NotificationsEnabled;
        }

        async private void SaveTerm_Clicked(object sender, EventArgs e)
        {
            Term term = TermViewViewModel.instance._term;
            term.Title = ((Entry)this.FindByName("EditTerm_Title")).Text;
            term.Start = ((DatePicker)this.FindByName("EditTerm_StartDate")).Date;
            term.End = ((DatePicker)this.FindByName("EditTerm_EndDate")).Date;
            term.NotificationsEnabled = ((Switch)FindByName("EditTerm_NotificationsEnabled")).IsToggled;

            if (term.Start > term.End)
            {
                await DisplayAlert("Error", "Start date must be before end date", "OK");
                return;
            }

            await Data.UpdateTerm(term);
            TermListViewModel.instance.RefreshTerms();
            await Navigation.PopAsync();
        }

        async private void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            await Data.DeleteTerm(TermViewViewModel.instance._term);
            TermListViewModel.instance.RefreshTerms();
            await Navigation.PopAsync();
        }

        async private void AddCourse_Clicked(object sender, EventArgs e)
        {
            Term term = TermViewViewModel.instance._term;

            int courseCount = await Data.CountCourses(term);
            if (courseCount >= 6)
            {
                await DisplayAlert("Error", "A term may have no more than 6 courses", "OK");
                return;
            }

            Navigation.PushAsync(new Pages.CourseAdd(term));
        }

        private void Courses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Term term = TermViewViewModel.instance._term;
                Course course = (Course)e.SelectedItem;
                Navigation.PushAsync(new Pages.CourseView(term, course));
                ((ListView)FindByName("Courses_ListView")).SelectedItem = null;
            }
        }

        private void EditTerm_StartDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            ((DatePicker)FindByName("EditTerm_EndDate")).MinimumDate = e.NewDate;
        }
    }
}