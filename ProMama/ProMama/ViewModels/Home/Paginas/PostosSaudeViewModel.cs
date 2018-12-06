using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.XSnack;
using ProMama.Components;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class PostosSaudeViewModel : ViewModelBase
    {
        public List<Posto> postos = App.PostoDatabase.GetAll();
        private ObservableCollection<Posto> _postosSaude;
        public ObservableCollection<Posto> PostosSaude {
            get
            {
                return _postosSaude;
            }
            set
            {
                _postosSaude = value;
                Notify("PostosSaude");
            }
        }

        private bool _permissaoLocalizacaoConcedida;
        public bool PermissaoLocalizacaoConcedida {
            get
            {
                return _permissaoLocalizacaoConcedida;
            }
            set
            {
                _permissaoLocalizacaoConcedida = value;
                Notify("PermissaoLocalizacaoConcedida");
            }
        }

        public ICommand VerMapaCommand { get; set; }
        public ICommand LigarCommand { get; set; }
        public ICommand PermissaoLocalizacaoCommand { get; set; }

        // MessageService
        private static readonly IMessageService MessageService = DependencyService.Get<IMessageService>();

        public PostosSaudeViewModel()
        {
            VerMapaCommand = new Command<Posto>(VerMapa);
            LigarCommand = new Command<Posto>(Ligar);
            PermissaoLocalizacaoCommand = new Command(PermissaoLocalizacao);

            var indexToRemove = -1;

            foreach (var p in postos)
            {
                if (p.nome.Equals("Outro"))
                {
                    indexToRemove = postos.IndexOf(p);
                } else
                {
                    p.MostraTelefone = string.IsNullOrEmpty(p.telefone) ? false : true;
                    string[] coords = p.lat_long.Split(',');
                    p.Coordinates = new Coordinates(Convert.ToDouble(coords[0]), Convert.ToDouble(coords[1]));
                }
            }

            if (indexToRemove != -1)
                postos.RemoveAt(indexToRemove);

            OrdenaPostosPorProximidade();

            if (VerificaPermissao().Result != PermissionStatus.Granted && App.NaoPerguntePermissaoLocalizacao == false)
                CrossXSnack.Current.ShowMessage("Permissão de localização é necessária para mostrar os postos mais próximos.", 10, "OK", PermissaoLocalizacaoCommand);
        }

        private void OrdenaPostosPorProximidade()
        {
            // Verificação do posto de saúde mais próximo + reordenação
            PermissionStatus permissaoStatus = VerificaPermissao().Result;
            PermissaoLocalizacaoConcedida = permissaoStatus == PermissionStatus.Granted;

            if (permissaoStatus == PermissionStatus.Granted)
            {
                var currentLocation = GetCurrentPosition().Result;
                if (currentLocation != null)
                    postos = postos.OrderBy(x => Coordinates.DistanceBetween(x.Coordinates, currentLocation)).ToList();
            }

            PostosSaude = new ObservableCollection<Posto>(postos);
        }

        private async Task<PermissionStatus> VerificaPermissao()
        {
            return await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
        }

        private async void PermissaoLocalizacao()
        {
            App.VezesPerguntadoPermissaoLocalizacao++;

            // Pede permissão de acesso à localização
            if (App.VezesPerguntadoPermissaoLocalizacao <= 3)
            {
                var escolha = await MessageService.ConfirmationDialog("Se a permissão de localização for concedida, o aplicativo utilizará sua localização para organizar os postos de saúde por proximidade. Deseja concedê-la?", "Sim", "Não");
                if (escolha)
                    await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
            } else {
                var escolha = await MessageService.ConfirmationDialog("Se a permissão de localização for concedida, o aplicativo utilizará sua localização para organizar os postos de saúde por proximidade. Deseja concedê-la?", "Sim", "Não e não me peça novamente");
                if (escolha)
                    await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                else
                    App.NaoPerguntePermissaoLocalizacao = true;
            }
            

            OrdenaPostosPorProximidade();
        }

        private void VerMapa(Posto obj)
        {
            string url = "https://www.google.com/maps/search/?api=1&query=" + obj.lat_long;

            try
            {
                Device.OpenUri(new Uri(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Ligar(Posto obj)
        {
            string telefone = "tel:" + obj.telefone.Replace(" ", "");

            try
            {
                Device.OpenUri(new Uri(telefone));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        // https://jamesmontemagno.github.io/GeolocatorPlugin/CurrentLocation.html
        private static async Task<Coordinates> GetCurrentPosition()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                // Descomentar seção abaixo para ligar o cache de localização.
                /*position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cached position, so let's use it.
                    //return position;
                    return new Coordinates(position.Latitude, position.Longitude);
                }*/

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    Debug.WriteLine("Geolocation not available or disabled.");
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            Debug.WriteLine(output);
            
            return new Coordinates(position.Latitude, position.Longitude);
        }
    }
}
