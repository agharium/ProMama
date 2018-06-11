using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class FaleConoscoViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _conversaTexto;
        public string ConversaTexto
        {
            get
            {
                return _conversaTexto;
            }
            set
            {
                _conversaTexto = value;
                Notify("ConversaTexto");
            }
        }

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

        private ObservableCollection<Conversa> _Conversas;
        public ObservableCollection<Conversa> Conversas
        {
            get { return _Conversas; }
            set
            {
                _Conversas = value;
                Notify("Conversas");
            }
        }

        private INavigation Navigation { get; set; }

        public ICommand EnviarConversaCommand { get; set; }
        public ICommand OutrasConversasCommand { get; set; }
        public ICommand AbrirConversaCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;
        private readonly INavigationService NavigationService;

        public FaleConoscoViewModel(INavigation _navigation)
        {
            ConversaTexto = "";
            Navigation = _navigation;

            EnviarConversaCommand = new Command(EnviarConversa);
            OutrasConversasCommand = new Command(OutrasConversas);
            AbrirConversaCommand = new Command<Conversa>(AbrirConversa);

            RestService = DependencyService.Get<IRestService>();
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();

            ConversasRead();
        }

        private async void EnviarConversa()
        {
            if (ConversaTexto.Equals(string.Empty))
            {
                await MessageService.AlertDialog("Você deve preencher o campo de dúvida.");
            }
            else
            {
                var result = await RestService.ConversaCreate(new JsonMessage(ConversaTexto), app._usuario.api_token);
                if (!result.success)
                {
                    await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                }
                else
                {
                    Conversa c = new Conversa(ConversaTexto, "Aguardando resposta.");
                    Conversas.Insert(0, c);
                    ConversaTexto = string.Empty;
                    AvisoListaVazia = "False";
                }
            }
        }

        private async void ConversasRead()
        {
            AvisoListaVazia = "False";

            var conversas = await RestService.ConversasUsuarioRead(app._usuario.api_token);
            foreach (var c in conversas)
            {
                if (c.resposta == null)
                {
                    c.resposta = "Aguardando resposta.";
                    c.resumo = "Aguardando resposta.";
                }
                else
                {
                    c.resumo = String.Join(" ", c.resposta.Split().Take(20).ToArray());
                    c.resumo.Remove(c.resumo.Length - 1, 1);
                    c.resumo += "...";
                }
            }
            Conversas = new ObservableCollection<Conversa>(conversas);

            if (Conversas.Count == 0)
                AvisoListaVazia = "True";
        }

        private async void OutrasConversas()
        {
            await NavigationService.NavigateOutrasConversas(Navigation);
        }

        private async void AbrirConversa(Conversa conversa)
        {
            if (conversa.resposta != "Aguardando resposta.")
            {
                await NavigationService.NavigateConversa(Navigation, conversa);
            }
        }

    }
}
