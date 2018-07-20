using Plugin.Connectivity;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModels.Home.Paginas
{
    class SelecionarCriancaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        public List<Crianca> Criancas { get; set; }

        // Commands
        public ICommand SelecionarCriancaCommand { get; set; }
        public ICommand AddCriancaCommand { get; set; }

        // Navigation
        private INavigation Navigation { get; set; }
        private readonly INavigationService NavigationService;
        private readonly IMessageService MessageService;

        public SelecionarCriancaViewModel(INavigation _navigation)
        {
            Criancas = App.CriancaDatabase.GetCriancasByUser(app._usuario.id);

            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();
            MessageService = DependencyService.Get<IMessageService>();

            SelecionarCriancaCommand = new Command<Crianca>(SelecionarCrianca);
            AddCriancaCommand = new Command(AddCrianca);
        }

        // Método para selecionar criança
        private void SelecionarCrianca(Crianca c)
        {
            app._crianca = c;
            NavigationService.NavigateHome();
        }

        private async void AddCrianca()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await NavigationService.NavigateAddCriancaPush(Navigation);
            } else
            {
                await MessageService.AlertDialog("Você precisa estar conectado à internet para adicionar uma criança.");
            }
        }
    }
}
