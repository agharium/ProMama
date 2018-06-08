using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Models;

namespace ProMama.Database.Controllers
{
    public class ConfigDatabaseController
    {
        Session session = App.DB;

        private CollectionFile ConfigFile { get; set; }
        private Collection<Config, int> ConfigCollection { get; set; }

        public ConfigDatabaseController()
        {
            ConfigFile = session["configs.dat"];
            ConfigCollection = ConfigFile.Collection<Config, int>("configs", obj => obj.config_id);
        }

        public void Save(Config obj)
        {
            ConfigCollection.Persist(obj);
        }

        public Config Find()
        {
            return ConfigCollection.Find(1);
        }

        public void Delete()
        {
            ConfigCollection.Destroy(1);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ConfigCollection.All, Formatting.Indented));
        }
    }
}