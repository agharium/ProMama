using ProMama.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class GaleriaViewModel : ViewModelBase
    {
        private ObservableCollection<Foto> _fotos;
        public ObservableCollection<Foto> Fotos
        {
            get { return _fotos; }
            set
            {
                _fotos = value;
            }

        }

        //private INavigation Navigation { get; set; }
        //public ICommand NavigationCommand { get; set; }

        //private readonly Services.INavigationService _navigationService;

        public GaleriaViewModel()
        {
            //this.Navigation = Navigation;
            //this.NavigationCommand = new Command(this.NavigateToAddAcompanhamento);

            //this._navigationService = DependencyService.Get<Services.INavigationService>();

            Fotos = new ObservableCollection<Foto>();

            Foto teste1 = new Foto(1, "07/01/17", "baby.jpeg");
            Fotos.Add(teste1);

            Foto teste2 = new Foto(2, "17/02/17", "baby.jpeg");
            Fotos.Add(teste2);

            Foto teste3 = new Foto(3, "27/03/17", "baby.jpeg");
            Fotos.Add(teste3);
        }
    }
}
