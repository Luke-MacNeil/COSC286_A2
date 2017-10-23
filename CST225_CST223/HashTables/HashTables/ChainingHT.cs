using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public class ChainingHT<K, V> : A_HashTable<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        public ChainingHT()
        {
            oDataArray = new object[5];
        }

        public override void Add(K key, V vValue)
        {
            // How many attepmts were made to increment
            int iAttempt = 1;

            // Get the initial hash of the key
            int iInitialHash = HashFunction(key);

            // The current location
            int iCurrentLocation = iInitialHash;

            // Wrap the key and value in a KeyValue object
            KeyValue<K, V> kvNew = new KeyValue<K, V>(key, vValue);

            // Position to add the new element
            int iPositionToAdd = -1;

            BinarySearchTree.BST<KeyValue<K, V>> bst = null;

            if(oDataArray[iCurrentLocation] == null)
            {
                bst = new BinarySearchTree.BST<KeyValue<K,V>>();
            }



            bst.Add(vValue);


        }

        private void ExpandHashTable()
        {
            // Create a reference to the existing HashTable
            object[] oOldArray = oDataArray;

            // Create a new array length of old array times 2
            oDataArray = new object[oDataArray.Length * 2];

            // Reset the attributes
            iCount = 0;
            iNumCollisions = 0;

            // Loop through the existing tabel and re-hash each line
            for (int i = 0; i < oOldArray.Length; i++)
            {
                if (oOldArray[i] != null)
                {
                    // If the current value is a key-value (and not a tombstone).
                    // Use get GetType() when dealing with an instance of something
                    if (oOldArray[i].GetType() == typeof(KeyValue<K, V>))
                    {
                        // Get a reference to the current key-value
                        KeyValue<K, V> kv = (KeyValue<K, V>)oOldArray[i];
                        this.Add(kv.Key, kv.Value);
                    }
                }
            }
        }

        public override V Get(K key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator<V> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Remove(K key)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }


    }
}
