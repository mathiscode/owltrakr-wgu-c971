using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OwlTrakr
{
    public partial class App : Application
    {
        public App()
        {
            Startup();
            InitializeComponent();
            MainPage = new NavigationPage(new TermList());
        }

        private void Startup()
        {
            Data.connect();
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
