using System;
using System.Collections.Generic;
using System.Linq;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Linq();

            // object initialization syntax.
            // if no parens after MoviePlayer, zero-arg constructor "()" is assumed.
            var player = new MoviePlayer
            {
                CurrentMovie = "Lord of the Rings: The Fellowship of the Ring Extended Edition"
            };

            // the function must have a compatible signature
            // with the delegate of the event.
            MoviePlayer.MovieFinishedStringHandler handler = EjectDisc;

            // subscribe to events with +=
            player.MovieFinished += handler;
            // unsubscribe with -=
            //player.MovieFinished -= handler;
            // it's like you're appending to a list of functions.

            // when C# got generics, they added Func and Action generic classes.
            // and we can use these instead of delegate types.

            // Action is for void-return functions
            // Func is for non-void-return functions
            Action<string> handler2 = EjectDisc;

            //player.MovieFinished += handler2;

            // lambda expressions
            player.MovieFinished += s => Console.WriteLine("lambda subscribe");
            // this lambda takes in a string (inferred by compiler)
            // and returns nothing (because WriteLine returns nothing).
            // therefore it is compatible with that delegate type.
            // and we don't need to define a method like "EjectDisc".

            player.PlayMovie();

            // some func/action examples:

            // function taking int and string, returning bool:
            Func<int, string, bool> func = (num, str) => true;
            // the last type parameter is the return type,
            // and the ones before it are the arguments.

            // function taking zero arguments, returning bool:
            Func<bool> func2 = () => false;

            // function taking three arguments, returning void.
            Action<int, string, bool> func3 = (num, str, b) =>
            {
                if (b)
                {
                    Console.WriteLine(num);
                    Console.WriteLine(str);
                }
            };
            // lambdas can have a block body like methods

            // function taking bool, returning void.
            Action<bool> func4 = b => { return; };
        }

        static void EjectDisc()
        {
            Console.WriteLine("Ejecting disc.");
        }

        static void EjectDisc(string title)
        {
            Console.WriteLine($"Ejecting disc {title}.");
        }
        // having two methods with the same name but different arguments
        // is allowed, this is called method overloading.
        // it's not a problem because C# can always tell which one you mean by what arguments
        // you give it when you try to call it.
        
        // don't confuse this with method overriding (inheritance).

        static void Linq()
        {
            var x = new List<string>();
            // i want to know the max length of those strings
            int longestLength = x.Max(s => s.Length);

            // LINQ methods we should know:
            //  - Select (a mapping operation, will take each element and change it into something else.)
            var firstCharacters = x.Select(s => s[0]);
            //  - Average, Min, Max
            //  - All (expects a bool-returning lambda - checks that all elements meet some condition)
            bool allShorterThan5Chars = x.All(s => s.Length < 5);
            //  - Any (works like All, but returns true if there's ANY match, not just ALL matches)
            //  - Where (filters the sequence for only the elements that return true)
            var onlyTheLongElements = x.Where(s => s.Length > 20);

            // should also know that you can chain these together
            bool b = x.Where(s => s.Length > 20)
                      .Select(s => s[0])
                      .All(c => c == 'a' || c == 'b');
            // b will be true if every long element starts with a or b.

            // remember, LINQ uses "deferred execution" meaning it doesn't actually
            // "run the loop" until you need the result.
            List<char> listOfChars = firstCharacters.ToList();
            //  - ToList is a LINQ method that will actually run the loop (if necessary)
            //    and return you a proper List.
            // all IEnumerable can do is get put in foreach loops
            // and have LINQ methods called on it.
        }

        static void Finally()
        {
            try
            {
                // this code runs always
                Console.WriteLine("try");
                // until an exception in here

                // if i'm opening resources that need to be cleaned up
                
                // don't put cleanup code here because an exception beforehand might skip it.
            }
            catch (ArgumentException e)
            {
                // this code runs when there is a matching exception
                // from inside the try block.

                // only put ArgumentException-specific cleanup here
            }
            finally
            {
                // this code runs always, period,
                // even if there was an uncaught exception in the try block.

                // put general cleanup of "try" resources here
            }
            // don't put cleanup code here either
            // because uncaught exceptions will skip it

            // we can even have try and finally without any catch.
            // if you are using resources that you must clean up
            // but any error really still needs to propagate up
            // because you can't actually handle it.

            // there is a "using statement" syntax that can replace try-finally sometimes.
        }
    }
}
