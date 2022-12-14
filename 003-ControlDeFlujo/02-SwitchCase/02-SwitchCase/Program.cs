using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_SwitchCase
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de variables locales.
			// Texto introducido.
			string textoIntroducido;
			// Número de caracteres del texto introducido.
			int numeroDeCaracteres;

			// Solicitud de un valor para el texto.
			Console.WriteLine("Introcude el valor del texto:");
			// Almacenamiento del primer operando introducido por el usuario en su variable correspondiente.
			textoIntroducido = Console.ReadLine();

			// Cálculo del número de caracteres del texto introducido.
			numeroDeCaracteres = textoIntroducido.Count();

			// Evaluación del valor de 'numeroDeCaracteres' por medio de switch / case.
			switch (numeroDeCaracteres)
			{
				// Definición del candidato '1'.
				case 1:
					// Camino a seguir para el candidato.
					Console.WriteLine("El texto introducido tiene 1 caracter");
					break;

				// Definición del candidato '2'.
				case 2:
					// Camino a seguir para el candidato.
					Console.WriteLine("El texto introducido tiene 2 caracteres");
					break;

				// Definición del candidato '3'.
				case 3:
					// Camino a seguir para el candidato.
					Console.WriteLine("El texto introducido tiene 3 caracteres");
					break;

				// Definición de los candidatos '4' y '5'.
				case 4:
				case 5:
					// Camino a seguir para los candidatos.
					Console.WriteLine("El texto introducido tiene 4 ó 5 caracteres");
					break;

				// Definición del candidato 'por defecto'.
				default:
					// Camino a seguir por defecto.
					Console.WriteLine("El texto introducido tiene mas de 5 caracteres");
					break;
			}

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadLine para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
