using System.Collections.Generic;

namespace ProMama.Data
{
    public interface IDatabaseExtended<T>
    {
        void Save(T obj);

        void SaveList(List<T> list);

        T Find(int id);

        List<T> GetAll();

        void Delete(int id);

        void WipeTable();

        void DumpTable();
    }
}
