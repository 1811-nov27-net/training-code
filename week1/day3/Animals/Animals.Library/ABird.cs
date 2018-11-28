using System;

namespace Animals.Library
{
    public abstract class ABird : IAnimal
    {
        // abstract class cannot be instantiated, but can provide implementation
        // to be shared by subclasses.

        public abstract string Name { get; set; }

        public void GoTo(string location)
        {
            Console.WriteLine($"Flying to {location}.");
        }

        public abstract void MakeSound();
    }
}
