using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProMama.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class GaleriaViewModel : ViewModelBase
    {
        private ObservableCollection<Imagem> _fotos;
        public ObservableCollection<Imagem> Fotos
        {
            get { return _fotos; }
            set
            {
                _fotos = value;
                Notify("Fotos");
            }

        }

        public ICommand PickPhotoCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand VisualizarCommand { get; set; }

        private INavigation Navigation { get; set; }
        public ICommand NavigationCommand { get; set; }

        private readonly Services.INavigationService NavigationService;

        public GaleriaViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<Services.INavigationService>();

            Fotos = new ObservableCollection<Imagem>
            {
                new Imagem(0, "recém-nascido", "baby.jpeg"),
                new Imagem(1, "1º mês", "baby.jpeg"),
                new Imagem(2, "2º mês", "baby.jpeg"),
                new Imagem(2, "3º mês", "baby.jpeg"),
                new Imagem(2, "4º mês", "baby.jpeg")
            };

            PickPhotoCommand = new Command(PickPhoto);
            TakePhotoCommand = new Command(TakePhoto);
            VisualizarCommand = new Command<Imagem>(Visualizar);
        }

        private async void PickPhoto()
        {
            /*var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                Debug.WriteLine("File Location", file.Path, "OK");

                Foto f = new Foto(4, "28/04/18", "teste");
                f.Imagem = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                Fotos.Add(f);
            }
            else
            {
                Debug.WriteLine("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }*/

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                //AllowCropping = true,
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92/*,
                Directory = "Sample",
                Name = "test.jpg"*/
            });

            if (file == null)
                return;

            Debug.WriteLine("File Location", file.Path, "OK");

            Imagem f = new Imagem(4, "teste", "teste");
            f.Caminho = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            Fotos.Add(f);
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                AllowCropping = true,
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            Debug.WriteLine("File Location", file.Path, "OK");

            Imagem f = new Imagem(4, "teste", "teste");
            f.Caminho = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            Fotos.Add(f);
        }

        private async void Visualizar(Imagem imagem)
        {
            await NavigationService.NavigateImagem(Navigation, imagem);
        }
    }
}
