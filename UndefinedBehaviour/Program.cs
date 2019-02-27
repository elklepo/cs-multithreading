using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndefinedBehaviour_Example
{
    /*
     * Based on my question on StackOverflow:
     * https://stackoverflow.com/questions/43802728/exception-thrown-in-catch-and-finally-clr-behavior-vs-try-catch-block
     */
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MakeMess();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void MakeMess()
        {
            try
            {
                Console.WriteLine("try");
                throw new Exception(); // let's invoke catch
            }
            catch (Exception)
            {
                Console.WriteLine("catch");
                throw new Exception("A");
            }
            finally
            {
                Console.WriteLine("finally");
                throw new Exception("B");
            }
        }
    }
}