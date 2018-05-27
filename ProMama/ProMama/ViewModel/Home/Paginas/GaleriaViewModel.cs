using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProMama.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class GaleriaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

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

        private List<string> IdadesExtensoLista { get; set; }

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

            var meses = Math.Floor(app._crianca.IdadeMeses) + 1;

            Fotos = new ObservableCollection<Imagem>();

            IdadesExtensoLista = new List<string>() {
                "recém-nascido",
                "1 semana",
                "2 semanas",
                "3 semanas",
                "1 mês",
                "2 meses",
                "3 meses",
                "4 meses",
                "5 meses",
                "6 meses",
                "7 meses",
                "8 meses",
                "9 meses",
                "10 meses",
                "11 meses",
                "1 ano",
                "1 ano e 1 mês",
                "1 ano e 2 meses",
                "1 ano e 3 meses",
                "1 ano e 4 meses",
                "1 ano e 5 meses",
                "1 ano e 6 meses",
                "1 ano e 7 meses",
                "1 ano e 8 meses",
                "1 ano e 9 meses",
                "1 ano e 10 meses",
                "1 ano e 11 meses",
                "2 anos"
            };

            for (int i = 0; i < meses; i++)
            {
                new Imagem(i, IdadesExtensoLista[i], "baby.jpeg");
            }

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
