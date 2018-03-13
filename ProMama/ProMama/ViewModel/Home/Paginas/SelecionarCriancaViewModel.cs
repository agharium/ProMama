using ProMama.Model;
using ProMama.ViewModel.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProMama.ViewModel.Home.Paginas
{
    class SelecionarCriancaViewModel : ViewModelBase
    {
        private Aplicativo app = Aplicativo.Instance;

        private ObservableCollection<Crianca> _criancas;
        public ObservableCollection<Crianca> Criancas
        {
            get { return _criancas; }
            set { _criancas = value; }
        }

        // Commands
        public ICommand SelecionarCriancaCommand { get; set; }
        public ICommand AddCriancaCommand { get; set; }

        // Navigation
        private INavigation Navigation { get; set; }
        private readonly INavigationService _navigationService;

        public SelecionarCriancaViewModel(INavigation Navigation)
        {
            Criancas = new ObservableCollection<Crianca>(app._usuario.usuario_criancas);

            this.Navigation = Navigation;
            _navigationService = DependencyService.Get<INavigationService>();

            SelecionarCriancaCommand = new Command<Crianca>(SelecionarCrianca);
            AddCriancaCommand = new Command(AddCrianca);
        }

        // Método para selecionar criança
        private void SelecionarCrianca(Crianca c)
        {
            app._crianca = c;
            _navigationService.NavigateToHome();
        }

        private async void AddCrianca()
        {
            await _navigationService.NavigateToAddCriancaPush(Navigation);
        }
    }
}
