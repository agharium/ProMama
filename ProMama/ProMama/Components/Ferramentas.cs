using Plugin.LocalNotifications;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.Components
{
    public abstract class Ferramentas
    {
        private static Aplicativo app = Aplicativo.Instance;
        private static readonly IRestService RestService = DependencyService.Get<IRestService>();
        private static readonly IFileService FileService = DependencyService.Get<IFileService>();

        public static readonly List<string> IdadesExtensoFotos = new List<string>() {
                "recém-nascido",
                "1 mês",
                "2 meses",
                "3 meses",
                "4 meses",
                "5 meses",
                "6 meses",
                "7 meses",
                "8 meses",
                "9 meses",
                "10 meses",
                "11 meses",
                "1 ano",
                "1 ano e 1 mês",
                "1 ano e 2 meses",
                "1 ano e 3 meses",
                "1 ano e 4 meses",
                "1 ano e 5 meses",
                "1 ano e 6 meses",
                "1 ano e 7 meses",
                "1 ano e 8 meses",
                "1 ano e 9 meses",
                "1 ano e 10 meses",
                "1 ano e 11 meses",
                "2 anos"
            };

        public static async Task SincronizarBanco()
        {
            var syncAux = await RestService.SincronizacaoRead(app._usuario.api_token);

            if (app._sync == null)
                app._sync = new Sincronizacao(1);

            if (app._sync.bairro != syncAux.bairro)
            {
                App.BairroDatabase.WipeTable();
                App.BairroDatabase.SaveList(await RestService.BairrosRead());
            }

            if (app._sync.posto != syncAux.posto)
            {
                App.PostoDatabase.WipeTable();
                App.PostoDatabase.SaveList(await RestService.PostosRead(app._usuario.api_token));
            }

            if (app._sync.informacao != syncAux.informacao)
            {
                App.InformacaoDatabase.WipeTable();

                var infos = await RestService.InformacoesRead(app._usuario.api_token);
                foreach (var i in infos)
                {
                    i.informacao_imagem_visivel = !String.IsNullOrEmpty(i.informacao_foto);
                    i.informacao_resumo = CriarResumo(i.informacao_corpo);
                }

                App.InformacaoDatabase.SaveList(infos);
            }

            if (app._sync.duvidas_frequentes != syncAux.duvidas_frequentes)
            {
                App.DuvidaFrequenteDatabase.WipeTable();

                var duvidasFrequentes = await RestService.DuvidasFrequentesRead(app._usuario.api_token);
                foreach (var df in duvidasFrequentes)
                {
                    df.resumo = CriarResumo(df.texto);
                }

                App.DuvidaFrequenteDatabase.SaveList(duvidasFrequentes);
            }

            if (app._sync.notificacao != syncAux.notificacao)
            {
                App.NotificacaoDatabase.WipeTable();
                App.NotificacaoDatabase.SaveList(await RestService.NotificacoesRead(app._usuario.api_token));
            }

            if (app._sync.faleConoscoLastUpdate == null || (DateTime.Now - app._sync.faleConoscoLastUpdate).Seconds >= 86400)
            {
                if (app._usuario != null)
                {
                    var conversasTodos = await RestService.ConversasRead(app._usuario.api_token);
                    var conversasUser = await RestService.ConversasUsuarioRead(app._usuario.api_token);

                    foreach (var c in conversasTodos)
                    {
                        c.resumo = CriarResumo(c.resposta);
                    }
                    foreach (var c in conversasUser)
                    {
                        if (String.IsNullOrEmpty(c.resposta))
                        {
                            c.resposta = "Aguardando resposta.";
                            c.resumo = "Aguardando resposta.";
                        } else
                        {
                            c.resumo = CriarResumo(c.resposta);
                        }
                    }

                    App.ConversaDatabase.WipeTable();
                    App.ConversaDatabase.SaveList(conversasTodos);
                    App.ConversaDatabase.SaveList(conversasUser);

                    app._sync.faleConoscoLastUpdate = DateTime.Now;
                }
            }

            app._sync = syncAux;
            App.SincronizacaoDatabase.Save(app._sync);
        }

        public static async Task MarcarNotificacoes()
        {
            if (app._usuario.criancas.Count > 0 && app._usuario.criancas != null)
            {
                var notifications = App.NotificacaoDatabase.GetAll();
                var idadeAtual = (DateTime.Now - app._crianca.crianca_dataNascimento).Days;
                var oQuantoAntesCount = 1;

                foreach (var c in app._usuario.criancas)
                {
                    foreach (var n in notifications)
                    {
                        int nId = int.Parse(n.id.ToString() + c.crianca_id.ToString());
                        int notificacaoDias = (int)Math.Ceiling(n.semana / 0.1551871428571429);

                        var artigo = c.crianca_sexo == 0 ? "o" : "a";
                        var titulo = n.titulo.Replace("%NOMEDACRIANCA%", c.crianca_primeiro_nome).Replace("%ARTIGO%", app._usuario.name);
                        var texto = n.texto.Replace("%NOMEDACRIANCA%", c.crianca_primeiro_nome).Replace("%ARTIGO%", app._usuario.name);
                        titulo = char.ToUpper(titulo[0]) + titulo.Substring(1);
                        texto = char.ToUpper(texto[0]) + texto.Substring(1);

                        if (c.notificacoesMarcadas == null)
                            c.notificacoesMarcadas = new List<int>();

                        if (app._usuario.notificacoes_oQuantoAntes == null)
                            app._usuario.notificacoes_oQuantoAntes = new List<int>();

                        if (!c.notificacoesMarcadas.Contains(n.id) && notificacaoDias >= idadeAtual)
                        {
                            CrossLocalNotifications.Current.Show(titulo, texto, n.id, DateTime.Now.AddDays(notificacaoDias - idadeAtual));
                            c.notificacoesMarcadas.Add(n.id);
                            Debug.WriteLine("Notificação '" + titulo + "' marcada para " + DateTime.Now.AddDays(notificacaoDias - idadeAtual).ToString());
                        } else if (n.semana == -1 && !app._usuario.notificacoes_oQuantoAntes.Contains(n.id))
                        {
                            CrossLocalNotifications.Current.Show(titulo, texto, n.id, DateTime.Now.AddHours(oQuantoAntesCount));
                            app._usuario.notificacoes_oQuantoAntes.Add(n.id);
                            Debug.WriteLine("Notificação '" + titulo + "' marcada para " + DateTime.Now.AddHours(oQuantoAntesCount).ToString());
                            oQuantoAntesCount++;
                        }
                    }
                    App.CriancaDatabase.Save(c);
                }
                App.UsuarioDatabase.Save(app._usuario);
            }
        }

        public static async Task UploadInformacoesUser()
        {
            var acompanhamentos = App.AcompanhamentoDatabase.GetAll();
            var fotos = App.FotoDatabase.GetAll();
            var marcos = App.MarcoDatabase.GetAll();

            foreach (var obj in acompanhamentos)
            {
                if (!obj.uploaded)
                {
                    var result = await RestService.AcompanhamentoUpload(obj, app._usuario.api_token);
                    if (result.success)
                    {
                        var aux = App.AcompanhamentoDatabase.Find(result.id);

                        if (aux != obj && aux != null)
                        {
                            App.AcompanhamentoDatabase.SaveIncrementing(aux);
                        }

                        App.AcompanhamentoDatabase.Delete(obj.id);

                        obj.uploaded = true;
                        obj.id = result.id;
                        App.AcompanhamentoDatabase.Save(obj);
                    }
                }
            }

            foreach (var obj in fotos)
            {
                if (!obj.uploaded)
                {
                    var result = await RestService.FotoUpload(obj, app._usuario.api_token);
                    if (result.success)
                    {
                        var aux = App.FotoDatabase.Find(result.id);

                        if (aux != obj && aux != null)
                        {
                            App.FotoDatabase.SaveIncrementing(aux);
                        }

                        App.FotoDatabase.Delete(obj.id);

                        obj.uploaded = true;
                        obj.id = result.id;
                        App.FotoDatabase.Save(obj);
                    }
                }
            }

            foreach (var obj in marcos)
            {
                if (!obj.uploaded)
                {
                    var result = await RestService.MarcoUpload(obj, app._usuario.api_token);
                    if (result.success)
                    {
                        var aux = App.MarcoDatabase.Find(result.id);

                        if (aux != obj && aux != null)
                        {
                            App.MarcoDatabase.SaveIncrementing(aux);
                        }

                        App.MarcoDatabase.Delete(obj.id);

                        obj.uploaded = true;
                        obj.id = result.id;
                        App.MarcoDatabase.Save(obj);
                    }
                }
            }
        }

        public static async Task DownloadInformacoesUser()
        {
            var acompanhamentos = await RestService.AcompanhamentoRead(app._usuario.api_token);
            var fotos = await RestService.FotoRead(app._usuario.api_token);
            var marcos = await RestService.MarcoRead(app._usuario.api_token);

            foreach (var obj in acompanhamentos)
            {
                obj.uploaded = true;
                App.AcompanhamentoDatabase.Save(obj);
            }

            foreach (var obj in fotos)
            {
                obj.caminho = FileService.DownloadFile(obj.url, app._usuario.api_token);
                obj.source = obj.caminho;
                obj.uploaded = true;
                obj.titulo = IdadesExtensoFotos[obj.mes];
                App.FotoDatabase.Save(obj);
            }

            foreach (var obj in marcos)
            {
                obj.uploaded = true;
                App.MarcoDatabase.Save(obj);
            }
        }

        public static string CriarResumo(string texto)
        {
            var resumo = String.Join(" ", texto.Split().Take(20).ToArray());
            resumo.Remove(resumo.Length - 1, 1);
            resumo += "...";

            return resumo;
        }

        public static string DefineIdadeExtenso(double IdadeSemanas, double IdadeMeses)
        {
            if (IdadeSemanas < 4.34524)
            {
                return SemanasToString(IdadeSemanas);
            }
            else if (IdadeMeses < 12)
            {
                return MesesToString(IdadeMeses);// + " e " + SemanasToString();
            }
            else
            {
                if (IdadeMeses > 12 && IdadeMeses < 13)
                {
                    return "1 ano";
                }
                else if (IdadeMeses > 24)
                {
                    return "2 anos";
                }
                else
                {
                    return "1 ano e " + MesesToString(IdadeMeses);
                }
            }
        }

        private static string SemanasToString(double IdadeSemanas)
        {
            double semanas = IdadeSemanas;
            while (semanas > 4.34524)
            {
                semanas -= 4.34524;
            }

            int semanasAux = Convert.ToInt32(semanas);

            if (IdadeSemanas < 1.08631)
            {
                return "recém-nascido";
            }
            else if (IdadeSemanas >= 1.08631 && IdadeSemanas < 2.17262)
            {
                return "1 semana";
            }
            else if (IdadeSemanas >= 2.17262 && IdadeSemanas < 3.25893)
            {
                return "2 semanas";
            }
            else
            {
                return "3 semanas";
            }
        }

        private static string MesesToString(double IdadeMeses)
        {
            int m = (IdadeMeses > 12) ? (int)Math.Floor(IdadeMeses - 12) : (int)Math.Floor(IdadeMeses);
            return m == 1 ? m + " mês" : m + " meses";
        }

        private static string DiasToString(double IdadeSemanas)
        {
            double semanas = IdadeSemanas;
            while (semanas > 1.08631)
            {
                semanas -= 1.08631;
            }
            int dias = (int)Math.Floor(semanas);
            if (dias <= 0)
            {
                return "";
            }
            else if (dias == 1)
            {
                return "1 dia";
            }
            else
            {
                return dias + " dias";
            }
        }

        private static int _dias;
        private static int Dias
        {
            get
            {
                return _dias;
            }
            set
            {
                if (value == 7)
                {
                    _dias = 0;
                    Semanas = Semanas + 1;
                } else
                {
                    _dias = value;
                }
            }
        }

        private static int _semanas;
        private static int Semanas
        {
            get
            {
                return _semanas;
            }
            set
            {
                if (value == 4)
                {
                    _semanas = 0;
                    Meses = Meses + 1;
                } else
                {
                    _semanas = value;
                }
            }
        }

        private static int _meses;
        private static int Meses
        {
            get
            {
                return _meses;
            }
            set
            {
                if (value == 12)
                {
                    _meses = 0;
                    Anos = Anos + 1;
                } else
                {
                    _meses = value;
                }
            }
        }
        
        private static int Anos { get; set; }

        public static string DaysToFullString(int tDias, int tipo)
        {
            // tipo: 1 = DefineIdadeExtenso da classe Criança / 2 = Marcos e Acompanhamento
            Anos = 0;
            Meses = 0;
            Semanas = 0;
            Dias = 0;

            //double numeroMagico = 0.15518714285;
            double numeroMagico = 93857.18399568;
            double totalDias = (tDias+1) * 86400;
            while (totalDias >= numeroMagico)
            {
                Dias = Dias + 1;
                totalDias -= numeroMagico;
            }

            if ((Anos == 0 && Meses == 0 &&
                Semanas == 0 && Dias == 0) ||
                (tipo == 1 &&
                Anos == 0 && Meses == 0 &&
                Semanas == 0 && Dias < 7))
                return "recém-nascido";

            string strReturn = "";

            if (Anos == 1)
            {
                strReturn += "1 ano, ";
            }
            else if (Anos > 1)
            {
                strReturn += Anos + " anos, ";
            }

            if (Meses == 1)
            {
                strReturn += "1 mês, ";
            }
            else if (Meses > 1)
            {
                strReturn += Meses + " meses, ";
            }

            if (Semanas > 0 && (
                    tipo == 2 || (
                        tipo == 1 && (
                            Anos == 0 &&
                            Meses == 0)
                        )
                    )
                )
            {
                if (Semanas == 1)
                {
                    strReturn += "1 semana, ";
                }
                else if (Semanas > 1)
                {
                    strReturn += Semanas + " semanas, ";
                }
            }

            if (tipo == 2)
            {
                if (Dias == 1)
                {
                    strReturn += "1 dia";
                }
                else if (Dias > 1)
                {
                    strReturn += Dias + " dias";
                }
            }

            if (strReturn[strReturn.Length - 2].Equals(','))
                strReturn = strReturn.Substring(0, strReturn.Length - 2);

            if (strReturn.Contains(", "))
            {
                var index = strReturn.LastIndexOf(", ");
                strReturn = strReturn.Remove(index, 2).Insert(index, " e ");
            }

            return strReturn;
        }

        // https://pt.stackoverflow.com/questions/2/como-fa%C3%A7o-para-remover-acentos-em-uma-string
        public static string RemoverAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        // https://pt.stackoverflow.com/a/15741
        public static bool ValidarNomeRegex(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                var NomePattern = "^[a-zA-Z\u00C0-\u00FF ]+$";
                return Regex.IsMatch(nome, NomePattern);
            } else
            {
                return true;
            }
        }

        // http://www.rhyous.com/2010/06/15/csharp-email-regular-expression
        public static bool VerificarEmailRegex(string email)
        {
            var RegexEmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                  + "@"
                                  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

            return Regex.IsMatch(email, RegexEmailPattern);
        }

        // http://codesnippets.fesslersoft.de/how-to-remove-unicode-characters-from-a-string-in-c-and-vb-net/
        public static string StripUnicodeCharactersFromString(string inputValue)
        {
            if (!string.IsNullOrEmpty(inputValue))
                return Regex.Replace(inputValue, @"[^\u0000-\u007F]", string.Empty);
            else
                return "";
        }

        // http://lukealderton.com/blog/posts/2016/may/autocustom-height-on-xamarin-forms-webview-for-android-and-ios/
        public static ExtendedWebView CreateBrowser(string text)
        {
            return new ExtendedWebView()
            {
                Source = new HtmlWebViewSource()
                {
                    Html = "<html>" +
                                "<body style=\"text-align: justify; font-size: 90%; background-color: #00000000; padding: 0; margin: 0\">" +
                                    String.Format("<p>{0}</p>", text) +
                                "</body>" +
                            "</html>"
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
        }
    }
}