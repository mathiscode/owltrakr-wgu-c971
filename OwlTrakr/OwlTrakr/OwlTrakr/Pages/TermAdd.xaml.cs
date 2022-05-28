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
            InitializeComponent();
            BindingContext = TermListViewModel.instance;

            Func<Task<bool>> startup = async () =>
            {
                int termCount = await Data.CountTerms();
                Entry termEntry = (Entry)this.FindByName("NewTerm_Title");
                termEntry.Text = "Term " + (termCount + 1).ToString();
                return true;
            };

            startup();
        }

        private void SaveTerm_Clicked(object sender, EventArgs e)
        {
            Term newTerm = new Term()
            {
                Title = ((Entry)this.FindByName("NewTerm_Title")).Text,
                Start = ((DatePicker)this.FindByName("NewTerm_StartDate")).Date,
                End   = ((DatePicker)this.FindByName("NewTerm_EndDate")).Date
            };

            _ = Data.NewTerm(newTerm);
            TermListViewModel.instance.Terms.Add(newTerm);
            Navigation.PopAsync();
        }
    }
}