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

        public ICommand MostrarCommand { get; set; }
        public ICommand AbrirCommand { get; set; }

        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;

        public MarcosViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            
            var list = new List<Marco>
            {
                new Marco(1, "Primeiro dentinho", Color.FromHex("#EC407A"), false, "marco1.jpg"),
                new Marco(2, app._crianca.crianca_sexo == 0 ? "Virou-se sozinho" : "Virou-se sozinha", Color.FromHex("#8BC34A"), false, "marco2.jpg"),
                new Marco(3, app._crianca.crianca_sexo == 0 ? "Sentou-se sozinho" : "Sentou-se sozinha", Color.FromHex("#FFC107"), false, "marco3.jpg"),
                new Marco(4, "Parou o aleitamento materno exclusivo", Color.FromHex("#2196F3"), false, "marco4.jpg"),
                new Marco(5, "Comeu a primeira fruta", Color.FromHex("#26A69A"), false, "marco5.jpg"),
                new Marco(6, "Comeu a primeira papa salgada", Color.FromHex("#AB47BC"), false, "marco6.jpg"),
                new Marco(7, "Engatinhou", Color.FromHex("#3F51B5"), false, "marco7.jpg"),
                new Marco(8, "Primeira palavra", Color.FromHex("#CDDC39"), false, "marco8.jpg"),
                new Marco(9, "Primeiros passos", Color.FromHex("#FF5722"), false, "marco8.jpg"),
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

            MostrarCommand = new Command<Marco>(Mostrar);
            AbrirCommand = new Command<Marco>(AbrirMarco);
        }

        private void Mostrar(Marco marco)
        {
            var index = Marcos.IndexOf(marco);
            Marcos.RemoveAt(index);
            marco.Visivel = !marco.Visivel;
            Marcos.Insert(index, marco);
        }

        private async void AbrirMarco(Marco marco)
        {
            await NavigationService.NavigateMarcoVisualizacao(Navigation, marco);
        }

    }
}
