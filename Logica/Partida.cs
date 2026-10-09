using System;

namespace TresEnRaya
{
    /// <summary>
    /// Coordina una sesión individual del juego: turnos, entrada del usuario
    /// y comprobación de victoria o empate.
    /// </summary>
    public class Partida
    {
        public Tablero Tablero { get; }
        private readonly Jugador jugador1;
        private readonly Jugador jugador2;
        private Jugador jugadorActual;

        /// <exception cref="ArgumentNullException">Si algún jugador es nulo.</exception>
        /// <exception cref="ArgumentException">Si ambos jugadores usan el mismo símbolo.</exception>
        public Partida(Jugador jugador1, Jugador jugador2)
        {
            this.jugador1 = jugador1 ?? throw new ArgumentNullException(nameof(jugador1));
            this.jugador2 = jugador2 ?? throw new ArgumentNullException(nameof(jugador2));

            if (jugador1.Simbolo == jugador2.Simbolo)
                throw new ArgumentException("Los jugadores deben tener símbolos distintos.");

            Tablero = new Tablero();
            jugadorActual = jugador1;
        }

        /// <summary>Ejecuta el bucle principal hasta que haya un ganador o un empate.</summary>
        public void Iniciar()
        {
            Tablero.ReiniciarTablero();
            jugadorActual = jugador1;
            Tablero.Dibujar();

            while (true)
            {
                SolicitarJugada(jugadorActual, out int fila, out int columna);
                Tablero.MarcarCasilla(fila, columna, jugadorActual.Simbolo);
                Tablero.Dibujar();

                if (Tablero.HayVictoria(jugadorActual.Simbolo))
                {
                    Console.WriteLine($"¡{jugadorActual.Nombre} ({jugadorActual.Simbolo}) ha ganado la partida!");
                    break;
                }

                if (Tablero.EstaLleno())
                {
                    Console.WriteLine("¡Empate! No quedan casillas libres.");
                    break;
                }

                CambiarTurno();
            }
        }

        /// <summary>
        /// Pide una posición (1-9) hasta recibir una válida y la convierte en fila/columna.
        /// </summary>
        private void SolicitarJugada(Jugador jugador, out int fila, out int columna)
        {
            while (true)
            {
                Console.Write($"Turno de {jugador}. Elige una casilla (1-9): ");

                try
                {
                    int posicion = int.Parse(Console.ReadLine() ?? string.Empty);

                    if (posicion < 1 || posicion > 9)
                        throw new ArgumentOutOfRangeException(nameof(posicion), "El número debe estar entre 1 y 9.");

                    fila = (posicion - 1) / 3;
                    columna = (posicion - 1) % 3;

                    if (!Tablero.EsCasillaValida(fila, columna))
                        throw new InvalidOperationException("Esa casilla ya está ocupada.");

                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada no válida: escribe un número, no texto.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Entrada no válida: el número es demasiado grande.");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void CambiarTurno()
        {
            jugadorActual = jugadorActual == jugador1 ? jugador2 : jugador1;
        }
    }
}
