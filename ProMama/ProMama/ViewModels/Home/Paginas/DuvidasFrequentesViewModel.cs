using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class DuvidasFrequentesViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public List<DuvidaFrequente> DuvidasFrequentes { get; set; }

        public ICommand AbrirDuvidaCommand { get; set; }

        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;
        private readonly IRestService RestService;

        public DuvidasFrequentesViewModel(INavigation _navigation)
        {
            AbrirDuvidaCommand = new Command<DuvidaFrequente>(AbrirDuvida);

            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            RestService = DependencyService.Get<IRestService>();

            DuvidasFrequentes = App.DuvidaFrequenteDatabase.GetAll();
        }

        private async void AbrirDuvida(DuvidaFrequente duvidaFrequente)
        {
            await NavigationService.NavigateDuvidaFrequente(Navigation, duvidaFrequente);
        }
    }
}
