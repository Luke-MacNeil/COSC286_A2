using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class KeyValue<K,V> : IComparable<KeyValue<K,V>>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        // Store the ke
        K kKey;
        V vValue;

        public KeyValue(K key, V value)
        {
            kKey = key;
            vValue = value;
        }


        public K Key
        {
            get
            {
                return kKey;
            }
        }

        public V Value
        {
            get
            {
                return vValue;
            }
        }

        public int CompareTo(KeyValue<K, V> other)
        {
            return this.Key.CompareTo(other.Key);
        }

        public override bool Equals(object obj)
        {
            KeyValue<K, V> kv = (KeyValue<K, V>) obj;
            return this.Key.CompareTo(kv.Key) == 0;
        }

    }
}
