using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_UndefinedBehaviour
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeMess();
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
