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
        static void TestAdd(A_HashTable<int, string> ht)
        {
            ht.Add(11, "Rob");
            ht.Add(15, "Grace");
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
                Console.WriteLine(ht.Get(23));
                Console.WriteLine(ht.Get(33));
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
            Linear<Person, Person> linear = new Linear<Person, Person>();
            Quadratic<Person, Person> quad = new Quadratic<Person, Person>();
            DoubleHash<Person, Person> dh = new DoubleHash<Person, Person>();

            TestHT(linear);
            TestHT(quad);
            TestHT(dh);










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
