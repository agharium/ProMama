using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class MarcosViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private ObservableCollection<Marco> _marcos;
        public ObservableCollection<Marco> Marcos
        {
            get
            {
                return _marcos;
            }
            set
            {
                _marcos = value;
                Notify("Marcos");
            }
        }
        
        public ICommand AbrirMarcoCommand { get; set; }

        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;

        public MarcosViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            
            var list = new List<Marco>
            {
                new Marco(1, "Primeiro dentinho", Color.FromHex("#ff5656"), false, "marco1.jpg"),
                new Marco(2, app._crianca.crianca_sexo == 0 ? "Virou-se sozinho" : "Virou-se sozinha", Color.FromHex("#fd5353"), false, "marco2.jpg"),
                new Marco(3, app._crianca.crianca_sexo == 0 ? "Sentou-se sozinho" : "Sentou-se sozinha", Color.FromHex("#fb5150"), false, "marco3.jpg"),
                new Marco(4, "Parou o aleitamento materno exclusivo", Color.FromHex("#f84e4d"), false, "marco4.jpg"),
                new Marco(5, "Comeu a primeira fruta", Color.FromHex("#f64c4a"), false, "marco5.jpg"),
                new Marco(6, "Comeu a primeira papa salgada", Color.FromHex("#f44948"), false, "marco6.jpg"),
                new Marco(7, "Engatinhou", Color.FromHex("#f24745"), false, "marco7.jpg"),
                new Marco(8, "Primeira palavra", Color.FromHex("#f04442"), false, "marco8.jpg"),
                new Marco(9, "Primeiros passos", Color.FromHex("#ee423f"), false, "marco8.jpg"),
            };

            foreach (var obj in App.MarcoDatabase.FindByChildId(app._crianca.crianca_id))
            {
                list[obj.marco - 1].Alcancado = true;
                list[obj.marco - 1].id = obj.id;
                list[obj.marco - 1].crianca = obj.crianca;
                list[obj.marco - 1].data = obj.data;
                list[obj.marco - 1].extra = obj.extra;
            }

            Marcos = new ObservableCollection<Marco>(list);

            AbrirMarcoCommand = new Command<Marco>(AbrirMarco);
        }

        private async void AbrirMarco(Marco marco)
        {
            await NavigationService.NavigateMarcoVisualizacao(Navigation, marco);
        }

    }
}
