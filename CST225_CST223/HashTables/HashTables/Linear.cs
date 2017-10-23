using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class Linear<K, V> : A_OpenAddressing<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        protected override int GetIncrement(int iAttempt, K key)
        {
            // Note that the increment should be less than the hash table size
            int iIncrement = 1;
            
            // Each time return the total distance from the initial hash location
            return iIncrement + iAttempt;
        }
    }
}
