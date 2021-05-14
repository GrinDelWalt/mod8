using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod8
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository rep = new Repository();
            rep.Menu();
            Console.ReadKey();

            

        }
        //static void Test()
        //{
        //    string[] words = new string[10];
        //    words = words.Select((word, index) =>
        //    {
        //        Console.Write(string.Format("Введите строку {0}: ", (index + 1)));
        //        return Console.ReadLine();
        //    }).ToArray();
        //    string newStr = string.Join(Environment.NewLine, words.Select((word, index) =>
        //        (index + 1).ToString() + ": " + word).ToArray());
        //    Console.WriteLine("Массив строк: ");
        //    Console.WriteLine(newStr);
        //    Console.ReadKey(true);
        //}
    }
}
