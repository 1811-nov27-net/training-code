using System;
using Animals.Library;

namespace Animals.UI
{
    class Program
    {
        // your entry point needs a static void Main(string[] args) method
        // and that is where the execution starts

        // "Program.cs" and Program class name are just conventions.

        // naming convention in C# - PascalCase aka TitleCase for
        //    classes, methods, properties, namespaces
        // camelCase (first letter lowercase) for local variables.
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            dog.Bark();

            Console.WriteLine("Hello World!");
        }
    }
}
