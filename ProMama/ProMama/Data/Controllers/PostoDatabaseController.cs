using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data.Controllers
{
    public class PostoDatabaseController : IDatabaseExtended<Posto>
    {
        Session session = App.DB;

        private CollectionFile PostoFile { get; set; }
        private Collection<Posto, int> PostoCollection { get; set; }

        public PostoDatabaseController()
        {
            PostoFile = session["postos.dat"];
            PostoCollection = PostoFile.Collection<Posto, int>("postos", obj => obj.posto_id);
        }

        public void Save(Posto obj)
        {
            PostoCollection.Persist(obj);
        }

        public void SaveList(List<Posto> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Posto Find(int id)
        {
            return PostoCollection.Find(id);
        }

        public List<Posto> GetAll()
        {
            return PostoCollection.All.ToList();
        }

        public void Delete(int id)
        {
            PostoCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var obj in GetAll())
            {
                Delete(obj.posto_id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(PostoCollection.All, Formatting.Indented));
        }
    }
}