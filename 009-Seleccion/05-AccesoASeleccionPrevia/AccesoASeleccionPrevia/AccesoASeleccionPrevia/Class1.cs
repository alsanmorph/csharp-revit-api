using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace AccesoASeleccionPrevia
{
	// La clase que va a contener el comando externo de Revit, ha de implementar la interface 'IExternalCommand'.
	[Transaction(TransactionMode.Manual)]
	public class Class1 : IExternalCommand
	{
		// Método público el cual buscará Revit para ejecutar el comando externo.
		public Result Execute(
			ExternalCommandData DatosDelComandoExterno,
			ref string mensaje,
			ElementSet elementos)
		{
			// Declaración de una variable local con el documento activo.
			Document documento = DatosDelComandoExterno.Application.ActiveUIDocument.Document;

			// Declaración de una variable local con la selección del documento.
			Selection seleccion = DatosDelComandoExterno.Application.ActiveUIDocument.Selection;

			// Declaración de un diccionario para almacenar la categoría y el número de elementos en la selección.
			Dictionary<string, int> informaciónDeSeleccion = new Dictionary<string, int>();

			// Declaración de una cadena de texto con la información a publicar.
			string mensajeConInformacion = "La selección contiene:" + Environment.NewLine;

			// Acceso a la selección en curso a la hora de ejecutar el comando externo.
			ICollection<ElementId> seleccionPrevia = seleccion.GetElementIds();

			// Iteración por todos los 'ElementId' de la selección previa.
			foreach(ElementId cualquierElementId in seleccionPrevia)
			{
				// Obtener el elemento correspondiente.
				Element elemento = documento.GetElement(cualquierElementId);

				// Código a ejecutar si el diccionario ya contiene una clave con el nombre de
				// la categoría.
				if (informaciónDeSeleccion.ContainsKey(elemento.Category.Name))
				{
					// Incremento del entero almacenado en el valor correspondiente.
					informaciónDeSeleccion[elemento.Category.Name]++;
				}

				else
				{
					// Adición al diccionario del par clave-valor con el nombre de la categoría
					// y una unidad de elementos.
					informaciónDeSeleccion.Add(elemento.Category.Name, 1);
				}
			}

			// Iteración por todos los pares clave-valor que contiene el diccionario.
			foreach(KeyValuePair<string,int> cualquierParClaveValor in informaciónDeSeleccion)
			{
				// Adición al mensaje con la información de la información en el par.
				mensajeConInformacion += cualquierParClaveValor.Key + ":  " +
					cualquierParClaveValor.Value.ToString() +
					Environment.NewLine;
			}

			// Publicación del mensaje.
			TaskDialog.Show("Selección previa", mensajeConInformacion);

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
