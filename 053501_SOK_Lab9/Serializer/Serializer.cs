using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;
using _053501_SOK_Lab9.Domain;

namespace Serializer
{
    public class Serializer : ISerializer
    {
        public IEnumerable<Factory> DeSerializeByLINQ(string fileName)
        {
            List<Factory> result = new();
            XDocument document = XDocument.Load(fileName);
            foreach (XElement factoryElement in document.Element("Factories").Elements("Factory"))
            {
                Factory factory = new();
                factory.Name = factoryElement.Element("Name").Value;
                factory.Description = factoryElement.Element("Description").Value;
                factory.Address = factoryElement.Element("Address").Value;
                XElement storageElement = factoryElement.Element("Storage");
                Storage storage = new();
                storage.Address = storageElement.Element("Address").Value;
                storage.Capasity = int.Parse(storageElement.Element("Capasity").Value);
                foreach(XElement detailElement in storageElement.Element("Details").Elements("Detail"))
                {
                    Detail detail = new();
                    detail.Description = detailElement.Element("Description").Value;
                    detail.Weight = double.Parse(detailElement.Element("Weight").Value.Replace(".", ","));                  
                    detail.ID = uint.Parse(detailElement.Element("id").Value);
                    storage.AddDetail(detail);
                }
                factory.Storage = storage;
                result.Add(factory);
            }
            return result;
        }

        public IEnumerable<Factory> DeSerializeJSON(string fileName)
        {
            return JsonSerializer.Deserialize<IEnumerable<Factory>>(File.ReadAllText(fileName));
        }

        public IEnumerable<Factory> DeSerializeXML(string fileName)
        {
            List<Factory> result = new();
            XmlDocument document = new();
            document.Load(fileName);
            XmlElement root = document.DocumentElement;
            if (root != null)
            {
                foreach (XmlElement factoryElement in root)
                {
                    Factory factory = new();
                    foreach (XmlElement factoryChild in factoryElement)
                    {
                        switch (factoryChild.Name)
                        {
                            case "Name":
                                factory.Name = factoryChild.InnerText;
                                break;
                            case "Description":
                                factory.Description = factoryChild.InnerText;
                                break;
                            case "Address":
                                factory.Address = factoryChild.InnerText;
                                break;
                            case "Storage":
                                Storage storage = new();
                                foreach (XmlElement storageChild in factoryChild)
                                {
                                    switch (storageChild.Name)
                                    {
                                        case "Address":
                                            storage.Address = storageChild.InnerText;
                                            break;
                                        case "Capasity":
                                            storage.Capasity = int.Parse(storageChild.InnerText);
                                            break;
                                        case "Details":
                                            Detail detail = new();
                                            foreach(XmlElement DetailChild in storageChild)
                                            {
                                                switch (DetailChild.Name)
                                                {
                                                    case "Description":
                                                        detail.Description = DetailChild.InnerText;
                                                        break;
                                                    case "Weight":
                                                        detail.Weight = double.Parse(DetailChild.InnerText);
                                                        break;
                                                    case "id":
                                                        detail.ID = uint.Parse(DetailChild.InnerText);
                                                        break;
                                                }
                                            }
                                            storage.AddDetail(detail);
                                            break;
                                    }
                                }
                                factory.Storage = storage;
                                break;
                        }
                    }
                    result.Add(factory);
                }
            }
            return result;
        }

        public void SerializeByLINQ(IEnumerable<Factory> factories, string fileName)
        {
            XDocument document = new();
            XElement factoriesElement = new("Factories");
            foreach(var factory in factories)
            {
                XElement factoryElement = new("Factory");
                factoryElement.Add(new XElement("Name", factory.Name));
                factoryElement.Add(new XElement("Description", factory.Description));
                factoryElement.Add(new XElement("Address", factory.Address));
                XElement storageElement = new("Storage");
                storageElement.Add(new XElement("Address", factory.Storage.Address));
                storageElement.Add(new XElement("Capasity", factory.Storage.Capasity));
                XElement detailsElement = new("Details");
                foreach(var detail in factory.Storage.Details)
                {
                    XElement detailElement = new("Detail");
                    detailElement.Add(new XElement("Description", detail.Description));
                    detailElement.Add(new XElement("Weight", detail.Weight));
                    detailElement.Add(new XElement("id", detail.ID));
                    detailsElement.Add(detailElement);
                }
                storageElement.Add(detailsElement);
                factoryElement.Add(storageElement);
                factoriesElement.Add(factoryElement);
            }
            document.Add(factoriesElement);
            document.Save(fileName);
        }

        public void SerializeJSON(IEnumerable<Factory> factories, string fileName)
        {
            File.WriteAllText(fileName, JsonSerializer.Serialize<IEnumerable<Factory>>(factories));
        }

        private XmlElement CreateElementWithNode(ref XmlDocument document, string name, string node)
        {
            XmlElement element = document.CreateElement(name);
            XmlText text = document.CreateTextNode(node);
            element.AppendChild(text);
            return element;
        }

        public void SerializeXML(IEnumerable<Factory> factories, string fileName)
        {
            XmlDocument document = new();
            XmlElement factoriesElement = document.CreateElement("Factories");
            foreach (var factory in factories)
            {
                XmlElement factoryElement = document.CreateElement("Factory");
                factoryElement.AppendChild(CreateElementWithNode(ref document, "Name", factory.Name));
                factoryElement.AppendChild(CreateElementWithNode(ref document, "Description", factory.Description));
                factoryElement.AppendChild(CreateElementWithNode(ref document, "Address", factory.Address));
                XmlElement storageElement = document.CreateElement("Storage");
                storageElement.AppendChild(CreateElementWithNode(ref document, "Address", factory.Storage.Address));
                storageElement.AppendChild(CreateElementWithNode(ref document, "Capasity", factory.Storage.Capasity.ToString()));    
                XmlElement detailsElement = document.CreateElement("Details");
                foreach (var detail in factory.Storage.Details)
                {
                    XmlElement detailElement = document.CreateElement("Detail");
                    detailElement.AppendChild(CreateElementWithNode(ref document, "Description", detail.Description));
                    detailElement.AppendChild(CreateElementWithNode(ref document, "Weight", detail.Weight.ToString()));
                    detailElement.AppendChild(CreateElementWithNode(ref document, "id", detail.ID.ToString()));
                    detailsElement.AppendChild(detailElement);
                }
                storageElement.AppendChild(detailsElement);
                factoryElement.AppendChild(storageElement);
                factoriesElement.AppendChild(factoryElement);
            }
            document.AppendChild(factoriesElement);
            document.Save(fileName);
        }
    }
}
