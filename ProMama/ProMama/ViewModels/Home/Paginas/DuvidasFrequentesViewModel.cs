using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class DuvidasFrequentesViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private List<DuvidaFrequente> DuvidasFrequentesAux { get; set; }

        private ObservableCollection<DuvidaFrequente> _duvidasFrequentes;
        public ObservableCollection<DuvidaFrequente> DuvidasFrequentes {
            get
            {
                return _duvidasFrequentes;
            }
            set
            {
                _duvidasFrequentes = value;
                Notify("DuvidasFrequentes");
            }
        }

        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;
        private readonly IRestService RestService;

        public ICommand AbrirDuvidaCommand { get; set; }
        public ICommand BuscarCommand { get; set; }

        public DuvidasFrequentesViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            RestService = DependencyService.Get<IRestService>();

            AbrirDuvidaCommand = new Command<DuvidaFrequente>(AbrirDuvida);
            BuscarCommand = new Command<string>(Buscar);

            DuvidasFrequentes = new ObservableCollection<DuvidaFrequente>(App.DuvidaFrequenteDatabase.GetAll());
            DuvidasFrequentesAux = new List<DuvidaFrequente>(DuvidasFrequentes);
        }

        private void Buscar(string termo)
        {
            DuvidasFrequentes = string.IsNullOrEmpty(termo) ?
                new ObservableCollection<DuvidaFrequente>(DuvidasFrequentesAux) :
                new ObservableCollection<DuvidaFrequente>(DuvidasFrequentesAux.Where(df => Ferramentas.RemoverAcentos(df.titulo.ToLower()).Contains(Ferramentas.RemoverAcentos(termo.ToLower()))));
        }

        private async void AbrirDuvida(DuvidaFrequente duvidaFrequente)
        {
            await NavigationService.NavigateDuvidaFrequente(Navigation, duvidaFrequente);
        }

        
    }
}
