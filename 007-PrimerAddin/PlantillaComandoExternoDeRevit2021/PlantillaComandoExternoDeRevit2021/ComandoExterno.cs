using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace PlantillaComandoExternoDeRevit2021
{
    // Etiqueta que indica el modo de transacción del comando externo.
    [Transaction(TransactionMode.Manual)]

    // Clase pública que contiene el comando externo.
    public class ComandoExterno : IExternalCommand
    {
		// Método público el cual Revit buscará para ejecutar el comando externo.
		public Result Execute(
			ExternalCommandData datosDelComandoExterno,
			ref string mensaje,
			ElementSet elementos)
		{
			// Declaración de variables locales para almacenar tanto el documento activo
			// como la selección.
			Document documento = datosDelComandoExterno.Application.ActiveUIDocument.Document;
			Selection seleccion = datosDelComandoExterno.Application.ActiveUIDocument.Selection;

			// Publicación de un menaje en pantalla indicando que le comando externo ha
			// sido implementado correctamente.
			TaskDialog.Show(
				"Plantilla de comando externo",
				"El comando externo se ha creado con éxito");

			// Retorno satisfactorio del método.
			return Result.Succeeded;
		}
	}
}
