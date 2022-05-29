using OwlTrakr.Models;
using OwlTrakr.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OwlTrakr
{
    public partial class TermList : ContentPage
    {
        public static ListView termListView;

        public TermList()
        {
            InitializeComponent();
            termListView = (ListView)FindByName("TermListView");
        }

        async public static Task Startup()
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

        private void Show_AboutInfo(object sender, EventArgs e)
        {
            DisplayAlert("OwlTrakr", "By Jamey Mathis, for WGU C971", "OK");
        }
    }
}
