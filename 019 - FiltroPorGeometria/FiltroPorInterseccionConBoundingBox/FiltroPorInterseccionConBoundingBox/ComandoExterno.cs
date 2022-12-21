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

namespace FiltroPorInterseccionConBoundingBox
{
	[Transaction(TransactionMode.Manual)]
	public class ComandoExterno : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			Document documento = commandData.Application.ActiveUIDocument.Document;
			Selection seleccion = commandData.Application.ActiveUIDocument.Selection;

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
							// Acceszo a la boundig box de la habitación.
							BoundingBoxXYZ cajaDeHabitacion = cualquierHabitacion.get_BoundingBox(null);
							
							// Declaración de un par de variables locales para almacenar los puntos mínimo y máximo de la bounding box.
							XYZ puntoMinimo = cajaDeHabitacion.Min;
							XYZ puntoMaximo = cajaDeHabitacion.Max;

							// Inicialización de un contorno el cual será utilizado para capturar los elementos que intersequen con el.
							Outline limite = new Outline(puntoMinimo, puntoMaximo);

							// Inicialización de un filtro de elemento por intersección con bounding box.
							BoundingBoxIntersectsFilter filtroDeInterseccion = new BoundingBoxIntersectsFilter(limite);
							FilteredElementCollector colectorDeElementos = new FilteredElementCollector(documento).WherePasses(filtroDeInterseccion);

							// Iteración sobre todos los elementos capturados.
							foreach (Element cualquierElemento in colectorDeElementos)
							{
								// Código a ejecutar si el elemento actual es una instancia de familia.
								if (cualquierElemento is FamilyInstance)
								{
									// Casteo del elemento a una instancia de FamilyInstance.
									FamilyInstance instanciaDeFamilia = cualquierElemento as FamilyInstance;

									// Inicialización de una variable para almacenar el punto que será utilizado para localizar al elemento.
									XYZ punto = null;

									// Código a ejecutar si la instancia de familia tiene punto de cálculo de habitación.
									if (instanciaDeFamilia.HasSpatialElementCalculationPoint)
									{
										// Asignación de valor a la variable que almacena el punto de la instancia de familia.
										punto = instanciaDeFamilia.GetSpatialElementCalculationPoint();
									}

									// Código a ejecutar si la instancia de familia no tiene punto de cálculo de habitación.
									else
									{
										// Obtención de la localización de la instancia de familia.
										Location localizacion = instanciaDeFamilia.Location;

										// Código a ejecutar si la localización es un punto.
										if (localizacion is LocationPoint)
										{
											// Asignación de valor a la variable que almacena el punto de la instancia de familia.
											LocationPoint localizacionPunto = localizacion as LocationPoint;
											punto = localizacionPunto.Point;
										}

										// Código a ejecutar si la localización es una línea.
										else if (localizacion is LocationCurve)
										{
											// Acceso a la línea de localización de la instancia de familia y
											// Asignación de valor a la variable que almacena su punto con el punto medio de la misma.
											LocationCurve localizacionCurva = localizacion as LocationCurve;
											Curve curva = localizacionCurva.Curve;
											punto = curva.ComputeDerivatives(0.5, true).Origin;
										}
									}

									// Código a ejecutar si se ha obtenido un punto para localizar la instancia de familia.
									if (punto != null)
									{
										// Código a ejecutar si el punto está dentro de la habitación.
										if (cualquierHabitacion.IsPointInRoom(punto))
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


							//// Recuperación del sólido de la habitación.
							//GeometryElement geometria = cualquierHabitacion.get_Geometry(new Options());

							//Solid solido = null;
							//foreach(GeometryObject cualquierObjetoGeometrico in geometria)
							//{
							//	if(cualquierObjetoGeometrico is Solid)
							//	{
							//		solido = cualquierObjetoGeometrico as Solid;
							//		break;
							//	}
							//}

							//if(solido != null)
							//{
							//	ElementIntersectsSolidFilter filtroDeIntersccionConSolido = new ElementIntersectsSolidFilter(solido);

							//	FilteredElementCollector colectorDeElementosEnHabitacion = new FilteredElementCollector(documento).WherePasses(filtroDeIntersccionConSolido);

							//	string nombreYNumero = cualquierHabitacion.Name;

							//	foreach (Element cualquierElemento in colectorDeElementosEnHabitacion)
							//	{
							//		Parameter parametroComentarios = cualquierElemento.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

							//		if (parametroComentarios != null)
							//		{
							//			parametroComentarios.Set(nombreYNumero);
							//		}
							//	}
							//}
						}
					}

					transaccion.Commit();
				}
				return Result.Succeeded;
			}
		}
	}
}
