using System;
using System.Collections.Generic;
using _053501_SOK_Lab9.Domain;

namespace _053501_SOK_Lab9
{
    class Program
    {
        static void Main()
        {
            Factory factory1 = new("LDN CORPORATION", "This is the best company of all time","Address is classified", new(50, "Address is classified"));
            factory1.Storage.AddDetail(new("Head",8));
            factory1.Storage.AddDetail(new("Left hand", 4));
            factory1.Storage.AddDetail(new("Right hand", 4));
            factory1.Storage.AddDetail(new("Body", 30));
            factory1.Storage.AddDetail(new("Left leg", 12));
            factory1.Storage.AddDetail(new("Right leg", 12));
            Console.WriteLine("First factory is created");
            Factory factory2 = new("Apple", "Think different", "Address is classified", new(500, "California"));
            factory2.Storage.AddDetail(new("Frame", 0.02));
            factory2.Storage.AddDetail(new("Glass", 0.02));
            factory2.Storage.AddDetail(new("Internal components", 0.15));
            Console.WriteLine("Second factory is created");
            Serializer.Serializer serializer = new();
            List<Factory> factories = new();
            factories.Add(factory1);
            factories.Add(factory2);
            Console.WriteLine("Factories are placed to the lest");
            serializer.SerializeXML(factories, "Factories.xml");
            Console.WriteLine("Factories are serialized in Xml");
            serializer.SerializeXML(serializer.DeSerializeXML("Factories.xml"), "TestOfXmlSerialization.xml");
            Console.WriteLine("Factories are deserialized from Xml and serialised to another Xml");
            serializer.SerializeByLINQ(factories, "FactoriesLINQ.xml");
            Console.WriteLine("Factories are serialized in Xml by LINQ");
            serializer.SerializeByLINQ(serializer.DeSerializeByLINQ("FactoriesLINQ.xml"), "TestOfSerializationByLINQ.xml");
            Console.WriteLine("Factories are deserialized from Xml by LINQ and serialised to another Xml by LINQ");
            serializer.SerializeJSON(factories, "Factories.json");
            Console.WriteLine("Factories are serialized in json");
            serializer.SerializeJSON(serializer.DeSerializeJSON("Factories.json"), "TestOfJSONSerialization.json");
            Console.WriteLine("Factories are deserialized from Json and serialised to another Json");
            Console.ReadLine();
        }
    }
}
