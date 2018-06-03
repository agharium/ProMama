using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data.Controllers
{
    public class BairroDatabaseController : IDatabaseExtended<Bairro>
    {
        Session session = App.DB;

        private CollectionFile BairroFile { get; set; }
        private Collection<Bairro, int> BairroCollection { get; set; }

        public BairroDatabaseController()
        {
            BairroFile = session["bairros.dat"];
            BairroCollection = BairroFile.Collection<Bairro, int>("bairros", obj => obj.bairro_id);
        }

        public void Save(Bairro obj)
        {
            BairroCollection.Persist(obj);
        }

        public void SaveList(List<Bairro> list)
        {
            foreach (var b in list)
            {
                Save(b);
            }
        }

        public Bairro Find(int id)
        {
            return BairroCollection.Find(id);
        }

        public List<Bairro> GetAll()
        {
            return BairroCollection.All.ToList();
        }

        public void Delete(int id)
        {
            BairroCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var obj in GetAll())
            {
                Delete(obj.bairro_id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(BairroCollection.All, Formatting.Indented));
        }
    }
}