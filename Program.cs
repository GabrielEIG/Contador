using Contadores_simultáneos_con_hilos;
using System;

class Program
{
    Thread[] hilos = new Thread[5];
    Counter[] contadores = new Counter[5]; 
    static void Main(string[] args)
    {
        Program program = new Program();
        program.Menu();
    }

    void Menu()
    {
        Console.Clear();
        Console.WriteLine("------------------------");
        Console.WriteLine("--- Menu de contadoresss ---");
        Console.WriteLine("------------------------");
        Console.WriteLine("1- Iniciar contadorrr.");
        Console.WriteLine("2- Detener un contador.");
        Console.WriteLine("3- Mostrar el estado actual de los contadores.");
        Console.WriteLine("4- Salir del programa.");
        Console.Write("Seleccione la opción: ");

        int opcion = int.Parse(Console.ReadLine() ?? "4");

        switch (opcion)
        {
            case 1:
                IniciarContadores();
                break;
            case 2:
                DetenerContador();
                break;
            case 3:
                MostrarEstado();
                break;
            case 4:
                Console.WriteLine("Saliendo del programa...");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Opción inválida. Intente de nuevo.");
                Menu();
                break;
        }
    }

    void IniciarContadores()
    {
        Console.Write("¿Cuántos contadores desea iniciar (1-5)? ");
        int counterIndex = int.Parse(Console.ReadLine() ?? "0") - 1;


            if (hilos[counterIndex] == null || !contadores[counterIndex]?.IsActive == true)
            {
                contadores[counterIndex] = new Counter(counterIndex + 1); 
                hilos[counterIndex] = new Thread(contadores[counterIndex].Increment); 
                hilos[counterIndex].Start();
                Console.WriteLine($"Contador {counterIndex + 1} iniciado.");
            }

        Console.WriteLine("Presione cualquier tecla para volver al menu.\n");
        Console.ReadKey();
        Menu();
    }

    void DetenerContador()
    {
        Console.Write("Ingrese el número del contador a detener (1-5): ");
        int indice = int.Parse(Console.ReadLine() ?? "0") - 1;

        if (indice >= 0 && indice < hilos.Length && contadores[indice]?.IsActive == true)
        {
            contadores[indice].IsActive = false;  
            hilos[indice].Join();  
            hilos[indice] = null;  
            Console.WriteLine($"Contador {indice + 1} detenido.");
        }
        else
        {
            Console.WriteLine("Contador inválido o ya detenido.");
        }
        Console.WriteLine("Presione cualquier tecla para volver al menu.\n");
        Console.ReadKey();

        Menu();
    }

    void MostrarEstado()
    {
        Console.Clear();

        while (!Console.KeyAvailable)  
        {
            Console.SetCursorPosition(0, 2);  
            for (int i = 0; i < contadores.Length; i++)
            {
                if (contadores[i] != null)
                {
                    string estado = contadores[i].IsActive ? "Activo" : "Inactivo";
                    Console.WriteLine($"Contador {contadores[i].Id}: {estado}, Valor: {contadores[i].Count}");
                }
                else
                {
                    Console.WriteLine($"Contador {i + 1}: No iniciado, Valor: 0");
                }
            }

            Console.WriteLine("Presione cualquier tecla para volver al menu.\n");
            Thread.Sleep(1000);  
            Console.Clear();
        }

        Console.ReadKey(true);  
        Menu();
    }
}
