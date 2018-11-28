using System;

namespace Animals.Library {
    public class Dog : IAnimal {
        // not fields - properties

        // only auto-properties have implicit backing fields.
        // as soon as you give a body to the get or the set,
        // you need to add a private field yourself.
        public int Id { get; set; }

        // weird example to show you don't even need a field
        public string Name
        {
            // getters and setters can contain arbitrary logic
            get { return "Bob"; }
            set { Console.WriteLine("inside property setter"); }
        }

        // property with validation
        private string _breed;
        public string Breed
        {
            get { return _breed; }
            set
            {
                // validation - no null or empty allowed.
                if (value != null && value != "")
                {
                    _breed = value;
                }
            }
        }

        // property (with explicit backing field)
        private int _age;
        public int Age {
            get {
                return _age;
            }
            // get and set can have access modifiers of their own
            private set {
                _age = value;
                // keyword "value" in a setter takes in the assigned value
            }
        }

        // classic getters and setters
        private int Weight;

        public int GetWeight () {
            return Weight;
        }

        public void SetWeight (int weight) {
            Weight = weight;
        }

        public void Bark()
        {
            Console.WriteLine("Woof");
        }

        public void MakeSound()
        {
            Bark();
        }

        public void GoTo(string location)
        {
            // string interpolation syntax
            //   prefix with $
            //   inside {} you should give an expression
            //      either string or any other object that ToString will be called on
            string output = $"Walking to {location.ToLower()}.";
            // basically equiv to:
            //   output = "Walking to " + location.ToString() + ".";
            Console.WriteLine(output);
        }

        // snippets: prop, propfull
    }
}
