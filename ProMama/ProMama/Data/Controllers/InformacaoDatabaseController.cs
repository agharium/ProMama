using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data.Controllers
{
    public class InformacaoDatabaseController : IDatabaseExtended<Informacao>
    {
        Session session = App.DB;

        private CollectionFile InformacaoFile { get; set; }
        private Collection<Informacao, int> InformacaoCollection { get; set; }

        public InformacaoDatabaseController()
        {
            InformacaoFile = session["informacoes.dat"];
            InformacaoCollection = InformacaoFile.Collection<Informacao, int>("informacoes", obj => obj.informacao_id);
        }

        public void Save(Informacao obj)
        {
            InformacaoCollection.Persist(obj);
        }

        public void SaveList(List<Informacao> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Informacao Find(int id)
        {
            return InformacaoCollection.Find(id);
        }

        public List<Informacao> GetAll()
        {
            return InformacaoCollection.All.ToList();
        }

        public void Delete(int id)
        {
            InformacaoCollection.Destroy(id);
        }

        public void WipeTable()
        {
            foreach (var obj in GetAll())
            {
                Delete(obj.informacao_id);
            }
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(InformacaoCollection.All, Formatting.Indented));
        }
    }
}
