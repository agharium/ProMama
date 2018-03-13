using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data
{
    public class UsuarioDatabaseController
    {
        Session session = App.DB;

        private CollectionFile UsuarioFile { get; set; }
        private Collection<Usuario, int> UsuarioCollection { get; set; }

        public UsuarioDatabaseController()
        {
            UsuarioFile = session["usuarios.dat"];
            UsuarioCollection = UsuarioFile.Collection<Usuario, int>("usuarios", u => u.usuario_id);
        }

        public void SaveUsuario(Usuario u)
        {
            UsuarioCollection.Persist(u);
        }

        public Usuario FindUsuario(int id)
        {
            return UsuarioCollection.Find(id);
        }

        public void DeleteUsuario(int id)
        {
            UsuarioCollection.Destroy(id);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(UsuarioCollection.All, Formatting.Indented));
        }
    }
}
