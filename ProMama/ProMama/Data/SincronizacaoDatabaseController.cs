using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data
{
    public class SincronizacaoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile SincronizacaoFile { get; set; }
        private Collection<Sincronizacao, int> SincronizacaoCollection { get; set; }

        public SincronizacaoDatabaseController()
        {
            SincronizacaoFile = session["sincronizacoes.dat"];
            SincronizacaoCollection = SincronizacaoFile.Collection<Sincronizacao, int>("sincronizacoes", s => s.id);
        }

        public void SaveSincronizacao(Sincronizacao s)
        {
            SincronizacaoCollection.Persist(s);
        }

        public Sincronizacao FindSincronizacao()
        {
            return SincronizacaoCollection.Find(1);
        }

        public void DeleteSincronizacao()
        {
            SincronizacaoCollection.Destroy(1);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(SincronizacaoCollection.All, Formatting.Indented));
        }
    }
}