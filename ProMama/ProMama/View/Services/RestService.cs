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
        public async Task<JsonMessage> UsuarioCreate(Usuario u)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("http://jpfilho.com.br/promama/api/usuario/create.php", content);
                var obj = await result.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("API: CRIAÇÃO DE USUÁRIO");
                System.Diagnostics.Debug.WriteLine(obj);
                return JsonConvert.DeserializeObject<JsonMessage>(obj);
            }
        }

        public async Task<JsonMessage> UsuarioLogin(Usuario u)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");

                    var result = await client.PostAsync("http://jpfilho.com.br/promama/api/usuario/login.php", content);
                    var obj = await result.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("API: LOGIN DE USUÁRIO");
                    System.Diagnostics.Debug.WriteLine(obj);
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<JsonMessage> UsuarioGet(JsonMessage msg)
        {   
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");

                var result = await client.PostAsync("http://jpfilho.com.br/promama/api/usuario/read.php", content);
                var obj = await result.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("API: LEITURA DE USUÁRIO");
                System.Diagnostics.Debug.WriteLine(obj.ToString());
                return JsonConvert.DeserializeObject<JsonMessage>(obj);
            }
        }

        public async Task<JsonMessage> CriancaCreate(Crianca c)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");

                    var result = await client.PostAsync("http://jpfilho.com.br/promama/api/crianca/create.php", content);
                    var obj = await result.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("API: CRIAÇÃO DE CRIANÇA");
                    System.Diagnostics.Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public bool TranslateStrToBool(string str)
        {
            return (str.Equals("false")) ? false : true;
        }
    }
}
