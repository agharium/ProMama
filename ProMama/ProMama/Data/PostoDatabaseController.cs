using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data
{
    public class PostoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile PostoFile { get; set; }
        private Collection<Posto, int> PostoCollection { get; set; }

        public PostoDatabaseController()
        {
            PostoFile = session["postos.dat"];
            PostoCollection = PostoFile.Collection<Posto, int>("postos", p => p.posto_id);
        }

        public void SavePosto(Posto p)
        {
            PostoCollection.Persist(p);
        }

        public Posto FindPosto(int id)
        {
            return PostoCollection.Find(id);
        }

        public void DeletePosto(int id)
        {
            PostoCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(PostoCollection.All, Formatting.Indented));
        }
    }
}