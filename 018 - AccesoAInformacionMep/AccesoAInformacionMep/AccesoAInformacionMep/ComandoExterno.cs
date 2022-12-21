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
			// Acceso al documento activo y a la selección del mismo.
			Document documento = commandData.Application.ActiveUIDocument.Document;
			Selection seleccion = commandData.Application.ActiveUIDocument.Selection;

			// solicitud al usuario de la selección de un elemento.
			Reference referencia = seleccion.PickObject(ObjectType.Element, "Selecciona un elemento MEP");
			Element elemento = documento.GetElement(referencia);

			// Declaración de una variable local para almacenar el gestor de conectores del elemento.
			ConnectorManager connectorManager = null;

			// Código a ejecutar si el elemento seleccionado es un elemento nativo MEP (conducto, tubería o bandeja).
			if (elemento is MEPCurve)
			{
				// Casteo del elemento a una instancia de la clase MEPCurve.
				MEPCurve elementoMep = elemento as MEPCurve;

				// Asignación de valor a la variable que almacena el gestor de conectores.
				connectorManager = elementoMep.ConnectorManager;
			}

			// Código a ejecutar si el elemento seleccionado es una instancia de familia.
			else if (elemento is FamilyInstance)
			{
				// Casteo del elemento a una instancia de la clase FamilyInstance.
				FamilyInstance instanciaDeFamilia = elemento as FamilyInstance;

				// Asignación de valor a la valriable que almacena el gestor de conectores.
				connectorManager = instanciaDeFamilia.MEPModel.ConnectorManager;
			}

			// Código a ejecutar si el gestor de conectores tiene valor nulo.
			if (connectorManager is null)
			{
				message = "El elemento seleccionano no tiene información MEP";
				return Result.Failed;
			}

			// Acceso a la colección de conectores del elemento.
			ConnectorSet coleccionDeConectores = connectorManager.Connectors;

			// Inicialización de una coleccción de identificadores de elelemento para almacenar los elementos que serán seleccionados.
			List<ElementId> elementosConectados = new List<ElementId>();

			// Iteración entre todos los conectores.
			foreach (Connector cualquierconector in coleccionDeConectores)
			{
				// Código a ejecutar si el conector actual no es un conector lógico.
				if (cualquierconector.ConnectorType != ConnectorType.Logical)
				{
					// Acceso a todos los conectores que estén conectados al conector actual.
					ConnectorSet conectoresConectados = cualquierconector.AllRefs;

					// Iteración sobre todos los conectores conectados.
					foreach (Connector cualquierConectorconectado in conectoresConectados)
					{
						// Acceso al elemento propietario del conector.
						Element propietario = cualquierConectorconectado.Owner;

						// Código a ejecutar si el elemento propietario no es un sistema MEP y es diferente al elemento seleccionado.
						if (propietario is MEPSystem == false && propietario.Id.IntegerValue != elemento.Id.IntegerValue)
						{
							// Adición del identificador de elemento a la colección de elementos a seleccionar.
							elementosConectados.Add(propietario.Id);
						}
					}
				}
			}

			// Código a ejecutar si la colección de identificadores de elemento no está vacía.
			if (elementosConectados.Count > 0)
			{
				// Selección de los elementos.
				seleccion.SetElementIds(elementosConectados);
			}
			
			return Result.Succeeded;
		}
	}
}
