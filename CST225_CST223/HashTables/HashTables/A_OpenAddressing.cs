using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    public abstract class A_OpenAddressing<K,V> : A_HashTable<K,V>
        where K : IComparable<K>
        where V : IComparable<V>
    {
        // Abstract method to get the increment value (varies depending on child class implementation)
        protected abstract int GetIncrement(int iAttempt, K key);

        // Local instance of the prime number generator
        private PrimeNumber pn = new PrimeNumber();

        // Constructor so set up the hash table array
        public A_OpenAddressing()
        {
            oDataArray = new object[pn.GetNextPrime()];
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

            // while the data at the current location is not null (has a value), this will
            // find an empty location to add the current item
            while(oDataArray[iCurrentLocation] != null)
            {
                // If the current item is a key-value
                if (oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    // Check to see if the current value is the same key as the value we are adding
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];

                    if (kv.Equals(kvNew))
                    {
                        throw new ApplicationException("Item alrwady exists");
                    }
                }
                // It is a tobmstone
                else
                {
                    // If this is the first tombstone
                    if(iPositionToAdd == -1)
                    {
                        iPositionToAdd = iCurrentLocation;
                    }
                }

                // Increment to the next location
                iCurrentLocation = iInitialHash + GetIncrement(iAttempt++, key);

                // Loop back up to the top of the table if we fall off the bottom
                iCurrentLocation %= HTSize;


                // For stats
                iNumCollisions++;
            }

            // Check to see if we didn't hit a tombstone
            if(iPositionToAdd == -1)
            {
                iPositionToAdd = iCurrentLocation;
            }

            // Add the KeyValue to the current location
            oDataArray[iPositionToAdd] = kvNew;

            // Increment the count
            iCount++;


            // If the table is overloaded, then expand it
            if(IsOverLoaded())
            {
                ExpandHashTable();
            }
        }

        private void ExpandHashTable()
        {
            // Create a reference to the existing HashTable
            object[] oOldArray = oDataArray;

            // Create a new array, the next prime number size
            oDataArray = new object[pn.GetNextPrime()];

            // Reset the attributes
            iCount = 0;
            iNumCollisions = 0;

            // Loop through the existing tabel and re-hash each line
            for(int i = 0; i < oOldArray.Length; i++)
            {
                if(oOldArray[i] != null)
                {
                    // If the current value is a key-value (and not a tombstone).
                    // Use get GetType() when dealing with an instance of something
                    if(oOldArray[i].GetType() == typeof(KeyValue<K,V>))
                    {
                        // Get a reference to the current key-value
                        KeyValue<K, V> kv = (KeyValue<K, V>)oOldArray[i];
                        this.Add(kv.Key, kv.Value);
                    }
                }
            }
        }

        private bool IsOverLoaded()
        {
            return (iCount / (double) HTSize) > dLoadfactor;
        }

        public override V Get(K key)
        {
            V vReturn = default(V);

            int iInitialHash = HashFunction(key);

            // Current location we are looking at in the collision chain
            int iCurrentLocation = iInitialHash;

            // How many attempts were made to increment
            int iAttempts = 1;

            // Indicator that the item was found
            Boolean found = false;

            while (!found && oDataArray[iCurrentLocation] != null)
            {
                // If the current location contains a key-value
                if (oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K, V>))
                {
                    // It is a key-value
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];

                    // Check if it is the key-value we are looking for
                    if (kv.Key.CompareTo(key) == 0)
                    {
                        vReturn = kv.Value;
                        found = true;
                    }
                }
                // Increment to the next location
                iCurrentLocation = iInitialHash + GetIncrement(iAttempts, key);
                iCurrentLocation %= HTSize;
            }

            if (!found)
            {
                throw new KeyNotFoundException("Key not found");
            }

            return vReturn;
        }

        public override IEnumerator<V> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override void Remove(K key)
        {
            // Get the hashcode
            int iInitialHash = HashFunction(key);

            // Current location we are looking at in the collision chain
            int iCurrentLocation = iInitialHash;

            // How many attempts were made to increment
            int iAttempts = 1;

            // Indicator that the item was found
            Boolean found = false;

            while(!found &&  oDataArray[iCurrentLocation] != null)
            {
                // If the current location contains a key-value
                if(oDataArray[iCurrentLocation].GetType() == typeof(KeyValue<K,V>))
                {
                    // It is a key-value
                    KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[iCurrentLocation];

                    // Check if it is the key-value we are looking for
                    if (kv.Key.CompareTo(key) == 0)
                    {
                        oDataArray[iCurrentLocation] = new Tombstone();
                        found = true;
                        iCount--;
                    }
                }
                // Increment to the next location
                iCurrentLocation = iInitialHash + GetIncrement(iAttempts, key);
                iCurrentLocation %= HTSize;
            }

            if(!found)
            {
                throw new KeyNotFoundException("Key not found");
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
                    // If the current location contains a key-value
                    if(oDataArray[i].GetType() == typeof(KeyValue<K,V>))
                    {
                        KeyValue<K, V> kv = (KeyValue<K, V>)oDataArray[i];
                        sb.Append(kv.Value.ToString() + " IH = " + HashFunction(kv.Key));
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
    }
}
