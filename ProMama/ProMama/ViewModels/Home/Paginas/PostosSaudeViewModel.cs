using ProMama.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var postos = App.PostoDatabase.GetAll();
            var indexToRemove = -1;

            foreach (var p in postos)
            {
                if (p.nome.Equals("Outro"))
                    indexToRemove = postos.IndexOf(p);
            }

            if (indexToRemove != -1)
                postos.RemoveAt(indexToRemove);
            PostosSaude = postos;
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
    }
}
