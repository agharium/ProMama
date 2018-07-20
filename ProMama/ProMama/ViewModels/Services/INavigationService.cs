using ProMama.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.ViewModels.Services
{
    interface INavigationService
    {
        void NavigateCadastroLogin();

        void NavigateAddCrianca();

        Task NavigateAddCriancaPush(INavigation Navigation);

        void NavigateHome();

        Task NavigatePerfilCriancaEdit(INavigation Navigation);

        Task NavigatePerfilMaeEdit(INavigation Navigation);

        Task NavigateAddAcompanhamento(INavigation Navigation);

        Task NavigateInformacao(INavigation Navigation, Informacao informacao);

        Task NavigateConversa(INavigation Navigation, Conversa duvida);

        Task NavigateFoto(INavigation Navigation, Foto foto);

        Task NavigateOutrasConversas(INavigation Navigation);

        Task NavigateMarcoVisualizacao(INavigation Navigation, Marco marco);

        Task NavigateDuvidaFrequente(INavigation Navigation, DuvidaFrequente duvidaFrequente);

        Task NavigateTrocarSenha(INavigation Navigation);

        void NavigateNovaSenha();
    }
}
