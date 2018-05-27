using ProMama.Model;
using ProMama.View.Home;
using ProMama.View.Home.Paginas;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home
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

        public HomeMasterViewModel()
        {
            app._master = this;
            Load();
        }

        public void Load(){
            Nome = app._crianca.crianca_primeiro_nome;
            Idade = app._crianca.IdadeExtenso;
            Foto = app._crianca.Foto == null ? "avatar_default.jpg" : app._crianca.Foto;

            Type duvidaType = null;
            if (App.BairroDatabase.FindBairro(app._usuario.bairro).bairro_nome.Equals("Outro"))
            {
                duvidaType = typeof(DuvidasOutrosView);
            } else
            {
                duvidaType = typeof(DuvidasView);
            }

            MenuItems = new ObservableCollection<HomeMenuItem>(new[]
            {
                    new HomeMenuItem(0, "Início",                    "fa-home",     typeof(HomeDetail)),
                    new HomeMenuItem(1, "Perfil da Criança",         "fa-child",    typeof(PerfilCriancaView)),
                    new HomeMenuItem(2, "Perfil da Mãe",             "fa-user",     typeof(PerfilMaeView)),
                    new HomeMenuItem(3, "Galeria",                   "fa-image",    typeof(GaleriaView)),
                    new HomeMenuItem(4, "Acompanhamento da Criança", "fa-table",    typeof(AcompanhamentoView)),
                    new HomeMenuItem(5, "Marcos do Desenvolvimento", "fa-trophy",   typeof(MarcosView)),
                    new HomeMenuItem(6, "Dúvidas",                   "fa-comments", duvidaType),
                    new HomeMenuItem(7, "Postos de Saúde",           "fa-map",      typeof(HomeDetail)),
                    new HomeMenuItem(8, "Redes Sociais",             "fa-globe",    typeof(HomeDetail)),
                    new HomeMenuItem(9, "Selecionar Criança",        "fa-exchange", typeof(SelecionarCriancaView)),
                    new HomeMenuItem(10, "Sair",                     "fa-sign-out", typeof(LogoutView))
                }
            );
        }
    }
}
