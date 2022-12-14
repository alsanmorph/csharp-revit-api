using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB;

namespace SeleccionMultiple
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
			IList<Reference> referencias = seleccion.PickObjects(
				ObjectType.Element,
				"Selecciona varios elementos");

			// Creación de un cuadro de diálogo personalizado para mostrar información de la selección.
			TaskDialog vistaConInformacion = new TaskDialog("Información de la selección");
			vistaConInformacion.MainInstruction = $"Las categorías de los {referencias.Count} elementos selecionados son:";
			vistaConInformacion.MainContent = "Desplegar el diálogo para obtener mas información";

			// Creación de una cadena de texto con la lista de categorías;
			string listaDeCategorias = String.Empty;

			// Iteración por cada una de las referencias para obtener su categoría.
			foreach(Reference cualquierReferencia in referencias)
			{
				// Creación de una variable local para almacenar el elemento correspondiente a la referencia.
				Element elemento = documento.GetElement(cualquierReferencia);

				// Código a ejecutar si la lista de categorias no contiene la categoría del elemento.
				if (!listaDeCategorias.Contains(elemento.Category.Name))
				{
					// Código a ejecutar si la lista de categorías está vacía.
					if (listaDeCategorias.Equals(string.Empty))
					{
						listaDeCategorias +=
							elemento.Category.Name;
					}

					// Código a ejecutar en caso contrario.
					else
					{
						listaDeCategorias +=
							Environment.NewLine +
							elemento.Category.Name;
					}
	
				}
			}

			// Adición de la lista de categorías seleccionadas al cuadro de diálogo.
			vistaConInformacion.ExpandedContent = listaDeCategorias;

			// Publicación del cuadro de diálogo.
			vistaConInformacion.Show();

			// Retorno del método.
			return Result.Succeeded;
		}
	}
}
