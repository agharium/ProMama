using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.ViewModel.Services
{
    interface INavigationService
    {
        void NavigateToCadastroLogin();

        void NavigateToAddCrianca();

        void NavigateToHome();

        Task NavigateToPerfilCrianca(NavigationPage navigation);

        Task NavigateToPerfilMae(NavigationPage navigation);
    }
}
