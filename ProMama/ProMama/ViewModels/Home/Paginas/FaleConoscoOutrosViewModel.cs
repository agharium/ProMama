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
    class FaleConoscoOutrosViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private bool _avisoListaVazia;
        public bool AvisoListaVazia
        {
            get
            {
                return _avisoListaVazia;
            }
            set
            {
                _avisoListaVazia = value;
                Notify("AvisoListaVazia");
            }
        }

        private ObservableCollection<Conversa> _conversas;
        public ObservableCollection<Conversa> Conversas
        {
            get { return _conversas; }
            set
            {
                _conversas = value;
                Notify("Conversas");
            }
        }

        private List<Conversa> ConversasAux { get; set; }

        private INavigation Navigation { get; set; }
        private readonly IRestService RestService;
        private readonly INavigationService NavigationService;
        
        public ICommand AbrirConversaCommand { get; set; }
        public ICommand BuscarCommand { get; set; }

        public FaleConoscoOutrosViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            RestService = DependencyService.Get<IRestService>();
            NavigationService = DependencyService.Get<INavigationService>();

            BuscarCommand = new Command<string>(Buscar);
            AbrirConversaCommand = new Command<Conversa>(AbrirConversa);

            Conversas = new ObservableCollection<Conversa>(App.ConversaDatabase.GetConversasTodos());
            AvisoListaVazia = Conversas.Count == 0 ? true : false;
            ConversasAux = new List<Conversa>(Conversas);
        }

        private void Buscar(string termo)
        {
            Conversas = string.IsNullOrEmpty(termo) ? 
                new ObservableCollection<Conversa>(ConversasAux) : 
                new ObservableCollection<Conversa>(ConversasAux.Where(c => Ferramentas.removerAcentos(c.pergunta.ToLower()).Contains(Ferramentas.removerAcentos(termo.ToLower()))));
            AvisoListaVazia = Conversas.Count == 0 ? true : false;
        }

        private async void AbrirConversa(Conversa conversa)
        {
            await NavigationService.NavigateConversa(Navigation, conversa);
        }
    }
}
