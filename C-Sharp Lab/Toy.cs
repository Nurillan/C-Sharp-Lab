using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Lab
{

    [Serializable]
    public class Toy : IComparable<Toy>
    {

        public ToyTypes Type { get; set; }

        private byte age; 
        public byte Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value >= 0 && value <= 100)
                    age = value;
                else throw new Exception("Invalid types of parametr");
            }
        }

        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value >= 0)
                    price = value;
                else throw new Exception("Invalid types of parametr");
            }
        }

        public Toy()
        {
            Type = ToyTypes.Ball;
            Age = 0;
            Price = 0;
        }

        public Toy(ToyTypes type, byte age, double price)
        {
            Type = type;
            Age = age;
            Price = price;
        }

        public void Change(ToyTypes type, byte age, double price)
        {
            this.Type = type;
            this.Age = age;
            this.Price = price;
        }

        public override string ToString()
        {
            string result = Type.ToString() + $", Min age {Age}, Price — {Price}.";
            return result;
        }

        public int CompareTo(Toy toy)
        {
            int result = this.Type.CompareTo(toy.Type);
            if (result == 0)
                result = this.Price.CompareTo(toy.Price);
            if (result == 0)
                result = this.Age.CompareTo(toy.Age);

            return result;
        }

    }
}
