using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace ConversionDeUnidades
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
			#region Declaración de variables locales.
			// Declaración de una variable local con el documento activo.
			Document documento = DatosDelComandoExterno.Application.ActiveUIDocument.Document;

			// Declaración de una variable local con la selección del documento.
			Selection seleccion = DatosDelComandoExterno.Application.ActiveUIDocument.Selection;

			// Almacenamiento del mensaje a mostrar en una cadena de texto.
			string informaciónAMostrar = "El valor de la longitud del elemento es:" +
				Environment.NewLine;
			#endregion

			#region Selección de un elemento.
			// Provocar que el usuario seleccione un elemento.
			Reference referenciaSeleccionada = seleccion.PickObject(
				ObjectType.Element,
				"Seleccione un elemento para consultar su longitud.");

			// Obtener el elemento correspondiente a la referencia seleccionada.
			Element elementoSeleccionado = documento.GetElement(referenciaSeleccionada);
			#endregion

			#region Lectura del parámetro.
			// Buscar el parámetro de sistema que almacena la longitud de un elemento lineal
			// (Muro, tubería...)
			Parameter parametroDeLongitud = elementoSeleccionado.get_Parameter(
				BuiltInParameter.CURVE_ELEM_LENGTH); 
			#endregion

			// Código a ejecutar si el elemento NO contiene el parámetro de sistema buscado.
			// Si el elemento no lo contiene, la variable donde se almacena el mismo tendrá
			// un valor nulo (null).
			if(parametroDeLongitud == null)
			{
				// Generación del mensaje de error.
				mensaje = "El elemento seleccionado no contiene el parámetro de sistema " +
					"que almacena la longitud de los elemento lineales";

				// Finalización del comando externo devolviendo un resultado fallido.
				return Result.Failed;
			}

			// Código a ejecutar si el elemento SI contiene el parámetro de sistema buscado.
			else
			{
				// Lectura del valor (doble) del parámetro en unidades internas de Revit.
				double longitudEnUnidadesInternas = parametroDeLongitud.AsDouble();

				// Adición del valor del parámetro en unidades internas de revit
				// al mensaje a mostrar.
				informaciónAMostrar += "En unidades internas: " +
					longitudEnUnidadesInternas;

				// Adición del valor del parámetro en milímetros al mensaje a mostrar.
				informaciónAMostrar += Environment.NewLine +
					"En milímetros: " +
					UnitUtils.ConvertFromInternalUnits(
						longitudEnUnidadesInternas,
						DisplayUnitType.DUT_MILLIMETERS);

				// Adición del valor del parámetro en metros al mensaje a mostrar.
				informaciónAMostrar += Environment.NewLine +
					"En metros: " +
					UnitUtils.ConvertFromInternalUnits(
						longitudEnUnidadesInternas,
						DisplayUnitType.DUT_METERS);
			}

			// Publicación del mensaje con información.
			TaskDialog.Show(
				"Conversión de unidades",
				informaciónAMostrar);

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
