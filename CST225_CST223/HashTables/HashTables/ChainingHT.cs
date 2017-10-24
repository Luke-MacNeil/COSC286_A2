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

            BST<KeyValue<K, V>> bst = null;

            if (oDataArray[iCurrentLocation] == null)
            {
                bst = new BinarySearchTree.BST<KeyValue<K, V>>();
            }

            // this comment is my only change

            bst.Add(kvNew);




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

        // changed get to take in a KeyValue object so we can track both the key and data of what to find
        public override V Get(KeyValue<K, V> keyValue)
        {
            // Get the hashcode
            int iInitialHash = HashFunction(keyValue.Key);

            // Current location we are looking at in the collision chain
            int iCurrentLocation = iInitialHash;

            // How many attempts were made to increment
            int iAttempts = 1;

            BST<KeyValue<K, V>> bst = oDataArray[iCurrentLocation];

            // call find in BST and just return the value of the KeyValue object returned
            return bst.Find(keyValue).Value;
        }

        public override IEnumerator<V> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        // changed remove to take in a KeyValue object so we can track both the key and data of what to remove
        public override void Remove(KeyValue<K, V> keyValue)
        {
            // Get the hashcode (the key of the object passed in)
            int iInitialHash = HashFunction(keyValue.Key);

            // Current location we are looking at in the collision chain
            int iCurrentLocation = iInitialHash;

            // How many attempts were made to increment
            int iAttempts = 1;

            // Indicator that the item was found
            Boolean found = false;


            // planning on having this loop through every node in the bst, to try and remove the node
            BST<KeyValue<K, V>> currBst = oDataArray[iCurrentLocation];

            while (!found && currBst.Left != null)
            {

            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < oDataArray.Length; i++)
            {
                sb.Append("Bucket " + i + ": ");
                if (oDataArray[i] != null)
                {
                    // If the current location contains a BST
                    if (oDataArray[i].GetType() == typeof(BST<KeyValue<K, V>>))
                    {

                        // Don't look at this, just messing around trying to work out ToString()

                        BST<KeyValue<K, V>> bstKV = (BST<KeyValue<K, V>>)oDataArray[i];

                        IEnumerator<KeyValue<K, V>> myEnum = GetEnumerator();
                        myEnum.Reset();

                        sb.Append(bstKV.ToString() + " IH = " + HashFunction(keyValue.Key) + "[");

                        foreach (V item in this)
                        {
                            result.Append(item + ", ");
                        }

                        if (Count > 0)
                        {
                            result.Remove((result.Length - 2), (2));
                        }

                        result.Append("]");

                        return result.ToString();

                        sb.Append(bstKV.ToString() + " IH = " + HashFunction(keyValue.Key));
                    }
                    else // it is a tombstone
                    {
                        sb.Append("Tombstone");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }



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
}
