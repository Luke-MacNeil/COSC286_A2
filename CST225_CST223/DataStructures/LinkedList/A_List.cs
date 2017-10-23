using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresCommon;

namespace LinkedList
{
    public abstract class A_List<T> : A_Collection<T>, I_List<T> where T : IComparable<T>
    {
        #region Abstract Methods
        public abstract void Insert(int index, T data);
        public abstract T RemoveAt(int index);
        public abstract T ReplaceAt(int index, T data);
        #endregion


        #region I_List Implementation
        public T ElementAt(int index)
        {
            // Assign the default value for T. Null if it is an object, 0 for an int, etc.
            T tOriginal = default(T);

            // Count how many times we have looped
            int count = 0;

            //Bounds check
            if (index < 0 || index >= this.Count)
            {
                // Throw an index out of bounds exception
                throw new IndexOutOfRangeException("Invalid index " + index);
            }

            IEnumerator<T> myEnum = GetEnumerator();
            myEnum.Reset();


            // Loop there are more data items and not at the current index
            while(myEnum.MoveNext() && count != index)
            {
                count++;
            }

            // This is a loop you made to try to mimic what he did in class
            //for(int i = 0; i != index; i++)
            //{
            //    myEnum.MoveNext();
            //}

            // get the current value from the enumerator
            tOriginal = myEnum.Current;

            return tOriginal;
        }

        public int IndexOf(T data)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
