using ProMama.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMaster : ContentPage
    {
        public ListView ListView;

        public HomeMaster()
        {
            InitializeComponent();

            BindingContext = new HomeMasterViewModel();
            ListView = MenuItemsListView;
        }

        class HomeMasterViewModel : INotifyPropertyChanged
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

            public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

            public HomeMasterViewModel()
            {
                Nome = app._crianca.crianca_primeiro_nome;
                Idade = app._crianca.IdadeExtenso;

                MenuItems = new ObservableCollection<HomeMenuItem>(new[]
                {
                    new HomeMenuItem { Id = 0, Title = "Início" },
                    new HomeMenuItem { Id = 1, Title = "Perfil da Criança", TargetType = typeof(Paginas.PerfilCriancaCreateView) },
                    new HomeMenuItem { Id = 2, Title = "Perfil da Mãe", TargetType = typeof(Paginas.PerfilMaeCreateView) },
                    new HomeMenuItem { Id = 3, Title = "Galeria", TargetType = typeof(Paginas.GaleriaView) },
                    new HomeMenuItem { Id = 4, Title = "Marcos da Criança", TargetType = typeof(Paginas.MarcosCriancaView) },
                    new HomeMenuItem { Id = 5, Title = "Acompanhamento da Criança", TargetType = typeof(Paginas.AcompanhamentoView) },
                    new HomeMenuItem { Id = 6, Title = "Dúvidas Frequentes", TargetType = typeof(Paginas.DuvidasFrequentesView) },
                    new HomeMenuItem { Id = 7, Title = "Selecionar Criança", TargetType = typeof(Paginas.SelecionarCriancaView) },
                    new HomeMenuItem { Id = 8, Title = "Sair", TargetType = typeof(Paginas.LogoutView) }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}