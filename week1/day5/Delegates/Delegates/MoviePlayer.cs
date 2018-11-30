using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Delegates
{
    public class MoviePlayer
    {
        // C# supports some things called delegates and events.
        // the idea here is, i can write a class that expects it's consumer
        // to actually "inject" behavior into it for it to use.

        // this enables some polymorphism, you can write classes that support
        // a lot of different behaviors to be decided by other code.

        public string CurrentMovie { get; set; }

        // this delegate type can hold any function with zero parameters and void return.
        public delegate void MovieFinishedHandler();
        //    return type /^                      ^\ zero 

        // now, any variable of type "MovieFinishedHandler" can hold 
        // zero-arg functions with void return.

        // (delegates are reference types)

        public delegate void MovieFinishedStringHandler(string title);

        // an event is something you can subscribe to with any number of functions.
        // when the event is "called" as if it itself were a function,
        // all subscribing functions are called.

        // you need a delegate type to declare the event,
        // and all subscribing functions must match that signature.
        // event delegates should always be a void-returning type.
        public event MovieFinishedStringHandler MovieFinished;
        //public event Action<string> MovieFinished;
        
        // Func and Action types can be used instead of delegates
        // the only downside is you lose the self-documenting aspect
        // of the delegate having a name, like "MovieFInishedHandler".

        public void PlayMovie()
        {
            Thread.Sleep(3000); // wait for 3 seconds

            Console.WriteLine($"Finished movie {CurrentMovie}");

            // we'll fire an event when the movie's finished,
            // and any code using this movie player
            // can subscribe to that event with whatever function/code they want.

            // have to check that events are not null before firing them.
            // (events without ANY subscribers are == null.)
            if (MovieFinished != null)
            {
                // when you call an event that needs arguments,
                // the arguments will go to the subscribing functions
                MovieFinished(CurrentMovie);
            }

            // or, use null conditional operator
            // "?." does a null check on the left hand side first, and
            // if the left hand side is null, it'll do nothing.
            // just syntax sugar.
            // X?.Y?.Z?.A

            //MovieFinished?.Invoke();
        }
    }
}
