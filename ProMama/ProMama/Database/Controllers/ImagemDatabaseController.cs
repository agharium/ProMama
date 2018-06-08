using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;

namespace ProMama.Database.Controllers
{
    public class ImagemDatabaseController
    {
        Session session = App.DB;

        private CollectionFile ImagemFile { get; set; }
        private Collection<Imagem, int> ImagemCollection { get; set; }

        public ImagemDatabaseController()
        {
            ImagemFile = session["imagens.dat"];
            ImagemCollection = ImagemFile.Collection<Imagem, int>("imagens", obj => obj.id);
        }

        public void Save(Imagem obj)
        {
            ImagemCollection.Persist(obj);
        }

        public Imagem Find(int id)
        {
            return ImagemCollection.Find(id);
        }

        public void Delete(int id)
        {
            ImagemCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ImagemCollection.All, Formatting.Indented));
        }
    }
}
