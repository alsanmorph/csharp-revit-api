using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de una lista vacía de cadenas de texto.
			List<string> frutas;

			// Inicialización de la lista 'frutas' para porder ser utilizada.
			frutas = new List<string>();

			// Adición de elementos, de uno en uno, a una lista.
			frutas.Add("Manzana");
			frutas.Add("Platano");

			// Declaración de una lista vacía e inicialización de la misma en una línea.
			List<string> frutasPequeñas = new List<string>();

			// Adición de varios elementos a una lista.
			frutasPequeñas.Add("Fresas");
			frutasPequeñas.Add("Cerezas");

			// Declaración de una lista con varias cadenas de texto.
			List<string> frutasGrandes = new List<string>()
			{
				"Melón",
				"Sandía"
			};

			// Unión de dos listas.
			frutas.AddRange(frutasGrandes);
			frutas.AddRange(frutasPequeñas);

			// Acceso a elementos de una lista por su índice.
			string terceraFrutaEnLista = frutas[2];

			// Muestra de información en la consola.
			Console.WriteLine(String.Format(
				"La tercera fruta en la lista es: {0}. Y su nombre tiene {1} letras.",
				terceraFrutaEnLista, terceraFrutaEnLista.Count()));

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
