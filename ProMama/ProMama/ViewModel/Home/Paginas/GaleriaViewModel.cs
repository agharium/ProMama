using Plugin.Media;
using Plugin.Media.Abstractions;
using ProMama.Model;
using ProMama.ViewModel.Services;
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

        private ObservableCollection<Foto> _fotos;
        public ObservableCollection<Foto> Fotos
        {
            get { return _fotos; }
            set
            {
                _fotos = value;
                Notify("Fotos");
            }
        }

        private List<string> IdadesExtensoLista { get; set; }

        private INavigation Navigation { get; set; }
        public ICommand NavigationCommand { get; set; }
        public ICommand FotoCommand { get; set; }

        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public GaleriaViewModel(INavigation _navigation)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();

            FotoCommand = new Command<Foto>(Foto);

            Load();
        }

        private void Load()
        {
            var meses = Math.Floor(app._crianca.IdadeMeses) + 1;

            Fotos = new ObservableCollection<Foto>();

            IdadesExtensoLista = new List<string>() {
                "recém-nascido",
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

            var FotosBanco = App.FotoDatabase.GetAll();
            for (int i = 0; i < meses; i++)
            {
                var added = false;
                foreach (var f in FotosBanco)
                {
                    if (f.mes == i)
                    {
                        f.source = f.caminho;
                        Fotos.Add(f);
                        added = true;
                    }
                }
                if (added == false)
                    Fotos.Add(new Foto(i, IdadesExtensoLista[i], null, app._crianca.crianca_id));
            }
        }

        private async void Foto(Foto foto)
        {
            if (!foto.caminho.ToString().Contains("avatar_default.png"))
            {
                await NavigationService.NavigateFoto(Navigation, foto);
            }
            else
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

                        foto.source = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                        Notify("Fotos");
                        foto.caminho = file.Path;

                        App.FotoDatabase.Save(foto);
                        Load();
                        await NavigationService.NavigateFoto(Navigation, foto);
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
}
