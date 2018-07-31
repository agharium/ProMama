using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;

namespace ProMama.Database.Controllers
{
    public class ExcluirDatabaseController
    {
        Session session = App.DB;

        private CollectionFile ExcluirFile { get; set; }
        private Collection<Excluir, int> ExcluirCollection { get; set; }

        public ExcluirDatabaseController()
        {
            ExcluirFile = session["exclusoes.dat"];
            ExcluirCollection = ExcluirFile.Collection<Excluir, int>("exclusoes", obj => obj.Id);
        }

        public void Save(Excluir obj)
        {
            ExcluirCollection.Persist(obj);
        }

        public Excluir Find()
        {
            return ExcluirCollection.Find(1);
        }

        public void DeleteExcluir()
        {
            ExcluirCollection.Destroy(1);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ExcluirCollection.All, Formatting.Indented));
        }
    }
}
