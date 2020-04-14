using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Lab
{
    [Serializable]
    public enum ToyTypes
    {
        Ball = 1,
        Car,
        Cubes,
        Doll,
        Lego,
        SoftToy,
        Soldiers,
    }

    public static class ToyTypesConverter
    {
        public static Dictionary<int, ToyTypes> ToyTypesDict = new Dictionary<int, ToyTypes>();

        public static void InitDict()
        {
            foreach (ToyTypes make in Enum.GetValues(typeof(ToyTypes)))
            {
                ToyTypesDict.Add((int)(make), make);
            }
        }
    }
}
