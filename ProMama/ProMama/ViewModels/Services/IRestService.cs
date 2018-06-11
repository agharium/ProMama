using ProMama.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProMama.ViewModels.Services
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

        Task<JsonMessage> ConversaCreate(JsonMessage pergunta, string token);

        Task<List<Conversa>> ConversasRead(string token);

        Task<List<Conversa>> ConversasUsuarioRead(string token);

        Task<List<Bairro>> BairrosRead();

        Task<List<Posto>> PostosRead(string token);

        Task<Sincronizacao> SincronizacaoRead(string token);

        Task<List<Notificacao>> NotificacoesRead(string token);

        Task<JsonMessage> UploadImage(Foto foto, string token);
    }
}
