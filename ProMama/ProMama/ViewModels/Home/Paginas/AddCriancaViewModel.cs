using Acr.UserDialogs;
using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class AddCriancaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public string PrimeiroNome { get; set; }

        private DateTime _dataMinima;
        public DateTime DataMinima
        {
            get
            {
                return _dataMinima;
            }
            set
            {
                _dataMinima = value;
                Notify("DataMinima");
            }
        }

        private DateTime _dataMaxima;
        public DateTime DataMaxima
        {
            get
            {
                return _dataMaxima;
            }
            set
            {
                _dataMaxima = value;
                Notify("DataMaxima");
            }
        }

        private DateTime _dataSelecionada;
        public DateTime DataSelecionada
        {
            get
            {
                return _dataSelecionada;
            }
            set
            {
                _dataSelecionada = value;
                Notify("DataSelecionada");
            }
        }

        private List<string> _sexos;
        public List<string> Sexos
        {
            get
            {
                return _sexos;
            }
            set
            {
                _sexos = value;
                Notify("Sexos");
            }
        }

        private int _sexoSelecionado;
        public int SexoSelecionado
        {
            get
            {
                return _sexoSelecionado;
            }
            set
            {
                _sexoSelecionado = value;
                Notify("SexoSelecionado");
            }
        }

        public ICommand AddCriancaCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public AddCriancaViewModel()
        {
            AddCriancaCommand = new Command(AddCrianca);

            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();

            DataMinima = DateTime.Now.AddYears(-2);
            DataMaxima = DateTime.Now;
            DataSelecionada = DateTime.Now;
            Sexos = new List<string>
            {
                "Menino",
                "Menina"
            };
            SexoSelecionado = -1;
        }

        private async void AddCrianca()
        {
            IProgressDialog LoadingDialog = UserDialogs.Instance.Loading("Por favor, aguarde...", null, null, true, MaskType.Black);

            if (string.IsNullOrEmpty(PrimeiroNome) || SexoSelecionado == -1)
            {
                LoadingDialog.Hide();
                await MessageService.AlertDialog("Nenhum campo pode estar vazio.");
            } else
            {
                if (await MessageService.ConfirmationDialog("Você tem certeza que esta é a data de nascimento da criança? Você não poderá alterar esta informação posteriormente.", "Continuar", "Voltar")){
                    var c = new Crianca(PrimeiroNome, DataSelecionada, SexoSelecionado);
                    c.crianca_tipo_parto = -1;
                    c.crianca_idade_gestacional = -1;
                    var result = await RestService.CriancaCreate(c, app._usuario.api_token);

                    if (result.success)
                    {
                        c.crianca_id = result.id;
                        App.CriancaDatabase.Save(c);

                        if (app._usuario.criancas == null)
                            app._usuario.criancas = new List<Crianca>();

                        app._usuario.criancas.Add(c);
                        App.UsuarioDatabase.Save(app._usuario);
                        app._crianca = c;

                        LoadingDialog.Hide();
                        NavigationService.NavigateHome();
                    }
                    else
                    {
                        LoadingDialog.Hide();
                        await MessageService.AlertDialog("Ocorreu um erro. Tente novamente mais tarde.");
                    }
                }
            }
        }
    }
}