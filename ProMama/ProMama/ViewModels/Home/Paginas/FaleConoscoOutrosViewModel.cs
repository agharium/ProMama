using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
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

        private string _avisoListaVazia;
        public string AvisoListaVazia
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

        private List<Conversa> ConversasAux { get; set; }

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

        private INavigation Navigation { get; set; }

        private readonly IRestService RestService;
        private readonly INavigationService NavigationService;

        public ICommand BuscarCommand { get; set; }
        public ICommand AbrirConversaCommand { get; set; }

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
            AvisoListaVazia = "False";

            Conversas = new ObservableCollection<Conversa>(await RestService.ConversasRead(app._usuario.api_token));
            foreach (var c in Conversas)
            {
                c.resumo = String.Join(" ", c.resposta.Split().Take(20).ToArray());
                c.resumo.Remove(c.resumo.Length - 1, 1);
                c.resumo += "...";
            }
            
            if (Conversas.Count == 0)
                AvisoListaVazia = "True";

            ConversasAux = new List<Conversa>(Conversas);
        }

        private void Buscar(string termo)
        {
            Conversas = string.IsNullOrEmpty(termo) ? new ObservableCollection<Conversa>(ConversasAux) : new ObservableCollection<Conversa>(ConversasAux.Where(c => c.pergunta.Contains(termo)));
            AvisoListaVazia = Conversas.Count == 0 ? "True" : "False";
        }

        private async void AbrirConversa(Conversa conversa)
        {
            await NavigationService.NavigateConversa(Navigation, conversa);
        }
    }
}
