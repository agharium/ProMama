using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProMama.Database.Controllers
{
    public class UsuarioDatabaseController
    {
        Session session = App.DB;

        private CollectionFile UsuarioFile { get; set; }
        private Collection<Usuario, int> UsuarioCollection { get; set; }

        public UsuarioDatabaseController()
        {
            UsuarioFile = session["usuarios.dat"];
            UsuarioCollection = UsuarioFile.Collection<Usuario, int>("usuarios", obj => obj.id);
        }

        public void Save(Usuario obj)
        {
            UsuarioCollection.Persist(obj);
        }

        public Usuario Find(int id)
        {
            return UsuarioCollection.Find(id);
        }

        public List<Usuario> GetAll()
        {
            return UsuarioCollection.All.ToList();
        }

        public void Delete(int id)
        {
            UsuarioCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(UsuarioCollection.All, Formatting.Indented));
        }
    }
}
