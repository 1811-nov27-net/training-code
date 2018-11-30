using System;
using System.Collections.Generic;

namespace MemoryList.Library
{
    /// <summary>
    /// Represents a strongly typed list of objects that can be accessed by index. Provides
    /// methods to search, sort, and manipulate lists. Supports a check for whether a certain
    /// item has ever been a member of the list.
    /// </summary>
    /// <remarks>
    /// To implement anything like this functionality, it is necessary to override either every
    /// method which removes elements, or every method which adds elements.
    /// </remarks>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class MemoryList<T> : List<T>
    {
        private readonly ISet<T> _everContained = new HashSet<T>();

        /// <summary>
        /// Initializes a new instance of the MemoryList.Library.MemoryList`1 class that
        /// is empty and has the default initial capacity.
        /// </summary>
        /// <remarks>
        /// Constructors are not inherited, so we remake equivalent ones
        /// to support all behavior the parent class had.
        /// The ": base(args)" syntax is how we make sure a parent class constructor
        /// is called before this one.
        /// </remarks>
        public MemoryList() : base() { }

        /// <summary>
        /// Initializes a new instance of the MemoryList.Library.MemoryList`1 class that
        /// contains elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public MemoryList(IEnumerable<T> collection) : base(collection)
        {
            _everContained.UnionWith(collection);
        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.List`1 class that
        /// is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">capacity is less than 0.</exception>
        public MemoryList(int capacity) : base(capacity) { }

        /// <summary>
        /// Determines whether an element is or has ever been in the MemoryList.Library.MemoryList`1
        /// since it has been created.
        /// </summary>
        /// <param name="item">
        /// The object to locate in the MemoryList.Library.MemoryList`1. The value can
        /// be null for reference types.
        /// </param>
        /// <returns>true if item has ever been in the MemoryList.Library.MemoryList`1; otherwise, false.</returns>
        public bool HasEverContained(T item)
        {
            return _everContained.Contains(item);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <remarks>
        /// C# supports operator overloading, including the indexing operator [].
        /// That is done with syntax including "this[arg]" and similar to property
        /// syntax, except there is no "auto-implemented" version.
        /// </remarks>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index is less than 0. -or- index is equal to or greater than MemoryList.Library.MemoryList`1.Count.
        /// </exception>
        public new T this[int index]
        {
            get => base[index]; // for access (e.g. var x = list[0])
            set                 // for assignment (e.g. list[0] = x)
            {
                base[index] = value;
                _everContained.Add(value);
            }
        }

        /// <summary>
        /// Adds an object to the end of the MemoryList.Library.MemoryList`1.
        /// </summary>
        /// <param name="item">
        /// The object to be added to the end of the MemoryList.Library.MemoryList`1. The
        /// value can be null for reference types.
        /// </param>
        public new void Add(T item)
        {
            base.Add(item); // call the parent class's implementation

            _everContained.Add(item);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the MemoryList.Library.MemoryList`1.
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the System.Collections.Generic.List`1.
        /// The collection itself cannot be null, but it can contain elements that are null,
        /// if type T is a reference type.
        /// </param>
        /// <exception cref="System.ArgumentNullException">collection is null.</exception>
        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);

            _everContained.UnionWith(collection);
        }
    }
}
