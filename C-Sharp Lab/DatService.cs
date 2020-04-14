using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace C_Sharp_Lab
{
    class DatService : IToyListService
    {
        public void Save(List<Toy> toys, string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(file, toys);
            }
        }

        public List<Toy> Load(string fileName)
        {
            List<Toy> result = new List<Toy>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                result = (List<Toy>)formatter.Deserialize(file);
            }
            return result;
        }
    }
}
