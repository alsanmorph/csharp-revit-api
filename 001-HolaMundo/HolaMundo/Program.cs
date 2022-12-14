using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolaMundo
{
	// La clase llamada 'Program' es la clase base para una aplicación de consola.
	class Program
	{
		// El método llamado 'Main' es el método que se va a ejecutar cuando inicializemos
		// el programa.
		static void Main(string[] args)
		{


	// 1. HOLA MUNDO

			// Mostrar el mensaje 'Hola Mundo' en consola.
			Console.WriteLine("Hola Mundo.");

			// Con el fin de que no se cierre la consola inmediatamente
			// utilizamos el método ReadKey para que se quede abierta.
			Console.ReadKey();



    // 2. VARIABLES NUMERICAS

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


    // 3. MÁS OPERACIONES NUMÉRICAS

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



    // 4. OPERACIONES CON CADENA DE TEXTO

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
