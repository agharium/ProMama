using ProMama.Model;
using ProMama.ViewModel.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
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

        public SelecionarCriancaViewModel(INavigation _navigation)
        {
            Criancas = app._usuario.criancas;

            Navigation = _navigation;
            NavigationService = DependencyService.Get<INavigationService>();

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
            await NavigationService.NavigateAddCriancaPush(Navigation);
        }
    }
}
