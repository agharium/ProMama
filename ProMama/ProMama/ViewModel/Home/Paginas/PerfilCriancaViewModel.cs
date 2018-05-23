using Plugin.Connectivity;
using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class PerfilCriancaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _nomeCompleto;
        public string NomeCompleto
        {
            get { return _nomeCompleto; }
            set
            {
                _nomeCompleto = value;
                Notify("NomeCompleto");
            }
        }

        private string _dataNascimento;
        public string DataNascimento
        {
            get { return _dataNascimento; }
            set
            {
                _dataNascimento = value;
                Notify("DataNascimento");
            }
        }

        private string _sexo;
        public string Sexo
        {
            get { return _sexo; }
            set
            {
                _sexo = value;
                Notify("Sexo");
            }
        }

        private string _pesoAoNascer;
        public string PesoAoNascer
        {
            get { return _pesoAoNascer; }
            set
            {
                _pesoAoNascer = value;
                Notify("PesoAoNascer");
            }
        }

        private string _alturaAoNascer;
        public string AlturaAoNascer
        {
            get { return _alturaAoNascer; }
            set
            {
                _alturaAoNascer = value;
                Notify("AlturaAoNascer");
            }
        }

        private string _tipoDeParto;
        public string TipoDeParto
        {
            get { return _tipoDeParto; }
            set
            {
                _tipoDeParto = value;
                Notify("TipoDeParto");
            }
        }

        private string _idadeGestacional;
        public string IdadeGestacional
        {
            get { return _idadeGestacional; }
            set
            {
                _idadeGestacional = value;
                Notify("IdadeGestacional");
            }
        }

        private string _outrasInformacoes;
        public string OutrasInformacoes
        {
            get { return _outrasInformacoes; }
            set
            {
                _outrasInformacoes = value;
                Notify("OutrasInformacoes");
            }
        }

        private INavigation Navigation { get; set; }
        public ICommand EditarCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public PerfilCriancaViewModel(INavigation Navigation)
        {
            NomeCompleto      = app._crianca.crianca_primeiro_nome + " " + app._crianca.crianca_sobrenome;
            DataNascimento    = app._crianca.crianca_dataNascimento.ToString("dd/MM/yyyy");
            Sexo              = app._crianca.crianca_sexo == 1 ? "Menina" : "Menino";
            PesoAoNascer      = app._crianca.crianca_pesoAoNascer != 0 ? app._crianca.crianca_pesoAoNascer.ToString() + "kg" : "";
            AlturaAoNascer    = app._crianca.crianca_alturaAoNascer != 0 ? app._crianca.crianca_alturaAoNascer.ToString() + "cm" : "";
            TipoDeParto       = app._crianca.crianca_tipo_parto != -1 ? (app._crianca.crianca_tipo_parto == 1 ? "Cesária" : "Normal") : "";
            IdadeGestacional  = app._crianca.crianca_idade_gestacional != -1 ? app._crianca.crianca_idade_gestacional.ToString() + " semanas" : "";
            OutrasInformacoes = app._crianca.crianca_outrasInformacoes;

            this.Navigation = Navigation;
            EditarCommand = new Command(Editar);

            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
        }

        private async void Editar()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await NavigationService.NavigatePerfilCriancaEdit(Navigation);
            }
            else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para editar o perfil da criança.");
            }
        }
    }
}
