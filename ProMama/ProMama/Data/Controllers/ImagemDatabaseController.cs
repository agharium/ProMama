using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data.Controllers
{
    public class ImagemDatabaseController : IDatabaseMinimal<Imagem>
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
