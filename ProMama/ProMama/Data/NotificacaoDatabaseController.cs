using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data
{
    public class NotificacaoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile NotificacaoFile { get; set; }
        private Collection<Notificacao, int> NotificacaoCollection { get; set; }

        public NotificacaoDatabaseController()
        {
            NotificacaoFile = session["notificacoes.dat"];
            NotificacaoCollection = NotificacaoFile.Collection<Notificacao, int>("notificacoes", n => n.id);
        }

        public void SaveNotificacao(Notificacao n)
        {
            NotificacaoCollection.Persist(n);
        }

        public void SaveNotificacaoList(List<Notificacao> notificacoes)
        {
            foreach (var n in notificacoes)
            {
                SaveNotificacao(n);
            }
        }

        public Notificacao FindNotificacao(int id)
        {
            return NotificacaoCollection.Find(id);
        }

        public List<Notificacao> GetAllNotificacao()
        {
            return NotificacaoCollection.All.ToList();
        }

        public void DeleteNotificacao(int id)
        {
            NotificacaoCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var n in GetAllNotificacao())
            {
                DeleteNotificacao(n.id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(NotificacaoCollection.All, Formatting.Indented));
        }
    }
}