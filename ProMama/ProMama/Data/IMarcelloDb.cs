namespace ProMama.Data
{
    public interface IMarcelloDB
    {
        MarcelloDB.Session GetSession();
    }
}
