using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    class Program
    {
        static void TestAdd(LinkedList<int> obList)
        {
            obList.Add(3);
            obList.Add(5);
            obList.Add(7);
            obList.Add(10);
            obList.Add(15);
            obList.Add(11);
        }

        static void TestInsert(LinkedList<int> obList)
        {
            //obList.Insert(2, 9);
            //obList.Insert(0, 9);
            //obList.Insert(5, 9);
            //obList.Insert(6, 9);

            //try
            //{
            //    obList.Insert(-1, 9);
            //}
            //catch(IndexOutOfRangeException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        static void TestRemove(LinkedList<int> obList)
        {
            Console.WriteLine(obList.Remove(11));
        }

        static void TestRemoveAt(LinkedList<int> obList)
        {
            //obList.RemoveAt(0);
            //obList.RemoveAt(6);
            //obList.RemoveAt(3);

            //try
            //{
            //    obList.RemoveAt(-2);
            //    obList.RemoveAt(7);
            //}
            //catch (IndexOutOfRangeException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        static void TestClear(LinkedList<int> obList)
        {
            obList.Clear();
        }

        static void TestReplaceAt(LinkedList<int> obList)
        {
            //obList.ReplaceAt(0, 1);
            //obList.ReplaceAt(5, 1);
            //obList.ReplaceAt(6, 1);
            //obList.ReplaceAt(3, 1);

            //try
            //{
            //    obList.ReplaceAt(-1, 1);
            //    obList.ReplaceAt(7, 1);
            //}
            //catch(IndexOutOfRangeException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        public static void Main(string[] args)
        {
            LinkedList<int> obList = new LinkedList<int>();
            TestAdd(obList);
            //TestInsert(obList);

            //TestClear(obList);
            //TestRemove(obList);
            //TestRemoveAt(obList);
            //TestReplaceAt(obList);
            Console.WriteLine(obList.Count);
            Console.WriteLine(obList.ToString());
        }
    }
}
