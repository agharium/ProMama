using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class DetalhesViewModel : ViewModelBase
    {
        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public bool ImagemVisivel { get; set; }
        public string Texto { get; set; }
        public List<Link> Links { get; set; }
        public bool LinksVisivel { get; set; }

        public ICommand AbrirLinkCommand { get; set; }
        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;

        public DetalhesViewModel(INavigation _navigation, Informacao i)
        {
            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();

            Titulo = i.informacao_titulo;
            Imagem = i.informacao_foto;
            ImagemVisivel = i.informacao_imagem_visivel;
            Texto = i.informacao_corpo;
            Links = i.links;
            LinksVisivel = Links.Count == 0 ? false : true;
            
            AbrirLinkCommand = new Command<Link>(AbrirLink);
        }

        private void AbrirLink(Link l)
        {
            if (l.url.Contains("DUVIDAFREQUENTE:"))
            {
                try
                {
                    int id = Convert.ToInt32(l.url.Substring(l.url.IndexOf("DUVIDAFREQUENTE:") + "DUVIDAFREQUENTE:".Length));
                    DuvidaFrequente df = App.DuvidaFrequenteDatabase.Find(id);
                    if (df != null)
                    {
                        NavigationService.NavigateDuvidaFrequente(Navigation, df);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
            else
            {
                try
                {
                    Device.OpenUri(new Uri(l.url));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        public DetalhesViewModel(Conversa c)
        {
            Titulo = c.pergunta;
            ImagemVisivel = false;
            Texto = c.resposta;
            LinksVisivel = false;
        }

        public DetalhesViewModel(DuvidaFrequente df)
        {
            Titulo = df.titulo;
            ImagemVisivel = false;
            Texto = df.texto;
            LinksVisivel = false;
        }
    }
}
