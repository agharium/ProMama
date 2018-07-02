using Plugin.LocalNotifications;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProMama.Components
{
    public abstract class Ferramentas
    {
        private static Aplicativo app = Aplicativo.Instance;
        private static readonly IRestService RestService = DependencyService.Get<IRestService>();

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
                if (app._crianca != null)
                {
                    var currentNotifications = App.NotificacaoDatabase.GetAll();
                    var newNotifications = await RestService.NotificacoesRead(app._usuario.api_token);
                    var idadeAtual = (DateTime.Now - app._crianca.crianca_dataNascimento).Days;

                    foreach (var n in newNotifications)
                    {
                        if (currentNotifications.FirstOrDefault(o => o.id == n.id) == null)
                        {
                            App.NotificacaoDatabase.Save(n);
                            if (n.semana == -1)
                            {
                                CrossLocalNotifications.Current.Show(n.titulo, n.texto);
                            }
                            else if (n.semana >= idadeAtual)
                            {
                                CrossLocalNotifications.Current.Show(n.titulo, n.texto, n.id, DateTime.Now.AddDays(n.semana - idadeAtual));
                            }
                        }
                    }
                }
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

        private static string CriarResumo(string texto)
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

            if (Anos == 0 && Meses == 0 &&
                Semanas == 0 && Dias == 0)
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
        public static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }
    }
}