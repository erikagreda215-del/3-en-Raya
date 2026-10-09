using System;
using System.Text;

namespace TresEnRaya
{
    /// <summary>Punto de entrada de la aplicación. No contiene lógica de juego.</summary>
    internal static class Program
    {
        private static void Main()
        {
            // Permite mostrar correctamente tildes, ñ y signos como ¡ y ¿
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            new MenuAplicacion().Ejecutar();
        }
    }
}
