using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;

namespace AccesoAParametro
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

			// Provocar que el usuario seleccione un elemento.
			Reference referencia = seleccion.PickObject(
				ObjectType.Element,
				"Selecciona un elemento");

			// Declaración de una variable local con el elemento obtenido a través de la referencia seleccionada.
			Element elementoSeleccionado = documento.GetElement(referencia);

			// Declaración de una variable con la lista de parámetros del elemento.
			string textoDeInformacion = string.Empty;

			// Acceso al parámetro de sistema 'Comentarios' a través de su BuiltinParameter.
			Parameter parametroComentarios = elementoSeleccionado.get_Parameter(
				BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);

			// Adición al texto informativo de la información del parámetro 'Comentarios'.
			textoDeInformacion += string.Format("El parámetro de sistema con el nombre {0}, tiene el valor {1}",
				parametroComentarios.Definition.Name,
				parametroComentarios.AsString() +
				Environment.NewLine);

			// Acceso al parámetro creado por usuario 'Color de pintura'.
			Parameter parametroColorDePintura = elementoSeleccionado.LookupParameter("Color de pintura");

			// Adición al texto informativo de la información del parámetro 'Color de pintura'.
			textoDeInformacion += string.Format("El parámetro con el nombre {0}, tiene el valor {1}",
				parametroColorDePintura.Definition.Name,
				parametroColorDePintura.AsString());


			// Publicación de un cuadro de diálogo con la lista de parámetros.
			TaskDialog.Show(
				"Lista de prámetros",
				textoDeInformacion);

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
