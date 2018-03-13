using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data
{
    public class CriancaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile CriancaFile { get; set; }
        private Collection<Crianca, int> CriancaCollection { get; set; }

        public CriancaDatabaseController()
        {
            CriancaFile = session["criancas.dat"];
            CriancaCollection = CriancaFile.Collection<Crianca, int>("criancas", c => c.crianca_id);
        }

        public void SaveCrianca(Crianca c)
        {
            CriancaCollection.Persist(c);
        }

        public Crianca FindCrianca(int id)
        {
            return CriancaCollection.Find(id);
        }

        public void DeleteCrianca(int id)
        {
            CriancaCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(CriancaCollection.All, Formatting.Indented));
        }
    }
}