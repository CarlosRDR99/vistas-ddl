using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Program
    {
        static void Main(string[] args)
        {
            PL.Usuario.CargaMasiva();
            //byte Opcion;
            //do
            //{
            //    Console.WriteLine("1 = Agregar Usuario");
            //    Console.WriteLine("2 = Eliminar Usuario");
            //    Console.WriteLine("3 = Editar Usuario");
            //    Console.WriteLine("4 = Ver usuarios");
            //    Console.WriteLine("5 = Mostrar un usuario");
            //    Console.WriteLine("0 = Salir");
            //    Console.WriteLine("---------------------------");
            //    Console.WriteLine("Digite su opción:");
            //    Opcion = byte.Parse(Console.ReadLine());
            //    Console.WriteLine("---------------------------");

            //    switch (Opcion)
            //    {
            //            default:
            //            break;
            //            case 1:
            //            PL.Usuario.Add();
            //            break;
            //            case 2:
            //            PL.Usuario.Delete();
            //            break;
            //            case 3:
            //            PL.Usuario.Update();
            //            break;
            //            case 4:
            //            PL.Usuario.GetAll();
            //            break;
            //            case 5:
            //            PL.Usuario.GetByID();
            //            break;
            //    }
            //    Console.Clear();
            //} while (Opcion!=0);

        }
    }
}
