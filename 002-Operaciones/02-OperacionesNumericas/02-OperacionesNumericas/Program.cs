using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_OperacionesNumericas
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de variables locales.
			// Cadenas de texto con la introducción de los operandos.
			string primerOperandoIntroducido;
			string segundoOperandoIntroducido;

			// Dobles con la conversión de los operandos introducidos.
			double primerOperando;
			double segundoOperando;

			// Doble con el resultado.
			double resultado;

			// Solicitud de un valor para el primer operando.
			Console.WriteLine("Introcude el valor del primer operando:");
			// Almacenamiento del primer operando introducido por el usuario en su variable correspondiente.
			primerOperandoIntroducido = Console.ReadLine();
			// Conversión a tipo doble del valor introducido.
			primerOperando = double.Parse(primerOperandoIntroducido);

			// Solicitud de un valor para el segundo operando.
			Console.WriteLine("Introcude el valor del segundo operando:");
			// Almacenamiento del segundo operando introducido por el usuario en su variable correspondiente.
			segundoOperandoIntroducido = Console.ReadLine();
			// conversión a tipo doble del valor introducido.
			segundoOperando = double.Parse(segundoOperandoIntroducido);

			// Realización de una suma entre los dos operandos.
			resultado = primerOperando + segundoOperando;
			// Mostrado en consla del resultado.
			Console.WriteLine("El resultado de la suma de los dos operandos es: " + resultado);
			// Realización de una resta entre los dos operandos.
			resultado = primerOperando - segundoOperando;
			// Mostrado en consla del resultado.
			Console.WriteLine("El resultado de la resta de los dos operandos es: " + resultado);
			// Realización de una multiplicación entre los dos operandos.
			resultado = primerOperando * segundoOperando;
			// Mostrado en consla del resultado.
			Console.WriteLine("El resultado de la multiplicación de los dos operandos es: " + resultado);
			// Realización de una división entre los dos operandos.
			resultado = primerOperando / segundoOperando;
			// Mostrado en consla del resultado.
			Console.WriteLine("El resultado de la división de los dos operandos es: " + resultado);

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el método ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
