using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    interface IMessageService
    {
        Task AlertDialog(string message);

        Task<bool> ConfirmationDialog(string message, string negacao, string confirmacao);

        Task<string> ActionSheet(string message, string[] opcoes);
    }
}
