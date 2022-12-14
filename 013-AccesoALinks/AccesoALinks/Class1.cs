using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.IO;

namespace AccesoALinks
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

			// Decalaración de una variable local con el documento vinculado sobre el que trabajar.
			// Su valo rinicial será nulo.
			Document documentoVinculado = null;

			// Declaración de una variable local con la selección del documento.
			Selection seleccion = DatosDelComandoExterno.Application.ActiveUIDocument.Selection;

			// Declaración de una cadena de texto con el nombre del documento vinculado sobre el que operar.
			string nombreDelDocumentoVinculado = "Link1.rvt";

			// Iteración por todos los documentos de la sesión de Revit y búsqueda del documento vinculado sobre
			// el que trabajar.
			foreach(Document cualqueirDocumentoAbierto in DatosDelComandoExterno.
				Application.Application.Documents)
			{
				// Código a ejecutar si el nombre del archivo del documento es igual que el nombre del documento
				// sobre el qye se quiere trabajar.
				if (Path.GetFileName(cualqueirDocumentoAbierto.PathName).
					Equals(nombreDelDocumentoVinculado))
				{
					// Almacenar el documento en la variable 'documentoVinculado' e interrumpir el bucle.
					documentoVinculado = cualqueirDocumentoAbierto;
					break;
				}
			}

			// Filtrado de todos los muros que se encuentran en el modelo vinculado.
			FilteredElementCollector colectorDeMuros = new FilteredElementCollector(documentoVinculado).
				OfCategory(BuiltInCategory.OST_Walls).
				WhereElementIsNotElementType();

			// Publicación de un mensaje con información acerca del número de muros filtrados,
			TaskDialog.Show("Acceso a modelos vinculados",
				string.Format(
					"El modelo vinculado {0}, contiene {1} muros.",
					documentoVinculado.Title, colectorDeMuros.Count()));

			// Selección manual de un elemento en un modelo vinculado.
			Reference referenciaSeleccionada = seleccion.PickObject(
				ObjectType.LinkedElement,
				"Selecciona un elemento en el modelo vinculado");

			// Obtención del elemento correspondiente a la referencia.
			Element elementoEnModeloVinculado = documentoVinculado.GetElement(
				referenciaSeleccionada.LinkedElementId);

			// Publicación de un mensaje con informaión acerca de la categoría del elemento
			// seleccionado.
			TaskDialog.Show("Acceso a modelos vinculados",
				string.Format(
					"El elemento seleccionado pertenece a la categoría {0}.",
					elementoEnModeloVinculado.Category.Name));

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
