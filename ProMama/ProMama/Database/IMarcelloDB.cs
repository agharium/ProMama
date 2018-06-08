using MarcelloDB;

namespace ProMama.Database
{
    public interface IMarcelloDB
    {
        Session GetSession();
    }
}
