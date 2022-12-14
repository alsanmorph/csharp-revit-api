using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace DuplicadorDePlanos
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

			// Recuperación de la selección actual en el documento activo.
			ICollection<ElementId> idsDeElementosSelecionados = seleccion.GetElementIds();

			// Creación de un recurso con una transacción.
			using (Transaction transaccion = new Transaction(documento))
			{
				// Apertura de la transacción.
				transaccion.Start("Creación automática de planos");

				// Iteración por todos los 'ElementId' recuperados.
				foreach (ElementId cualquierElementId in idsDeElementosSelecionados)
				{
					// Obtanción del elemento correspondiente a la Id.
					Element plano = documento.GetElement(cualquierElementId);

					// Código a ejecutar en caso de que el elemento obtenido sea una vista de plano.
					if (plano.Category.Id.IntegerValue.Equals((int)BuiltInCategory.OST_Sheets))
					{
						#region Búsqueda del cajetín a utilizar.
						// Creación de un filtro para buscar el cajetín insertado en el plano seleccionado.
						Element cajetín = new FilteredElementCollector(documento, plano.Id).
							OfCategory(BuiltInCategory.OST_TitleBlocks).
							FirstOrDefault();
						#endregion

						#region Creación del plano.
						// Creación de un nuevo plano, usando como cajetín ei ID del tipo de
						// cajetín del plano que se ha duplicado.
						ViewSheet NuevoPlano = ViewSheet.Create(
							documento,
							cajetín.GetTypeId());

						// Asignación al nombre del nuevo plano.
						NuevoPlano.Name = "Copia de " + plano.Name;
						#endregion

						#region Clonación de las vistas y colocación de las mismas dentro del nuevo plano.

						// Creación de una colección para almacenar todas las ventanas de vista y tablas que tiene insertado el plano
						// de origen.
						List<Element> elementosEnPlanoDeOrigen = new List<Element>();

						// Adición a la colección de todas las ventanas gráficas colocadas en el plano de origen.
						elementosEnPlanoDeOrigen.AddRange(new FilteredElementCollector(documento, plano.Id).
							OfCategory(BuiltInCategory.OST_Viewports).ToList());

						// Adición a la coleccion de todas lsa tablas d eplanificación colocadsa en el plano de origen.
						elementosEnPlanoDeOrigen.AddRange(new FilteredElementCollector(documento, plano.Id).
							OfClass(typeof(ScheduleSheetInstance)).ToList());

						// Iteración por todas las ventanas encontradas para crear una copia de su vista correspondiente e insertarla en el nuevo
						// plano en el mismo lugar.
						foreach(Element elementoEnPlano in elementosEnPlanoDeOrigen)
						{
							// Código a ejecutar si el elemento actual es una ventana de vista.
							if (elementoEnPlano is Viewport)
							{
								// Conversión del elemento actual de una instancia de la clase 'Element' a una instancoa de la clase 'Viewport'.
								Viewport ventanaGrafica = elementoEnPlano as Viewport;

								// Extracción del punto de inserción de la ventana para utilizarlo a al ahora de insertar la nueva ventana equivalente
								// en el nuevo plano.
								XYZ puntoDeInsercionDeLaVentana = ventanaGrafica.GetBoxCenter();

								// Obtención de la vista asociada a la ventana.
								View vistaAsociadaAVentana = (View)documento.GetElement(ventanaGrafica.ViewId);

								// Declaración de una variable para almacenar el ID de la ventana a insertar en el nuevo plano.
								// Dependiendo de si es una leyanda o no, deberá ser una vista duplicada.
								ElementId idDeLaVistaAInsertar = null;

								// Código a ejecutar en caso de que la vista asociada a la ventana NO sea una leyenda.
								if (!vistaAsociadaAVentana.ViewType.Equals(ViewType.Legend))
								{
									// Duplicación de la vista asociada.
									idDeLaVistaAInsertar = vistaAsociadaAVentana.
										Duplicate(ViewDuplicateOption.Duplicate);
								}

								// Código a ejecutar en caso contrario.
								else
								{
									// Asignación del valor de la variable para almacenar el ID de la ventana a insertar con
									// el ID de la vista asociada a la ventana (al tratarse de una leyenda, no es necesario
									// duplicarla).
									idDeLaVistaAInsertar = vistaAsociadaAVentana.Id;
								}

								// Inserción de la nueva vista en el plano.
								Viewport nuevaVentana = Viewport.Create(
									documento,
									NuevoPlano.Id,
									idDeLaVistaAInsertar,
									puntoDeInsercionDeLaVentana); 
							}

							// Código a ejecutar si el elemento actual es una tabla de planificación insertada en el plano.
							else if(elementoEnPlano is ScheduleSheetInstance)
							{
								// Conversión del elemento actual de una instancia de la clase 'Element' a una instancoa de la clase 'ScheduleSheetInstance'.
								ScheduleSheetInstance tablaInsertadaEnPlano = elementoEnPlano as ScheduleSheetInstance;

								// Código a utilizar si la tabla insertada en plano NO es una tabla de revisiones.
								if (!tablaInsertadaEnPlano.IsTitleblockRevisionSchedule)
								{
									// Extracción del punto de inserción de la ventana para utilizarlo a al ahora de insertar la nueva ventana equivalente
									// en el nuevo plano.
									XYZ puntoDeInsercionDeLaTablaDePlanificacion = tablaInsertadaEnPlano.Point;

									// Obtención de la vista asociada a la ventana.
									View tablaAsociadaAVentana = documento.GetElement(tablaInsertadaEnPlano.ScheduleId) as View;

									// Inserción de la tabla de planificación en el nuevo plano.
									ScheduleSheetInstance nuevaTablaInsertadaEnPlano = ScheduleSheetInstance.Create(
										documento,
										NuevoPlano.Id,
										tablaAsociadaAVentana.Id,
										puntoDeInsercionDeLaTablaDePlanificacion);
								}
							}
						}
						#endregion
					}
				}

				// Cierre de la transacción.
				transaccion.Commit();
			}

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
