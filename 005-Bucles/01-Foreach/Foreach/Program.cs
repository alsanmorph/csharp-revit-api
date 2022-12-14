using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foreach
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

			// Iteración por cada elemento de la lista para mostrar su contenido
			// en la consola.
			foreach(string cualquierFruta in frutasGrandes)
			{
				Console.WriteLine(String.Format("Hay una fruta que se llama {0}", cualquierFruta));
			}

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();

		}
	}
}
