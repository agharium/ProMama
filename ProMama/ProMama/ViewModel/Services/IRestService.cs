using ProMama.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProMama.ViewModel.Services
{
    public interface IRestService
    {
        Task<JsonMessage> UsuarioCreate(Usuario u);

        Task<JsonMessage> UsuarioUpdate(Usuario u);

        Task<JsonMessage> UsuarioLogin(Usuario u);

        Task<Usuario> UsuarioRead(JsonMessage msg);

        Task<JsonMessage> CriancaCreate(Crianca c, string token);

        Task<JsonMessage> CriancaUpdate(Crianca c, string token);

        Task<List<Informacao>> InformacoesRead(string token);

        Task<JsonMessage> DuvidaCreate(JsonMessage pergunta, string token);

        Task<List<Duvida>> DuvidasRead(string token);

        Task<List<Duvida>> DuvidasUsuarioRead(string token);

        Task<List<Bairro>> BairrosRead();

        Task<List<Posto>> PostosRead(string token);

        Task<Sincronizacao> SincronizacaoRead(string token);

        Task<List<Notificacao>> NotificacoesRead(string token);

        Task<JsonMessage> UploadImage(Foto foto, string token);
    }
}
