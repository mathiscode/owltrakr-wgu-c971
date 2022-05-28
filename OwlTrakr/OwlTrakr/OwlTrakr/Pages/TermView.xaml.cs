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
        public Term Term;

        public TermView(Term term)
        {
            Term = term;
            BindingContext = new TermViewViewModel(term);
            InitializeComponent();
        }

        public async Task<Term> Fetch(int termId)
        {
            Term = await Data.FetchTerm(termId);
            BindingContext = new TermViewViewModel(Term);
            return Term;
        }

        private void SaveTerm_Clicked(object sender, EventArgs e)
        {
            Term.Title = ((Entry)this.FindByName("EditTerm_Title")).Text;
            Term.Start = ((DatePicker)this.FindByName("EditTerm_StartDate")).Date;
            Term.End = ((DatePicker)this.FindByName("EditTerm_EndDate")).Date;

            Data.UpdateTerm(Term);
            int oldIndex = TermListViewModel.instance.Terms.IndexOf(Term);
            TermListViewModel.instance.Terms[oldIndex] = Term;
            Navigation.PopAsync();
        }

        private void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            Data.DeleteTerm(Term);
            TermListViewModel.instance.Terms.Remove(Term);
            Navigation.PopAsync();
        }

        private void ViewCourse_Clicked(object sender, EventArgs e)
        {

        }

        private void AddCourse_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.CourseAdd(Term));
        }
    }
}