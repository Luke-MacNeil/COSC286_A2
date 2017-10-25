using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresCommon;

namespace LinkedList
{
    public interface I_List<T> : I_Collection<T> where T: IComparable<T>
    {
        /// <summary>
        /// Returns the element at a particular index
        /// </summary>
        /// <param name="index">The index of the item to find</param>
        /// <returns>The item at index</returns>
        T ElementAt(int index);

        /// <summary>
        /// Given a data item, find its first instance and return the index
        /// </summary>
        /// <param name="data">The item to find</param>
        /// <returns>The index of the item found</returns>
        int IndexOf(T data);

        /// <summary>
        /// Insert an item at a particular location.
        /// </summary>
        /// <param name="index">Location to insert at</param>
        /// <param name="data">Data item to insert</param>
        void Insert(int index, T data);

        /// <summary>
        /// Remove an element at a particular location
        /// </summary>
        /// <param name="index">Index of item to remove</param>
        /// <returns>Item that was removed</returns>
        T RemoveAt(int index);

        /// <summary>
        /// Replace existing data item with the one passed in
        /// </summary>
        /// <param name="index">Item to be replaced</param>
        /// <param name="data">Data item to replace existing one</param>
        /// <returns>Existing data item that was replaced</returns>
        T ReplaceAt(int index, T data);
    }
}
