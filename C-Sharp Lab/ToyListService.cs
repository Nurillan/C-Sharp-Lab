using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Lab
{
    public interface IToyListService
    {
        List<Toy> Load(string fileName);
        void Save(List<Toy> toys, string fileName);
    }

    public static class ServiceFactory
    {        
        public static IToyListService getService(string ex)
        {   
            switch (ex)
            {
                case ".txt":
                    return new TxtService();

                case ".xml":
                    return new XmlService();

                case ".dat":
                    return new DatService();

                default: throw new Exception("Unknown file extension");
            }        
        } 
    }    
}
