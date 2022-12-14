using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicacionVistas
{
	[Transaction(TransactionMode.Manual)]
	public class ComandoExterno : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// Declaración de una variable local con el documento activo.
			Document documento = commandData.Application.ActiveUIDocument.Document;

			// Declaración de una variable local con la selección del documento.
			Selection seleccion = commandData.Application.ActiveUIDocument.Selection;

			// Recuperación de la selección actual del documento.
			ICollection<ElementId> elementosSeleccionados = seleccion.GetElementIds();

			// Recuperación de la plantilla de vista con nombre "Architectural Plan".
			View plantillaDeVista = null;
			FilteredElementCollector colectorDeVistas = new FilteredElementCollector(documento);
			colectorDeVistas.OfClass(typeof(View));
			foreach(View cualquierVista in colectorDeVistas)
			{
				if (cualquierVista.IsTemplate)
				{
					if(cualquierVista.Name == "Architectural Plan")
					{
						plantillaDeVista = cualquierVista;
					}
				}
			}

			// Declaración de un par de variables locales para almacenar el texto a buscar y el texto por el que reemplazarlo en el nombre de vista.
			string textoABuscar = "Level";
			string textoNuevo = "Nivel";

			// Inicialización de la transacción.
			using (Transaction transaccion = new Transaction(documento))
			{
				if(transaccion.Start("duplicado de vistas") == TransactionStatus.Started)
				{
					// Iteración sobre todos los identificadores de elementos de la selección activa.
					foreach (ElementId cualquierId in elementosSeleccionados)
					{
						// Obtención del elemento correspondiente.
						Element cualquierElemento = documento.GetElement(cualquierId);

						// Código a ejecutar si el elemento correspondiente es una vista.
						if (cualquierElemento is View)
						{
							// Casteo del elemento a la clase "View".
							View cualquierVista = cualquierElemento as View;

							// Código a ejecutar si la vista puede ser duplicada.
							if (cualquierVista.CanViewBeDuplicated(ViewDuplicateOption.Duplicate))
							{
								// Duplicación de la nueva vista y almacenamiento de su identificador en una variable local.
								ElementId idDeNuevaVista = cualquierVista.Duplicate(ViewDuplicateOption.Duplicate);

								// Obtrención de la nueva vista y casteo a la clase "View".
								Element nuevoElemento = documento.GetElement(idDeNuevaVista);
								View nuevaVista = nuevoElemento as View;

								// Asignación de la plantilla de vista a la nueva vista.
								nuevaVista.ViewTemplateId = plantillaDeVista.Id;

								// Código a ejecutar si el nombre de la nueva vista contiene el texto a buscar.
								if (nuevaVista.Name.Contains(textoABuscar))
								{
									// Modificación del texto de la nueva vista.
									nuevaVista.Name = cualquierVista.Name.Replace(textoABuscar, textoNuevo);
								}
							}
						}
					}

					transaccion.Commit();
				}
			}





				


			return Result.Succeeded;
		}
	}
}
