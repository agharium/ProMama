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
        private ObservableCollection<Foto> _fotos;
        public ObservableCollection<Foto> Fotos
        {
            get { return _fotos; }
            set
            {
                _fotos = value;
            }

        }

        public ICommand PickPhotoCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }

        //private INavigation Navigation { get; set; }
        //public ICommand NavigationCommand { get; set; }

        //private readonly Services.INavigationService _navigationService;

        public GaleriaViewModel()
        {
            //this.Navigation = Navigation;
            //this.NavigationCommand = new Command(this.NavigateToAddAcompanhamento);

            //this._navigationService = DependencyService.Get<Services.INavigationService>();

            Fotos = new ObservableCollection<Foto>();

            Foto teste1 = new Foto(1, "baby.jpeg");
            Fotos.Add(teste1);

            Foto teste2 = new Foto(2, "baby.jpeg");
            Fotos.Add(teste2);

            Foto teste3 = new Foto(3, "baby.jpeg");
            Fotos.Add(teste3);

            PickPhotoCommand = new Command(PickPhoto);
            TakePhotoCommand = new Command(TakePhoto);
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

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
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

            Foto f = new Foto(4, "teste");
            f.Imagem = ImageSource.FromStream(() =>
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

            Foto f = new Foto(4, "teste");
            f.Imagem = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
            Fotos.Add(f);
        }

    }
}
