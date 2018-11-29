using System;
using System.Collections.Generic;

namespace Collections
{
    class Program // this is internal by default
    {
        static void Main(string[] args) // this is private by default
        {
            Arrays();
            Lists();
            Sets();
            Dictionaries();
            StringEquality();
        }

        static void Arrays()
        {
            // arrays are the same they are in any language
            // a fixed-length row of slots for some type.

            int[] intArray = new int[10]; // length 10 array (brackets not parens)
            // the array now has 10 default values of whatever type
            //    e.g. null for objects, 0 for ints, false for bool
            Console.WriteLine(intArray[5]);

            // iterate over arrays with regular for loop or foreach
            for (int i = 0; i < intArray.Length; i++)
            {
                Console.Write(intArray[i]);
            }
            Console.WriteLine();

            foreach (var item in intArray) // iterate when you don't need the index
            {
                Console.Write(item);
            }
            Console.WriteLine();

            // you can use a foreach loop with *anything* implementing the
            // IEnumerable interface, which is one of the most important interfaces
            // in C#. (also used with LINQ!)

            // jagged / nested arrays
            int[][] arrayOfFourArrays = new int[4][];
            // this is now an array of four nulls
            //Console.WriteLine(arrayOfFourArrays[0][0]); // exception
            arrayOfFourArrays[0] = new int[3];
            arrayOfFourArrays[0] = new int[300];

            // multi-dimensional arrays (rectangular, never jagged)
            int[,] trueMultiDArray = new int[5, 5];
            // this is an array with 25 zeroes that we index with row and column.
            trueMultiDArray[3, 4] = 5;
            // use this instead of jagged array generally

            // actually don't use arrays hardly ever
            // instead use generic collection classes for everything
            // that's not some performance-critical loop

            // (never pre-optimize your code, write it the simple/understandable
            // way first, and in necessary, later, profile it, and optimize where
            // actually useful.)

            int[] array = new int[]
            {
                4, 8, 3, 2, 1, 2
            };
            // array initializiation syntax, length inferred
        }

        static void Lists()
        {
            // depending on abstractions not concretions
            // i.e., on interfaces, not concrete implementations/classes
            // so the actual class should be swapped out later easily.

            // that's more of a big deal for method signatures (parameters,
            // return types), less so for local variables like this,
            // but still good practice.
            IList<bool> list = new List<bool>();

            list.Add(true);
            list.Add(true);
            list.Add(false);

            // Lists have dynamic length, grow and shrink as you
            // add and remove values.

            var list2 = new List<bool>() { false, false, false };
            list2.AddRange(list); // add a range

            // use these pretty much always instead of arrays

            var x = list2[2]; // you can index Lists just like arrays
            list[1] = true;
            // and of course you can use foreach.
        }

        static void Sets()
        {
            var set = new HashSet<string>();

            // sets have no particular order to them
            // and do not allow duplicates.

            // adding an element that's already in there has zero effect.

            set.Add("abc");
            set.Add("abc"); // does nothing
            set.Add("def");

            Console.WriteLine(set.Count); // prints 2

            // this is based on the mathematical concept of "set"
            // so we have standard "set operations"
            // like union, intersection, difference
            // comparisons like subset

            // sets are very fast to search for a specific value
            // even if there's millions of things in the set
            // because it's implemented with a "hashtable"

            // slower to iterate over than list
            // but faster to lookup.
        }

        static void Dictionaries()
        {
            var dict = new Dictionary<string, string>();

            dict.Add("Germany", "Berlin");
            dict.Add("USA", "Washington, DC");

            // you can use "indexing" syntax on these too.
            Console.WriteLine(dict["USA"]); // prints Washington, DC

            dict["Mexico"] = "Mexico City"; // inserts/overwrites

            IDictionary<string, string> dict2 = new Dictionary<string, string>
            {
                { "Germany", "Berlin" },
                { "USA", "Washington, DC" }
            }; // this is dictionary initialization syntax.

            // you can use foreach with dictionaries in a few ways

            foreach (string key in dict.Keys) { }
            foreach (string value in dict.Values) { }
            foreach (KeyValuePair<string, string> pair in dict)
            {
                //pair.Key
                //pair.Value
            }
        }

        static void StringEquality()
        {
            // in some languages, == always means "references the same object in memory"
            // while .Equals is for "references an EQUIVALENT object"

            bool stringsEqual = "abc" == "abc";
            Console.WriteLine(stringsEqual);

            // in C#, strings with == compare value, not reference.
            // we have operator overloading in C# (though we don't use it much)
            // and you can override == to do value comparison too on your classes.

            // for basically all reference types except string, == compares
            // "exact same object yes or no"

            Console.WriteLine(new Dummy() == new Dummy()); // prints false
        }

        public class Dummy { }
    }
}
