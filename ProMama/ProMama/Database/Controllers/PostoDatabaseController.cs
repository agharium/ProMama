using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class PostoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile PostoFile { get; set; }
        private Collection<Posto, int> PostoCollection { get; set; }

        public PostoDatabaseController()
        {
            PostoFile = session["postos.dat"];
            PostoCollection = PostoFile.Collection<Posto, int>("postos", obj => obj.id);
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
            var postos = PostoCollection.All.OrderBy(obj => obj.nome).ToList();

            if (postos.Count() > 0)
            {
                int outroIndex = -1;

                foreach (var obj in postos)
                {
                    if (obj.nome.Equals("Outro"))
                        outroIndex = postos.IndexOf(obj);
                }
                
                if (outroIndex != -1)
                {
                    var outroObj = postos[outroIndex];

                    postos.RemoveAt(outroIndex);
                    postos.Insert(postos.Count(), outroObj);
                }
            }

            return postos;
        }

        public void Delete(int id)
        {
            PostoCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var obj in GetAll())
            {
                Delete(obj.id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(PostoCollection.All, Formatting.Indented));
        }
    }
}