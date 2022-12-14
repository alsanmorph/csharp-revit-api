using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de una lista con varias cadenas de texto.
			List<string> frutasGrandes = new List<string>()
			{
				"Melón",
				"Sandía",
				"Manzana",
				"Pera"
			};

			for(int indice = 0;
				indice < frutasGrandes.Count;
				indice = indice + 1)
			{
				Console.WriteLine("La fruta que está en la posición " +
					(indice + 1) + " de la lista se llama " +
					frutasGrandes[indice]);
			}


			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
