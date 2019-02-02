using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class NotificacaoAtivaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile NotificacaoAtivaFile { get; set; }
        private Collection<NotificacaoAtiva, int> NotificacaoAtivaCollection { get; set; }

        public NotificacaoAtivaDatabaseController()
        {
            NotificacaoAtivaFile = session["notificacoesAtivas.dat"];
            NotificacaoAtivaCollection = NotificacaoAtivaFile.Collection<NotificacaoAtiva, int>("notificacoesAtivas", obj => obj.id);
        }

        public void Save(NotificacaoAtiva obj)
        {
            NotificacaoAtivaCollection.Persist(obj);
        }

        public int SaveIncrementing(NotificacaoAtiva obj)
        {
            obj.id = GetAll().Count() + 1;
            Save(obj);
            return obj.id;
        }

        public void SaveList(List<NotificacaoAtiva> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public NotificacaoAtiva Find(int id)
        {
            return NotificacaoAtivaCollection.Find(id);
        }

        public List<NotificacaoAtiva> GetAll()
        {
            return NotificacaoAtivaCollection.All.ToList();
        }

        public List<NotificacaoAtiva> GetAllByChildId(int id)
        {
            var retorno = new List<NotificacaoAtiva>();
            foreach (var obj in GetAll())
            {
                if (obj.crianca_id == id)
                    retorno.Add(obj);
            }
            return retorno;
        }

        public bool CheckIfExists(int crianca_id, int notificacao_id)
        {
            foreach (var obj in GetAll())
            {
                if (obj.crianca_id == crianca_id && obj.notificacao_id == notificacao_id)
                    return true;
            }
            return false;
        }

        public void Delete(int id)
        {
            NotificacaoAtivaCollection.Destroy(id);
        }

        public void DeleteByChildId(int id)
        {
            foreach (var obj in GetAll())
            {
                if (obj.crianca_id == id)
                {
                    Delete(obj.id);
                }
            }
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
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(NotificacaoAtivaCollection.All, Formatting.Indented));
        }
    }
}