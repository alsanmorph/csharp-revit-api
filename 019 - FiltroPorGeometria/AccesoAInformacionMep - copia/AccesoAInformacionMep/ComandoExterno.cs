using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoAInformacionMep
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
			Document documentoVinculado = null;
			foreach(Document cualquierDocumentoAbierto in commandData.Application.Application.Documents)
			{
				if(cualquierDocumentoAbierto.Title == nombreDeModeloVinculado)
				{
					documentoVinculado = cualquierDocumentoAbierto;
					break;
				}
			}

			if(documentoVinculado is null)
			{
				message = "El docuemnto vinculado no está disponible.";
				return Result.Failed;
			}

			FilteredElementCollector colectorDeHabitaciones = new FilteredElementCollector(documentoVinculado).OfCategory(BuiltInCategory.OST_Rooms);

			using(Transaction transaccion = new Transaction(documento))
			{
				if(transaccion.Start("captura por solido") == TransactionStatus.Started)
				{
					foreach (Room cualquierHabitacion in colectorDeHabitaciones)
					{
						if (cualquierHabitacion.Area > 0)
						{




							BoundingBoxXYZ cajaDeHabitacion = cualquierHabitacion.get_BoundingBox(null);
							XYZ puntoMinimo = cajaDeHabitacion.Min;
							XYZ puntoMaximo = cajaDeHabitacion.Max;

							Outline limite = new Outline(puntoMinimo, puntoMaximo);

							BoundingBoxIntersectsFilter filtroDeInterseccion = new BoundingBoxIntersectsFilter(limite);

							FilteredElementCollector colectorDeElementos = new FilteredElementCollector(documento).WherePasses(filtroDeInterseccion);

							foreach(Element cualquierElemento in colectorDeElementos)
							{
								if(cualquierElemento is FamilyInstance)
								{
									FamilyInstance instanciaDeFamilia = cualquierElemento as FamilyInstance;

									XYZ punto = null;

									if (instanciaDeFamilia.HasSpatialElementCalculationPoint)
									{
										punto = instanciaDeFamilia.GetSpatialElementCalculationPoint();
									}
									else
									{
										Location localizacion = instanciaDeFamilia.Location;

										if(localizacion is LocationPoint)
										{
											LocationPoint localizacionPunto = localizacion as LocationPoint;
											punto = localizacionPunto.Point;
										}
										else if(localizacion is LocationCurve)
										{
											LocationCurve localizacionCurva = localizacion as LocationCurve;
											Curve curva = localizacionCurva.Curve;
											punto = curva.ComputeDerivatives(0.5, true).Origin;
										}
									}

									if(punto != null)
									{
										if (cualquierHabitacion.IsPointInRoom(punto))
										{
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


					FilteredElementCollector colectorDeMobiliario = new FilteredElementCollector(documento).OfCategory(BuiltInCategory.OST_Furniture).WhereElementIsNotElementType();

					List<string> lineasDeTexto = new List<string>();

					foreach(Element cualquierElemento in colectorDeMobiliario)
					{
						Parameter parametroComentarios = cualquierElemento.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
						string comentarios = parametroComentarios.AsString();
						if(comentarios is null)
						{
							comentarios = string.Empty;
						}

						if(parametroComentarios != null)
						{
							string lineaDeTexto = $"{cualquierElemento.Id.IntegerValue};{cualquierElemento.Name};{comentarios}";
							lineasDeTexto.Add(lineaDeTexto);
						}
					}

					File.WriteAllLines(@"C:\Users\chema.jimenez\Documents\_BORRAR\Morph.txt", lineasDeTexto);

				}
			}


			












































			//Reference referencia = seleccion.PickObject(ObjectType.Element, "Selecciona un elemento MEP");

			//Element elemento = documento.GetElement(referencia);

			//ConnectorManager connectorManager = null;

			//if(elemento is MEPCurve)
			//{
			//	MEPCurve elementoMep = elemento as MEPCurve;
			//	connectorManager = elementoMep.ConnectorManager;
			//}

			//else if(elemento is FamilyInstance)
			//{
			//	FamilyInstance instanciaDeFamilia = elemento as FamilyInstance;
			//	connectorManager = instanciaDeFamilia.MEPModel.ConnectorManager;
			//}

			//if(connectorManager is null)
			//{
			//	message = "El elemento seleccionano no tiene información MEP";
			//	return Result.Failed;
			//}


			//ConnectorSet coleccionDeConectores = connectorManager.Connectors;
			
			//List<ElementId> elementosConectados = new List<ElementId>();

			//foreach(Connector cualquierconector in coleccionDeConectores)
			//{
			//	if(cualquierconector.ConnectorType != ConnectorType.Logical)
			//	{
			//		ConnectorSet conectoresConectados = cualquierconector.AllRefs;


			//		foreach(Connector cualquierConectorconectado in conectoresConectados)
			//		{
			//			Element propietario = cualquierConectorconectado.Owner;
			//			if(propietario is MEPSystem == false && propietario.Id.IntegerValue != elemento.Id.IntegerValue)
			//			{
			//				elementosConectados.Add(propietario.Id);
			//			}
			//		}
			//	}
			//}

			//if(elementosConectados.Count > 0)
			//{
			//	seleccion.SetElementIds(elementosConectados);
			//}







			return Result.Succeeded;
		}
	}
}
