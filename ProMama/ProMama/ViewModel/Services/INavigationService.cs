using ProMama.Model;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.ViewModel.Services
{
    interface INavigationService
    {
        void NavigateToCadastroLogin();

        void NavigateToAddCrianca();

        Task NavigateToAddCriancaPush(INavigation Navigation);

        void NavigateToHome();

        Task NavigateToPerfilCrianca(INavigation Navigation);

        Task NavigateToPerfilMae(INavigation Navigation);

        Task NavigateToAddAcompanhamento(INavigation Navigation);

        Task NavigateToInfoPage(INavigation Navigation, Informacao info);

        Task NavigateToFotoPage(INavigation Navigation, ImageSource foto);
    }
}
