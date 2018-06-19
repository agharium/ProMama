using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class DuvidaFrequenteDatabaseController
    {
        Session session = App.DB;

        private CollectionFile DuvidaFrequenteFile { get; set; }
        private Collection<DuvidaFrequente, int> DuvidaFrequenteCollection { get; set; }

        public DuvidaFrequenteDatabaseController()
        {
            DuvidaFrequenteFile = session["duvidasfrequentes.dat"];
            DuvidaFrequenteCollection = DuvidaFrequenteFile.Collection<DuvidaFrequente, int>("duvidasfrequentes", obj => obj.id);
        }

        public void Save(DuvidaFrequente obj)
        {
            DuvidaFrequenteCollection.Persist(obj);
        }

        public void SaveList(List<DuvidaFrequente> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public DuvidaFrequente Find(int id)
        {
            return DuvidaFrequenteCollection.Find(id);
        }

        public List<DuvidaFrequente> GetAll()
        {
            return DuvidaFrequenteCollection.All.ToList().OrderBy(o => o.titulo).ToList();
        }

        public void Delete(int id)
        {
            DuvidaFrequenteCollection.Destroy(id);
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
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(DuvidaFrequenteCollection.All, Formatting.Indented));
        }
    }
}