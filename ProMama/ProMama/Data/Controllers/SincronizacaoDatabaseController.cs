using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data.Controllers
{
    public class SincronizacaoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile SincronizacaoFile { get; set; }
        private Collection<Sincronizacao, int> SincronizacaoCollection { get; set; }

        public SincronizacaoDatabaseController()
        {
            SincronizacaoFile = session["sincronizacoes.dat"];
            SincronizacaoCollection = SincronizacaoFile.Collection<Sincronizacao, int>("sincronizacoes", obj => obj.id);
        }

        public void Save(Sincronizacao obj)
        {
            SincronizacaoCollection.Persist(obj);
        }

        public Sincronizacao Find()
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