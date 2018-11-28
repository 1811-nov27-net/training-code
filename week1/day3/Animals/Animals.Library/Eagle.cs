using System;

namespace Animals.Library
{
    // can derive from only one class, but can implement many interfaces.
    public class Eagle : ABird
    {
        public override string Name
        {
            get;
            set;
        }

        public override void MakeSound()
        {
            Console.WriteLine("Caw");
        }

        public void Fly()
        {

        }
    }
}
