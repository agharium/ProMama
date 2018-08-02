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

        private INavigation Navigation;
        private readonly IMessageService MessageService;
        private readonly IRestService RestService;

        public FotoViewModel(Foto _foto, INavigation _navigation)
        {
            Foto = _foto;
            Titulo = Foto.titulo;
            Caminho = Foto.source;

            EditarCommand = new Command(Editar);

            Navigation = _navigation;
            MessageService = DependencyService.Get<IMessageService>();
            RestService = DependencyService.Get<IRestService>();
        }

        private async void Editar()
        {
            int escolha = await Ferramentas.FotoActionSheet(2);
            if (escolha == 1 || escolha == 2)
            {
                Foto = await Ferramentas.SelecionarFoto(Foto, escolha);

                if (Foto != null)
                {
                    Foto.uploaded = false;
                    App.FotoDatabase.Save(Foto);
                    app._master.SetFoto();

                    Caminho = Foto.source;
                }
            }
            else if (escolha == 3)
            {
                if (await MessageService.ConfirmationDialog("Você tem certeza que deseja excluir esta foto?", "Sim", "Não"))
                {
                    App.FotoDatabase.Delete(Foto.id);
                    App.Excluir.Fotos.Add(Foto.id);
                    App.ExcluirDatabase.Save(App.Excluir);

                    Ferramentas.UploadThread();

                    app._master.SetFoto();
                    await Navigation.PopAsync();
                }
            }
        }
    }
}
