using ProMama.Models;
using ProMama.ViewModels.Services;
using ProMama.Views.Home;
using ProMama.Views.Home.Paginas;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home
{
    class HomeMasterViewModel : ViewModelBase
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

        private ImageSource _foto { get; set; }
        public ImageSource Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value;
                Notify("Foto");
            }
        }

        private ObservableCollection<HomeMenuItem> _menuItems;
        public ObservableCollection<HomeMenuItem> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                Notify("MenuItems");
            }
        }

        public ICommand BackToHomeCommand { get; set; }

        public HomeMasterViewModel()
        {
            BackToHomeCommand = new Command(BackToHome);
            app._master = this;
            Load();
        }

        public void Load(){
            Nome = app._crianca.crianca_primeiro_nome;
            Nome += string.IsNullOrEmpty(app._crianca.crianca_sobrenome) ? "" : " " + app._crianca.crianca_sobrenome;
            Idade = app._crianca.IdadeExtenso;
            SetFoto();

            string FaleConoscoTitle = "";
            Type FaleConoscoType = null;
            if (App.BairroDatabase.Find(app._usuario.bairro).bairro_nome.Equals("Não moro em Osório"))
            {
                FaleConoscoTitle = "Dúvidas Respondidas";
                FaleConoscoType = typeof(FaleConoscoOutrosView);
            } else
            {
                FaleConoscoTitle = "Fale Conosco";
                FaleConoscoType = typeof(FaleConoscoView);
            }

            MenuItems = new ObservableCollection<HomeMenuItem>(new[]
                {
                    new HomeMenuItem(0, "Início", "fas-home", typeof(HomeDetail)),
                    new HomeMenuItem(1, "Perfil da Criança", "fas-child", typeof(PerfilCriancaView)),
                    new HomeMenuItem(2, "Perfil da Mãe", "fas-user", typeof(PerfilMaeView)),
                    new HomeMenuItem(3, "Galeria", "fas-image", typeof(GaleriaView)),
                    new HomeMenuItem(4, "Acompanhamento da Criança", "fas-table", typeof(AcompanhamentoView)),
                    new HomeMenuItem(5, "Marcos do Desenvolvimento", "fas-trophy", typeof(MarcosView)),
                    new HomeMenuItem(6, "Fale Conosco", "fas-comments", FaleConoscoType),
                    new HomeMenuItem(7, "Dúvidas Frequentes", "fas-question-circle", typeof(DuvidasFrequentesView)),
                    new HomeMenuItem(8, "Postos de Saúde", "fas-map", typeof(PostosSaudeView)),
                    new HomeMenuItem(9, "Redes Sociais", "fas-globe", typeof(RedesSociaisView)),
                    new HomeMenuItem(10, "Sobre", "fas-info-circle", typeof(SobreView)),
                    new HomeMenuItem(11, "Selecionar Criança", "fas-exchange-alt", typeof(SelecionarCriancaView)),
                    new HomeMenuItem(12, "Sair", "fas-sign-out-alt", typeof(LogoutView))
                }
            );
        }

        public void SetFoto()
        {
            Foto = App.FotoDatabase.GetMostRecent(app._crianca.crianca_id);
        }

        private void BackToHome()
        {
            var NavigationService = DependencyService.Get<INavigationService>();
            NavigationService.NavigateHome();
        }
    }
}
