using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermAdd : ContentPage
    {
        public TermAdd()
        {
            BindingContext = TermListViewModel.instance;

            Func<Task<bool>> startup = async () =>
            {
                int termCount = await Data.CountTerms();
                Entry termEntry = (Entry)this.FindByName("NewTerm_Title");
                termEntry.Text = "Term " + (termCount + 1).ToString();
                return true;
            };

            startup();
            InitializeComponent();
        }

        async private void SaveTerm_Clicked(object sender, EventArgs e)
        {
            Term newTerm = new Term()
            {
                Title = ((Entry)this.FindByName("NewTerm_Title")).Text,
                Start = ((DatePicker)this.FindByName("NewTerm_StartDate")).Date,
                End   = ((DatePicker)this.FindByName("NewTerm_EndDate")).Date,
                NotificationsEnabled = ((Switch)FindByName("NewTerm_NotificationsEnabled")).IsToggled
            };

            if (String.IsNullOrEmpty(newTerm.Title))
            {
                await DisplayAlert("Error", "You must provide the term title", "OK");
                return;
            }

            if (newTerm.Start > newTerm.End)
            {
                await DisplayAlert("Error", "Start date must be before end date", "OK");
                return;
            }

            await Data.NewTerm(newTerm);
            TermListViewModel.instance.Terms.Add(newTerm);
            await Navigation.PopAsync();
        }

        private void NewTerm_StartDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            ((DatePicker)FindByName("NewTerm_EndDate")).MinimumDate = e.NewDate;
        }
    }
}