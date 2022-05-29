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
    public partial class AssessmentList : ContentPage
    {
        public static ListView assessmentsListView;

        public AssessmentList(Term term, Course course)
        {
            InitializeComponent();
            assessmentsListView = (ListView)FindByName("AssessmentsListView");
            Startup(term, course);
        }

        async public void Startup(Term term, Course course)
        {

            AssessmentListViewModel viewModel = await AssessmentListViewModel.Create(term, course);
            BindingContext = viewModel;
        }

        private void NewAssessment_Clicked(object sender, EventArgs e)
        {
            Term term = ((AssessmentListViewModel)BindingContext)._term;
            Course course = ((AssessmentListViewModel)BindingContext)._course;
            Navigation.PushAsync(new Pages.AssessmentAdd(term, course));
        }

        private void Assessment_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Term term = ((AssessmentListViewModel)BindingContext)._term;
                Course course = ((AssessmentListViewModel)BindingContext)._course;
                Assessment assessment = (Assessment)e.SelectedItem;
                Navigation.PushAsync(new Pages.AssessmentView(term, course, assessment));
                assessmentsListView.SelectedItem = null;
            }
        }
    }
}