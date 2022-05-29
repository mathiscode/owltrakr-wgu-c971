using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentView : ContentPage
    {
        public Term _term { get; set; }
        public Course _course { get; set; }
        public Assessment _assessment { get; set; }
        public string PageTitle { get; set; }

        public AssessmentView(Term term, Course course, Assessment assessment)
        {
            _term = term;
            _course = course;
            _assessment = assessment;
            BindingContext = this;
            PageTitle = "Assessment: " + course.Title;
            InitializeComponent();

            List<string> TypeOptions = new List<string>();
            TypeOptions.Add("Objective Assessment");
            TypeOptions.Add("Performance Assessment");

            Picker type = (Picker)FindByName("EditAssessment_Type");
            type.ItemsSource = TypeOptions;
            type.SelectedItem = assessment.Type;
        }

        async private void SaveAssessment_Clicked(object sender, EventArgs e)
        {
            Picker type = (Picker)FindByName("EditAssessment_Type");
            _assessment.Type = (string)type.SelectedItem;

            if (String.IsNullOrEmpty(_course.Title))
            {
                await DisplayAlert("Error", "You must provide the assessment title", "OK");
                return;
            }

            await Data.UpdateAssessment(_assessment);
            AssessmentListViewModel.instance.RefreshAssessments();
            await Navigation.PopAsync();
        }

        async private void DeleteAssessment_Clicked(object sender, EventArgs e)
        {
            await Data.DeleteAssessment(_assessment);
            AssessmentListViewModel.instance.RefreshAssessments();
            await Navigation.PopAsync();
        }
    }
}