using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresCommon
{
    // This collection uses Generics. The generics type T must be IComparable
    // The : below is the "implements" of "C". We are just saying to implement "IEnumerable<T>"
    public interface I_Collection<T> : IEnumerable<T> where T:IComparable<T>
    {
        /// <summary>
        /// Adds the given data to the collection
        /// </summary>
        /// <param name="data">Item to add</param>
        void Add(T data);

        /// <summary>
        /// Removes all items from the collection
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines if data is in the collection or not
        /// </summary>
        /// <param name="data">data item to look for</param>
        /// <returns>True if found, else false</returns>
        bool Contains(T data);

        /// <summary>
        /// Determines if this data structure is equal to another instance
        /// </summary>
        /// <param name="other">The passed in data structure to compare to the calling one</param>
        /// <returns>true if equal, else false</returns>
        bool Equals(Object other);

        /// <summary>
        /// Remove the first instance of a value if it exists
        /// </summary>
        /// <param name="data">The item to remove</param>
        /// <returns>true if removed, else false</returns>
        bool Remove(T data);

        /// <summary>
        /// A property used to access the number of elements in a collection
        /// A property is similar to a getter/setter
        /// </summary>
        int Count
        {
            get;
        }
    }
}
