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
    }
}