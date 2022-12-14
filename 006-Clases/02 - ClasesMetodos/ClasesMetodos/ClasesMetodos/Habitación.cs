using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesMetodos
{
	/// <summary>
	/// Clase pública que define una habitación y sus propiedades.
	/// </summary>
	public class Habitación
	{
		#region Propiedades de la clase.
		/// <summary>
		/// Nombre de la habitación.
		/// </summary>
		public string Nombre { get; set; }

		/// <summary>
		/// Área de la habiración.
		/// </summary>
		public double Area { get; set; }

		/// <summary>
		/// Uso al que está destinada la habitación.
		/// </summary>
		public string Uso { get; set; }

		/// <summary>
		/// Indica si la habitación necesita detectores o no.
		/// </summary>
		public bool NecesitaDetectores { get; set; }

		/// <summary>
		/// Número de detectores a instalar
		/// </summary>
		public int NumeroDeDetectores { get; set; } 
		#endregion

		/// <summary>
		/// Método público que calcula la ocupación de la habitación
		/// </summary>
		/// <param name="numeroDePersonasPorMetroCuadrado"></param>
		/// <returns></returns>
		public int CalcularOcupacion(int numeroDePersonasPorMetroCuadrado)
		{
			// Cálculo de un número entero que indica la ocupación de la habitación.
			// Se calcula haciendo la multiplicación del entero indicado en el parámetro de entrada
			// 'numeroDePersonasPorMetroCuadrado' por el área de la habitación.
			// Para conseguir que el resultado sea un número entero, redondearemos al número entero mayor
			// haciendo uso de 'Math.Ceiling()'
			double ocupaciónConDecimales = numeroDePersonasPorMetroCuadrado * this.Area;
			int ocupación = (int)Math.Ceiling(ocupaciónConDecimales);

			// Retorno del método.
			return ocupación;
		}


		/// <summary>
		/// Método constructor de la clase.
		/// </summary>
		/// <param name="nombreIntroducido">Nombre de la habiración</param>
		/// <param name="usoIntroducido">Uso al que será destinado la habitación</param>
		public Habitación(
			string nombreIntroducido,
			string usoIntroducido)
		{
			// En el momento de la creación de un objeto de la clase, se le asigna a la propiedad 'Nombre',
			// el valor del parámetro de entrada 'nombreIntroducido'.
			this.Nombre = nombreIntroducido;

			// En el momento de la creación de un objeto de la clase, se le asigna a la propiedad 'Uso',
			// el valor del parámetro de entrada 'usoIntroducido'.
			this.Uso = usoIntroducido;
		}
	}
}
