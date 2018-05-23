using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data
{
    public class InformacaoDatabaseController
    {
        Session session = App.DB;

        private CollectionFile InformacaoFile { get; set; }
        private Collection<Informacao, int> InformacaoCollection { get; set; }

        public InformacaoDatabaseController()
        {
            InformacaoFile = session["informacoes.dat"];
            InformacaoCollection = InformacaoFile.Collection<Informacao, int>("informacoes", i => i.informacao_id);
        }

        public void SaveInformacao(Informacao i)
        {
            InformacaoCollection.Persist(i);
        }

        public void SaveInformacaoList(List<Informacao> informacoes)
        {
            foreach (var i in informacoes)
            {
                SaveInformacao(i);
            }
        }

        public Informacao FindInformacao(int id)
        {
            return InformacaoCollection.Find(id);
        }

        public List<Informacao> GetAllInformacao()
        {
            return InformacaoCollection.All.ToList();
        }

        public void DeleteInformacao(int id)
        {
            InformacaoCollection.Destroy(id);
        }


        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(InformacaoCollection.All, Formatting.Indented));
        }
    }
}
