using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesMetodos
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de una instancia de la clase habitación.
			Habitación nuevaHabitacion = new Habitación("Baño 1", "Cuarto húmedo");

			// Asignación de la propiedad 'Area'.
			nuevaHabitacion.Area = 5;

			// Cálculo de la ocupación de la habitación.
			int ocupacion = nuevaHabitacion.CalcularOcupacion(2);
		}
	}
}
