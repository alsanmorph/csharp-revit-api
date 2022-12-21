using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.DB.Architecture;

namespace DibujarMuro
{
	[Transaction(TransactionMode.Manual)]
	public class Class1 : IExternalCommand
	{
		// Método de inicio de Revit.
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			Document documento = commandData.Application.ActiveUIDocument.Document;

			//XYZ puntodeInsercion = new XYZ();

			FilteredElementCollector colectorDeFamilias = new FilteredElementCollector(documento).OfClass(typeof(Family));
			//Family familiaAInsertar = colectorDeFamilias.FirstOrDefault(cualquierElemento => cualquierElemento.Name == "Silla") as Family;

			//Family familiasAInsertar = (
			//	from cualquierFamilia
			//	in colectorDeFamilias
			//	where cualquierFamilia.Name == "Silla"
			//	select cualquierFamilia).FirstOrDefault() as Family;



			Family familiaAInsertar = null;

			foreach (Family cualquierFamilia in colectorDeFamilias)
			{
				if (cualquierFamilia.Name == "Silla")
				{
					familiaAInsertar = cualquierFamilia;
					break;
				}
			}

			FamilySymbolFilter filtroDeTiposDeFamilia = new FamilySymbolFilter(familiaAInsertar.Id);
			FamilySymbol tipoDeFamilia = new FilteredElementCollector(documento).WherePasses(filtroDeTiposDeFamilia).FirstOrDefault(
				cualquierTipo =>
				cualquierTipo.Name == "Generic") as FamilySymbol;

			FamilyInstanceFilter filtroDeInstanciasDeFamilia = new FamilyInstanceFilter(documento, tipoDeFamilia.Id);
			FilteredElementCollector colectorDeInstanciasDeSilla = new FilteredElementCollector(documento).WherePasses(filtroDeInstanciasDeFamilia);


			FilteredElementCollector colectorDeHabitaciones = new FilteredElementCollector(documento).OfCategory(BuiltInCategory.OST_Rooms);
			
			
			using(Transaction transaccion = new Transaction(documento))
			{
				if(transaccion.Start("inserción de familia") == TransactionStatus.Started)
				{
					foreach(Room cualquierHabitacion in colectorDeHabitaciones)
					{
						Parameter parametroComentarios = cualquierHabitacion.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

						if(parametroComentarios.AsString() == "Silla")
						{
							XYZ puntoDeInsercion = (cualquierHabitacion.Location as LocationPoint).Point;

							Element silla = null;
							foreach(Element cualquierSillaExistente in colectorDeInstanciasDeSilla)
							{
								Parameter parametroComentariosDeSilla = cualquierSillaExistente.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

								if(parametroComentariosDeSilla.AsString() == cualquierHabitacion.UniqueId)
								{
									silla = cualquierSillaExistente;
									break;
								}
							}


							if(silla != null)
							{
								XYZ puntoDeInsercionDeSilla = (silla.Location as LocationPoint).Point;

								if (!puntoDeInsercionDeSilla.IsAlmostEqualTo(puntoDeInsercion))
								{
									ElementTransformUtils.MoveElement(
									documento,
									silla.Id,
									puntoDeInsercion - puntoDeInsercionDeSilla);
								}
							}

							else
							{
								FamilyInstance elementoCreado = documento.Create.NewFamilyInstance(
								puntoDeInsercion,
								tipoDeFamilia,
								StructuralType.NonStructural);

								Parameter parametroComentariosDeSilla = elementoCreado.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

								parametroComentariosDeSilla.Set(cualquierHabitacion.UniqueId);
							}








						}
					}
					transaccion.Commit();
				}
			}

			








			//Document documento = commandData.Application.ActiveUIDocument.Document;

			//// Obtención de un nivel.
			//FilteredElementCollector colectorDeNiveles = new FilteredElementCollector(
			//	documento).OfCategory(BuiltInCategory.OST_Levels).WhereElementIsNotElementType();
			//Element primerNivelEncontrado = colectorDeNiveles.FirstOrDefault();
			//ElementId idDeNivel = primerNivelEncontrado.Id;

			//FilteredElementCollector colectorDeTiposDeBandejas = new FilteredElementCollector(
			//	documento).OfCategory(BuiltInCategory.OST_CableTray).WhereElementIsElementType();
			//Element primerTipoDebandejaEncontrado = colectorDeTiposDeBandejas.FirstOrDefault();
			//ElementId idDeTipoDeBandeja = primerTipoDebandejaEncontrado.Id;

			//// Definición de la línea del muro.
			//XYZ puntoInicial = new XYZ();
			//XYZ puntoFinal = new XYZ(
			//	UnitUtils.ConvertToInternalUnits(20, UnitTypeId.Meters),
			//	UnitUtils.ConvertToInternalUnits(20, UnitTypeId.Meters),
			//	0);
			//Line linea = Line.CreateBound(puntoInicial, puntoFinal);

			//using(Transaction transaccion = new Transaction(documento))
			//{
			//	if(transaccion.Start("Dibujar muro") == TransactionStatus.Started)
			//	{
			//		Wall muro = Wall.Create(
			//			documento,
			//			linea,
			//			idDeNivel,
			//			false);


			//		CableTray bandeja = CableTray.Create(
			//			documento,
			//			idDeTipoDeBandeja,
			//			puntoInicial,
			//			puntoFinal,
			//			idDeNivel);


			//		transaccion.Commit();
			//	}
			//}



			return Result.Succeeded;
		}

	}
}
