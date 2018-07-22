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
                new Marco(1, "Primeiro sorriso", Color.FromHex("#FF9696"), false, "marco1.jpg"),
                new Marco(2, app._crianca.crianca_sexo == 0 ? "Virou-se sozinho" : "Virou-se sozinha", Color.FromHex("#F78888"), false, "marco2.jpg"),
                new Marco(3, "Primeiro dentinho", Color.FromHex("#EF7A7A"), false, "marco3.jpg"),
                new Marco(4, app._crianca.crianca_sexo == 0 ? "Sentou-se sozinho" : "Sentou-se sozinha", Color.FromHex("#E76D6D"), false, "marco4.jpg"),
                new Marco(5, "Parou o aleitamento materno exclusivo", Color.FromHex("#DF5F5F"), false, "marco5.jpg"),
                new Marco(6, "Comeu a primeira fruta", Color.FromHex("#D75252"), false, "marco6.jpg"),
                new Marco(7, "Comeu a primeira papa salgada", Color.FromHex("#CF4444"), false, "marco7.jpg"),
                new Marco(8, "Engatinhou", Color.FromHex("#C73737"), false, "marco8.jpg"),
                new Marco(9, "Primeira palavra", Color.FromHex("#BF2929"), false, "marco9.jpg"),
                new Marco(10, "Primeiros passos", Color.FromHex("#B71C1C"), false, "marco10.jpg"),
            };

            foreach (var obj in App.MarcoDatabase.GetAllByChildId(app._crianca.crianca_id))
            {
                list[obj.marco - 1].Alcancado = true;
                list[obj.marco - 1].Icone = "circle_checked.png"; //"fas-check-circle";
                list[obj.marco - 1].id = obj.id;
                list[obj.marco - 1].crianca = obj.crianca;
                list[obj.marco - 1].data = obj.data;
                list[obj.marco - 1].extra = obj.extra;
                list[obj.marco - 1].dataPorExtenso = obj.dataPorExtenso;
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
