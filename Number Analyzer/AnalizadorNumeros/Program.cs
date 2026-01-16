using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Analizador de Números ===\n");

        int n = LeerEntero("¿Cuántos números quieres introducir? ");
        while (n <= 0)
        {
            Console.WriteLine("Debe ser un entero mayor que 0.\n");
            n = LeerEntero("¿Cuántos números quieres introducir? ");
        }

        int[] numeros = new int[n];

        int suma = 0;
        int minimo = int.MaxValue;
        int maximo = int.MinValue;

        for (int i = 0; i < n; i++)
        {
            numeros[i] = LeerEntero($"Introduce el número #{i + 1}: ");
            suma += numeros[i];
            if (numeros[i] < minimo) minimo = numeros[i];
            if (numeros[i] > maximo) maximo = numeros[i];
        }

        double media = (double)suma / n;

        Console.WriteLine("\n--- Resumen inicial ---");
        Console.WriteLine($"Cantidad: {n}");
        Console.WriteLine($"Suma: {suma}");
        Console.WriteLine($"Media: {media:F2}");
        Console.WriteLine($"Mínimo: {minimo}");
        Console.WriteLine($"Máximo: {maximo}");

        int opcion;
        do
        {
            MostrarMenu();
            opcion = LeerEntero("Elige una opción: ");
            Console.WriteLine();

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("Números introducidos:");
                    foreach (int num in numeros)
                        Console.Write(num + " ");
                    Console.WriteLine("\n");
                    break;

                case 2:
                    int objetivo = LeerEntero("Número a buscar: ");
                    int conteo = 0;
                    for (int i = 0; i < numeros.Length; i++)
                        if (numeros[i] == objetivo) conteo++;

                    if (conteo > 0)
                        Console.WriteLine($"El número {objetivo} aparece {conteo} vez/veces.\n");
                    else
                        Console.WriteLine("No se encontraron coincidencias.\n");
                    break;

                case 3:
                    Console.WriteLine("Pares:");
                    foreach (int num in numeros)
                        if (num % 2 == 0) Console.Write(num + " ");
                    Console.WriteLine();

                    Console.WriteLine("Impares:");
                    foreach (int num in numeros)
                        if (num % 2 != 0) Console.Write(num + " ");
                    Console.WriteLine("\n");
                    break;

                case 4:
                    int t = LeerEntero("¿Tabla de qué número? ");
                    Console.WriteLine($"Tabla del {t}:");
                    for (int i = 1; i <= 10; i++)
                        Console.WriteLine($"{t} x {i} = {t * i}");
                    Console.WriteLine();
                    break;

                case 0:
                    Console.WriteLine("Saliendo... ¡Gracias!\n");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Intenta de nuevo.\n");
                    break;
            }

        } while (opcion != 0);
    }

    static int LeerEntero(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int valor))
                return valor;

            Console.WriteLine("Entrada no válida. Introduce un número entero.");
        }
    }

    static void MostrarMenu()
    {
        Console.WriteLine("--- Menú ---");
        Console.WriteLine("1) Ver todos los números");
        Console.WriteLine("2) Buscar un número (contar apariciones)");
        Console.WriteLine("3) Mostrar pares e impares");
        Console.WriteLine("4) Tabla de multiplicar de un número");
        Console.WriteLine("0) Salir");
    }
}
