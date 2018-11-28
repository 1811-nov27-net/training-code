using System;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            // exceptions are runtime errors that we can potentially handle.
            // exceptions are objects and defined by classes like anything else.

            try
            {
                BadCode();

                // try goes around code that is expected to throw an exception.
                // (now never runs because of BadCode exception)
                var x = 4;
                var y = x / 0;

                Console.WriteLine("never prints because an exception is thrown first");

                // you can throw exceptions yourself with a throw statement
                throw new ArgumentNullException();
            }
            catch (DivideByZeroException e)
            {
                // handle the exception in catch block.
                Console.WriteLine("divided by zero, moving on");
                // at the end of catch, we move on with business
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("handled bad cast.");

                throw; // re-throws the current exception
                // (only works inside catch)
            }
            catch (Exception e)
            {
                Console.WriteLine("handle ANY exception (don't do this)");
            }

            Console.WriteLine("the program continues");
        }

        static void BadCode()
        {
            try
            {
                object o = true;
                string s = (string)o;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("won't print because this isn't a div by zero!");
            }
            // we're not handling the specific exception, so it will
            // propagate up the call stack (to Main in this case)
            // and eventually crash the app if never handled.
        }
    }
}
