using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Data.Controllers
{
    public class FotoDatabaseController : IDatabaseExtended<Foto>
    {
        Session session = App.DB;

        private CollectionFile FotoFile { get; set; }
        private Collection<Foto, int> FotoCollection { get; set; }

        public FotoDatabaseController()
        {
            FotoFile = session["fotos.dat"];
            FotoCollection = FotoFile.Collection<Foto, int>("fotos", obj => obj.id);
        }

        public void Save(Foto obj)
        {
            FotoCollection.Persist(obj);
        }

        public void SaveList(List<Foto> list)
        {
            foreach (var obj in list)
            {
                Save(obj);
            }
        }

        public Foto Find(int id)
        {
            return FotoCollection.Find(id);
        }

        public List<Foto> FindByChildId(int id)
        {
            var retorno = new List<Foto>();
            foreach (var obj in GetAll())
            {
                if (obj.crianca == id)
                    retorno.Add(obj);
            }
            return retorno;
        }

        public List<Foto> GetAll()
        {
            return FotoCollection.All.ToList();
        }

        public void Delete(int id)
        {
            FotoCollection.Destroy(id);
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
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(FotoCollection.All, Formatting.Indented));
        }
    }
}