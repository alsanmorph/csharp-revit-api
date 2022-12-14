using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_OperacionesNumericas
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declaración de un par de variables para almacenar los operandos.
			double primerOperando = 8;
			double segundoOperando = 4;

			// Declaración de una variable para almacenar el resultado.
			// Esta variable no tiene una valor asignado a la hora de ser declarada.
			double resultado;

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

			// Cálculo del resto resultante de la división del primer operando entre el segundo operando (módulo).
			resultado = primerOperando % segundoOperando;

			// Mostrado en consla del resultado.
			Console.WriteLine("El resto de dividir el pimer operando entre el segundo operando es: " + resultado);

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el métoso ReadKey para que la consola se quede abierta.
			Console.ReadKey();
		}
	}
}
