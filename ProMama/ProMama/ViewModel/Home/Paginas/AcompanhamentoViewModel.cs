using ProMama.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class AcompanhamentoViewModel : ViewModelBase
    {
        private ObservableCollection<Medicao> _medicoes;
        public ObservableCollection<Medicao> Medicoes
        {
            get { return _medicoes; }
            set
            {
                _medicoes = value;
            }

        }

        private INavigation Navigation { get; set; }
        public ICommand NavigationCommand { get; set; }

        private readonly Services.INavigationService _navigationService;

        public AcompanhamentoViewModel(INavigation Navigation)
        {
            this.Navigation = Navigation;
            this.NavigationCommand = new Command(this.NavigateToAddAcompanhamento);

            this._navigationService = DependencyService.Get<Services.INavigationService>();

            Medicoes = new ObservableCollection<Medicao>();

            Medicao teste1 = new Medicao("07/01/17", "4,5kg", "60cm", "Tipo 1");
            Medicoes.Add(teste1);

            Medicao teste2 = new Medicao("17/02/17", "5,5kg", "70cm", "Tipo 2");
            Medicoes.Add(teste2);

            Medicao teste3 = new Medicao("27/03/17", "6,5kg", "80cm", "Tipo 3");
            Medicoes.Add(teste3);
        }

        private async void NavigateToAddAcompanhamento()
        {
            await this._navigationService.NavigateToAddAcompanhamento(Navigation);
        }
    }
}
