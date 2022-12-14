using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;

namespace LecturaDeElemento
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
			// Declaración de una variable local con el documento actual.
			Document documento = DatosDelComandoExterno.Application.ActiveUIDocument.Document;

			// Declaración de una variable local con un ElementId para obtener el elemento correspondiente.
			ElementId idDelElementoALeer = new ElementId(2586);

			// Declaración de una variable local con el elemento obtenido a través de la referencia seleccionada.
			Element elementoSeleccionado = documento.GetElement(idDelElementoALeer);

			// Muestra de un cuadro de diálogo con la información del elemento seleccionado.
			TaskDialog.Show(
				"Consulta de elemento",
			$"El elemento consultado pertenece a la categoría {elementoSeleccionado.Category.Name}");

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
