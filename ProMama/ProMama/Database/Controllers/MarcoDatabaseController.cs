using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class MarcoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile MarcoFile { get; set; }
        private Collection<Marco, int> MarcoCollection { get; set; }

        public MarcoDatabaseController()
        {
            MarcoFile = session["marcos.dat"];
            MarcoCollection = MarcoFile.Collection<Marco, int>("marcos", obj => obj.id);
        }

        public void Save(Marco obj)
        {
            MarcoCollection.Persist(obj);
        }

        public void SaveIncrementing(Marco obj)
        {
            obj.id = GetAll().Count() + 1;
            Save(obj);
        }

        public void SaveList(List<Marco> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Marco Find(int id)
        {
            return MarcoCollection.Find(id);
        }

        public List<Marco> FindByChildId(int id)
        {
            var retorno = new List<Marco>();
            foreach (var obj in GetAll())
            {
                if (obj.crianca == id)
                    retorno.Add(obj);
            }
            return retorno;
        }

        public List<Marco> GetAll()
        {
            return MarcoCollection.All.ToList();
        }

        public void Delete(int id)
        {
            MarcoCollection.Destroy(id);
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
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(MarcoCollection.All, Formatting.Indented));
        }
    }
}