using ProMama.View.Home.Paginas;
using ProMama.View.Inicio;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.View.Services
{
    class NavigationService : ViewModel.Services.INavigationService
    {
        public void NavigateToCadastroLogin()
        {
            Application.Current.MainPage = new NavigationPage(new LoginCadastroTabbedView());
        }

        public void NavigateToAddCrianca()
        {
            Application.Current.MainPage = new NavigationPage(new AddCriancaView());
        }

        public async Task NavigateToAddCriancaPush(INavigation Navigation)
        {
            await Navigation.PushAsync(new AddCriancaView());
        }

        public void NavigateToHome()
        {
            Application.Current.MainPage = new Home.Home();
        }

        public async Task NavigateToPerfilCrianca(INavigation Navigation)
        {
            await Navigation.PushAsync(new PerfilCriancaView());
        }

        public async Task NavigateToPerfilMae(INavigation Navigation)
        {
            await Navigation.PushAsync(new PerfilMaeView());
        }

        public async Task NavigateToAddAcompanhamento(INavigation Navigation)
        {
            await Navigation.PushAsync(new AddAcompanhamentoView());
        }

        public async Task NavigateToInfoPage(INavigation Navigation, Model.Informacao info)
        {
            await Navigation.PushAsync(new InformacaoView(info));
        }
    }
}
