using System;
using System.IO;

namespace TresEnRaya
{
    /// <summary>
    /// Pruebas manuales (sin framework externo) de los casos del MVP.
    /// Se ejecutan con: dotnet run -- --pruebas
    /// </summary>
    internal static class PruebasMVP
    {
        private static int superadas;
        private static int fallidas;

        public static void Ejecutar()
        {
            superadas = 0;
            fallidas = 0;

            Console.WriteLine("=== PRUEBAS MVP ===");
            Verificar("1. Casilla ocupada es rechazada", CasillaOcupada);
            Verificar("2. Coordenadas fuera de rango son rechazadas", FueraDeRango);
            Verificar("3. Victoria horizontal", VictoriaHorizontal);
            Verificar("4. Victoria diagonal", VictoriaDiagonal);
            Verificar("5. Empate (tablero lleno sin ganador)", Empate);
            Verificar("6. Partida: texto y rango inválido no la rompen, y gana X", PartidaConEntradasInvalidas);

            Console.WriteLine($"Resultado: {superadas} superadas, {fallidas} fallidas.");
        }

        private static bool CasillaOcupada()
        {
            var tablero = new Tablero();
            tablero.MarcarCasilla(1, 1, 'X');

            bool lanzaExcepcion = false;
            try { tablero.MarcarCasilla(1, 1, 'O'); }
            catch (InvalidOperationException) { lanzaExcepcion = true; }

            return !tablero.EsCasillaValida(1, 1) && lanzaExcepcion && tablero.EsCasillaValida(0, 0);
        }

        private static bool FueraDeRango()
        {
            var tablero = new Tablero();
            return !tablero.EsCasillaValida(-1, 0)
                && !tablero.EsCasillaValida(0, 3)
                && !tablero.EsCasillaValida(3, 3)
                && tablero.EsCasillaValida(2, 2);
        }

        private static bool VictoriaHorizontal()
        {
            var tablero = new Tablero();
            tablero.MarcarCasilla(0, 0, 'X');
            tablero.MarcarCasilla(0, 1, 'X');
            bool antes = tablero.HayVictoria('X'); // con dos no debe haber victoria
            tablero.MarcarCasilla(0, 2, 'X');

            return !antes && tablero.HayVictoria('X') && !tablero.HayVictoria('O');
        }

        private static bool VictoriaDiagonal()
        {
            var tablero = new Tablero();
            tablero.MarcarCasilla(0, 2, 'O');
            tablero.MarcarCasilla(1, 1, 'O');
            tablero.MarcarCasilla(2, 0, 'O');

            return tablero.HayVictoria('O') && !tablero.HayVictoria('X');
        }

        private static bool Empate()
        {
            // X O X
            // X O O
            // O X X
            char[,] disposicion =
            {
                { 'X', 'O', 'X' },
                { 'X', 'O', 'O' },
                { 'O', 'X', 'X' }
            };

            var tablero = new Tablero();
            for (int f = 0; f < 3; f++)
                for (int c = 0; c < 3; c++)
                    tablero.MarcarCasilla(f, c, disposicion[f, c]);

            return tablero.EstaLleno() && !tablero.HayVictoria('X') && !tablero.HayVictoria('O');
        }

        /// <summary>
        /// Integración: simula el teclado. Entradas inválidas ("abc", "0", "10") y luego
        /// la secuencia 1,4,2,5,3 con la que X completa la fila superior.
        /// </summary>
        private static bool PartidaConEntradasInvalidas()
        {
            TextReader entradaOriginal = Console.In;
            TextWriter salidaOriginal = Console.Out;
            var salida = new StringWriter();

            try
            {
                Console.SetIn(new StringReader("abc\n0\n10\n1\n4\n2\n5\n3\n"));
                Console.SetOut(salida);

                new Partida(new Jugador("Ana", 'X'), new Jugador("Luis", 'O')).Iniciar();
            }
            finally
            {
                Console.SetIn(entradaOriginal);
                Console.SetOut(salidaOriginal);
            }

            string texto = salida.ToString();
            return texto.Contains("Entrada no válida")
                && texto.Contains("entre 1 y 9")
                && texto.Contains("Ana (X) ha ganado");
        }

        /// <summary>Ejecuta una prueba, captura fallos inesperados y muestra OK/FALLO.</summary>
        private static void Verificar(string nombre, Func<bool> prueba)
        {
            bool ok;
            try { ok = prueba(); }
            catch (Exception ex)
            {
                Console.WriteLine($"[FALLO] {nombre} (excepción: {ex.Message})");
                fallidas++;
                return;
            }

            Console.WriteLine($"[{(ok ? "OK" : "FALLO")}] {nombre}");
            if (ok) superadas++; else fallidas++;
        }
    }
}
