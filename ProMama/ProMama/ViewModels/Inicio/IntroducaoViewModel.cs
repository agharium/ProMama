using Plugin.Connectivity;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Inicio
{
    class IntroducaoViewModel : ViewModelBase
    {
        public ICommand NavigateToCadastroLoginCommand { get; set; }
        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public List<PaginaCarousel> PaginasCarousel { get; set; }

        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                Notify("Position");
            }
        }

        public IntroducaoViewModel()
        {
            NavigateToCadastroLoginCommand = new Command(NavigateToCadastroLogin);
            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();

            PaginasCarousel = new List<PaginaCarousel>()
            {
                new PaginaCarousel(
                    0,
                    "Bem-vindo ao Aplicativo Pró-Mamá!",
                    "O leite materno é o melhor alimento para o seu bebê, porém amamentar pode não ser tão simples assim! Com o apoio deste aplicativo tudo vai ficar mais fácil!",
                    "intro_baby1.png"),
                new PaginaCarousel(
                    1,
                    "O que o aplicativo faz?",
                    "Preencha seus dados corretamente e você receberá notícias semanais sobre o aleitamento materno, a alimentação e o desenvolvimento do seu bebê de acordo com a idade dele.",
                    "intro_baby2.png"),
                new PaginaCarousel(
                    2,
                    "Fotos e marcos de desenvolvimento!",
                    "Você vai poder registrar os melhores momentos do seu bebê e guardar para sempre! Vai poder acompanhar o desenvolvimento do seu bebê através de fotos mensais, peso, altura e gravar as datas mais importantes para vocês, como por exemplo: o primeiro sorriso!",
                    "intro_baby3.png"),
                new PaginaCarousel(
                    3,
                    "Aproveite esses dois anos!",
                    "Estaremos bem pertinho de vocês porque o aplicativo Pró-Mamá acompanha seu bebê de zero a 2 anos de idade. Todo seu conteúdo foi pensado por profissionais da saúde pensando no melhor para vocês!",
                    "intro_baby4.png"),
                new PaginaCarousel(
                    4,
                    "Uma parceria",
                    "",
                    "logos_parceria.png")
            };

            Position = 0;
        }

        private async void NavigateToCadastroLogin()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                NavigationService.NavigateCadastroLogin();
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para poder realizar o primeiro acesso ao aplicativo.");
            }
        }
    }
}
