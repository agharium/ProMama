using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
            try
            {
                var escolha = await MessageService.ActionSheet("Escolher foto", new string[] { "Selecionar foto da galeria", "Abrir câmera" });

                if (!escolha.Equals("Cancelar") && escolha != null)
                {
                    await CrossMedia.Current.Initialize();

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                        return;
                    }

                    MediaFile file = null;

                    if (escolha.Equals("Selecionar foto da galeria"))
                    {
                        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                        {
                            PhotoSize = PhotoSize.Medium,
                            CompressionQuality = 92
                        });
                    }
                    else
                    {
                        file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            AllowCropping = true,
                            PhotoSize = PhotoSize.Medium,
                            CompressionQuality = 92,
                            Directory = "Sample",
                            Name = "sample.jpg"
                        });
                    }

                    if (file == null)
                        return;

                    Debug.WriteLine("File Location", file.Path, "OK");

                    Foto.source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        return stream;
                    });
                    Notify("Fotos");
                    Foto.caminho = file.Path;

                    App.FotoDatabase.Save(Foto);

                    // TESTE
                    Task.Run(async () =>
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            var result = await RestService.UploadImage(Foto, app._usuario.api_token);
                            if (result.success)
                                Debug.WriteLine("Deu certo!");
                        }
                    });
                    // TESTE

                    Caminho = Foto.source;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Debug.WriteLine("Usuário tocou fora do ActionSheet.");
            }
        }
    }
}
