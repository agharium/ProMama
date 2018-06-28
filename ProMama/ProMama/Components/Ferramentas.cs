using Plugin.LocalNotifications;
using ProMama.Models;
using ProMama.ViewModels.Services;
using System;
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

                    app._sync = syncAux;
                    App.SincronizacaoDatabase.Save(app._sync);
                }
            }
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

        public static string DaysToFullString(double totalDias)
        {
            Anos = 0;
            Meses = 0;
            Semanas = 0;
            Dias = 0;

            double numeroMagico = 0.1551871428571429;
            while (totalDias > numeroMagico)
            {
                Dias = Dias + 1;
                totalDias -= numeroMagico;
            }

            if (Anos == 0 && Meses == 0 &&
                Semanas == 0 && Dias == 0)
                return "recém-nascido";

            string AnosString = "";
            string SemanasString = "";
            string MesesString = "";
            string DiasString = "";
            int SeparadorCount = 0;

            if (Anos > 0)
            {
                if (Anos == 1)
                {
                    AnosString = "1 ano";
                }
                else if (Anos > 1)
                {
                    AnosString = Anos + " anos";
                }
                SeparadorCount++;
            }      
            
            if (Meses > 0)
            {
                if (Meses == 1)
                {
                    MesesString = "1 mês";
                }
                else if (Meses > 1)
                {
                    MesesString = Meses + " meses";
                }
                SeparadorCount++;
            }

            if (Semanas > 0)
            {
                if (Semanas == 1)
                {
                    SemanasString = "1 semana";
                }
                else if (Semanas > 1)
                {
                    SemanasString = Semanas + " semanas";
                }
                SeparadorCount++;
            }

            if (Dias == 1)
            {
                DiasString = "1 dia";
            }
            else if (Dias > 1)
            {
                DiasString = Dias + " dias";
            }

            string[] strReturn = new string[7] { "", "", "", "", "", "", ""};

            strReturn[0] = AnosString;
            strReturn[2] = MesesString;
            strReturn[4] = SemanasString;
            strReturn[6] = DiasString;

            bool first = true;
            for (int i = 5; i >= 1; i = i-2)
            {
                if (SeparadorCount > 0)
                {
                    if (!strReturn[i-1].Equals(String.Empty) &&
                        !strReturn[i + 1].Equals(String.Empty))
                    {
                        if (first)
                        {
                            strReturn[i] = " e ";
                            first = false;
                        }
                        else
                        {
                            strReturn[i] = ", ";
                        }
                        SeparadorCount--;
                    }
                }
            }
            
            return string.Join("", strReturn);
        }
    }
}