using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class Program
    {
        class ArchivosBinariosEmpleados
        {
            //declaracion de flujos
            BinaryWriter bw = null;
            BinaryReader br = null;

            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearArchivo(string Archivo)
            {
                char resp;

                try
                {
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    do
                    {
                        Console.Clear();
                        Console.Write("Número del empleado: "); NumEmp = int.Parse(Console.ReadLine());

                        Console.Write("Nombre del empleado: "); Nombre = Console.ReadLine();

                        Console.Write("Direccion del empleado: "); Direccion = Console.ReadLine();

                        Console.Write("Telefono del Empleado: "); Telefono = long.Parse(Console.ReadLine());


                        Console.Write("Dias Trabajados del Empleado: "); DiasTrabajados = Int32.Parse(Console.ReadLine());


                        Console.Write("Salario Diario del Empleado: "); SalarioDiario = Int32.Parse(Console.ReadLine());


                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\nDeseas Almacenar otro Registro (s/n)");

                        resp = Char.Parse(Console.ReadLine());

                    } while (((resp == 's') || (resp == 'S')));
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                    Console.WriteLine("\nRuta: " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close();
                    Console.Write("\n Presione <enter> para terminar la Escritura de Datos y regresar al menu ");
                    Console.ReadKey();
                }
            }
            public void MostrarArchivos(string Archivo)
            {
                try
                {
                    if (File.Exists(Archivo))
                    {
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        Console.Clear();

                        do
                        {
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();


                            Console.WriteLine("Numero de empleado: " + NumEmp);
                            Console.WriteLine("Nombre del empleado: " + Nombre);
                            Console.WriteLine("Direccion: " + Direccion);
                            Console.WriteLine("Telefono: " + Telefono);
                            Console.WriteLine("Dias trabajados: " + DiasTrabajados);
                            Console.WriteLine("Salario diario del empleado: {0:C} ", SalarioDiario);
                            Console.WriteLine("Sueldo total del empleado: {0:C}", (DiasTrabajados * SalarioDiario));

                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo" + Archivo + "No Existe en el disco!!");

                        Console.WriteLine("\nPresione <enter> para continuar.....");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado Empleados");
                    Console.Write("\n Presione <enter> para continuar");
                }
                finally
                {
                    if (br != null) br.Close();//cierra el flujo
                    Console.Write("\nPresione ENTER para terminar la lectura de datos y regresar al menu");
                    Console.ReadKey();

                }
            }
        }
        static void Main(string[] args)
        {
            string Arch = null;
            int opcion;

            ArchivosBinariosEmpleados A1 = new ArchivosBinariosEmpleados();

            do
            {
                Console.Clear();
                Console.WriteLine("\n***ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1 Creacion del archivo");
                Console.WriteLine("2 Lectura de archivo");
                Console.WriteLine("3 Salida del programa");
                Console.Write("\nQue opción desea: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura de archivo 
                            Console.Write("\nAlimenta el nombre del archivo a crear: "); Arch = Console.ReadLine();

                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlos (s/n) ?");
                                resp = char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                            {
                                A1.CrearArchivo(Arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //bloque de lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                            Arch = Console.ReadLine();
                            A1.MostrarArchivos(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: " + e.Message);
                            Console.WriteLine("\nRuta: " + e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("\nPresione <ENTER> para Salir del Programa...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción no Existe !!, Presione <ENTER> para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
        }
    }
}

