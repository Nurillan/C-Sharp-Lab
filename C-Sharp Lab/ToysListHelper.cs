using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Lab
{
    [Serializable]
    public class ToysListHelper
    {
        public List<Toy> toys;

        public ToysListHelper()
        {
            toys = new List<Toy>();
        }

        public ToysListHelper(List<Toy> toys)
        {
            this.toys = toys;
        }

        public List<Toy> selectMinAgeMaxPrice(byte minAge, double maxPrice)
        {
            List<Toy> resultToys = new List<Toy>();

            foreach (Toy toy in toys)
            {
                if ((toy.Age >= minAge) && (toy.Price <= maxPrice))
                    resultToys.Add(toy);                
            }
            resultToys.Sort();

            return resultToys;
        }

        public List<Toy> ListDif(List<Toy> otherToys)
        {
            List<Toy> result = new List<Toy>();
            foreach(Toy toy in toys)
            {
                if (otherToys.IndexOf(toy) == -1)
                    result.Add(toy);
            }

            foreach (Toy toy in otherToys)
            {
                if (toys.IndexOf(toy) == -1)
                    result.Add(toy);
            }
            return result;
        }
    }
}
