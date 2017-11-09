using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    interface INavigationService
    {
        void NavigateToCadastroLogin();

        void NavigateToAddCrianca();

        void NavigateToHome();
    }
}
