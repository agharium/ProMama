using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class BairroDatabaseController
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
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Bairro Find(int id)
        {
            return BairroCollection.Find(id);
        }

        public List<Bairro> GetAll()
        {
            var bairros = BairroCollection.All.OrderBy(obj => obj.bairro_nome).ToList();

            if (bairros.Count() > 0)
            {
                var outroIndex = bairros.FindIndex(obj => obj.bairro_nome.Equals("Não moro em Osório"));
                if (outroIndex != -1)
                {
                    var outroObj = bairros[outroIndex];

                    bairros.RemoveAt(outroIndex);
                    bairros.Insert(bairros.Count(), outroObj);
                }
            }

            return bairros;
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