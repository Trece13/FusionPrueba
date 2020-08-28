using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (whusa.DAL.BaseDAL.BaseDal.creaConexion())
                    Console.Write("Conectado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            Console.WriteLine("Fin");
            Console.ReadKey();
        }
    }
}
