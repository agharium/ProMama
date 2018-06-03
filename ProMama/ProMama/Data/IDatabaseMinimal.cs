namespace ProMama.Data
{
    public interface IDatabaseMinimal<T>
    {
        void Save(T obj);

        T Find(int id);

        void Delete(int id);

        void DumpTable();
    }
}
