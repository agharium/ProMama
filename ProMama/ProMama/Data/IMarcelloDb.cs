using MarcelloDB;

namespace ProMama.Data
{
    public interface IMarcelloDB
    {
        Session GetSession();
    }
}
