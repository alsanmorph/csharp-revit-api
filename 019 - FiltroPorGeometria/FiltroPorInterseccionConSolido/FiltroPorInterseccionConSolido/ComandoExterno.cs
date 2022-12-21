using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiltroPorInterseccionConSolido
{
	[Transaction(TransactionMode.Manual)]
	public class ComandoExterno : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			Document documento = commandData.Application.ActiveUIDocument.Document;
			Selection seleccion = commandData.Application.ActiveUIDocument.Selection;

			// Declaración de una variable local para almacenar el nombre del modelo vinculado a buscar.
			string nombreDeModeloVinculado = "Habitaciones";

			// Recuperación del documento vinculado.
			// Inicialización de una variable local para almacenar el dopcumento vinculado. Por defecto con valro nulo.
			Document documentoVinculado = null;

			// Iteración sobre todos los documentos abiertos en la sesión de Revit.
			foreach (Document cualquierDocumentoAbierto in commandData.Application.Application.Documents)
			{
				// Código a ejecutar si el nombre del documento actual coincide con el nombre indicado del modelo vinculado deseado.
				if (cualquierDocumentoAbierto.Title == nombreDeModeloVinculado)
				{
					// Asignación de varlo a la variable que almacena el documento vinculado.
					documentoVinculado = cualquierDocumentoAbierto;
					break;
				}
			}

			// Código a ejecutar si no se ha encontrado el documento vinculado.
			if (documentoVinculado is null)
			{
				message = "El docuemnto vinculado no está disponible.";
				return Result.Failed;
			}

			// Inciaialización de un colector de elementos de habitaciones.
			FilteredElementCollector colectorDeHabitaciones = new FilteredElementCollector(documentoVinculado).OfCategory(BuiltInCategory.OST_Rooms);

			// Inicialización de una transacción.
			using (Transaction transaccion = new Transaction(documento))
			{
				if (transaccion.Start("captura por solido") == TransactionStatus.Started)
				{
					// Iteración sobre todas las habitaciones recolectadas.
					foreach (Room cualquierHabitacion in colectorDeHabitaciones)
					{
						// Código a ejecutar si la habitación tiene área mayor que 0.
						if (cualquierHabitacion.Area > 0)
						{
							// Acceso al geometría de la habitación.
							GeometryElement geometria = cualquierHabitacion.get_Geometry(new Options());

							// Declaración de una variable local para almacenar el sólido correspondiente a la habitación.
							Solid solido = null;

							// Iteración sobre todos los objetos de la geometría.
							foreach (GeometryObject cualquierObjetoGeometrico in geometria)
							{
								// Código a ejecutar si el objeto es un sólido.
								if (cualquierObjetoGeometrico is Solid)
								{
									// Asignación de valor a la variable que almacena el sólido.
									solido = cualquierObjetoGeometrico as Solid;
									break;
								}
							}

							// Código a ejecutar si el sólido está disponible.
							if (solido != null)
							{
								// Inicialización de un filtro de elementos por intersección con sólido.
								ElementIntersectsSolidFilter filtroDeIntersccionConSolido = new ElementIntersectsSolidFilter(solido);

								// Incicialización de un colector de elementos con el filtro creado.
								FilteredElementCollector colectorDeElementosEnHabitacion = new FilteredElementCollector(documento).WherePasses(filtroDeIntersccionConSolido);

								// Iteración sobre todos los elementos capturados.
								foreach (Element cualquierElemento in colectorDeElementosEnHabitacion)
								{
									// Obtención del parámetro "Comentarios" y si este está disponible, asignación de un valor al mismo con el
									// nombre de la habitación.
									Parameter parametroComentarios = cualquierElemento.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
									if (parametroComentarios != null)
									{
										parametroComentarios.Set(cualquierHabitacion.Name);
									}
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
