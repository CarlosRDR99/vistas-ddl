using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Digita el Nombre de usuario");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Digita el nombre");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Digita el apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Digita el apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Digita el email");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Digita la contraseña");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Digita el sexo (M|F)");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Digita el telefono");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Digita el celular");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Digita la fecha de nacimiento (yyyy-MM-dd)");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Digita el CURP");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("Digita tu Rol ID");
            usuario.Rol = new Rol();
            usuario.Rol.IdRol = byte.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.Add(usuario);
            //ML.Result result = BL.Usuario.AddEF(usuario);
            ML.Result result = BL.Usuario.AddLINQ(usuario);
            if (result.Correct) Console.WriteLine("Usuario insertado exitosamente");
            else Console.WriteLine("No se pudo agregar al usuario");

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Delete()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Digita el ID del usuario a elminar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.Delete(usuario);
            //ML.Result result = BL.Usuario.DeleteEF(usuario);
            ML.Result result = BL.Usuario.DeleteLINQ(usuario);
            if (result.Correct) Console.WriteLine("Eliminado exitosamente");
            else Console.WriteLine("No se encontró al usuario y no se pudo elminar");

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Digita el ID del usuario a modificar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());
            Console.WriteLine("Digita el User name");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Digita el nombre");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Digita el apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Digita el apellido materno");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Digita el email");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Digita la contraseña");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Digita el sexo (M|F)");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Digita el telefono");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Digita el celular");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Digita la fecha de nacimiento (yyyy-MM-dd)");
            usuario.FechaNacimiento = Console.ReadLine();
            Console.WriteLine("Digita el CURP");
            usuario.CURP = Console.ReadLine();
            Console.WriteLine("Digita tu Rol ID");
            usuario.Rol = new Rol();
            usuario.Rol.IdRol = byte.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.Update(usuario);
            //ML.Result result = BL.Usuario.UpdateEF(usuario);
            ML.Result result = BL.Usuario.UpdateLINQ(usuario);
            if (result.Correct) Console.WriteLine("El usuario se actualizo correctamente");
            else Console.WriteLine("No se se pudo actualizar el usuario");

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            //ML.Result result = BL.Usuario.GetAllEF();
            //ML.Result result = BL.Usuario.GetAllLINQ();
            if (result.Correct)
            {
                //por cada fila en dt:
                foreach (ML.Usuario fila in result.Objects)
                {

                    Console.WriteLine("ID: " + fila.IdUsuario);
                    Console.WriteLine("Nombre de Usuario: " + fila.UserName);
                    Console.WriteLine("Nombre: " + fila.Nombre);
                    Console.WriteLine("Apellido paterno: " + fila.ApellidoPaterno);
                    Console.WriteLine("Apellido materno: " + fila.ApellidoMaterno);
                    Console.WriteLine("Email: " + fila.Email);
                    Console.WriteLine("Password: " + fila.Password);
                    Console.WriteLine("Sexo: " + fila.Sexo);
                    Console.WriteLine("Telefono: " + fila.Telefono);
                    Console.WriteLine("Celular: " + fila.Celular);
                    DateTime SoloFecha = DateTime.Parse(fila.FechaNacimiento);
                    string DateNacimiento = SoloFecha.ToString("yyyy-MM-dd");
                    Console.WriteLine("Fecha de Nacimiento: " + DateNacimiento);
                    Console.WriteLine("CURP: " + fila.CURP);
                    Console.WriteLine("Rol ID: " + fila.Rol.IdRol);
                    Console.WriteLine("Rol: " + fila.Rol.RolNombre);
                    Console.WriteLine("---------------------------");
                }
            }
            else Console.WriteLine("No se puedo acceder a los usuarios");

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void GetByID()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Digita el ID del usuario a mostrar");
            usuario.IdUsuario = int.Parse(Console.ReadLine());

            //ML.Result result = BL.Usuario.GetByID(usuario);
            ML.Result result = BL.Usuario.GetByIDEF(usuario);
            //ML.Result result = BL.Usuario.GetByIDLINQ(usuario);
            if (result.Correct)
            {
                //usuario = (ML.Usuario)result.Object;
                Console.WriteLine($"ID: {usuario.IdUsuario}");
                Console.WriteLine($"Nombre de usuario: {usuario.UserName}");
                Console.WriteLine($"Nombre: {usuario.Nombre}");
                Console.WriteLine($"Apellido Paterno: {usuario.ApellidoPaterno}");
                Console.WriteLine($"Apellido Materno: {usuario.ApellidoMaterno}");
                Console.WriteLine($"Email: {usuario.Email}");
                Console.WriteLine($"Password: {usuario.Password}");
                Console.WriteLine($"Sexo: {usuario.Sexo}");
                Console.WriteLine($"Telefono: {usuario.Telefono}");
                Console.WriteLine($"Celular: {usuario.Celular}");
                DateTime SoloFecha = DateTime.Parse(usuario.FechaNacimiento);
                string DateNacimiento = SoloFecha.ToString("yyyy-MM-dd");
                Console.WriteLine("Fecha de Nacimiento: " + DateNacimiento);
                Console.WriteLine($"CURP: {usuario.CURP}");
                Console.WriteLine($"Rol ID: {usuario.Rol.IdRol}");
                Console.WriteLine($"Rol: {usuario.Rol.RolNombre}");
                Console.WriteLine("---------------------------");
            }
            else Console.WriteLine("No se encontró a un usuario con ese ID ");

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static ML.Result CargaMasiva()
        {
            string path = @"C:\Users\digis\Documents\Carlos Rodrigo Dominguez Romero\CDominguezProgramacionNCapas\PL\Files\cargaMasiva.txt";
            StreamReader txt = new StreamReader(path);
            txt.ReadLine();
            string linea;
            ML.Result result = new ML.Result();
            while ((linea = txt.ReadLine()) != null)
            {
                string[] fila = linea.Split('|');
                ML.Producto producto = new ML.Producto();
                producto.Nombre = fila[0];
                producto.Descripcion = fila[1];
                producto.Precio = decimal.Parse(fila[2]);
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Id = Convert.ToInt32(fila[3]);
                result = BL.Producto.Add(producto);
            }
            if (result.Correct)
            {
                Console.WriteLine("Carga exitosa");
            }
            else
            {
                Console.WriteLine("Hubo un problema");
            }
            return result;
        }
    }
}
