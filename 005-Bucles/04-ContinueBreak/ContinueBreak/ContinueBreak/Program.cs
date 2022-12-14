using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContinueBreak
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
				"Pera",
				"Manzana",
				"Plátano",
				"Fresa",
				"Uva",
			};

			// Iteración por cada elemento de la lista para mostrar su contenido
			// en la consola.
			foreach (string cualquierFruta in frutasGrandes)
			{
				// Si la fruta encontrada es 'Pera' se interrumpe el bucle y se continúa.
				if (cualquierFruta.Equals("Pera"))
				{
					continue;
				}

				// Si la fruta encontrada es 'Plátano' se interrumpe el buvle y se termina
				// la aplicación (se rompe el bucle).
				if (cualquierFruta.Equals("Plátano"))
				{
					break;
				}

				Console.WriteLine(String.Format("Hay una fruta que se llama {0}", cualquierFruta));
			}

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
