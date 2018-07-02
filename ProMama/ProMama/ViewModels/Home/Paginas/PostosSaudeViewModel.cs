using ProMama.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class PostosSaudeViewModel : ViewModelBase
    {
        public List<Posto> PostosSaude { get; set; }

        public ICommand VerMapaCommand { get; set; }
        public ICommand LigarCommand { get; set; }

        public PostosSaudeViewModel()
        {
            VerMapaCommand = new Command<Posto>(VerMapa);
            LigarCommand = new Command<Posto>(Ligar);

            PostosSaude = App.PostoDatabase.GetAll();
        }

        private void VerMapa(Posto obj)
        {
            string url = "https://www.google.com/maps/search/?api=1&query=" + WebUtility.UrlEncode(obj.posto_endereco);

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
            string telefone = "tel:" + obj.posto_telefone.Replace(" ", "");

            try
            {
                Device.OpenUri(new Uri(telefone));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
