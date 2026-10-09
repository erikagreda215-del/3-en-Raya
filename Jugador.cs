using System;

namespace TresEnRaya
{
    /// <summary>
    /// Representa a un jugador del Tres en Raya.
    /// Es inmutable: una vez creado, su nombre y símbolo no cambian.
    /// </summary>
    public class Jugador
    {
        /// <summary>Nombre del jugador (nunca vacío).</summary>
        public string Nombre { get; }

        /// <summary>Símbolo del jugador: 'X' u 'O'.</summary>
        public char Simbolo { get; }

        /// <summary>
        /// Crea un jugador validando sus datos.
        /// </summary>
        /// <param name="nombre">Nombre del jugador; no puede ser nulo, vacío ni solo espacios.</param>
        /// <param name="simbolo">'X' u 'O' (se admiten minúsculas y se normalizan a mayúsculas).</param>
        /// <exception cref="ArgumentException">Si el nombre está vacío o el símbolo no es válido.</exception>
        public Jugador(string nombre, char simbolo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

            char simboloNormalizado = char.ToUpperInvariant(simbolo);
            if (simboloNormalizado != 'X' && simboloNormalizado != 'O')
                throw new ArgumentException("El símbolo debe ser 'X' u 'O'.", nameof(simbolo));

            Nombre = nombre.Trim();
            Simbolo = simboloNormalizado;
        }

        /// <summary>Devuelve una representación legible, p. ej. "Ana (X)".</summary>
        public override string ToString() => $"{Nombre} ({Simbolo})";
    }
}
