using System;

namespace TresEnRaya
{
    /// <summary>
    /// Gestiona la navegación de la aplicación: menú principal, configuración
    /// de jugadores y el menú posterior a cada partida.
    /// </summary>
    public class MenuAplicacion
    {
        private enum OpcionPrincipal { Jugar = 1, Instrucciones = 2, Salir = 3 }
        private enum OpcionFinal { VolverAJugar = 1, MenuPrincipal = 2, Salir = 3 }

        /// <summary>Punto de entrada: mantiene la aplicación en marcha hasta que el usuario sale.</summary>
        public void Ejecutar()
        {
            bool salir = false;

            while (!salir)
            {
                switch (MostrarMenuPrincipal())
                {
                    case OpcionPrincipal.Jugar:
                        salir = JugarSesion();
                        break;
                    case OpcionPrincipal.Instrucciones:
                        MostrarInstrucciones();
                        break;
                    case OpcionPrincipal.Salir:
                        salir = true;
                        break;
                }
            }

            Console.WriteLine("¡Gracias por jugar! Hasta pronto.");
        }

        /// <summary>Muestra el menú y repite la petición hasta recibir una opción válida (1-3).</summary>
        private OpcionPrincipal MostrarMenuPrincipal()
        {
            Console.WriteLine();
            Console.WriteLine("=== TRES EN RAYA ===");
            Console.WriteLine("1. Jugar Partida");
            Console.WriteLine("2. Ver Instrucciones");
            Console.WriteLine("3. Salir");

            return (OpcionPrincipal)LeerOpcion("Elige una opción (1-3): ", 1, 3);
        }

        /// <summary>
        /// Pide los nombres de los dos jugadores y les asigna 'X' y 'O'.
        /// </summary>
        private (Jugador, Jugador) ConfigurarJugadores()
        {
            string nombre1 = LeerNombre("Nombre del Jugador 1 (X): ", null);
            string nombre2 = LeerNombre("Nombre del Jugador 2 (O): ", nombre1);

            return (new Jugador(nombre1, 'X'), new Jugador(nombre2, 'O'));
        }

        /// <summary>
        /// Configura jugadores y encadena partidas según la elección del usuario.
        /// Devuelve true si el usuario decide salir de la aplicación.
        /// </summary>
        private bool JugarSesion()
        {
            var (jugador1, jugador2) = ConfigurarJugadores();

            while (true)
            {
                new Partida(jugador1, jugador2).Iniciar();

                switch (MostrarMenuFinal())
                {
                    case OpcionFinal.VolverAJugar:
                        continue;
                    case OpcionFinal.MenuPrincipal:
                        return false;
                    default:
                        return true;
                }
            }
        }

        private OpcionFinal MostrarMenuFinal()
        {
            Console.WriteLine();
            Console.WriteLine("¿Qué quieres hacer ahora?");
            Console.WriteLine("1. Volver a jugar");
            Console.WriteLine("2. Ir al menú principal");
            Console.WriteLine("3. Salir");

            return (OpcionFinal)LeerOpcion("Elige una opción (1-3): ", 1, 3);
        }

        private void MostrarInstrucciones()
        {
            Console.WriteLine();
            Console.WriteLine("=== INSTRUCCIONES ===");
            Console.WriteLine("- Juegan dos jugadores por turnos: X empieza y después juega O.");
            Console.WriteLine("- En tu turno, escribe el número (1-9) de la casilla libre que quieras marcar.");
            Console.WriteLine("- Gana quien consiga tres símbolos en línea: fila, columna o diagonal.");
            Console.WriteLine("- Si se llenan las nueve casillas sin ganador, es empate.");
            Console.WriteLine();
            Console.Write("Pulsa Enter para volver al menú...");
            Console.ReadLine();
        }

        /// <summary>Lee un entero dentro de [min, max]; nunca lanza excepciones por entrada inválida.</summary>
        private static int LeerOpcion(string mensaje, int min, int max)
        {
            while (true)
            {
                Console.Write(mensaje);

                // TryParse evita excepciones con texto, vacío o números fuera del rango de int
                if (int.TryParse(Console.ReadLine(), out int opcion) && opcion >= min && opcion <= max)
                    return opcion;

                Console.WriteLine($"Opción no válida. Introduce un número entre {min} y {max}.");
            }
        }

        /// <summary>Pide un nombre no vacío y distinto de <paramref name="nombreProhibido"/>.</summary>
        private static string LeerNombre(string mensaje, string nombreProhibido)
        {
            while (true)
            {
                Console.Write(mensaje);
                string nombre = (Console.ReadLine() ?? string.Empty).Trim();

                if (nombre.Length == 0)
                    Console.WriteLine("El nombre no puede estar vacío.");
                else if (string.Equals(nombre, nombreProhibido, StringComparison.OrdinalIgnoreCase))
                    Console.WriteLine("Ese nombre ya lo usa el otro jugador. Elige uno distinto.");
                else
                    return nombre;
            }
        }
    }
}
