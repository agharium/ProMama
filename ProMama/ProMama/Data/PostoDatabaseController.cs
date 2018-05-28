using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

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

        public void SavePostoList(List<Posto> postos)
        {
            foreach (var p in postos)
            {
                SavePosto(p);
            }
        }

        public Posto FindPosto(int id)
        {
            return PostoCollection.Find(id);
        }

        public List<Posto> GetAllPosto()
        {
            return PostoCollection.All.ToList();
        }

        public void DeletePosto(int id)
        {
            PostoCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var p in GetAllPosto())
            {
                DeletePosto(p.posto_id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(PostoCollection.All, Formatting.Indented));
        }
    }
}