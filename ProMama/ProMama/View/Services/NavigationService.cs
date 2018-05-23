using ProMama.Model;
using ProMama.View.Home.Paginas;
using ProMama.View.Inicio;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.View.Services
{
    class NavigationService : ViewModel.Services.INavigationService
    {
        public void NavigateCadastroLogin()
        {
            Application.Current.MainPage = new NavigationPage(new LoginCadastroTabbedView());
        }

        public void NavigateAddCrianca()
        {
            Application.Current.MainPage = new NavigationPage(new AddCriancaView());
        }

        public async Task NavigateAddCriancaPush(INavigation Navigation)
        {
            await Navigation.PushAsync(new AddCriancaView());
        }

        public void NavigateHome()
        {
            Application.Current.MainPage = new Home.Home();
        }

        public async Task NavigatePerfilCriancaEdit(INavigation Navigation)
        {
            await Navigation.PushAsync(new PerfilCriancaEditView());
        }

        public async Task NavigatePerfilMaeEdit(INavigation Navigation)
        {
            await Navigation.PushAsync(new PerfilMaeEditView());
        }

        public async Task NavigateAddAcompanhamento(INavigation Navigation)
        {
            await Navigation.PushAsync(new AddAcompanhamentoView());
        }

        public async Task NavigateInformacao(INavigation Navigation, Informacao informacao)
        {
            await Navigation.PushAsync(new InformacaoView(informacao));
        }

        public async Task NavigateImagem(INavigation Navigation, Imagem imagem)
        {
            await Navigation.PushAsync(new ImagemView(imagem));
        }

        public async Task NavigateOutrasDuvidas(INavigation navigation)
        {
            await navigation.PushAsync(new DuvidasOutrosView());
        }
    }
}
