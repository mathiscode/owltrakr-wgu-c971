using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OwlTrakr.ViewModels
{
    class TermListViewModel : BaseViewModel
    {
        ObservableCollection<Term> _terms;
        public static TermListViewModel instance;

        public static async Task<TermListViewModel> Create()
        {
            ObservableCollection<Term> terms = await Data.FetchTerms();
            return new TermListViewModel(terms);
        }

        public TermListViewModel(ObservableCollection<Term> terms)
        {
            instance = this;
            Terms = terms;
        }

        public ObservableCollection<Term> Terms
        {
            get { return _terms; }
            set
            {
                SetValue(ref _terms, value);
                OnPropertyChanged();
            }
        }
    }
}
