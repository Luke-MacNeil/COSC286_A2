using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTables
{
    class Program
    {
        //public void TestChainingHT()
        //{
        //    //Test the AVL Tree
        //    long start;
        //    long end;
        //    AVLT<int> balanceTree = new AVLT<int>();
        //    //BST<int> balanceTree = new BST<int>();
        //    start = Environment.TickCount;
        //    Random randomNumber = new Random((int)start);
        //    int iMax = 10000;
        //    int iLargest = iMax * 10;

        //    for (int i = 0; i < iMax; i++)
        //    {
        //        balanceTree.Add(randomNumber.Next(1, iLargest));
        //    }
        //    end = Environment.TickCount;

        //    Console.WriteLine("Time to add: " + (end - start).ToString() + " ms");
        //    Console.WriteLine("Theoretical Minimum Height: " + Math.Truncate(Math.Log(iMax, 2.0)));
        //    Console.WriteLine("Actual Height: " + balanceTree.Height());
        //}


        static void TestAdd(A_HashTable<int, string> ht)
        {
            ht.Add(11, "Rob");
            ht.Add(15, "Grace");
            ht.Add(15, "John");
            ht.Add(20, "Luke");
            ht.Add(25, "Cuong");
            ht.Add(26, "Jaryd");
            ht.Add(9, "Austin");
            //ht.Add(17, "Bryce");
        }

        static void TestExpand(A_HashTable<int, string> ht)
        {
            ht.Add(23, "Ron");
            ht.Add(26, "Jim");
            ht.Add(43, "Bob");
            ht.Add(33, "Jane");
            ht.Add(17, "Bryce");
        }

        static void TestRemove(A_HashTable<int, string> ht)
        {
            try
            {
                ht.Remove(11);
                ht.Remove(9);
                //ht.Remove(33);
            }
            catch(KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }
        }

        static void TestGet(A_HashTable<int, string> ht)
        {
            try
            {
                Console.WriteLine(ht.Get(25));
                //Console.WriteLine(ht.Get(33));
            }
            catch (KeyNotFoundException knfe)
            {
                Console.WriteLine(knfe.Message);
            }
        }

        static void LoadDataFromFile(A_HashTable<Person, Person> ht)
        {
            StreamReader sr = new StreamReader(File.Open("People.txt", FileMode.Open));
            string sInput = "";

            try
            {
                //Read a line from the file
                while ((sInput = sr.ReadLine()) != null)
                {
                    try
                    {
                        char[] cArray = { ' ' };
                        string[] sArray = sInput.Split(cArray);
                        int iSSN = Int32.Parse(sArray[0]);
                        Person p = new Person(iSSN, sArray[2], sArray[1]);
                        ht.Add(p, p);

                    }
                    catch (ApplicationException ae)
                    {
                        //Console.WriteLine("Exception: " + ae.Message);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sr.Close();
        }

        static void TestHT(A_HashTable<Person, Person> ht)
        {
            LoadDataFromFile(ht);
            //Console.WriteLine(ht);
            Console.WriteLine("Hash table type " + ht.GetType().ToString());
            Console.WriteLine("# of People = " + ht.Count);
            Console.WriteLine("Number of collisions: " + ht.NumCollisions + "\n");
        }

        static void Main(string[] args)
        {
            //Linear<Person, Person> linear = new Linear<Person, Person>();
            //Quadratic<Person, Person> quad = new Quadratic<Person, Person>();
            //DoubleHash<Person, Person> dh = new DoubleHash<Person, Person>();

            //TestHT(linear);
            //TestHT(quad);
            //TestHT(dh);

            ChainingHT<int, string> cht = new ChainingHT<int, string>();

            TestAdd(cht);
            //TestGet(cht);
            TestRemove(cht);
            Console.WriteLine(cht.ToString());






            //TestAdd(ht);
            //Console.WriteLine(ht.ToString());
            //TestExpand(ht);
            //Console.WriteLine(ht.ToString());
            //TestRemove(ht);
            //Console.WriteLine(ht.ToString());
            //TestGet(ht);
        }
    }
}
