using System;
using System.Collections.Generic;
using System.Text;

namespace LINQAndTesting.Library
{
    // in c sharp, there's regular parameters - maybe a method Add can accept
    // 2 and 2, 5 and 3, 1 and 0, it can accept many values.
    // there's also "type parameters" which means, a class or a method
    // can work with different types without being a whole new class/method.

    // "type parameters" aka generics.

    // the way we do type parameters is with angle brackets <type>
    // some, like Dictionary, take more than one e.g. Dictionary<string, int>

    // "readonly" just means i can't reassign "_list" to a different object
    // later. it doesn't mean i can't modify the object with its methods.

    // make class generic with angle brackets in its definition
    //  - this defines a type parameter in that class. by convention,
    //  - call it "T" if there's only one. (stands for type)

    // generic / type-parameter constraints:
    //     you can require that it is derived from some type (class, interface)
    //            MyGenericCollection<T> where T : SomeType
    //     you can require that it be a struct
    //            MyGenericCollection<T> where T : struct
    //     you can require that it have a default contructor
    //            MyGenericCollection<T> where T : new()
    // can have multiple constraints
    public class MyGenericCollection<T> where T : new()
    {
        protected List<T> _list = new List<T>();
        // we don't know what T is, it could be anything
        // so this member will have a different type for every instance of
        // MyGenericCollection

        public void AddDefaultValue()
        {
            _list.Add(new T());
            // not allowed unless we put "new()" constraint where T is declared.
        }

        public void Sort()
        {
            _list.Sort();
        }

        public virtual void Add(T item)
        {
            _list.Add(item);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }
    }
}
