using Plugin.Connectivity;
using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class PerfilCriancaEditViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _primeiroNome;
        public string PrimeiroNome
        {
            get { return _primeiroNome; }
            set
            {
                _primeiroNome = value;
                Notify("PrimeiroNome");
            }
        }

        private string _sobrenome;
        public string Sobrenome
        {
            get { return _sobrenome; }
            set
            {
                _sobrenome = value;
                Notify("Sobrenome");
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

        private int _sexoSelecionado;
        public int SexoSelecionado
        {
            get { return _sexoSelecionado; }
            set
            {
                _sexoSelecionado = value;
                Notify("SexoSelecionado");
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

        private int _partoSelecionado;
        public int PartoSelecionado
        {
            get { return _partoSelecionado; }
            set
            {
                _partoSelecionado = value;
                Notify("PartoSelecionado");
            }
        }

        private int _idadeGestacionalSelecionado;
        public int IdadeGestacionalSelecionado
        {
            get { return _idadeGestacionalSelecionado; }
            set
            {
                _idadeGestacionalSelecionado = value;
                Notify("IdadeGestacionalSelecionado");
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
        public ICommand SalvarCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public PerfilCriancaEditViewModel(INavigation _navigation)
        {
            PrimeiroNome = app._crianca.crianca_primeiro_nome;
            Sobrenome = app._crianca.crianca_sobrenome;
            DataNascimento = app._crianca.crianca_dataNascimento.ToString("dd/MM/yyyy");
            SexoSelecionado = app._crianca.crianca_sexo;
            PesoAoNascer = app._crianca.crianca_pesoAoNascer != 0 ? app._crianca.crianca_pesoAoNascer.ToString() : "";
            AlturaAoNascer = app._crianca.crianca_alturaAoNascer != 0 ? app._crianca.crianca_alturaAoNascer.ToString(): "";
            PartoSelecionado = app._crianca.crianca_tipo_parto;
            IdadeGestacionalSelecionado = app._crianca.crianca_idade_gestacional != -1 ? app._crianca.crianca_idade_gestacional - 20 : -1;
            OutrasInformacoes = app._crianca.crianca_outrasInformacoes;

            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Salvar()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Crianca c = new Crianca();

                c.crianca_id = app._crianca.crianca_id;
                c.crianca_dataNascimento = app._crianca.crianca_dataNascimento;

                c.crianca_primeiro_nome = PrimeiroNome;
                c.crianca_sobrenome = Sobrenome;
                c.crianca_sexo = SexoSelecionado;
                c.crianca_pesoAoNascer = PesoAoNascer.Equals(string.Empty) ? 0 : Convert.ToDouble(PesoAoNascer);
                c.crianca_alturaAoNascer = AlturaAoNascer.Equals(string.Empty) ? 0 : Convert.ToDouble(AlturaAoNascer);
                c.crianca_tipo_parto = PartoSelecionado;
                c.crianca_idade_gestacional = IdadeGestacionalSelecionado + 20;
                c.crianca_outrasInformacoes = OutrasInformacoes;

                var result = await RestService.CriancaUpdate(c, app._usuario.api_token);
                if (result.success)
                {
                    app._crianca = c;
                    App.CriancaDatabase.Save(c);
                    app._master.Load();
                    await Navigation.PopAsync();
                }
                else
                {
                    await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                }
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para atualizar o perfil da criança.");
            }
        }
    }
}
