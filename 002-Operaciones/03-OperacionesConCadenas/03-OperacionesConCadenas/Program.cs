using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_OperacionesConCadenas
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de variables locales.
			// Cadenas de texto con los textos introducidos.
			string primerTextoIntroducido;
			string segundoTextoIntroducido;

			// Enteros con el número de caracteres de cada texto.
			int numeroDeCaracteresDelPrimerTexto;
			int numeroDeCaracteresDelSegundoTexto;

			// Cadena de texto con texo que buscar (operación 'busca-reemplaza).
			string textoQueBuscar;
			// Cadena de texto con texo por el que reemplazar (operación 'busca-reemplaza).
			string textoPorElQueReemplazar;

			// Cadena de texto con el resultado.
			string resultado;


			#region Solicitud de valores de los textos introducidos por el usuario.
			// Solicitud de un valor para el primer texto.
			Console.WriteLine("Introcude el valor del primer texto:");
			// Almacenamiento del primer texto introducido por el usuario en su variable correspondiente.
			primerTextoIntroducido = Console.ReadLine();

			// Solicitud de un valor para el segundo texto.
			Console.WriteLine("Introcude el valor del segundo texto:");
			// Almacenamiento del segundo texto introducido por el usuario en su variable correspondiente.
			segundoTextoIntroducido = Console.ReadLine();
			#endregion

			#region Unión de textos.
			// Unión de las dos cadenas.
			resultado = primerTextoIntroducido + segundoTextoIntroducido;
			// Muestra en consola del resultado.
			Console.WriteLine("La unión de los dos textos introducidos es:" +
				Environment.NewLine +
				resultado);
			#endregion

			#region Cálculo del número de caracteres de los textos.
			// Cálculo del número de caracteres del primer texto.
			numeroDeCaracteresDelPrimerTexto = primerTextoIntroducido.Count();
			// Muestra en consola del número de caracteres del primer texto.
			Console.WriteLine("El número de caracteres del primer texto introducido es:" +
				Environment.NewLine +
				numeroDeCaracteresDelPrimerTexto);

			// Cálculo del número de caracteres del segundo texto.
			numeroDeCaracteresDelSegundoTexto = segundoTextoIntroducido.Count();
			// Muestra en consola del número de caracteres del segundo texto.
			Console.WriteLine("El número de caracteres del segundo texto introducido es:" +
				Environment.NewLine +
				numeroDeCaracteresDelSegundoTexto);
			#endregion

			#region Conversión a mayúsculas y minúsculas de los textos.
			// Conversión a mayuúsculas del primer texto.
			resultado = primerTextoIntroducido.ToUpper();
			// Muestra en consola el primer texto convertido a mayúsculas.
			Console.WriteLine("El primer texto convertido a mayusculas es:" +
				Environment.NewLine +
				resultado);

			// Conversión a minúsculas del segundo texto.
			resultado = segundoTextoIntroducido.ToLower();
			// Muestra en consola el segundo texto convertido a minúsculas.
			Console.WriteLine("El segundo texto convertido a minúsculas es:" +
				Environment.NewLine +
				resultado);
			#endregion

			#region Operación 'Busca-Reemplaza'
			// Solicitud de un valor para el texto a buscar.
			Console.WriteLine("Introcude el valor del texto a buscar:");
			textoQueBuscar = Console.ReadLine();

			// Solicitud de un valor para el texto por el que reemplazar.
			Console.WriteLine("Introcude el valor del texto por el que reemplazar:");
			textoPorElQueReemplazar = Console.ReadLine();

			// Creación del primer texto aplicando la opereación busca-reemplaza.
			resultado = primerTextoIntroducido.Replace(textoQueBuscar, textoPorElQueReemplazar);
			// Muestra en consola el primer texto convertido.
			Console.WriteLine("El primer texto aplicando la operación busca-reemplaza es:" +
				Environment.NewLine +
				resultado);

			// Creación del segundo texto aplicando la opereación busca-reemplaza.
			resultado = segundoTextoIntroducido.Replace(textoQueBuscar, textoPorElQueReemplazar);
			// Muestra en consola el primer texto convertido.
			Console.WriteLine("El segundo texto aplicando la operación busca-reemplaza es:" +
				Environment.NewLine +
				resultado); 
			#endregion

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
