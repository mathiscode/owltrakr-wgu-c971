using OwlTrakr.ViewModels;
using System;
using Xamarin.Forms;

namespace OwlTrakr
{
    public partial class TermList : ContentPage
    {
        public static ListView termListView = null;
        //public static Label termListEmptyLabel = null;

        public TermList()
        {
            InitializeComponent();
            termListView = (ListView)this.FindByName("TermListView");
            //termListEmptyLabel = (Label)this.FindByName("TermListEmptyLabel");
            Startup();
        }

        public async void Startup()
        {
            termListView.BindingContext = await TermListViewModel.Create();
        }

        private void NewTerm_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.TermAdd());
        }

        private void Term_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Term term = (Term)e.SelectedItem;
                Navigation.PushAsync(new Pages.TermView(term));
                termListView.SelectedItem = null;
            }
        }
    }
}
