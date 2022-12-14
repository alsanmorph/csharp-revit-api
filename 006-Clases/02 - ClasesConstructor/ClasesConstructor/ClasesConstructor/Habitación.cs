using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesConstructor
{
	/// <summary>
	/// Clase pública que define una habitación y sus propiedades.
	/// </summary>
	public class Habitación
	{
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
