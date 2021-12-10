using System.Collections.Generic;

namespace _053501_SOK_Lab9.Domain
{
    public interface ISerializer
    {
        IEnumerable<Factory> DeSerializeByLINQ(string fileName);
        IEnumerable<Factory> DeSerializeXML(string fileName);
        IEnumerable<Factory> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Factory> xxx, string fileName);
        void SerializeXML(IEnumerable<Factory> xxx, string fileName);
        void SerializeJSON(IEnumerable<Factory> xxx, string fileName);
    }
}
