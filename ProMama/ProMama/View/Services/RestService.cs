using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProMama.Model;
using ProMama.ViewModel.Services;

namespace ProMama.View.Services
{
    class RestService : IRestService
    {
        public async Task<string> UsuarioCreate(Usuario u)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("http://jpfilho.com.br/promama/api/usuario/create.php", content);
                return await result.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> UsuarioLogin(Usuario u)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("http://jpfilho.com.br/promama/api/usuario/login.php", content);
                return await result.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> CriancaCreate(Crianca c)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("http://jpfilho.com.br/promama/api/crianca/create.php", content);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
