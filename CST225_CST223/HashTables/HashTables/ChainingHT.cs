using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinarySearchTree;

namespace HashTables
{
    public class ChainingHT<K, V> : A_HashTable<K, V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        private Queue<KeyValue<K, V>> qNodes = null;

        public ChainingHT()
        {
            oDataArray = new object[5];
        }

        public override void Add(K key, V vValue)
        {
            // How many attepmts were made to increment
            //int iAttempt = 1;

            // Get the initial hash of the key
            int iInitialHash = HashFunction(key);

            // The current location
            int iCurrentLocation = iInitialHash;

            // Wrap the key and value in a KeyValue object
            KeyValue<K, V> kvNew = new KeyValue<K, V>(key, vValue);

            // Position to add the new element
            //int iPositionToAdd = -1;

            BST<KeyValue<K, V>> bst = null;

            if (oDataArray[iCurrentLocation] == null)
            {
                bst = new BST<KeyValue<K, V>>();
            }

            // Call find on the bst to see if the data we are adding already exists.
            // We do not want to add duplicate data.
            if(bst.Find(kvNew).Value.CompareTo(vValue) != 0)
            {
                bst.Add(kvNew);
            }
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

        protected void IterateTree(BST<KeyValue<K,V>> bst)
        {
            qNodes = null;
            bst.Iterate(getValue, TRAVERSALORDER.PRE_ORDER);
        }

        protected void getValue(KeyValue<K,V> nCurrent)
        {
            qNodes.Enqueue(nCurrent);
        }

        public override V Get(K key)
        {
            V result = default(V);

            // Get the hashcode
            int iInitialHash = HashFunction(key);

            // Current location we are looking at in the collision chain
            int iCurrentLocation = iInitialHash;

            // How many attempts were made to increment
            //int iAttempts = 1;

            BST<KeyValue<K, V>> bst = (BST<KeyValue<K,V>>) oDataArray[iCurrentLocation];

            qNodes = null;

            bst.Iterate(getValue, TRAVERSALORDER.PRE_ORDER);

            for(int i = 0; i < qNodes.Count; i++)
            {
                KeyValue<K, V> current = qNodes.Dequeue();

                if(current.Key.CompareTo(key) == 0)
                {
                    result = current.Value;
                }
            }

            return result;
        }

        public override IEnumerator<V> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Remove(K key)
        {
            // Get the hashcode (the key of the object passed in)
            int iInitialHash = HashFunction(key);

            // Current location we are looking at in the collision chain
            int iCurrentLocation = iInitialHash;

            // How many attempts were made to increment
            //int iAttempts = 1;

            // planning on having this loop through every node in the bst, to try and remove the node
            BST<KeyValue<K, V>> bst = (BST<KeyValue<K, V>>)oDataArray[iCurrentLocation];

            V value =  this.Get(key);

            KeyValue<K, V> kvNew = new KeyValue<K, V>(key, value);

            bst.Remove(kvNew);
        }

        public override string ToString()
        {
            object[] currDataArray = oDataArray;

            StringBuilder result = new StringBuilder("[");

            for(int i = 0; i < currDataArray.Length; i++)
            {
                IterateTree((BST<KeyValue<K,V>>) currDataArray[i]);

                if (currDataArray[i] != null)
                {
                    result.Append(qNodes.Dequeue() + ", ");
                }
                else if(currDataArray[i] == new Tombstone())
                {
                    result.Append("Tombstone\n");
                }
                else
                {
                    result.Append("null\n");
                }

                result.Append("]\n");
            }



            //IEnumerator<T> myEnum = GetEnumerator();
            //myEnum.Reset();

            //StringBuilder result = new StringBuilder("[");

            //foreach (T item in this)
            //{
            //    result.Append(item + ", ");
            //}

            //if (Count > 0)
            //{
            //    result.Remove((result.Length - 2), (2));
            //}

            //result.Append("]");






            //object[] currDataArray = oDataArray;

            //StringBuilder sb = new StringBuilder("[");
            //for (int i = 0; i < currDataArray.Length; i++)
            //{
            //    sb.Append("Bucket " + i + ": ");
            //    if (oDataArray[i] != null)
            //    {
            //        // If the current location contains a key-value
            //        if (oDataArray[i].GetType() == typeof(BST<KeyValue<K, V>>))
            //        {
            //            BST<KeyValue<K, V>> bst = (BST<KeyValue<K, V>>)oDataArray[i];
            //            sb.Append(bst.Value.ToString() + " IH = " + HashFunction(bst.Key));
            //        }
            //        else // it is a tombstone
            //        {
            //            sb.Append("Tombstone");
            //        }
            //    }
            //    sb.Append("\n");
            //}



            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < oDataArray.Length; i++)
            //{
            //    sb.Append("Bucket " + i + ": ");
            //    if (oDataArray[i] != null)
            //    {
            //        // If the current location contains a key-value
            //        if (oDataArray[i].GetType() == typeof(KeyValue<K, V>))
            //        {
            //            KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[i];
            //            sb.Append(kv.Value.ToString() + " IH = " + HashFunction(kv.Key));
            //        }
            //        else // it is a tombstone
            //        {
            //            sb.Append("Tombstone");
            //        }
            //    }
            //    sb.Append("\n");
            //}
            //return sb.ToString();






            return result.ToString();
        }
    }
}
