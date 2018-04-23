using ProMama.Model;
using ProMama.View.Home;
using ProMama.View.Home.Paginas;
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

        public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

        public HomeMasterViewModel()
        {
            Nome = app._crianca.crianca_primeiro_nome;
            Idade = app._crianca.IdadeExtenso;
            Foto = app._crianca.Foto == null ? "avatar_default.jpg" : app._crianca.Foto;

            MenuItems = new ObservableCollection<HomeMenuItem>(new[]
            {
                    new HomeMenuItem(0, "Início",                    "fa-home",     typeof(HomeDetail)),
                    new HomeMenuItem(1, "Perfil da Criança",         "fa-child",    typeof(PerfilCriancaCreateView)),
                    new HomeMenuItem(2, "Perfil da Mãe",             "fa-user",     typeof(PerfilMaeCreateView)),
                    new HomeMenuItem(3, "Galeria",                   "fa-image",    typeof(GaleriaView)),
                    new HomeMenuItem(4, "Acompanhamento da Criança", "fa-table",    typeof(AcompanhamentoView)),
                    new HomeMenuItem(5, "Marcos do Desenvolvimento", "fa-trophy",   typeof(MarcosCriancaView)),
                    new HomeMenuItem(6, "Dúvidas",                   "fa-comments", typeof(DuvidasView)),
                    new HomeMenuItem(7, "Postos de Saúde",           "fa-map",      typeof(HomeDetail)),
                    new HomeMenuItem(8, "Redes Sociais",             "fa-globe",    typeof(HomeDetail)),
                    new HomeMenuItem(9, "Selecionar Criança",        "fa-exchange", typeof(SelecionarCriancaView)),
                    new HomeMenuItem(10, "Sair",                     "fa-sign-out", typeof(LogoutView))
                }
            );
        }
    }
}
