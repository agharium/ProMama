using MarcelloDB;
using MarcelloDB.Platform;
using ProMama.Database;
using ProMama.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MarcelloDB_Android))]
namespace ProMama.Droid.Services
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