using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public abstract class A_HashTable<K, V> : I_HashTable<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        // In the case of chaining, this array will store secondary data structures.
        // For this course we will use ArrayLists.
        // In the case of open-addressing (probing algorithms) the array
        //  will store key-value pairs directly.
        protected object[] oDataArray;

        // Store the number of elements in the array
        protected int iCount;

        // Load factor - used to track the maximum percentage full that we will allow
        //  the array to fill.
        protected double dLoadfactor = 0.72;


        // Collision count - mostly for statistical purposes
        protected int iNumCollisions = 0;


        #region hash Function
        /// <summary>
        /// Given a key, return an integer within the bounds of the indices of the array
        /// </summary>
        /// <param name="key">Key of the data</param>
        /// <returns>Initial location in the array</returns>
        protected int HashFunction(K key)
        {
            return Math.Abs(key.GetHashCode() % HTSize);
        }
        #endregion


        #region Properties
        public int Count { get => iCount; }
        public int NumCollisions { get => iNumCollisions; }
        public int HTSize { get => oDataArray.Length; }
        #endregion


        #region I_HashTable implementation
        public abstract void Add(K key, V vValue);
        public abstract V Get(K key);
        public abstract void Remove(K key);

        public void Clear()
        {
            throw new NotImplementedException();
        }

        // We will code in the child class as it is dependent on the implementation.
        public abstract IEnumerator<V> GetEnumerator();

        // Weird Microsoft thingy. Just code and forget about it.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
