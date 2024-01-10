using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JaProj
{
    internal class Program
    {

        [DllImport(@"C:\Users\Artur\source\repos\BloomEffect\x64\Debug\JAAsm.dll")]
        static extern int MyProc1(int a, int b, int c);
        static void Main(string[] args)
        {
            int a = 5, b=4, c=1;
            int retVal = MyProc1(a, b, c);
            Console.Write("Moja pierwsza wartość obliczona w asm to:");
            Console.WriteLine(retVal);
            Console.ReadLine();
        }
    }
}
