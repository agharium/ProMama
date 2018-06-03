using ProMama.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class MarcosViewModel : ViewModelBase
    {
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

        public MarcosViewModel()
        {
            Marcos = new ObservableCollection<Marco>
            {
                new Marco("Primeiro dentinho", Color.DarkCyan, true, "marco1.jpg"),
                new Marco("Virou-se sozinho", Color.Violet, false, "marco2.jpg"),
                new Marco("Sentou-se sozinho", Color.LightBlue, true, "marco3.jpg"),
                new Marco("Parou o aleitamento materno exclusivo", Color.Goldenrod, false, "marco4.jpg"),
                new Marco("Comeu a primeira fruta", Color.LightCoral, false, "marco5.jpg"),
                new Marco("Comeu a primeira papa salgada", Color.Cyan, false, "marco6.jpg"),
                new Marco("Engatinhou", Color.LightPink, false, "marco7.jpg"),
                new Marco("Primeira palavra", Color.LightSkyBlue, false, "marco8.jpg"),
                new Marco("Primeiros passos", Color.LightSeaGreen, false, "marco8.jpg"),
            };

            MostrarCommand = new Command<Marco>(Mostrar);
        }

        private void Mostrar(Marco marco)
        {
            var index = Marcos.IndexOf(marco);
            Marcos.RemoveAt(index);
            marco.Visivel = !marco.Visivel;
            Marcos.Insert(index, marco);
        }

    }
}
