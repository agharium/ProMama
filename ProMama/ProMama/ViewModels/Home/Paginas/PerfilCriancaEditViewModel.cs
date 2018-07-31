using Acr.UserDialogs;
using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
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

        private int _sexoSelecionadoIndex;
        public int SexoSelecionadoIndex
        {
            get { return _sexoSelecionadoIndex; }
            set
            {
                _sexoSelecionadoIndex = value;
                Notify("SexoSelecionadoIndex");
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

        private int _partoSelecionadoIndex;
        public int PartoSelecionadoIndex
        {
            get { return _partoSelecionadoIndex; }
            set
            {
                _partoSelecionadoIndex = value;
                Notify("PartoSelecionadoIndex");
            }
        }

        private int _idadeGestacionalSelecionadoIndex;
        public int IdadeGestacionalSelecionadoIndex
        {
            get { return _idadeGestacionalSelecionadoIndex; }
            set
            {
                _idadeGestacionalSelecionadoIndex = value;
                Notify("IdadeGestacionalSelecionadoIndex");
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
        public ICommand ExcluirCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly INavigationService NavigationService;
        private readonly IRestService RestService;

        public PerfilCriancaEditViewModel(INavigation _navigation)
        {
            PrimeiroNome = app._crianca.crianca_primeiro_nome;
            Sobrenome = app._crianca.crianca_sobrenome;
            DataNascimento = app._crianca.crianca_dataNascimento.ToString("dd/MM/yyyy");
            SexoSelecionadoIndex = app._crianca.crianca_sexo;
            PesoAoNascer = app._crianca.crianca_pesoAoNascer != 0 ? app._crianca.crianca_pesoAoNascer.ToString() : "";
            AlturaAoNascer = app._crianca.crianca_alturaAoNascer != 0 ? app._crianca.crianca_alturaAoNascer.ToString(): "";
            PartoSelecionadoIndex = app._crianca.crianca_tipo_parto;
            IdadeGestacionalSelecionadoIndex = app._crianca.crianca_idade_gestacional >= 20 ? app._crianca.crianca_idade_gestacional - 20 : -1;
            OutrasInformacoes = app._crianca.crianca_outrasInformacoes;

            Navigation = _navigation;
            SalvarCommand = new Command(Salvar);
            ExcluirCommand = new Command(Excluir);
            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Salvar()
        {
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);
            if (string.IsNullOrEmpty(PrimeiroNome))
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("O nome da criança é um campo obrigatório.");
            } else if (PrimeiroNome.Length < 2)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("O nome da criança deve ter no mínimo 2 caracteres.");
            }
            else {
                Crianca c = new Crianca();

                c.crianca_id = app._crianca.crianca_id;
                c.crianca_dataNascimento = app._crianca.crianca_dataNascimento;
                c.user_id = app._crianca.user_id;
                c.uploaded = false;

                c.crianca_primeiro_nome = PrimeiroNome;
                c.crianca_sobrenome = string.IsNullOrEmpty(Sobrenome) ? "" : Sobrenome;
                c.crianca_sexo = SexoSelecionadoIndex;
                c.crianca_pesoAoNascer = string.IsNullOrEmpty(PesoAoNascer) || PesoAoNascer.Equals(",") ? 0 : Convert.ToInt32(PesoAoNascer);
                c.crianca_alturaAoNascer = string.IsNullOrEmpty(AlturaAoNascer) || AlturaAoNascer.Equals(",") ? 0 : Convert.ToInt32(AlturaAoNascer);
                c.crianca_tipo_parto = PartoSelecionadoIndex;
                c.crianca_idade_gestacional = IdadeGestacionalSelecionadoIndex == -1 ? -1 : IdadeGestacionalSelecionadoIndex + 20;
                c.crianca_outrasInformacoes = string.IsNullOrEmpty(OutrasInformacoes) ? "" : OutrasInformacoes;
                
                app._crianca = c;
                App.CriancaDatabase.Save(c);
                app._master.Load();

                LoadingDialog.Hide();
                await Navigation.PopAsync();
            }
        }

        private async void Excluir()
        {
            if (await MessageService.ConfirmationDialog("Você tem certeza que deseja excluir esta criança?", "Sim", "Não"))
            {
                var prompt = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                                .SetTitle("Insira o primeiro nome da criança para confirmar")
                                .SetPlaceholder("Nome da criança")
                                .SetInputMode(InputType.Name)
                                .SetCancelText("Cancelar")
                                .SetOkText("Confirmar"));
                if (prompt.Ok)
                {
                    if (prompt.Text.ToLower().Equals(app._crianca.crianca_primeiro_nome.ToLower()))
                    {
                        App.Excluir.Criancas.Add(app._crianca.crianca_id);
                        App.ExcluirDatabase.Save(App.Excluir);

                        app._usuario.criancas.Remove(app._crianca.crianca_id);
                        App.UsuarioDatabase.Save(app._usuario);

                        Ferramentas.DeletarCrianca(app._crianca.crianca_id);

                        App.UltimaCrianca = 0;

                        if (app._usuario.criancas.Count == 0)
                        {
                            app._crianca = null;
                            NavigationService.NavigateAddCrianca();
                        } else
                        {
                            app._crianca = App.CriancaDatabase.Find(app._usuario.criancas[app._usuario.criancas.Count - 1]);
                            NavigationService.NavigateHome();
                        }
                    } else {
                        UserDialogs.Instance.Toast(new ToastConfig("Nome da criança incorreto."));
                    }
                }
            }
        }
    }
}
