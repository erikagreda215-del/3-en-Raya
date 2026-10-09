using System;
using System.Text;

namespace TresEnRaya
{
    /// <summary>Punto de entrada de la aplicación. No contiene lógica de juego.</summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Permite mostrar correctamente tildes, ñ y signos como ¡ y ¿
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            if (args.Length > 0 && args[0] == "--pruebas")
            {
                PruebasMVP.Ejecutar();
                return;
            }

            new MenuAplicacion().Ejecutar();
        }
    }
}
