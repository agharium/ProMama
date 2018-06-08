using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProMama.Models;
using ProMama.ViewModels.Services;
using Xamarin.Forms;

namespace ProMama.Views.Services
{
    class RestService : IRestService
    {
        private readonly string ApiUrl = "http://promama.cf/api";
        private readonly string TokenPadrao = "token1";

        public async Task<JsonMessage> UsuarioCreate(Usuario u)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/user?api_token=" + TokenPadrao, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: ENVIO DE USUÁRIO P/ CRIAÇÃO");
                    Debug.WriteLine(JsonConvert.SerializeObject(u));
                    Debug.WriteLine("API: CRIAÇÃO DE USUÁRIO");
                    Debug.WriteLine(obj);
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<JsonMessage> UsuarioUpdate(Usuario u)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/user/" + u.id + "/editar?api_token=" + u.api_token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: ENVIO DE USUÁRIO P/ ATUALIZAÇÃO");
                    Debug.WriteLine(JsonConvert.SerializeObject(u));
                    Debug.WriteLine("API: ATUALIZAR USUÁRIO");
                    Debug.WriteLine(obj);
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<JsonMessage> UsuarioLogin(Usuario u)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(u), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/login?api_token=" + TokenPadrao, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: LOGIN DE USUÁRIO");
                    Debug.WriteLine(obj);
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<Usuario> UsuarioRead(JsonMessage msg)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(ApiUrl + "/user/" + msg.id + "?api_token=" + msg.message);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: LEITURA DE USUÁRIO");
                    Debug.WriteLine(obj.ToString());

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    var usuario = JsonConvert.DeserializeObject<Usuario>(obj, settings);
                    usuario.api_token = msg.message;
                    return usuario;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<JsonMessage> CriancaCreate(Crianca c, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/criancas?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: ENVIO DE CRIANÇA P/ CRIAÇÃO");
                    Debug.WriteLine(JsonConvert.SerializeObject(c));
                    Debug.WriteLine("API: CRIAÇÃO DE CRIANÇA");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<JsonMessage> CriancaUpdate(Crianca c, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/criancas/" + c.crianca_id + "/editar?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: ATUALIZAR CRIANÇA");
                    Debug.WriteLine(obj);
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<List<Informacao>> InformacoesRead(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(ApiUrl + "/informacoes?api_token=" + token);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: LEITURA DE INFORMAÇÕES");
                    Debug.WriteLine(obj.ToString());

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    return JsonConvert.DeserializeObject<List<Informacao>>(obj, settings);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
                return new List<Informacao>();
            }
        }

        public async Task<JsonMessage> DuvidaCreate(JsonMessage msg, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/duvidas?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: CRIAÇÃO DE DÚVIDA");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + e.ToString());
            }
        }

        public async Task<List<Duvida>> DuvidasRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/duvidas-todos?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE TODAS AS DÚVIDAS");
                Debug.WriteLine(obj.ToString());

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Duvida>() : JsonConvert.DeserializeObject<List<Duvida>>(obj, settings);
            }
        }

        public async Task<List<Duvida>> DuvidasUsuarioRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/duvidas-do-user?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE DÚVIDAS DO USUÁRIO");
                Debug.WriteLine(obj.ToString());

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Duvida>() : JsonConvert.DeserializeObject<List<Duvida>>(obj, settings);
            }
        }

        public async Task<List<Bairro>> BairrosRead()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/bairros?api_token=" + TokenPadrao);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE BAIRROS");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Bairro>() : JsonConvert.DeserializeObject<List<Bairro>>(obj);
            }
        }

        public async Task<List<Posto>> PostosRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/postos?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE POSTOS");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Posto>() : JsonConvert.DeserializeObject<List<Posto>>(obj);
            }
        }

        public async Task<Sincronizacao> SincronizacaoRead(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(ApiUrl + "/sync?api_token=" + token);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: LEITURA DE SINCRONIZAÇÃO");
                    Debug.WriteLine(obj.ToString());

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    return JsonConvert.DeserializeObject<Sincronizacao>(obj, settings);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Notificacao>> NotificacoesRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/notificacao?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE NOTIFICAÇÕES");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Notificacao>() : JsonConvert.DeserializeObject<List<Notificacao>>(obj);
            }
        }

        public async Task<JsonMessage> UploadImage(Foto foto, string token)
        {
            IFileService File = DependencyService.Get<IFileService>();
            try
            {
                using (var client = new HttpClient())
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();

                    ByteArrayContent imagem = new ByteArrayContent(File.ReadAllBytes(foto.caminho));
                    StringContent crianca = new StringContent(foto.crianca.ToString());
                    StringContent mes = new StringContent(foto.mes.ToString());

                    content.Add(imagem, "foto", foto.caminho);
                    content.Add(crianca, "crianca");
                    content.Add(mes, "mes");

                    var result = await client.PostAsync(ApiUrl + "/uploadfoto?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: UPLOAD DE IMAGEM");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception ex)
            {
                return new JsonMessage(false, "Ocorreu um erro inesperado. Para propósitos de debug: " + ex.ToString());
            }
        }
    }
}