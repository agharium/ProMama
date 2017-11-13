using System.Collections.ObjectModel;

namespace ProMama.ViewModel.Home
{
    class HomeDetailViewModel : ViewModelBase
    {
        private ObservableCollection<CustomComponent.Info> _infoItems;
        public ObservableCollection<CustomComponent.Info> InfoItems
        {
            get { return _infoItems; }
            set
            {
                _infoItems = value;
                //this.Notify("Email");
            }
        }

        public HomeDetailViewModel()
        {
            InfoItems = new ObservableCollection<CustomComponent.Info>() {
                new CustomComponent.Info(
                    0, 0,
                    "Exemplo de Informação",
                    "Este CardView foi adicionado no XAML de forma programática utilizando o code-behind do C#.",
                    "baby.jpeg"
                ),
                new CustomComponent.Info(
                    0, 0,
                    "Exemplo de Informação",
                    "Este CardView foi adicionado no XAML de forma programática utilizando o code-behind do C#.",
                    "baby.jpeg"
                ),
                new CustomComponent.Info(
                    0, 0,
                    "Exemplo de Informação",
                    "Este CardView foi adicionado no XAML de forma programática utilizando o code-behind do C#.",
                    "baby.jpeg"
                )
            };
        }
    }
}