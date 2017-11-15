using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.View.Services
{
    class NavigationService : ViewModel.Services.INavigationService
    {
        public void NavigateToCadastroLogin()
        {
            Application.Current.MainPage = new NavigationPage(new Inicio.LoginCadastroTabbedView());
        }

        public void NavigateToAddCrianca()
        {
            Application.Current.MainPage = new NavigationPage(new Extra.AddCriancaView());
        }

        public void NavigateToHome()
        {
            Application.Current.MainPage = new Home.Home();
        }

        public async Task NavigateToPerfilCrianca(INavigation Navigation)
        {
            await Navigation.PushAsync(new Home.Paginas.PerfilCriancaView());
        }

        public async Task NavigateToPerfilMae(INavigation Navigation)
        {
            await Navigation.PushAsync(new Home.Paginas.PerfilMaeView());
        }
    }
}
