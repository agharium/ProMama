using ProMama.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class DetalhesViewModel : ViewModelBase
    {
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Texto { get; set; }
        public List<Link> Links { get; set; }

        private ICommand AbrirLinkCommand { get; set; }

        public DetalhesViewModel(Informacao i)
        {
            Titulo = i.informacao_titulo;
            Imagem = i.informacao_foto;
            Texto = i.informacao_corpo;
            Links = i.informacao_links;

            AbrirLinkCommand = new Command<string>(AbrirLink);
        }

        private void AbrirLink(string url)
        {
            Device.OpenUri(new Uri(url));
        }

        public DetalhesViewModel(Duvida d)
        {
            Titulo = d.duvida_pergunta;
            Texto = d.duvida_resposta;
        }
    }
}
