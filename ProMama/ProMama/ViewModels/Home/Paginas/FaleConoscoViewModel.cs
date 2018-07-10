using Acr.UserDialogs;
using Plugin.Connectivity;
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
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);

            if (CrossConnectivity.Current.IsConnected)
            {
                if (string.IsNullOrEmpty(ConversaTexto))
                {
                    LoadingDialog.Hide();
                    await MessageService.AlertDialog("Você deve preencher o campo de texto.");
                }
                else
                {
                    var result = await RestService.ConversaCreate(new JsonMessage(ConversaTexto), app._usuario.api_token);
                    if (!result.success)
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                    }
                    else
                    {
                        Conversa c = new Conversa(result.id, ConversaTexto, "Aguardando resposta.");
                        Conversas.Insert(0, c);
                        App.ConversaDatabase.Save(c);
                        ConversaTexto = string.Empty;
                        AvisoListaVazia = false;

                        LoadingDialog.Hide();
                    }
                }
            } else
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Você precisa estar conectado à internet para enviar um dúvida.");
            }
        }

        private void ConversasRead()
        {
            Conversas = new ObservableCollection<Conversa>(App.ConversaDatabase.GetConversasUser(app._usuario.id));
            AvisoListaVazia = Conversas.Count == 0 ? true : false;
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
