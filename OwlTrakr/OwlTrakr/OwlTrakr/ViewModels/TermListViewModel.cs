using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OwlTrakr.ViewModels
{
    class TermListViewModel : BaseViewModel
    {
        ObservableCollection<Term> _terms = Data.Terms;

        public TermListViewModel()
        {
            Fetch();
        }

        async public void Fetch()
        {
            Terms = await Data.FetchTerms();
        }

        public ObservableCollection<Term> Terms
        {
            get { return _terms; }
            set
            {
                SetValue(ref _terms, value);
                OnPropertyChanged();
                Fetch();
            }
        }
    }
}
