using ProMama.Model;
using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    public interface IRestService
    {
        Task<JsonMessage> UsuarioCreate(Usuario u);

        Task<JsonMessage> UsuarioLogin(Usuario u);

        Task<JsonMessage> UsuarioGet(JsonMessage msg);

        Task<JsonMessage> CriancaCreate(Crianca c);
    }
}
