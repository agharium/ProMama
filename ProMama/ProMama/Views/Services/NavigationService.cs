using ProMama.Models;
using ProMama.ViewModels.Services;
using ProMama.Views.Home.Paginas;
using ProMama.Views.Inicio;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.Views.Services
{
    class NavigationService : INavigationService
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
            await Navigation.PushAsync(new DetalhesView(informacao));
        }

        public async Task NavigateConversa(INavigation Navigation, Conversa conversa)
        {
            await Navigation.PushAsync(new DetalhesView(conversa));
        }

        public async Task NavigateFoto(INavigation Navigation, Foto foto)
        {
            await Navigation.PushAsync(new FotoView(foto));
        }

        public async Task NavigateOutrasConversas(INavigation Navigation)
        {
            await Navigation.PushAsync(new FaleConoscoOutrosView());
        }

        public async Task NavigateMarcoVisualizacao(INavigation Navigation, Marco marco)
        {
            await Navigation.PushAsync(new MarcoVisualizacaoView(marco));
        }

        public async Task NavigateDuvidaFrequente(INavigation Navigation, DuvidaFrequente duvidaFrequente)
        {
            await Navigation.PushAsync(new DetalhesView(duvidaFrequente));
        }

        public async Task NavigateTrocarSenha(INavigation Navigation)
        {
            await Navigation.PushAsync(new PerfilMaeEditSenhaView());
        }
    }
}
