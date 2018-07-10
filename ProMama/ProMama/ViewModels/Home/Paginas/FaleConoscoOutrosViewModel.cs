using Plugin.Connectivity;
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

        private bool _loadingVisibility;
        public bool LoadingVisibility
        {
            get
            {
                return _loadingVisibility;
            }
            set
            {
                _loadingVisibility = value;
                Notify("LoadingVisibility");
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

            ConversasRead();
        }

        private async void ConversasRead()
        {
            LoadingVisibility = true;
            if (CrossConnectivity.Current.IsConnected)
            {
                var conversas = new ObservableCollection<Conversa>(await RestService.ConversasRead(app._usuario.api_token));
                foreach (var obj in conversas)
                {
                     obj.resumo = Ferramentas.CriarResumo(obj.resposta);
                }
                LoadingVisibility = false;
                Conversas = new ObservableCollection<Conversa>(conversas);
            }
            else
            {
                LoadingVisibility = false;
                Conversas = new ObservableCollection<Conversa>(App.ConversaDatabase.GetConversasTodos());
            }

            ConversasAux = new List<Conversa>(Conversas);
            AvisoListaVazia = Conversas.Count == 0 ? true : false;
        }

        private void Buscar(string termo)
        {
            Conversas = string.IsNullOrEmpty(termo) ? 
                new ObservableCollection<Conversa>(ConversasAux) : 
                new ObservableCollection<Conversa>(ConversasAux.Where(c => Ferramentas.RemoverAcentos(c.pergunta.ToLower()).Contains(Ferramentas.RemoverAcentos(termo.ToLower()))));
            AvisoListaVazia = Conversas.Count == 0 ? true : false;
        }

        private async void AbrirConversa(Conversa conversa)
        {
            await NavigationService.NavigateConversa(Navigation, conversa);
        }
    }
}
