using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertarElementosDeAnotacion
{
	[Transaction(TransactionMode.Manual)]
	public class Class1 : IExternalCommand
	{
		// Método de inicio de Revit.
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			Document documento = commandData.Application.ActiveUIDocument.Document;

			ElementId idDeTipoDeEtiqueta = new ElementId(1101415);

			ElementId idDeVista = documento.ActiveView.Id;


			FilteredElementCollector colectorDeBandejas = new FilteredElementCollector(documento).OfCategory(
				BuiltInCategory.OST_CableTray).WhereElementIsNotElementType();


			double xMin = double.MaxValue;
			double yMin = double.MaxValue;
			double xMax = double.MinValue;
			double yMax = double.MinValue;

			Dictionary<Element, XYZ> bandejasYPuntos = new Dictionary<Element, XYZ>();

			foreach(Element cualquierBandeja in colectorDeBandejas)
			{
				LocationCurve localizacion = cualquierBandeja.Location as LocationCurve;
				Curve curva = localizacion.Curve;
				XYZ puntodeInicio = curva.GetEndPoint(0);
				XYZ puntoDeFin = curva.GetEndPoint(1);
				XYZ puntoMedio = (puntodeInicio + puntoDeFin) / 2;
				bandejasYPuntos.Add(cualquierBandeja, puntoMedio);
				if(puntoMedio.X < xMin)
				{
					xMin = puntoMedio.X;
				}
				if(puntoMedio.Y < yMin)
				{
					yMin = puntoMedio.Y;
				}
				if(puntoMedio.X > xMax)
				{
					xMax = puntoMedio.X;
				}
				if (puntoMedio.Y > yMax)
				{
					yMax = puntoMedio.Y;
				}
			}

			XYZ puntoMinimoDeTramo = new XYZ(xMin, yMin, 0);
			XYZ puntoMaximoDeTramo = new XYZ(xMax, yMax, 0);
			XYZ puntoMedioDeTramo = (puntoMinimoDeTramo + puntoMaximoDeTramo) / 2;
			double distancia = double.MaxValue;
			XYZ puntoMedioDeBandejaAEtiquetar = null;
			Element bandejaAEtiquetar = null;

			foreach(KeyValuePair<Element, XYZ> cualquierBandeja in bandejasYPuntos)
			{
				if (puntoMedioDeTramo.DistanceTo(cualquierBandeja.Value) < distancia)
				{
					bandejaAEtiquetar = cualquierBandeja.Key;
					puntoMedioDeBandejaAEtiquetar = cualquierBandeja.Value;
					distancia = puntoMedioDeTramo.DistanceTo(cualquierBandeja.Value);
				}
			}

			//Element elemento = documento.GetElement(new ElementId(1100719));

			//LocationCurve localizacion = elemento.Location as LocationCurve;

			//Curve curva = localizacion.Curve;

			//XYZ puntodeInicio = curva.GetEndPoint(0);
			//XYZ puntoDeFin = curva.GetEndPoint(1);

			//XYZ puntoMedio = (puntodeInicio + puntoDeFin) / 2;


			//XYZ punto = new XYZ();



			Reference referencia = new Reference(bandejaAEtiquetar);

			using (Transaction transaccion = new Transaction(documento))
			{
				if(transaccion.Start("Colocación de etiqueta") == TransactionStatus.Started)
				{
					IndependentTag etiqueta = IndependentTag.Create(
						documento,
						idDeTipoDeEtiqueta,
						idDeVista,
						referencia,
						false,
						TagOrientation.Horizontal,
						puntoMedioDeBandejaAEtiquetar);


					BoundingBoxXYZ caja = etiqueta.get_BoundingBox(documento.ActiveView);
					XYZ puntoMinimo = caja.Min;
					XYZ puntoMaximo = caja.Max;
					XYZ puntoMedioDeEtiqueta = (puntoMinimo + puntoMaximo) / 2;

					//ElementTransformUtils.MoveElement(
					//	documento,
					//	etiqueta.Id,
					//	puntoMedio - puntoMedioDeEtiqueta);

					transaccion.Commit();
				}
			}

			return Result.Succeeded;
		}

	}
}
