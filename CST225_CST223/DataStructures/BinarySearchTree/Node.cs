using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Node<T> where T : IComparable<T>
    {
        #region Attributes
        private T tData;
        private Node<T> nLeft;
        private Node<T> nRight;
        #endregion



        #region Constructors
        public Node() : this(default(T), null, null) { }
        public Node(T tData) : this(tData, null, null) { }
        public Node(T tData, Node<T> nLeft, Node<T> nRight)
        {
            this.tData = tData;
            this.nLeft = nLeft;
            this.nRight = nRight;
        }
        #endregion



        #region Properties
        // Note that get/set are both included. You should decide if you need both.
        // Property for data
        public T Data { get => tData; set => tData = value; }

        // Property for left node
        public Node<T> Left { get => nLeft; set => nLeft = value; }

        // Property for right node
        public Node<T> Right { get => nRight; set => nRight = value; }
        #endregion

        #region Other Functionality
        public bool IsLeaf()
        {
            return this.Left == null && this.Right == null;
        }
        #endregion
    }
}
