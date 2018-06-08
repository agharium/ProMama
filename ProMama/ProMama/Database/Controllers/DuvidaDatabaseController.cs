using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class DuvidaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile DuvidaFile { get; set; }
        private Collection<Duvida, int> DuvidaCollection { get; set; }

        public DuvidaDatabaseController()
        {
            DuvidaFile = session["duvidas.dat"];
            DuvidaCollection = DuvidaFile.Collection<Duvida, int>("duvidas", obj => obj.duvida_id);
        }

        public void Save(Duvida obj)
        {
            DuvidaCollection.Persist(obj);
        }

        public void SaveList(List<Duvida> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Duvida Find(int id)
        {
            return DuvidaCollection.Find(id);
        }

        public void Delete(int id)
        {
            DuvidaCollection.Destroy(id);
        }

        public List<Duvida> GetAll()
        {
            return DuvidaCollection.All.ToList();
        }

        public void WipeTable()
        {
            foreach (var obj in GetAll())
            {
                Delete(obj.duvida_id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(DuvidaCollection.All, Formatting.Indented));
        }
    }
}
