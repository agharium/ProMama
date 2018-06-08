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

        Task NavigateDuvida(INavigation Navigation, Duvida duvida);

        Task NavigateFoto(INavigation Navigation, Foto foto);

        Task NavigateOutrasDuvidas(INavigation Navigation);

        Task NavigateMarcoVisualizacao(INavigation Navigation, Marco marco);
    }
}
