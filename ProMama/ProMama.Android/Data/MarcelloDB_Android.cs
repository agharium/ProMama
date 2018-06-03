using MarcelloDB;
using MarcelloDB.Platform;
using ProMama.Data;
using ProMama.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(MarcelloDB_Android))]
namespace ProMama.Droid.Data
{
    class MarcelloDB_Android : IMarcelloDB
    {
        public MarcelloDB_Android() { }

        public Session GetSession()
        {
            IPlatform platform = new MarcelloDB.netfx.Platform();
            var dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return new Session(platform, dataPath);
        }
    }
}