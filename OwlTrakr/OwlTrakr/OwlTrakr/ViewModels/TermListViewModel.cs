using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OwlTrakr.ViewModels
{
    class TermListViewModel : BaseViewModel
    {
        public static TermListViewModel instance;
        ObservableCollection<Term> _terms = null;

        public TermListViewModel()
        {
            instance = this;
            if (_terms === null) Terms = 
        }

        async public void Fetch()
        {
            Terms = await Data.FetchTerms();
            OnPropertyChanged();
        }

        public ObservableCollection<Term> Terms
        {
            get { return _terms; }
            set
            {
                SetValue(ref _terms, value);
                OnPropertyChanged();
                //Fetch();
            }
        }
    }
}
