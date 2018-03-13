using MarcelloDB;
using MarcelloDB.Collections;
using Newtonsoft.Json;
using ProMama.Model;

namespace ProMama.Data
{
    public class ConfigDatabaseController
    {
        Session session = App.DB;

        private CollectionFile ConfigFile { get; set; }
        private Collection<Config, int> ConfigCollection { get; set; }

        public ConfigDatabaseController()
        {
            ConfigFile = session["configs.dat"];
            ConfigCollection = ConfigFile.Collection<Config, int>("configs", cfg => cfg.config_id);
        }

        public void SaveConfig(Config cfg)
        {
            ConfigCollection.Persist(cfg);
        }

        public Config FindConfig()
        {
            return ConfigCollection.Find(1);
        }

        public void DeleteConfig()
        {
            ConfigCollection.Destroy(1);
        }

        public void DumpTable()
        {
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ConfigCollection.All, Formatting.Indented));
        }
    }
}