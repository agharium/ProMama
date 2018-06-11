using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class ConversaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile ConversaFile { get; set; }
        private Collection<Conversa, int> ConversaCollection { get; set; }

        public ConversaDatabaseController()
        {
            ConversaFile = session["conversas.dat"];
            ConversaCollection = ConversaFile.Collection<Conversa, int>("conversas", obj => obj.id);
        }

        public void Save(Conversa obj)
        {
            ConversaCollection.Persist(obj);
        }

        public void SaveList(List<Conversa> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Conversa Find(int id)
        {
            return ConversaCollection.Find(id);
        }

        public void Delete(int id)
        {
            ConversaCollection.Destroy(id);
        }

        public List<Conversa> GetAll()
        {
            return ConversaCollection.All.ToList();
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
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ConversaCollection.All, Formatting.Indented));
        }
    }
}
