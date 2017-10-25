using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Program
    {
        static void TestAdd(BST<int> bst)
        {
            bst.Add(10);
            bst.Add(5);
            bst.Add(15);
            bst.Add(1);
            bst.Add(7);
            bst.Add(11);
            bst.Add(20);
            bst.Add(17);
            bst.Add(19);

        }

        static void DoSomethingToAnInt(int x)
        {
            Console.Write(x + " ");
        }

        static void TestTraversal(BST<int> bst)
        {
            bst.Iterate(DoSomethingToAnInt, TRAVERSALORDER.PRE_ORDER);
        }

        static void TestFindSmallest(BST<int> bst)
        {
            int x = bst.FindSmallest();
            Console.WriteLine(x);
        }

        static void TestHeight(BST<int> bst)
        {
            Console.WriteLine(bst.Height());
        }

        static void TestRandomAVLT()
        {
            //Test the AVL Tree
            long start;
            long end;
            AVLT<int> balanceTree = new AVLT<int>();
            //BST<int> balanceTree = new BST<int>();
            start = Environment.TickCount;
            Random randomNumber = new Random((int)start);
            int iMax = 10000;
            int iLargest = iMax * 10;

            for (int i = 0; i < iMax; i++)
            {
                balanceTree.Add(randomNumber.Next(1, iLargest));
            }
            end = Environment.TickCount;

            Console.WriteLine("Time to add: " + (end - start).ToString() + " ms");
            Console.WriteLine("Theoretical Minimum Height: " + Math.Truncate(Math.Log(iMax, 2.0)));
            Console.WriteLine("Actual Height: " + balanceTree.Height());
        }
        static void Main(string[] args)
        {
            BST<int> myBst = new BST<int>();
            TestAdd(myBst);
            Console.WriteLine(myBst.ToString());
            //TestTraversal(myBst);

            //TestFindSmallest(myBst);
            //TestHeight(myBst);

            TestRandomAVLT();
        }
    }
}
