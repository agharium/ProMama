using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class CriancaDatabaseController
    {
        Session session = App.DB;

        private CollectionFile CriancaFile { get; set; }
        private Collection<Crianca, int> CriancaCollection { get; set; }

        public CriancaDatabaseController()
        {
            CriancaFile = session["criancas.dat"];
            CriancaCollection = CriancaFile.Collection<Crianca, int>("criancas", obj => obj.crianca_id);
        }

        public void Save(Crianca obj)
        {
            CriancaCollection.Persist(obj);
        }

        public void SaveList(List<Crianca> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Crianca Find(int id)
        {
            return CriancaCollection.Find(id);
        }

        public List<Crianca> GetAll()
        {
            return CriancaCollection.All.ToList();
        }

        public List<Crianca> GetCriancasByUser(int id)
        {
            var retorno = new List<Crianca>();
            foreach (var obj in GetAll())
            {
                if (obj.user_id == id)
                    retorno.Add(obj);
            }
            return retorno;
        }

        public void Delete(int id)
        {
            CriancaCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(CriancaCollection.All, Formatting.Indented));
        }
    }
}