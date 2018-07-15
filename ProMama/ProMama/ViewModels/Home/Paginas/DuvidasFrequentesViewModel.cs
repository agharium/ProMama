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
            if (string.IsNullOrEmpty(termo))
            {
                DuvidasFrequentes = new ObservableCollection<DuvidaFrequente>(DuvidasFrequentesAux);
            } else
            {
                termo = Ferramentas.RemoverAcentos(termo.ToLower());
                var palavras = termo.Split(' ');

                DuvidasFrequentes = new ObservableCollection<DuvidaFrequente>(DuvidasFrequentesAux.Where(df => palavras.All(p => Ferramentas.RemoverAcentos(df.titulo.ToLower()).Contains(p))));
            }
        }

        private async void AbrirDuvida(DuvidaFrequente duvidaFrequente)
        {
            await NavigationService.NavigateDuvidaFrequente(Navigation, duvidaFrequente);
        }
    }
}
