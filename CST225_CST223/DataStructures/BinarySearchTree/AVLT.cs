using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class AVLT<T> : BST<T> where T: IComparable<T>
    {
        #region Balance Method
        /* Algorithm for Balance
         *  nNewRoot <-- current node
         *  if current node is not null
         *      iHeightDiff <-- GetHeightDiff(current node)
         *      if the tree is unbalanced to the right
         *          iRightChildHeightDiff <-- GetHeightDiff(current's right node)
         *          if the right child is left heavy
         *              nNewRoot <-- DoubleLeft(current)
         *          else
         *              nNewRoot <-- SingleLeft(current)
         *      else if the tree is unbalanced to the left
         *          iLeftChildHeightDiff <-- GetHeightDiff(current's left node)
         *          if the left child is right heavy
         *              nNewRoot <-- DoubleRight(current)
         *          else
         *              nNewRoot <-- SingleRight(current)
         *  return nNewRoot
         */
        internal override Node<T> Balance(Node<T> nCurrent)
        {
            //nNewRoot < --current node
            Node<T> nNewRoot = nCurrent;
            int iHeightDiff = 0;
            int iRightChildHeightDiff = 0;
            int iLeftChildHeightDiff = 0;

            //if current node is not null
            if (nCurrent != null)
            {
                // iHeightDiff < --GetHeightDiff(current node)
                iHeightDiff = GetHeightDifference(nCurrent);

                //if the tree is unbalanced to the right
                if (iHeightDiff < -1)
                {
                    //iRightChildHeightDiff <-- GetHeightDiff(current's right node)
                    iRightChildHeightDiff = GetHeightDifference(nCurrent.Right);

                    //if the right child is left heavy
                    if (iHeightDiff > 0)
                    {
                        //nNewRoot <-- DoubleLeft(current)
                        nNewRoot = DoubleLeft(nCurrent);
                    }
                    else
                    {
                        //nNewRoot <-- SingleLeft(current)
                        nNewRoot = SingleLeft(nCurrent);
                    }
                }
                //else if the tree is unbalanced to the left
                else if (iHeightDiff > 1)
                {
                    //iLeftChildHeightDiff <-- GetHeightDiff(current's left node)
                    iLeftChildHeightDiff = GetHeightDifference(nCurrent.Left);

                    //if the left child is right heavy
                    if (iHeightDiff < 0)
                    {
                        //nNewRoot <-- DoubleRight(current)
                        nNewRoot = DoubleRight(nCurrent);
                    }
                    else
                    {
                        //nNewRoot <-- SingleRight(current)
                        nNewRoot = SingleRight(nCurrent);
                    }
                }
            }

            //return nNewRoot
            return nNewRoot;
        }
        #endregion

        #region Height Balanced Methods
        /// <summary>
        /// Determines the height difference between the left and the right child nodes of the current node
        /// </summary>
        /// <param name="nCurrent"></param>
        /// <returns>Positive means left heavy, Negative means right heavy.
        /// The absolute value if the height difference.</returns>
        private int GetHeightDifference(Node<T> nCurrent)
        {
            int iHeightLeft = -1;
            int iHeightRight = -1;
            int iHeightDiff = 0;

            if(nCurrent != null)
            {
                if(nCurrent.Right != null)
                {
                    iHeightRight = RecHeight(nCurrent.Right);
                }
                if(nCurrent.Left != null)
                {
                    iHeightLeft = RecHeight(nCurrent.Left);
                }
            }

            iHeightDiff = iHeightLeft - iHeightRight;

            // if its is negative then the tree is right heavy
            // else it's left heavy
            return iHeightDiff;
        }

        /// <summary>
        /// Only for testing our helper methods. Not a part of an actual AVL Tree.
        /// </summary>
        public void TestHeightDiff()
        {
            Console.WriteLine(GetHeightDifference(nRoot));
        }
        #endregion


        #region Rotaion Methods
        private Node<T> SingleLeft(Node<T> nOldRoot)
        {
            // Get a reference to the old roots right node. This is the new root.
            Node<T> nNewRoot = nOldRoot.Right;

            // Assign new roots left child to old roots right child
            nOldRoot.Right = nNewRoot.Left;

            // New root's left child is set to the old root
            nNewRoot.Left = nOldRoot;

            // return the new root
            return nNewRoot;
        }

        private Node<T> SingleRight(Node<T> nOldRoot)
        {
            // Get a reference to the old roots left node. This is the new root.
            Node<T> nNewRoot = nOldRoot.Left;

            // Assign new roots right child to old roots left child
            nOldRoot.Left = nNewRoot.Left;

            // New root's right child is set to the old root
            nNewRoot.Right = nOldRoot;

            // return the new root
            return nNewRoot;
        }

        private Node<T> DoubleLeft(Node<T> nOldRoot)
        {
            nOldRoot.Right = SingleRight(nOldRoot.Right);
            return SingleLeft(nOldRoot);
        }

        private Node<T> DoubleRight(Node<T> nOldRoot)
        {
            nOldRoot.Left = SingleLeft(nOldRoot.Left);
            return SingleRight(nOldRoot);
        }
        #endregion
    }
}
