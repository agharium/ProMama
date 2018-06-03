using ProMama.Model;
using ProMama.ViewModel.Services;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class MarcoVisualizacaoViewModel : ViewModelBase
    {
        private string _titulo;
        public string Titulo
        {
            get
            {
                return _titulo;
            }
            set
            {
                _titulo = value;
                Notify("Titulo");
            }
        }

        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;

        public MarcoVisualizacaoViewModel(INavigation _navigation, Marco marco)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();

            Titulo = marco.Titulo;
        }
    }
}
