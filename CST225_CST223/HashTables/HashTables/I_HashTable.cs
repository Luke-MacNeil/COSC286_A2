using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    // K --> Generic value representing the data type of the key.
    // V --> Generic value representing the data type of the data stored.
    public interface I_HashTable<K, V> : IEnumerable<V>
        where K: IComparable<K>
        where V: IComparable<V>
    {
        /// <summary>
        /// Return a value from a hashtable
        /// </summary>
        /// <param name="key">The key of the value to return</param>
        /// <returns>The value of the item found</returns>
        V Get(K key);

        /// <summary>
        /// Add the key and value as a key-value pair to the hashtable
        /// </summary>
        /// <param name="key">Determine the location in the hashtable</param>
        /// <param name="vValue">Value to store at that location</param>
        void Add(K key, V vValue);

        /// <summary>
        /// Remove the value associated with the key passed in.
        /// </summary>
        /// <param name="key">Unique identifier of the elemnt to remove</param>
        void Remove(K key);

        /// <summary>
        /// Remove all key-value pair from the hashtable and initialize to the default array-size.
        /// </summary>
        void Clear();
    }
}
