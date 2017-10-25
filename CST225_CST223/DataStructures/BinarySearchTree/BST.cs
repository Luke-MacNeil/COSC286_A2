using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class BST<T> : A_BST<T>, ICloneable where T : IComparable<T>
    {
        /// <summary>
        /// Constructor - Not really necessary, but increases readanility
        /// </summary>
        public BST()
        {
            // Initialize the root node
            nRoot = null;

            // set count to 0
            iCount = 0;
        }

        #region Other Functionality
        public T FindSmallest()
        {
            if(nRoot != null)
            {
                return RecFindSmallest(nRoot);
            }
            else
            {
                throw new ApplicationException("Root is null");
            }
        }

        private T RecFindSmallest(Node<T> nCurrent)
        {
            if(nCurrent.Left == null)
            {
                return nCurrent.Data;
            }
            else
            {
                return RecFindSmallest(nCurrent.Left);
            }
        }

        public T FindLargest()
        {
            if (nRoot != null)
            {
                return RecFindLargest(nRoot);
            }
            else
            {
                throw new ApplicationException("Root is null");
            }
        }

        private T RecFindLargest(Node<T> nCurrent)
        {
            if (nCurrent.Right == null)
            {
                return nCurrent.Data;
            }
            else
            {
                return RecFindLargest(nCurrent.Right);
            }
        }
        #endregion

        #region A_BST Implementation
        public override void Add(T data)
        {
            if(nRoot == null)
            {
                nRoot = new Node<T>(data);
            }
            else
            {
                RecAdd(data, nRoot);
                // Balance here
                nRoot = Balance(nRoot);
            }

            iCount++;
        }

        private void RecAdd(T data, Node<T> nCurrent)
        {
            int iResult = data.CompareTo(nCurrent.Data);

            if (iResult < 0)
            {
                if (nCurrent.Left == null)
                {
                    nCurrent.Left = new Node<T>(data);
                }
                else
                {
                    RecAdd(data, nCurrent.Left);
                    // Balance here
                    nCurrent.Left = Balance(nCurrent.Left);
                }
            }
            else
            {
                if(nCurrent.Right == null)
                {
                    nCurrent.Right = new Node<T>(data);
                }
                else
                {
                    RecAdd(data, nCurrent.Right);
                    // Balance here
                    nCurrent.Right = Balance(nCurrent.Right);
                }
            }
        }

        // A virtual balance method that "May" be overriden in the child class
        // internal can be seen anywhere in the namespace
        // Note, for a BST, this method essentially does nothing. It is a placeholder for child classes.
        internal virtual Node<T> Balance(Node<T> nCurrent)
        {
            return nCurrent;
        }

        public override void Clear()
        {
            // Set the root to null
            nRoot = null;
            iCount = 0;
        }

        public override T Find(T data)
        {
            Boolean found = false;

            if (nRoot != null)
            {
                return RecFind(nRoot, data, ref found);
            }
            else
            {
                throw new ApplicationException("Root is null");
            }
        }

        private T RecFind(Node<T> nCurrent, T data, ref Boolean found)
        {
            T findMe = default(T);

            if (nCurrent != null)
            {
                // Find item
                int compare = data.CompareTo(nCurrent.Data);

                // if data item to remove is smaller than the current data
                if (compare < 0)
                {
                    nCurrent.Left = RecRemove(nCurrent.Left, data, ref found);
                }
                else if (compare > 0)
                {
                    nCurrent.Right = RecRemove(nCurrent.Right, data, ref found);
                }
                else // else we are on the item
                {
                    // Indicate that we found it
                    found = true;

                    findMe = nCurrent.Data;
                }
            }

            return findMe;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new BreadthFirstEnumerator(this);
        }

        public override int Height()
        {
            int iHeight = -1;

            if(nRoot != null)
            {
                iHeight = RecHeight(nRoot);
            }

            return iHeight;
        }

        protected int RecHeight(Node<T> nCurrent)
        {
            int iHeightLeft = 0;
            int iHeightRight = 0;

            // if the current node is not a leaf
            if (!nCurrent.IsLeaf())
            {
                // the current node has a left node, check it
                if (nCurrent.Left != null)
                {
                    iHeightLeft = RecHeight(nCurrent.Left) + 1;
                }

                // the current node has a right node, check it
                if (nCurrent.Right != null)
                {
                    iHeightRight = RecHeight(nCurrent.Right) + 1;
                }
            }

            // When we get them to the leaf, compare the two heights.
            // If the left is greater, return it, else return the right.
            // If they are the same, then returning the right is the same as returning the left.
            return iHeightLeft > iHeightRight ? iHeightLeft : iHeightRight;
        }

        public override void Iterate(ProcessData<T> pd, TRAVERSALORDER to)
        {
            if(nRoot != null)
            {
                RecIterate(nRoot, pd, to);
            }
        }

        private void RecIterate(Node<T> nCurrent, ProcessData<T> pd, TRAVERSALORDER to)
        {
            if(to == TRAVERSALORDER.PRE_ORDER)
            {
                // Process the data in the current node
                pd(nCurrent.Data);
            }

            // If current's left child exists, recurse left
            if(nCurrent.Left != null)
            {
                RecIterate(nCurrent.Left, pd, to);
            }

            if(to == TRAVERSALORDER.IN_ORDER)
            {
                // Process the data in the current node
                pd(nCurrent.Data);
            }

            // If current's right child exists, recurse right
            if (nCurrent.Right != null)
            {
                RecIterate(nCurrent.Right, pd, to);
            }

            if(to == TRAVERSALORDER.POST_ORDER)
            {
                // Process the data in the current node
                pd(nCurrent.Data);
            }
        }

        public override bool Remove(T data)
        {
            bool wasRemoved = false;
            nRoot = RecRemove(nRoot, data, ref wasRemoved);
            return wasRemoved;
        }

        private Node<T> RecRemove(Node<T> nCurrent, T data, ref bool wasRemoved)
        {
            T substitute = default(T);

            if(nCurrent != null)
            {
                // Find item to remove
                int compare = data.CompareTo(nCurrent.Data);

                // if data item to remove is smaller than the current data
                if(compare < 0)
                {
                    nCurrent.Left = RecRemove(nCurrent.Left, data, ref wasRemoved);
                }
                else if(compare > 0)
                {
                    nCurrent.Right = RecRemove(nCurrent.Right, data, ref wasRemoved);
                }
                else // else we are on the item to remove --- YAY!
                {
                    // Indicate that we found it
                    wasRemoved = true;

                    // Check the if current node is a leaf (base case)
                    if(nCurrent.IsLeaf())
                    {
                        // reduce the count of the tree
                        nCurrent = null;

                        // set its reference to null
                        iCount = iCount - 1;
                    }
                    // Else it is not a leaf
                    else
                    {
                        if(nCurrent.Left != null)
                        {
                            substitute = RecFindLargest(nCurrent.Left);
                            nCurrent.Data = substitute;
                            nCurrent.Left = RecRemove(nCurrent.Left, substitute, ref wasRemoved);
                        }
                        else
                        {
                            return nCurrent = nCurrent.Right;


                            //substitute = RecFindLargest(nCurrent.Right);
                            //nCurrent.Data = substitute;
                            //nCurrent.Right = RecRemove(nCurrent.Right, substitute, ref wasRemoved);
                        }
                    }
                }
            }

            return nCurrent;
        }
        #endregion


        #region ICloneable Implementation
        public object Clone()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Enumerator Implementation
        //private class DepthFirstEnumerator : IEnumerator<T>
        //{
        //    private BST<T> parent = null;
        //    private Node<T> nCurrent = null;
        //    private Stack<Node<T>> sNodes;

        //    public DepthFirstEnumerator(BST<T> parent)
        //    {
        //        this.parent = parent;
        //        Reset();
        //    }

        //    public T Current => nCurrent.Data;

        //    object IEnumerator.Current => nCurrent.Data;

        //    public void Dispose()
        //    {
        //        parent = null;
        //        nCurrent = null;
        //        sNodes = null;
        //    }

        //    public bool MoveNext()
        //    {
        //        bool bMoved = false;

        //        if(sNodes.Count > 0)
        //        {
        //            bMoved = true;
        //            nCurrent = sNodes.Pop();

        //            if(nCurrent.Right != null)
        //            {
        //                sNodes.Push(nCurrent.Right);
        //            }
        //            if(nCurrent.Left != null)
        //            {
        //                sNodes.Push(nCurrent.Left);
        //            }
        //        }

        //        return bMoved;
        //    }

        //    public void Reset()
        //    {
        //        // Set up the enumerator
        //        sNodes = new Stack<Node<T>>();

        //        // Push the root node on the stack
        //        if(parent.nRoot != null)
        //        {
        //            sNodes.Push(parent.nRoot);
        //        }

        //        // Set current to null
        //        nCurrent = null;
        //    }
        //}
        #endregion

        #region Enumerator Implementation
        private class BreadthFirstEnumerator : IEnumerator<T>
        {
            private BST<T> parent = null;
            private Node<T> nCurrent = null;
            private Queue<Node<T>> qNodes;

            public BreadthFirstEnumerator(BST<T> parent)
            {
                this.parent = parent;
                Reset();
            }

            public T Current => nCurrent.Data;

            object IEnumerator.Current => nCurrent.Data;

            public void Dispose()
            {
                parent = null;
                nCurrent = null;
                qNodes = null;
            }

            public bool MoveNext()
            {
                bool bMoved = false;

                if (qNodes.Count > 0)
                {
                    bMoved = true;
                    nCurrent = qNodes.Dequeue();

                    if (nCurrent.Right != null)
                    {
                        qNodes.Enqueue(nCurrent.Right);
                    }
                    if (nCurrent.Left != null)
                    {
                        qNodes.Enqueue(nCurrent.Left);
                    }
                }

                return bMoved;
            }

            public void Reset()
            {
                // Set up the enumerator
                qNodes = new Queue<Node<T>>();

                // Push the root node on the stack
                if (parent.nRoot != null)
                {
                    qNodes.Enqueue(parent.nRoot);
                }

                // Set current to null
                nCurrent = null;
            }
        }
        #endregion
    }
}
