using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class DuvidasViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _duvidaTexto;
        public string DuvidaTexto
        {
            get
            {
                return _duvidaTexto;
            }
            set
            {
                _duvidaTexto = value;
                Notify("DuvidaTexto");
            }
        }

        private string _indicadorLoading;
        public string IndicadorLoading
        {
            get
            {
                return _indicadorLoading;
            }
            set
            {
                _indicadorLoading = value;
                Notify("IndicadorLoading");
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

        private ObservableCollection<Duvida> _Duvidas;
        public ObservableCollection<Duvida> Duvidas
        {
            get { return _Duvidas; }
            set
            {
                _Duvidas = value;
                Notify("Duvidas");
            }
        }

        private INavigation Navigation { get; set; }

        public ICommand EnviarDuvidaCommand { get; set; }
        public ICommand OutrasDuvidasCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;
        private readonly INavigationService NavigationService;

        public DuvidasViewModel(INavigation _navigation)
        {
            DuvidaTexto = "";
            Navigation = _navigation;

            EnviarDuvidaCommand = new Command(EnviarDuvida);
            OutrasDuvidasCommand = new Command(OutrasDuvidas);

            RestService = DependencyService.Get<IRestService>();
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();

            DuvidasRead();
        }

        public async void EnviarDuvida()
        {
            if (DuvidaTexto.Equals(string.Empty))
            {
                await MessageService.AlertDialog("Você deve preencher o campo de dúvida.");
            }
            else
            {
                var result = await RestService.DuvidaCreate(new JsonMessage(DuvidaTexto), app._usuario.api_token);
                if (!result.success)
                {
                    await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                }
                else
                {
                    Duvida d = new Duvida(DuvidaTexto, "Aguardando resposta.");
                    Duvidas.Insert(0, d);
                    DuvidaTexto = string.Empty;
                    IndicadorLoading = "False";
                }
            }
        }

        public async void DuvidasRead()
        {
            IndicadorLoading = "True";
            AvisoListaVazia = "False";

            var duvidas = await RestService.DuvidasUsuarioRead(app._usuario.api_token);
            foreach (var d in duvidas)
            {
                if (d.duvida_resposta == null)
                {
                    d.duvida_resposta = "Aguardando resposta.";
                }
            }
            Duvidas = new ObservableCollection<Duvida>(duvidas);

            IndicadorLoading = "False";
            if (Duvidas.Count == 0) AvisoListaVazia = "True";
        }

        public async void OutrasDuvidas()
        {
            await NavigationService.NavigateOutrasDuvidas(Navigation);
        }
    }
}
