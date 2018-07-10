using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Home.Paginas;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProMama.Views.Home.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalhesView : ContentPage
    {
        public DetalhesView(Informacao informacao)
        {
            InitializeComponent();
            BindingContext = new DetalhesViewModel(Navigation, informacao);

            var texto = informacao.informacao_corpo.Replace("\r", "<br>");
            Texto.Children.Add(Ferramentas.CreateBrowser(texto));
        }

        public DetalhesView(Conversa duvida)
        {
            InitializeComponent();
            BindingContext = new DetalhesViewModel(duvida);

            var texto = duvida.resposta.Replace("\r", "<br>");
            Texto.Children.Add(Ferramentas.CreateBrowser(texto));
        }

        public DetalhesView(DuvidaFrequente duvidaFrequente)
        {
            InitializeComponent();
            BindingContext = new DetalhesViewModel(duvidaFrequente);

            var texto = duvidaFrequente.texto.Replace("\r", "<br>");
            Texto.Children.Add(Ferramentas.CreateBrowser(texto));
        }
    }
}