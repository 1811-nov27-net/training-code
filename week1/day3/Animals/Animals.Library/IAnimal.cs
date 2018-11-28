namespace Animals.Library
{
    public interface IAnimal
    {
        // an interface is a contract that a class has to follow
        // specifying some methods it needs to have, with their
        // argument types and return type.

        // no implementation possible in interfaces
        // no data either (but properties, ok)
        // just method names, arguments, and return type.

        // (no backing field or auto-implementation)
        // this is just "any IAnimal class must have a Name property"
        string Name { get; set; }

        void MakeSound();

        void GoTo(string location);

        // there is no "void type" or "void class" it just means returns nothing.

        // every interface member must have the access modifier of the whole
        // interface
        // (because if you really think about it, nothing else would ever be useful)
    }
}
