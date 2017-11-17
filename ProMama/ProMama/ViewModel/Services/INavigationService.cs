using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.ViewModel.Services
{
    interface INavigationService
    {
        void NavigateToCadastroLogin();

        void NavigateToAddCrianca();

        void NavigateToHome();

        Task NavigateToPerfilCrianca(INavigation Navigation);

        Task NavigateToPerfilMae(INavigation Navigation);

        Task NavigateToAddAcompanhamento(INavigation Navigation);

        Task NavigateToInfoPage(INavigation Navigation, Model.Informacao info);
    }
}
