using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_IfElse
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

			// Camino a seguir si el numero de caracteres del texto introducido es 3.
			if(numeroDeCaracteres == 3)
			{
				Console.WriteLine("El texto introducido tiene 3 caracteres");
			}
			// Camino a seguir en caso contrario.
			else
			{
				Console.WriteLine("El texto introducido no tiene 3 caracteres");
			}

			// Camino a seguir si el número de caracteres es mayor que 3
			if(numeroDeCaracteres > 3)
			{
				Console.WriteLine("El texto introducido tiene mas de 3 caracteres");
			}
			// Camino a seguir en caso de que la anterior condicón no se haya cumplido
			// y el número de caracteres es menor que 3.
			else if (numeroDeCaracteres < 3)
			{
				Console.WriteLine("El texto introducido tiene menos de 3 caracteres");
			}
			// Camino a seguir en caso que ninguna de las dos condiciones previas no se
			// hayan cumplido (la cadena tiene exactamente 3 caracteres).
			else
			{
				Console.WriteLine("El texto introducido tiene 3 caracteres");
			}

			// Muestra de información con el número de caracteres del texto introducido.
			// (Este código se ejecuta independientemente de los caminos que haya seguido
			// el programa previamente).
			Console.WriteLine("El texto introducido tiene " +
				numeroDeCaracteres +
				" caracteres");

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadLine para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
