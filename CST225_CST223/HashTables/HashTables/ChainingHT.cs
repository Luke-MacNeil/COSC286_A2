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
        private Queue<KeyValue<K, V>> qNodes = new Queue<KeyValue<K, V>>();

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

            if (oDataArray[iCurrentLocation] != null && oDataArray[iCurrentLocation].GetType() == typeof(BST<KeyValue<K,V>>))
            {
                bst = (BST<KeyValue<K,V>>)oDataArray[iCurrentLocation];
                // Call find on the bst to see if the data we are adding already exists.
                // We do not want to add duplicate data.
                IterateTree(bst);

                while(qNodes.Count > 0)
                {
                    K current = qNodes.Dequeue().Key;

                    // added "qNodes.Count == 0"
                    if (current.CompareTo(key) != 0 && qNodes.Count == 0)
                    {
                        iCount++;
                        bst.Add(kvNew);
                    }
                }
            }
            // removed "else if (oDataArray[iCurrentLocation] == null)"
            // should catch Tombstones
            else
            {
                bst = new BST<KeyValue<K, V>>();

                iCount++;
                bst.Add(kvNew);

                oDataArray[iCurrentLocation] = bst;
            }

            if((iCount / (double)HTSize) > dLoadfactor)
            {
                ExpandHashTable();
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
                    if (oOldArray[i].GetType() == typeof(BST<KeyValue<K, V>>))
                    {

                        // Get a reference to the current key-value
                        BST<KeyValue<K, V>> bst = (BST<KeyValue<K, V>>)oOldArray[i];

                        IterateTree(bst);

                        while(qNodes.Count > 0)
                        {
                            KeyValue<K, V> reHash = qNodes.Dequeue();

                            this.Add(reHash.Key, reHash.Value);
                        }
                    }
                }
            }
        }

        protected void IterateTree(BST<KeyValue<K,V>> bst)
        {
            qNodes = new Queue<KeyValue<K, V>>();
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

            BST<KeyValue<K, V>> bst = null;

            if(oDataArray[iCurrentLocation] != null && oDataArray[iCurrentLocation].GetType() == typeof(BST<KeyValue<K, V>>))
            {
                bst = (BST<KeyValue<K, V>>)oDataArray[iCurrentLocation];

                IterateTree(bst);

                while (qNodes.Count > 0)
                {
                    KeyValue<K, V> current = qNodes.Dequeue();

                    if (current.Key.CompareTo(key) == 0)
                    {
                        result = current.Value;
                    }
                }

                for (int i = 0; i < qNodes.Count; i++)
                {
                    KeyValue<K, V> current = qNodes.Dequeue();

                    if (current.Key.CompareTo(key) == 0)
                    {
                        result = current.Value;
                    }
                }
            }
            else
            {
                throw new KeyNotFoundException("Key to kind, not found");
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

            BST<KeyValue<K, V>> bst = null;

            // if key exist at current location
            if(oDataArray[iCurrentLocation] != null && oDataArray[iCurrentLocation].GetType() == typeof(BST<KeyValue<K,V>>))
            {
                // planning on having this loop through every node in the bst, to try and remove the node
                bst = (BST<KeyValue<K, V>>)oDataArray[iCurrentLocation];

                //Console.WriteLine(Get(key));
                V value = Get(key);

                KeyValue<K, V> kvNew = new KeyValue<K, V>(key, value);

                IterateTree(bst);

                if (bst.Remove(kvNew) && qNodes.Count == 1)
                {
                    oDataArray[iCurrentLocation] = new Tombstone();
                }
            }
            else
            {
                // otherwise throw an exception telling the user it doesn't exist
                throw new KeyNotFoundException("Key to remove does not exist in the Hash Table");
            }
        }

        //Cuong's
        //public override string ToString()
        //{
        //    object[] currDataArray = oDataArray;

        //    StringBuilder htString = new StringBuilder("[");

        //    //loop through hash table and check each hash
        //    for (int i = 0; i < currDataArray.Length; i++)
        //    {
        //        if (currDataArray[i] != null)
        //        {
        //            IterateTree((BST<KeyValue<K, V>>)currDataArray[i]);

        //            StringBuilder bstString = new StringBuilder("[");

        //            bstString.Append("(" + qNodes.Dequeue() + "), ");

        //            bstString.Append("]\n");
        //        }
        //        else if (currDataArray[i] == new Tombstone())
        //        {
        //            htString.Append("Tombstone\n");
        //        }
        //        else
        //        {
        //            htString.Append("null\n");
        //        }
        //    }

        //    if (Count > 0)
        //    {
        //        htString.Remove((htString.Length - 2), (2));
        //    }

        //    htString.Append("]");

        //    return htString.ToString();
        //}

        //Luke's
        public override string ToString()
        {
            object[] currDataArray = oDataArray;

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < currDataArray.Length; i++)
            {
                if (currDataArray[i] != null && currDataArray[i].GetType() == typeof(BST<KeyValue<K,V>>))
                {
                    IterateTree((BST<KeyValue<K, V>>)currDataArray[i]);

                    result.Append("[");
                    while (qNodes.Count > 0)
                    {
                        KeyValue<K, V> kvString = qNodes.Dequeue();

                        result.Append(kvString.Key + " " + kvString.Value + ", ");
                    }
                    result.Append("]\n");
                }
                else if (currDataArray[i] != null && currDataArray[i].GetType() == typeof(Tombstone))
                {
                    result.Append("Tombstone\n");
                }
                else
                {
                    result.Append("null\n");
                }
            }

            return result.ToString();
        }
    }
}
