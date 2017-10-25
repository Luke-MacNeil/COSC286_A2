using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList<T> : A_List<T> where T : IComparable<T>
    {
        #region
        private Node head;
        #endregion

        public override void Add(T data)
        {
            head = RecAdd(head, data);
        }

        private Node RecAdd(Node current, T data)
        {
            // Base Case, when current is null
            if(current == null)
            {
                // Create a new node with the data
                current = new Node(data);
            }
            // Recursive part
            else
            {
                current.next = RecAdd(current.next, data);
            }
            return current;
        }

        public override void Clear()
        {
            head = null;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public override void Insert(int index, T data)
        {
            if (index > Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index out of bounds: " + index);
            }
            else
            {
                head = RecInsert(index, head, data);
            }
        }

        private Node RecInsert(int index, Node currNode, T data)
        {
            if(index == 0)
            {
                Node newNode = new Node(data, currNode);
                currNode = newNode;
            }
            else
            {
                currNode.next = RecInsert(index - 1, currNode.next, data);
            }
            return currNode;
        }

        public override bool Remove(T data)
        {
            return RecRemove(ref head, data);
        }

        // recurse current.next by reference
        private bool RecRemove(ref Node current, T data)
        {
            bool found = false;

            if(current != null)
            {
                // Base case
                // if the current node contains the data to remove
                if(current.data.CompareTo(data) == 0)
                {
                    // set found to true
                    found = true;

                    // bypass the current node
                    current = current.next;
                }
                // Recursive case
                else
                {
                    // recursivly remove from remainder of the list
                    found = RecRemove(ref current.next, data);
                }
            }

            return found;
        }

        public override T RemoveAt(int index)
        {
            T removed = default(T);

            // if index is out of bounds display an error
            if(index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException("The index of: " + index + " is out of bounds");
            }
            // remove node at the specified index
            else
            {
                removed = RecRemoveAt(ref head, index);
            }

            // return the node we removed
            return removed;
        }

        private T RecRemoveAt(ref Node current, int index)
        {
            T removed = default(T);

            // if the reference to the current's next node is not null
            if(current.next != null)
            {
                // if we have found the node to remove
                if(index == 0)
                {
                    // set the value of the removed node to the reference of the current node
                    removed = ElementAt(index);

                    // set the value of the current/removed node in the LinkedList to equal the next node
                    current = current.next;
                }
                // if we didn't find the node
                else
                {
                    // recursivly set the vlaue of the removed node to equal to next node in the LinkedList
                    removed = RecRemoveAt(ref current.next, index - 1);
                }
            }
            // if the reference to the next node is null (we are at the end of the list)
            else
            {
                // set the value of the removed node equal the reference to the current node
                removed = ElementAt(index);

                // set the value of the current/removed node in the LinkedList equal to the next node.
                // the value of current.next is equal to null since we are at the end of the LinkedList
                current = current.next;
            }

            // return the value of the node we removed
            return removed;
        }

        public override T ReplaceAt(int index, T data)
        {
            T replaced = default(T);
            // THIS IS THE NEXT METHOD TO TRY ON YOUR OWN
            if(index < 0 || index > Count)
            {
                throw new IndexOutOfRangeException("The index of: " + index + " is out of bounds");
            }
            else
            {
                RecReplaceAt(ref head, index, data);
            }

            return replaced;
        }

        private T RecReplaceAt(ref Node current, int index, T data)
        {
            T replaced = default(T);

            if(current != null)
            {
                if(index == 0)
                {
                    Node oldReference = current.next;

                    Node newNode = new Node(data, current.next);
                    current = newNode;

                    current.next = oldReference;
                }
                else
                {
                    replaced = RecReplaceAt(ref current.next, index - 1, data);
                }
            }
            else
            {
                Node newNode = new Node(data);
                current = newNode;
            }

            return replaced;
        }

        #region Enumerator Implementation
        private class Enumerator : IEnumerator<T>
        {
            // A reference to the linked list
            private LinkedList<T> parent;
            // A reference to the current node we are visiting
            private Node lastVisited;
            // The next node we want to visit
            private Node scout;

            public Enumerator(LinkedList<T> parent)
            {
                this.parent = parent;
                Reset();
            }

            public T Current
            {
                get
                {
                    return lastVisited.data;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return lastVisited.data;
                }
            }

            public void Dispose()
            {
                parent = null;
                scout = null;
                lastVisited = null;
            }

            public bool MoveNext()
            {
                bool result = false;

                // if the scout is not null, we are not at the end of the list
                if(scout != null)
                {
                    // We can definately move
                    result = true;
                    // Move our current node reference to the next node
                    lastVisited = scout;
                    // Move scout to its next node
                    scout = scout.next;
                }

                return result;
            }

            public void Reset()
            {
                // Set the node currently looked at to null
                lastVisited = null;
                // Set trhe scout to the head of the list
                scout = parent.head;
            }
        }
        #endregion


        #region Node Class
        // A class that represents the data and a reference to the nodes neighbour to the right
        private class Node
        {
            #region Attributes
            public T data;
            public Node next;
            #endregion

            // Demo constructer chaining
            // This will call the second constructor before performing the functionality of the first constructor.
            // The two braces at the end of the line signify that the first constructor doesn't need to do anything.
            // This should all go on a single line, because it looks better in C
            public Node(T data) : this(data, null) { }

            public Node(T data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }
        #endregion
    }
}
