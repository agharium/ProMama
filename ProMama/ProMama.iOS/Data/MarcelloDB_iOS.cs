using MarcelloDB.Platform;
using ProMama.Data;
using ProMama.iOS.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(MarcelloDB_iOS))]
namespace ProMama.iOS.Data
{
    class MarcelloDB_iOS : IMarcelloDB
    {
        public MarcelloDB_iOS() { }

        public MarcelloDB.Session GetSession()
        {
            IPlatform platform = new MarcelloDB.netfx.Platform();
            var dataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return new MarcelloDB.Session(platform, dataPath);
        }
    }
}