using System.Threading.Tasks;

namespace ProMama.ViewModels.Services
{
    interface IMessageService
    {
        Task AlertDialog(string message);

        Task<bool> ConfirmationDialog(string message, string confirmacao, string negacao);
    }
}
