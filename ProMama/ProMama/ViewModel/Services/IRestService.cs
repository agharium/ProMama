using ProMama.Model;
using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    public interface IRestService
    {
        Task<string> UsuarioCreate(Usuario u);

        Task<string> UsuarioLogin(Usuario u);

        Task<string> CriancaCreate(Crianca c);
    }
}
