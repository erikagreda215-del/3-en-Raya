# Tres en Raya (consola, C#)

Juego de Tres en Raya para dos jugadores en la consola, hecho con Programación Orientada a Objetos.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) (proyecto creado con `net10.0`).
- Comprueba tu versión con: `dotnet --version`

## Cómo ejecutarlo

- clonar el repositorios de git https://github.com/erikagreda215-del/3-en-Raya 

- abrir la carpeta en Visual Studio, abrir `TresEnRaya.csproj` y pulsar F5.

## Cómo se juega

1. En el menú principal elige **1. Jugar Partida**.
2. Escribe el nombre de cada jugador. El Jugador 1 usa **X** (azul) y empieza; el Jugador 2 usa **O** (rojo). Los nombres no pueden estar vacíos ni repetirse.
3. En tu turno escribe el número de la casilla libre (1-9):

```
 1 | 2 | 3
---+---+---
 4 | 5 | 6
---+---+---
 7 | 8 | 9
```

4. Gana quien complete una fila, columna o diagonal. Si se llenan las 9 casillas sin ganador, es empate.
5. Al terminar puedes **volver a jugar** (mismos jugadores), **ir al menú principal** o **salir**.

Si escribes texto, un número fuera de 1-9 o una casilla ocupada, el juego muestra el error y vuelve a preguntar sin cerrarse.

## Estructura

```
TresEnRaya.csproj
Program.cs              Punto de entrada: configura la consola y lanza el menú
Modelos/
  Jugador.cs            Nombre y símbolo (X/O) con validación
  Tablero.cs            Matriz 3x3, dibujo con colores, victoria y empate
Logica/
  Partida.cs            Bucle de turnos, lectura y validación de jugadas
Interfaz/
  MenuAplicacion.cs     Menú principal, configuración de jugadores y menú final
Pruebas/
  PruebasMVP.cs         Pruebas manuales de los casos del MVP
```
## autor erik agreda
 