using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class FotoViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private Foto Foto { get; set; }
        public string Titulo { get; set; }

        private ImageSource _caminho;
        public ImageSource Caminho
        {
            get
            {
                return _caminho;
            }
            set
            {
                _caminho = value;
                Notify("Caminho");
            }
        }

        public ICommand EditarCommand { get; set; }

        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public FotoViewModel(Foto _foto)
        {
            Foto = _foto;
            Titulo = Foto.titulo;
            Caminho = Foto.source;

            EditarCommand = new Command(Editar);

            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Editar()
        {
            Foto = await Ferramentas.SelecionarFoto(Foto);

            if (Foto != null)
            {
                Foto.uploaded = false;
                App.FotoDatabase.Save(Foto);
                app._master.SetFoto();

                Caminho = Foto.source;
            }
        }
    }
}
