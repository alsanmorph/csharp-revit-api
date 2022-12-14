using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace While
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de una variable local para almacenar el texto introducido por el usuario.
			string textoIntroducido = "";

			// Declaración de una variable local para almacenar el número de caracteres
			// de la cadena introducida por el usuario. Inicialmente, su valor será de 0.
			int numeroDeCaracteres = 0;

			// Código a ejecutar mientras el valor de 'numeroDeCaracteres' sea diferente de 0.
			while (numeroDeCaracteres != 30)
			{
				// Solicitar que el usuario introduzca una cadena de texto.
				Console.WriteLine("Introduzca una línea de texto:");
				textoIntroducido = Console.ReadLine();

				// Almacenar en 'numeroDeCaracteres' el número de caracteres del
				// texto introducido.
				numeroDeCaracteres = textoIntroducido.Count();

				// Cuando el texto introducido no tiene 30 carateres se publica su número de caracteres.
				if (numeroDeCaracteres != 30)
				{
					Console.WriteLine($"{textoIntroducido} tiene {numeroDeCaracteres} caracteres."); 
				}
			}

			// Cuando eltexto introducido tiene 30 caracteres, se publica en consola y se termina la aplicación.
			Console.WriteLine($"{textoIntroducido} tiene {numeroDeCaracteres} caracteres y se termina la aplicación");

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
