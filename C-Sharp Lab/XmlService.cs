using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace C_Sharp_Lab
{
    class XmlService : IToyListService
    {
        public void Save(List<Toy> toys, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(file, toys);
            }
        }

        public List<Toy> Load(string fileName)
        {
            List<Toy> result = new List<Toy>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                result = (List<Toy>)serializer.Deserialize(file);
            }
            return result;
        }
    }
}
