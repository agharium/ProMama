using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

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

        public Bairro FindBairro(int id)
        {
            return BairroCollection.Find(id);
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