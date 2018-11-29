using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQAndTesting.Library
{
    /// <summary>
    /// a list with some extra helper methods.
    /// </summary>
    /// <remarks>
    /// two strategies we could use to leverage the builtin collection classes:
    /// - inheritance (MyCollection IS A List)
    /// - composition (MyCollection HAS A List)
    /// 
    /// </remarks>
    public class MyCollection : MyGenericCollection<string>
    {
        // now that we derive from MyGenericCollection, we don't need this
        //private readonly List<string> _list = new List<string>();

        // every class has at least one constructor.
        // if you don't define one, it has a default constructor without any
        //   parameters -
        //   "public MyCollection() {}"
        // but, as soon as you define any constructor, that default one will not
        //    be added.

        // virtual and override example (without really changing the method)
        public override void Add(string item)
        {
            _list.Add(item);
        }

        // property without a "set"
        // calling code can say "coll.Length" instead of coll.GetLength()
        public int Length
        {
            get
            {
                return _list.Count;
            }
        }

        public string Get(int index)
        {
            return _list[index];
        }

        public string Longest()
        {
            int longestLength = -1;
            string longest = null;
            foreach (var item in _list)
            {
                if (item != null && item.Length > longestLength)
                {
                    longestLength = item.Length;
                    longest = item;
                }
            }
            return longest;
        }

        public double AverageLength()
        {
            return _list.Average(x => x.Length);
            // much nicer than a manual loop and less error-prone
        }

        // return sequence of all lengths of members
        public IEnumerable<int> Lengths()
        {
            return _list.Select(x => x.Length);
        }

        // return number of elements that start with an "a"
        public int NumberOfAs()
        {
            // count the number of elements matching some condition
            return _list.Count(x => (x != null && x.Length > 0 && x[0] == 'a'));

            // we are using "lambda expressions"
            // which are like methods but you can pass them as parameters
            // and assign them to variables.
        }

        private static bool ContainsVowel(string s)
        {
            // lambda expressions are convertible to "Func" or "Action" types
            // so they can be assigned as variables like this:

            Func<char, bool> isVowel = (c => "AEIOUaeiou".Contains(c));

            // true if, for ANY element, it is true that...
            return s.Any(
                // this string of vowels contains it
                isVowel
            );
        }

        public int NumberWithVowels()
        {
            return _list.Count(ContainsVowel);
        }

        // returns first member in sorted order.
        // LINQ (and IEnumerable itself) uses "deferred execution"
        public string FirstAlphabetical()
        {
            // orderby will sort the sequence by some "key"
            // and "x => x" means, sort the strings using regular string sort
            IEnumerable<string> sorted = _list.OrderBy(x => x);
            // we haven't actually sorted the list in any way
            // or iterated over it yet
            // --- only set up how we WILL iterate, when we need the values.
            var first = sorted.First();
            // that method call actually ran the sort, and then discarded everything
            // but the first entry.
            return first;
        }
    }
}
