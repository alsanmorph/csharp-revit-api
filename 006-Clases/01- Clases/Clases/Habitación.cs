using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
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
	}
}
