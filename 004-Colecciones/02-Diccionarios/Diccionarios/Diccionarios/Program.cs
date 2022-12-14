using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionarios
{
	class Program
	{
		static void Main(string[] args)
		{
			// Creación e inicialización de un diccionario con claves de tipo cadena y
			// valores del tipo entero.
			Dictionary<string, int> inventarioDeFrutas = new Dictionary<string, int>();
			inventarioDeFrutas.Add("Manzanas", 4);
			inventarioDeFrutas.Add("Peras", 6);

			// Creación de una lista de cadenas accediendo a la colección de claves del diccionario.
			List<string> frutas = new List<string>(inventarioDeFrutas.Keys);

			// Creación de una lista de enteros accediendo a la colección de claves del diccionario.
			List<int> enteros = new List<int>(inventarioDeFrutas.Values);

			// Acceso a un miembro de un diccionario por su índice.
			KeyValuePair<string, int> fruta = inventarioDeFrutas.ElementAt(0);

			// Acceso a un miembro de un diccioinario por el nombre de su clave.
			int numeroDeManzanas = inventarioDeFrutas["manzana"];
		}
	}
}
