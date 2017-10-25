using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresCommon
{
    public abstract class A_Collection<T> : I_Collection<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Add(T data);
        public abstract void Clear();
        public abstract bool Remove(T data);
        public abstract IEnumerator<T> GetEnumerator();
        #endregion

        // Reacll that Count is a property, a C# construct similar to a getter/setter
        // Note that the following implementation of Count is probably not very efficient.
        // Therefore, we want the ability to override this property in a child implementation.
        // The keyword "virtual" allows this to occur.
        public virtual int Count
        {
            get
            {
                int count = 0;
                // The foreach statement works for collections that implement IEnumerable interface.
                // The foreach will autmatically call GetEnumerator and then use it to iterate through the colleaction.
                foreach(T item in this)
                {
                    count++;
                }
                return count;
            }
        }

        public bool Contains(T data)
        {
            bool found = false;

            // Get an instace of the enumerator
            IEnumerator<T> myEnum = GetEnumerator();
            // Begin enumeration at the start of the collection
            myEnum.Reset();

            // Loop through the data until the requested item is found or we reach the end of the collection
            while (!found && myEnum.MoveNext())
            {
                found = myEnum.Current.Equals(data);
            }

            return found;
        }

        // Override the implementation of ToString. Typically this method would not
        // be part of a data structure, at least not this implementation where all data items
        // are appended to the string. Could get really long ......
        public override string ToString()
        {
            IEnumerator<T> myEnum = GetEnumerator();
            myEnum.Reset();

            StringBuilder result = new StringBuilder("[");

            foreach(T item in this)
            {
                result.Append(item + ", ");
            }

            if(Count > 0)
            {
                result.Remove((result.Length - 2), (2));
            }

            result.Append("]");

            return result.ToString();
        }

        /// <summary>
        /// Simply call the GetEnumerator that returns the generic enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Equal(object other)
        {
            throw new NotImplementedException();
        }

        bool I_Collection<T>.Contains(T data)
        {
            bool found = false;

            // Get an instace of the enumerator
            IEnumerator<T> myEnum = GetEnumerator();
            // Begin enumeration at the start of the collection
            myEnum.Reset();

            // Loop through the data until the requested item is found or we reach the end of the collection
            while (!found && myEnum.MoveNext())
            {
                found = myEnum.Current.Equals(data);
            }

            return found;
        }
    }
}
