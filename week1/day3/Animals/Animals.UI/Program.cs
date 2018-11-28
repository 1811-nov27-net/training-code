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

            // using fields and properties

            // using getters and setters with private field. no reason to do this;
            //    use a property.
            dog.SetWeight(6);
            Console.WriteLine(dog.GetWeight());

            dog.Name = "Fido";
            Console.WriteLine(dog.Name);

            dog.Breed = "Golden retriever";

            dog.GoTo("the Park");

            Console.WriteLine("Hello World!");

            // ----------

            IAnimal animal = new Dog();
            animal = new Eagle();
            // this is okay because both classes are within/under the IAnimal type.
            // BUT - you're not allowed to do dog-specific or eagle-specific things
            // via this variable.
            //    error: animal.Fly();
            Eagle e = (Eagle) animal;
            // you can cast objects to more specific types.
            // it will fail at runtime if the object is not actually within/under that type.

            // these terms are interchangable:
            //   - superclass, base class, parent class
            //   - subclass, derived class, child class

            // good design (separation of concerns)
            // means you shouldn't write code needlessly tied to one specific
            // implementation.

            // then you use the same code with multiple implementations of the classes you're using
            DisplayData(new Dog());
            DisplayData(new Eagle());
        }

        public static void DisplayData(IAnimal animal)
        {
            Console.WriteLine(animal.Name);
        }
    }
}
