using ProMama.Model;
using System.Collections.ObjectModel;

namespace ProMama.ViewModel.Home.Paginas
{
    class DuvidasFrequentesViewModel : ViewModelBase
    {
        private ObservableCollection<Duvida> _duvidas;
        public ObservableCollection<Duvida> Duvidas
        {
            get { return _duvidas; }
            set
            {
                _duvidas = value;
            }

        }

        //private INavigation Navigation { get; set; }
        //public ICommand NavigationCommand { get; set; }

        //private readonly Services.INavigationService _navigationService;

        public DuvidasFrequentesViewModel()
        {
            //this.Navigation = Navigation;
            //this.NavigationCommand = new Command(this.NavigateToAddAcompanhamento);

            //this._navigationService = DependencyService.Get<Services.INavigationService>();

            Duvidas = new ObservableCollection<Duvida>();

            Duvida teste1 = new Duvida("Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? Pergunta longa? ", "Resposta curta");
            Duvidas.Add(teste1);

            Duvida teste2 = new Duvida("Pergunta curta?", "Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa Resposta longa ");
            Duvidas.Add(teste2);

            Duvida teste3 = new Duvida("Pergunta curta?", "Resposta curta.");
            Duvidas.Add(teste3);
        }
    }
}
