using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ConversionService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Conversion : IConversionService
    {
        public Temperature FahrenheitToCelsius(Temperature fahrenheit)
        {
            if (fahrenheit == null) throw new ArgumentNullException(nameof(fahrenheit));
            return new Temperature { Value = 5 / 9.0 * (fahrenheit.Value - 32) };
        }

        public Temperature CelsiusToFahrenheit(Temperature celsius)
        {
            return new Temperature { Value = 9 / 5.0 * celsius.Value + 32 };
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
