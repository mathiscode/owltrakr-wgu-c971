using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseAdd : ContentPage
    {
        public CourseAdd(Term term)
        {
            BindingContext = term;

            List<string> StatusOptions = new List<string>();
            StatusOptions.Add("In Progress");
            StatusOptions.Add("Completed");
            StatusOptions.Add("Dropped");
            StatusOptions.Add("Plan to Take");

            InitializeComponent();
            Picker status = (Picker)FindByName("NewCourse_Status");
            status.ItemsSource = StatusOptions;
            status.SelectedItem = "Plan to Take";
        }

        public void SaveCourse_Clicked (object sender, EventArgs e)
        {

        }
    }
}