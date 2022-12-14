using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;

namespace SeleccionSimple
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

			// Declaración de una variable local con una selección.
			Selection seleccion = DatosDelComandoExterno.Application.ActiveUIDocument.Selection;

			// Provocar que el usuario seleccione un elemento.
			Reference referencia = seleccion.PickObject(
				ObjectType.Element,
				"Selecciona un elemento");

			// Declaración de una variable local con el elemento obtenido a través de la referencia seleccionada.
			Element elementoSeleccionado = documento.GetElement(referencia);

			// Muestra de un cuadro de diálogo con la información del elemento seleccionado.
			TaskDialog.Show(
				"Selección simple",
				$"El elemento seleccionado pertenece a la categoría {elementoSeleccionado.Category.Name}");

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
