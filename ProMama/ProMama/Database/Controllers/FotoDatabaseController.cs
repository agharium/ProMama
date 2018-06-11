using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ProMama.Database.Controllers
{
    public class FotoDatabaseController
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

        public void SaveIncrementing(Foto obj)
        {
            obj.id = GetAll().Count() + 1;
            Save(obj);
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

        public ImageSource GetMostRecent()
        {
            var list = GetAll();
            var mesMaisRecente = -1;
            Foto maisRecente = null;

            if (list.Count() == 0)
            {
                return "avatar_default.png";
            } else
            {
                foreach (var f in list)
                {
                    if (f.mes > mesMaisRecente)
                    {
                        mesMaisRecente = f.mes;
                        maisRecente = f;
                    }
                }
                return maisRecente.caminho;
            }
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