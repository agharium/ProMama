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
        private readonly string ApiUrl = "http://saude.osorio.rs.gov.br:7083/api";
        private readonly string TokenPadrao = "PRO:7B68D5409F4E2A0F3F224F0C2E5D58FE7F6607EB6D9CEB1891033272CE263F44";

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
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
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
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
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
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
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
                Debug.WriteLine(e.ToString());
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
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
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
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Crianca>> CriancasReadByUser(Usuario u)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/criancas-user?api_token=" + u.api_token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE CRIANÇAS DO USUÁRIO");
                Debug.WriteLine(obj.ToString());

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Crianca>() : JsonConvert.DeserializeObject<List<Crianca>>(obj, settings);
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
                Debug.WriteLine(e);
                return new List<Informacao>();
            }
        }

        public async Task<JsonMessage> ConversaCreate(JsonMessage msg, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/conversa?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: CRIAÇÃO DE CONVERSA");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Conversa>> ConversasRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/conversa-todos?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE TODAS AS CONVERSAS");
                Debug.WriteLine(obj.ToString());

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Conversa>() : JsonConvert.DeserializeObject<List<Conversa>>(obj, settings);
            }
        }

        public async Task<List<Conversa>> ConversasUsuarioRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/conversa-user?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE CONVERSAS DO USUÁRIO");
                Debug.WriteLine(obj.ToString());

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Conversa>() : JsonConvert.DeserializeObject<List<Conversa>>(obj, settings);
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
                Debug.WriteLine(e);
                return null;
            }
        }

        public async Task<JsonMessage> SincronizacaoBairroRead()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(ApiUrl + "/bairrosync?api_token=" + TokenPadrao);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: LEITURA DE SINCRONIZAÇÃO DE BAIRRO");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Notificacao>> NotificacoesRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/notificacoes?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE NOTIFICAÇÕES");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Notificacao>() : JsonConvert.DeserializeObject<List<Notificacao>>(obj);
            }
        }

        public async Task<List<DuvidaFrequente>> DuvidasFrequentesRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/duvidas-frequentes?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE TODAS AS DÚVIDAS FREQUENTES");
                Debug.WriteLine(obj.ToString());

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                return string.IsNullOrEmpty(obj.ToString()) ? new List<DuvidaFrequente>() : JsonConvert.DeserializeObject<List<DuvidaFrequente>>(obj, settings);
            }
        }

        public async Task<JsonMessage> FotoCriancaUpload(Foto foto, string token)
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

                    var result = await client.PostAsync(ApiUrl + "/upload-foto-crianca?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: UPLOAD DE IMAGEM DA CRIANÇA");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<JsonMessage> FotoUserUpload(string foto, string token)
        {
            IFileService File = DependencyService.Get<IFileService>();
            try
            {
                using (var client = new HttpClient())
                {
                    MultipartFormDataContent content = new MultipartFormDataContent();

                    ByteArrayContent imagem = new ByteArrayContent(File.ReadAllBytes(foto));

                    content.Add(imagem, "foto", foto);

                    var result = await client.PostAsync(ApiUrl + "/upload-foto-user?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: UPLOAD DE IMAGEM DO USUÁRIO");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Foto>> FotoRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/fotos?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE FOTOS");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Foto>() : JsonConvert.DeserializeObject<List<Foto>>(obj);
            }
        }

        public async Task<JsonMessage> AcompanhamentoUpload(Acompanhamento acompanhamento, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(acompanhamento), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/acompanhamentos?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: UPLOAD DE ACOMPANHAMENTO");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Acompanhamento>> AcompanhamentoRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/acompanhamentos?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE ACOMPANHAMENTOS");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Acompanhamento>() : JsonConvert.DeserializeObject<List<Acompanhamento>>(obj);
            }
        }

        public async Task<JsonMessage> MarcoUpload(Marco marco, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(marco), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/marcos?api_token=" + token, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: UPLOAD DE MARCO");
                    Debug.WriteLine(obj.ToString());
                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (JsonReaderException e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Marco>> MarcoRead(string token)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(ApiUrl + "/marcos?api_token=" + token);
                var obj = await result.Content.ReadAsStringAsync();
                Debug.WriteLine("API: LEITURA DE MARCOS");
                Debug.WriteLine(obj.ToString());

                return string.IsNullOrEmpty(obj.ToString()) ? new List<Marco>() : JsonConvert.DeserializeObject<List<Marco>>(obj);
            }
        }

        public async Task<JsonMessage> RecuperarSenha(JsonMessage msg)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(ApiUrl + "/recuperar-senha?api_token=" + TokenPadrao, content);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: RECUPERAÇÃO DE SENHA");
                    Debug.WriteLine(obj.ToString());

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }

        public async Task<JsonMessage> RemoverFoto(int foto, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.DeleteAsync(ApiUrl + "/fotos/" + foto + "/remover?api_token=" + token);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: EXCLUSÃO DE FOTO");
                    Debug.WriteLine(obj.ToString());

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }
        
        public async Task<JsonMessage> RemoverCrianca(int crianca, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.DeleteAsync(ApiUrl + "/criancas/" + crianca + "/remover?api_token=" + token);
                    var obj = await result.Content.ReadAsStringAsync();
                    Debug.WriteLine("API: EXCLUSÃO DE CRIANÇA");
                    Debug.WriteLine(obj.ToString());

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    return JsonConvert.DeserializeObject<JsonMessage>(obj);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return new JsonMessage(false, "Ocorreu um erro inesperado. Tente novamente mais tarde.");
            }
        }
    }
}