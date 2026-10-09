using System;

namespace TresEnRaya
{
    /// <summary>
    /// Representa el tablero 3x3 del Tres en Raya: guarda las casillas,
    /// las dibuja y comprueba victoria o empate.
    /// </summary>
    public class Tablero
    {
        private const int Tamano = 3;
        private const char CasillaVacia = ' ';

        private readonly char[,] matriz = new char[Tamano, Tamano];

        public Tablero()
        {
            InicializarTablero();
        }

        /// <summary>Deja todas las casillas vacías.</summary>
        public void InicializarTablero()
        {
            for (int fila = 0; fila < Tamano; fila++)
                for (int columna = 0; columna < Tamano; columna++)
                    matriz[fila, columna] = CasillaVacia;
        }

        /// <summary>Vacía el tablero para empezar una nueva partida.</summary>
        public void ReiniciarTablero() => InicializarTablero();

        /// <summary>
        /// Imprime el tablero en consola. Las casillas libres muestran su número (1-9)
        /// para que el jugador sepa qué posición elegir.
        /// </summary>
        public void Dibujar()
        {
            Console.WriteLine();
            for (int fila = 0; fila < Tamano; fila++)
            {
                for (int columna = 0; columna < Tamano; columna++)
                {
                    Console.Write(" ");
                    EscribirCasilla(fila, columna);
                    Console.Write(columna < Tamano - 1 ? " |" : " ");
                }
                Console.WriteLine();

                if (fila < Tamano - 1)
                    Console.WriteLine("---+---+---");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Devuelve true si las coordenadas están en el rango 0-2 y la casilla está libre.
        /// </summary>
        public bool EsCasillaValida(int fila, int columna)
        {
            return EstaEnRango(fila) && EstaEnRango(columna)
                && matriz[fila, columna] == CasillaVacia;
        }

        /// <summary>
        /// Coloca el símbolo en la casilla indicada.
        /// </summary>
        /// <exception cref="InvalidOperationException">Si la casilla no es válida o está ocupada.</exception>
        public void MarcarCasilla(int fila, int columna, char simbolo)
        {
            if (!EsCasillaValida(fila, columna))
                throw new InvalidOperationException($"La casilla ({fila}, {columna}) no es válida o ya está ocupada.");

            matriz[fila, columna] = simbolo;
        }

        /// <summary>Comprueba si el símbolo completa alguna fila, columna o diagonal.</summary>
        public bool HayVictoria(char simbolo)
        {
            // Filas y columnas
            for (int i = 0; i < Tamano; i++)
            {
                if (matriz[i, 0] == simbolo && matriz[i, 1] == simbolo && matriz[i, 2] == simbolo)
                    return true;
                if (matriz[0, i] == simbolo && matriz[1, i] == simbolo && matriz[2, i] == simbolo)
                    return true;
            }

            // Diagonales
            return (matriz[0, 0] == simbolo && matriz[1, 1] == simbolo && matriz[2, 2] == simbolo)
                || (matriz[0, 2] == simbolo && matriz[1, 1] == simbolo && matriz[2, 0] == simbolo);
        }

        /// <summary>Devuelve true si no queda ninguna casilla libre (empate si nadie ganó).</summary>
        public bool EstaLleno()
        {
            foreach (char casilla in matriz)
                if (casilla == CasillaVacia)
                    return false;
            return true;
        }

        private static bool EstaEnRango(int indice) => indice >= 0 && indice < Tamano;

        /// <summary>
        /// Escribe una casilla: 'X' en azul, 'O' en rojo y, si está libre,
        /// su número de posición (1-9) con el color por defecto.
        /// </summary>
        private void EscribirCasilla(int fila, int columna)
        {
            char valor = matriz[fila, columna];

            if (valor == CasillaVacia)
            {
                Console.Write((char)('1' + fila * Tamano + columna));
                return;
            }

            ConsoleColor colorOriginal = Console.ForegroundColor;
            Console.ForegroundColor = valor == 'X' ? ConsoleColor.Blue : ConsoleColor.Red;
            Console.Write(valor);
            Console.ForegroundColor = colorOriginal;
        }
    }
}
