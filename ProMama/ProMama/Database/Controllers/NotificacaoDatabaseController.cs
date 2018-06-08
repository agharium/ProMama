using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class NotificacaoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile NotificacaoFile { get; set; }
        private Collection<Notificacao, int> NotificacaoCollection { get; set; }

        public NotificacaoDatabaseController()
        {
            NotificacaoFile = session["notificacoes.dat"];
            NotificacaoCollection = NotificacaoFile.Collection<Notificacao, int>("notificacoes", obj => obj.id);
        }

        public void Save(Notificacao obj)
        {
            NotificacaoCollection.Persist(obj);
        }

        public void SaveList(List<Notificacao> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Notificacao Find(int id)
        {
            return NotificacaoCollection.Find(id);
        }

        public List<Notificacao> GetAll()
        {
            return NotificacaoCollection.All.ToList();
        }

        public void Delete(int id)
        {
            NotificacaoCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var obj in GetAll())
            {
                Delete(obj.id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(NotificacaoCollection.All, Formatting.Indented));
        }
    }
}