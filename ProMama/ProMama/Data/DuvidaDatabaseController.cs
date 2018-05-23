using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;

namespace ProMama.Data
{
    public class DuvidaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile DuvidaFile { get; set; }
        private Collection<Duvida, int> DuvidaCollection { get; set; }

        public DuvidaDatabaseController()
        {
            DuvidaFile = session["duvidas.dat"];
            DuvidaCollection = DuvidaFile.Collection<Duvida, int>("duvidas", d => d.duvida_id);
        }

        public void SaveDuvida(Duvida d)
        {
            DuvidaCollection.Persist(d);
        }

        public void SaveDuvidaList(List<Duvida> duvidas)
        {
            foreach (var d in duvidas)
            {
                SaveDuvida(d);
            }
        }

        public Duvida FindDuvida(int id)
        {
            return DuvidaCollection.Find(id);
        }

        public void DeleteDuvida(int id)
        {
            DuvidaCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(DuvidaCollection.All, Formatting.Indented));
        }
    }
}
