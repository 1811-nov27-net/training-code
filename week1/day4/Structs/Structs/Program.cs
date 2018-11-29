using System;

namespace Structs
{
    class Program
    {
        /// <summary>
        /// run some badly motivated struct code
        /// </summary>
        /// <param name="args">command line arguments</param>
        static void Main(string[] args)
        {
            int x = new int();
            Int32 xyz = 4;

            // value type - two copies of the number 1.
            // each variable of a value type stores a new copy of that value.
            // all structs and enums are value types.
            int a = 1;
            int b = a; // copy the value

            // reference type - only one copy of the string "asdf", with two refs to it.
            // each variable of a reference type might all be pointing to the same object.

            string c = "asdf";
            string d = c; // new reference to the same value.

            string s = 123.ToString();

            Console.WriteLine(s);

            // good programming practices in C#
            // separation of concerns
            // DRY principle - don't repeat yourself
            // break common code out into new methods/classes
            // comment your code - inline comments and XML comments
            //    this stuff i'm doing right now is inline comments with //
            // XML comments for all classes and all public or protected members
            //    aka the "public API" of your code
            
            // KISS keep it simple stupid

            /*
               multiline comment
            */

            // to comment and uncomment -
                 // in visual studio - Ctrl-K, Ctrl-C and Ctrl-K, Ctrl-U
                 // in VS code - Ctrl-/ and Ctrl-/
        }
    }
}
