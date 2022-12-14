using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
	class Program
	{
		static void Main(string[] args)
		{
			// Creación de una instancia de la clase habitación.
			Habitación habitación = new Habitación();

			// Asignación de la propiedad 'Nombre'.
			habitación.Nombre = "Comedor";

			// Asignación de la propiedad 'Area'.
			habitación.Area = 360;

			// Asignación de la propiedad 'Uso'.
			habitación.Uso = "Cocina";

			// Asignación de la propiedad NecesitaDetectores.
			habitación.NecesitaDetectores = !habitación.Uso.Equals("Baño");

			// Código a ejecutar si la propiedad 'NcesitaDetectores' es true.
			if (habitación.NecesitaDetectores)
			{
				// Asignación de la propiedad 'NecesitaDetectores'
				habitación.NumeroDeDetectores = (int)Math.Abs(habitación.Area / 60);
			}
		}
	}
}
