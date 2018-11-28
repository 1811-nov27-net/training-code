using System;
using System.Collections.Generic;

namespace CSharpBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // local variables and types
            int x = 0;
            double y = 4.58; // (allows decimal - 64-bit float)
            decimal z = 5.001m; // even more precision - for financial etc

            string s = "string";
            bool b = true;
            b = false;

            // base class of *everything* - object
            object o = true;

            // var (compiler type inference)

            var xyz = "Hello";
            var b1 = true;
            //xyz = false; // error! no dynamic typing

            // use var when the type is clear to the person reading the code
            // don't use it when it obscures useful context.

            // control structures
            // loops
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }

            while (false)
            {
                // while loop
            }

            do
            {
                // do while loop
            } while (false);

            // conditionals
            if (true)
            {
                // if
            }
            else if (false)
            {

            }
            else
            {
                // else
            }

            // List is a class defined in another namespace
            // therefore it needs a "using statement" at the top of the file
            // (like an import in other languages)
            List<string> list = new List<string>();
            list.Add("asdf");

            // snippet: foreach
            foreach (var item in list)
            {
                // snippet: cw
                Console.WriteLine(item);
            }

            // object-oriented
            //     have objects which associate data and related behavior
            //     to represent "entities"/nouns
            //     create those objects from templates called classes
            //     which define a contract for those objects at runtime.

            // part of the .NET ecosystem/platform

            // strongly typed (more precisely, statically typed)
            // statically typed means, variables are locked
            //    to a certain type at compile time and cannot change.

            // unified type system
            //    "primitives" (types with value semantics instead of reference semantics)
            //    *also* inherit from object.

            // garbage collection
            // "managed" language (memory is managed for you)
            // the runtime is responsible for freeing unused objects
            //   from memory. saves developer time, fewer bugs, some performance
            //   penalty.

            // functions are not quite first-class but in practice
            //   more or less.
            // c sharp is somewhat functional, especially in practice with
            //   LINQ (one of the best parts of C#)
            //     (Language-Integrated Query Language)

            // asynchronous programming support with TPL (Task Processing Library)
            // parallel programming support

            // exception handling
        }
    }
}
