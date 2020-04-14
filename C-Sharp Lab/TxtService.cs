using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Lab
{

    class TxtService : IToyListService
    {
        private static string NumberFromString(string substring)
        {
            int i = 0;
            int len = substring.Length;
            string res = "";
            while ((i < len) && ((substring[i] > '9') || (substring[i] < '0')))
                i++;
            while ((i < len) && (substring[i] <= '9') && (substring[i] >= '0'))
            {
                res += substring[i];
                i++;
            }
            return (res == "") ? null : res;
        }

        private static Toy ToyFromString(string str) //string like "<type>, *<Age>*, *<Price>*"
        {
            string sType = str.Substring(0, str.IndexOf(',')).Trim();
            ToyTypes type = (ToyTypes)Enum.Parse(typeof(ToyTypes), sType);

            string temp = str.Substring(str.IndexOf(','), str.LastIndexOf(',')).Trim();
            string sAge = NumberFromString(temp);
            byte age = byte.Parse(sAge);

            temp = str.Substring(str.LastIndexOf(',')).Trim();
            string sPrice = NumberFromString(temp);
            double price = double.Parse(sPrice);

            return new Toy(type, age, price);
        }

        public void Save(List<Toy> toys, string fileName)
        {
            string text = "";
            foreach (Toy toy in toys)
                text += toy.ToString() + Environment.NewLine;

            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.Write(text);
            }
        }

        public List<Toy> Load(string fileName)
        {
            List<Toy> result = new List<Toy>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    result.Add(ToyFromString(str));
                }
            }
            return result;
        }
    }
}
