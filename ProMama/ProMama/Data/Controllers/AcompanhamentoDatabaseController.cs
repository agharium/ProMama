using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data.Controllers
{
    public class AcompanhamentoDatabaseController : IDatabaseExtended<Acompanhamento>
    {
        Session session = App.DB;

        private CollectionFile AcompanhamentoFile { get; set; }
        private Collection<Acompanhamento, int> AcompanhamentoCollection { get; set; }

        public AcompanhamentoDatabaseController()
        {
            AcompanhamentoFile = session["acompanhamentos.dat"];
            AcompanhamentoCollection = AcompanhamentoFile.Collection<Acompanhamento, int>("acompanhamentos", obj => obj.id);
        }

        public void Save(Acompanhamento obj)
        {
            AcompanhamentoCollection.Persist(obj);
        }

        public void SaveIncrementing(Acompanhamento obj)
        {
            obj.id = GetAll().Count() + 1;
            Save(obj);
        }

        public void SaveList(List<Acompanhamento> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Acompanhamento Find(int id)
        {
            return AcompanhamentoCollection.Find(id);
        }

        public List<Acompanhamento> FindByChildId(int id)
        {
            var retorno = new List<Acompanhamento>();
            foreach (var obj in GetAll())
            {
                if (obj.crianca == id) {
                    switch (obj.alimentacao)
                    {
                        case 0:
                            obj.alimentacao_texto = "Exclusivo";
                            break;
                        case 1:
                            obj.alimentacao_texto = "Predominante";
                            break;
                        case 2:
                            obj.alimentacao_texto = "Complementar";
                            break;
                        case 3:
                            obj.alimentacao_texto = "Não recebe leite materno";
                            break;
                    }
                    retorno.Add(obj);
                }
            }
            return retorno;
        }

        public List<Acompanhamento> GetAll()
        {
            return AcompanhamentoCollection.All.ToList();
        }

        public void Delete(int id)
        {
            AcompanhamentoCollection.Destroy(id);
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
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(AcompanhamentoCollection.All, Formatting.Indented));
        }
    }
}