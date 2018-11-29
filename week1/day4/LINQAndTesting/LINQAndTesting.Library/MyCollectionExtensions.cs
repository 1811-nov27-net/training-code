using System;
using System.Collections.Generic;
using System.Text;

namespace LINQAndTesting.Library
{
    // C# supports adding methods to classes that are already defined
    // even the framework's classes like List or string.
    public static class MyCollectionExtensions
    {
        public static bool Empty(this MyCollection coll)
        {
            return coll.Length == 0;
            // (equiv to "if length 0, return true, else return false")
            // but better to just return the comparison itself
            // since it's already true or false.
        }
        // as long as someone has a "using" statement to this namespace,
        // every MyCollection they see will have this extra method on it.
    }
}
