using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class AcompanhamentoViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private ObservableCollection<Acompanhamento> _medicoes;
        public ObservableCollection<Acompanhamento> Medicoes
        {
            get { return _medicoes; }
            set
            {
                _medicoes = value;
                Notify("Medicoes");
            }

        }

        private INavigation Navigation { get; set; }
        public ICommand NavigationCommand { get; set; }

        private readonly INavigationService NavigationService;

        public AcompanhamentoViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationCommand = new Command(NavigateToAddAcompanhamento);
            NavigationService = DependencyService.Get<INavigationService>();

            Medicoes = new ObservableCollection<Acompanhamento>(App.AcompanhamentoDatabase.FindByChildId(app._crianca.crianca_id));
        }

        private async void NavigateToAddAcompanhamento()
        {
            await NavigationService.NavigateAddAcompanhamento(Navigation);
        }
    }
}
