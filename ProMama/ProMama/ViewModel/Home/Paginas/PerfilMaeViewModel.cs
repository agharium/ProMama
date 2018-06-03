using Plugin.Connectivity;
using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class PerfilMaeViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
                Notify("Nome");
            }
        }

        private string _idade;
        public string Idade
        {
            get
            {
                return _idade;
            }
            set
            {
                _idade = value;
                Notify("Idade");
            }
        }

        private string _bairro;
        public string Bairro
        {
            get
            {
                return _bairro;
            }
            set
            {
                _bairro = value;
                Notify("Bairro");
            }
        }

        private string _postoSaude;
        public string PostoSaude
        {
            get
            {
                return _postoSaude;
            }
            set
            {
                _postoSaude = value;
                Notify("PostoSaude");
            }
        }

        private INavigation Navigation { get; set; }
        public ICommand EditarCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public PerfilMaeViewModel(INavigation _navigation)
        {
            Nome = app._usuario.name;
            var aux = (DateTime.Now.Year - app._usuario.data_nascimento.Year);
            Idade = (aux < 10 || aux > 100) ? "" : aux + " anos";
            Bairro = App.BairroDatabase.Find(app._usuario.bairro).bairro_nome;
            PostoSaude = app._usuario.posto_saude < 1 ? "" : App.PostoDatabase.Find(app._usuario.posto_saude).posto_nome;

            Navigation = _navigation;
            EditarCommand = new Command(Editar);

            MessageService = DependencyService.Get<IMessageService>();
            NavigationService = DependencyService.Get<INavigationService>();
        }

        private async void Editar()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await NavigationService.NavigatePerfilMaeEdit(Navigation);
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para editar o perfil da mãe.");
            }
        }
    }
}
