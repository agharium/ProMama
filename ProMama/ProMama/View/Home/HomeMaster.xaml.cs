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
            public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

            public HomeMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomeMenuItem>(new[]
                {
                    new HomeMenuItem { Id = 0, Title = "Início" },
                    new HomeMenuItem { Id = 1, Title = "Perfil da Criança", TargetType = typeof(Paginas.PerfilCriancaCreateView) },
                    new HomeMenuItem { Id = 2, Title = "Perfil da Mãe", TargetType = typeof(Paginas.PerfilMaeCreateView) },
                    new HomeMenuItem { Id = 3, Title = "Acompanhamento da Criança", TargetType = typeof(Paginas.AcompanhamentoView) },
                    new HomeMenuItem { Id = 4, Title = "Galeria", TargetType = typeof(Paginas.GaleriaView) },
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