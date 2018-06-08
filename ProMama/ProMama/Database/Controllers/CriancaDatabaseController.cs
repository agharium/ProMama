using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;

namespace ProMama.Database.Controllers
{
    public class CriancaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile CriancaFile { get; set; }
        private Collection<Crianca, int> CriancaCollection { get; set; }

        public CriancaDatabaseController()
        {
            CriancaFile = session["criancas.dat"];
            CriancaCollection = CriancaFile.Collection<Crianca, int>("criancas", obj => obj.crianca_id);
        }

        public void Save(Crianca obj)
        {
            CriancaCollection.Persist(obj);
        }

        public Crianca Find(int id)
        {
            return CriancaCollection.Find(id);
        }

        public void Delete(int id)
        {
            CriancaCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(CriancaCollection.All, Formatting.Indented));
        }
    }
}