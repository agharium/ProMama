using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data
{
    public class BairroDatabaseController
    {
        Session session = App.DB;

        private CollectionFile BairroFile { get; set; }
        private Collection<Bairro, int> BairroCollection { get; set; }

        public BairroDatabaseController()
        {
            BairroFile = session["bairros.dat"];
            BairroCollection = BairroFile.Collection<Bairro, int>("bairros", b => b.bairro_id);
        }

        public void SaveBairro(Bairro b)
        {
            BairroCollection.Persist(b);
        }

        public void SaveBairroList(List<Bairro> bairro)
        {
            foreach (var b in bairro)
            {
                SaveBairro(b);
            }
        }

        public Bairro FindBairro(int id)
        {
            return BairroCollection.Find(id);
        }

        public List<Bairro> GetAllBairro()
        {
            return BairroCollection.All.ToList();
        }

        public void DeleteBairro(int id)
        {
            BairroCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(BairroCollection.All, Formatting.Indented));
        }
    }
}