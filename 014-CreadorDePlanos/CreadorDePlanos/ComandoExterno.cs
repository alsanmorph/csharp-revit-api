using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreadorDePlanos
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

			// Recuperación de selección activa.
			ICollection<ElementId> idsDeSeleccion = seleccion.GetElementIds();

			// Recuperación del cajetín a utilizar.
			string nombreDeTipoDeCajetin = "A1 metric";
			FilteredElementCollector colectorDeTiposDeCartela = new FilteredElementCollector(documento).OfCategory(
				BuiltInCategory.OST_TitleBlocks).WhereElementIsElementType();
			Element tipoDeCartela = null;
			foreach(Element cualquierElemento in colectorDeTiposDeCartela)
			{
				if(cualquierElemento.Name == nombreDeTipoDeCajetin)
				{
					tipoDeCartela = cualquierElemento;
					break;
				}
			}

			// Definición del punto de inserción de la vista.
			double desfaseEnX = UnitUtils.ConvertToInternalUnits(370, UnitTypeId.Millimeters);
			double desfaseEnY = UnitUtils.ConvertToInternalUnits(297, UnitTypeId.Millimeters);
			XYZ puntoDeInsercion = new XYZ(desfaseEnX, desfaseEnY, 0);

			// Inicialización de la transacción.
			using(Transaction transaccion = new Transaction(documento))
			{
				if(transaccion.Start("Creador de planos") == TransactionStatus.Started)
				{
					// Iteración sobre todos los identificadores de elemento de la selección activa.
					foreach (ElementId cualquierId in idsDeSeleccion)
					{
						// Recuperación del elemento.
						Element elementoSeleccionado = documento.GetElement(cualquierId);

						// Código a ejecutar si el elemento es una vista la cual puede ser introducida en un plano.
						if (elementoSeleccionado is TableView
							|| elementoSeleccionado is View3D
							|| elementoSeleccionado is ViewDrafting
							|| elementoSeleccionado is ViewPlan
							|| elementoSeleccionado is ViewSection)
						{
							// Obtención del parámetro que almacena el código de plano en el que se encuentra la vista.
							Parameter parametroNumeroDePlano = elementoSeleccionado.get_Parameter(BuiltInParameter.VIEWPORT_SHEET_NUMBER);
							string numeroDePlanoDondeEsta = parametroNumeroDePlano.AsString();
							
							// Código a ejecutar si la vista no se encuentra en ningún plano.
							if(string.IsNullOrEmpty(numeroDePlanoDondeEsta))
							{
								// Creación de un nuevo plano.
								ViewSheet planoCreado = ViewSheet.Create(documento, tipoDeCartela.Id);

								// Modificación del nombre del nuevo plano.
								planoCreado.Name = elementoSeleccionado.Name;

								// Código a ejecutar si es posible añadir la vista al plano recién creado.
								if (Viewport.CanAddViewToSheet(documento, planoCreado.Id, elementoSeleccionado.Id))
								{
									// Introcducción de la vista en el plano.
									Viewport ventanaGrafica = Viewport.Create(documento, planoCreado.Id, elementoSeleccionado.Id, puntoDeInsercion);

									// Obtención de los tipos de ventana gráfica disponibles.
									ICollection<ElementId> idsDeTipos = ventanaGrafica.GetValidTypes();

									// Localización del tipo de ventana a utilizar (tipo cuyo nombre sea "No Title").
									string nombreDeTipoDeVentana = "No Title";
									Element tipoDeVentana = null;

									foreach(ElementId cualquierIdDeVentana in idsDeTipos)
									{
										Element cualquierTipoDeVentana = documento.GetElement(cualquierIdDeVentana);
										if (cualquierTipoDeVentana.Name == nombreDeTipoDeVentana)
										{
											tipoDeVentana = cualquierTipoDeVentana;
											break;
										}
									}

									// Cambio del tipo de ventana.
									Parameter parametroDeTipo = ventanaGrafica.get_Parameter(BuiltInParameter.ELEM_TYPE_PARAM);
									parametroDeTipo.Set(tipoDeVentana.Id);
								}

								// Código a ejecutar si no es posible añadir la vista al plano recién creado.
								else
								{
									// Eliminación del plano recién creado.
									documento.Delete(planoCreado.Id);
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
