using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConversionConsumer.ConversionReference;

namespace ConversionConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // use "Add Service Reference..." to do something like database-first scaffolding.

            using (var client = new ConversionServiceClient())
            {
                var input = new Temperature { Value = 100 };
                Temperature output = client.FahrenheitToCelsius(null);
                Console.WriteLine($"Service returned: {output.Value}");
            }
        }
    }
}
