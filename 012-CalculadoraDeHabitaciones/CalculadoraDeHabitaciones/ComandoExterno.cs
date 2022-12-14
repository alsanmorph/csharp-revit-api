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

namespace CalculadoraDeHabitaciones
{
	[Transaction(TransactionMode.Manual)]
	public class ComandoExterno : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			// Inicialización de la interfaz gráfica.
			VentanaPrincipal ventana = new VentanaPrincipal();

			// Declaración de una variable local con el documento activo.
			Document documento = commandData.Application.ActiveUIDocument.Document;

			// Declaración de una variable local con la selección del documento.
			Selection seleccion = commandData.Application.ActiveUIDocument.Selection;
			
			// Recuperación de todas las habitaciones del modelo.
			FilteredElementCollector colectorDeHabitaciones = new FilteredElementCollector(documento);
			colectorDeHabitaciones.OfCategory(BuiltInCategory.OST_Rooms);

			// Recuperación de una habitación para obtener sus parámetros.
			Element habitacionDeMuestra = colectorDeHabitaciones.FirstOrDefault();

			// Código a ejecutar si la habitación de muestra está disponible.
			// Si no estuvera, esto significa que el modelo no contiene habitaciones.
			if(habitacionDeMuestra is null)
			{
				message = "El modelo no contiene habitaciones";
				return Result.Failed;
			}

			// Recuperación de los parámetros de habitación.
			List<string> parametrosDeHabitacion = new List<string>();
			foreach(Parameter cualquierParametro in habitacionDeMuestra.Parameters)
			{
				if(cualquierParametro.StorageType == StorageType.Double)
				{
					parametrosDeHabitacion.Add(cualquierParametro.Definition.Name);
				}
			}
			parametrosDeHabitacion.Sort();

			// Adición de la colección de nombres de parámetros a la caja combo de la ventana principal.
			ventana.ListadoDeParametros.ItemsSource = parametrosDeHabitacion;

			// Publicación de la ventana.
			ventana.ShowDialog();

			// Código a ejecutar si desde la ventana principal se ha solicitado cancelar la ejecución de la herramienta.
			if (ventana.Ejecutar == false)
			{
				message = "Operación cancelada por el usuario.";
				return Result.Cancelled;
			}

			// Decalaración de una variable local que indique si, desde la ventana principal se ha solicitado
			// hacer una selección manual de las habitaciones a modificar.
			bool seleccionManual = (bool)ventana.SeleccionManual.IsChecked;

			// Inicialización de una colección para almacenar las habitaciones a modificarl
			List<SpatialElement> habitaciones = new List<SpatialElement>();

			// Código a ejecutar si se ha de hacer una selección manual.
			if(seleccionManual)
			{
				// Realización de la selección manual de habitaciones.
				IList<Reference> referencias = seleccion.PickObjects(ObjectType.Element, "Selecciona las habitaciones a calcular");

				// Iteración sombre todas las referencias obtenidas.
				foreach(Reference cualquierReferencia in referencias)
				{
					// Recuperación de cualquier elemento seleccionado.
					Element elemento = documento.GetElement(cualquierReferencia);

					// Código a ejecutar si el elemento recuperado es un elemento espacial.
					if(elemento is SpatialElement)
					{
						// Adición a la colección de habitaciones del elemento COMO ELEMENTO ESPACIAL.
						habitaciones.Add(elemento as SpatialElement);
					}
				}
			}

			// En caso contrario, se añaden todas las habitaciones del modelo.
			else
			{
				foreach(Element cualquierElemento in colectorDeHabitaciones)
				{
					// Adición a la colección de habitaciones del elemento COMO ELEMENTO ESPACIAL.
					habitaciones.Add(cualquierElemento as SpatialElement);
				}
			}

			// Obtención de la cadena de texto introducida para establecer la cobertura de cada detector.
			string textoCobertura = ventana.CoberturaDetector.Text;

			// Inicialización de una variable local para almacenar el valor numérico de la cobertura del detector.
			double coberturaDeDetector = 0;

			// Código a ejecutar si no es posible convertir el texto introducido en un número válido.
			if(double.TryParse(textoCobertura, out coberturaDeDetector) is false)
			{
				message = "La cobertura del detector no está correstamente escrita.";
				return Result.Failed;
			}

			// Conversión de la cobertuira del detector a unidades internas de Revit.
			double coberturaDeDetectorUnidadesInternas = UnitUtils.ConvertToInternalUnits(coberturaDeDetector, UnitTypeId.SquareMeters);
			
			// Inicialización de un contador de habitaciones actualizadas.
			int contadorDeHabitacionesActualizadas = 0;

			// Obtención del nombre del parámetro de la habitación en el que se almacenará el número de detectores calculado.
			string parametroNumeroDetectores = (string)ventana.ListadoDeParametros.SelectedItem;

			// Inicialización de la transacción.
			using(Transaction transaction = new Transaction(documento))
			{
				if (transaction.Start("Cálculo de número de detectores.") == TransactionStatus.Started)
				{
					// Iteración sobre todos los elementos espaciuales (habitación o espacio) a calcular.
					foreach (SpatialElement cualquierHabitacion in habitaciones)
					{
						// Obtención del área.
						double area = cualquierHabitacion.Area;

						// Cálculo del número de detectores.
						double numeroDeDetectores = area / coberturaDeDetectorUnidadesInternas;

						// Redondeo a un número entero de número de detectores.
						int numeroEnteroDeDetectores = (int)Math.Ceiling(numeroDeDetectores);

						// Obtención del parámetro que almacenará el número de detectores.
						Parameter parametroDeNumeroDeDetectores = cualquierHabitacion.LookupParameter(parametroNumeroDetectores);

						// Recuperación del número de detectores que originalmente tiene la habitación.
						int numeroAntiguoDeDetectores = parametroDeNumeroDeDetectores.AsInteger();

						// Código a ejecutar si el numero nuevo de detectores es diferente al número ya almacenado previamente.
						if(numeroEnteroDeDetectores != numeroAntiguoDeDetectores)
						{
							// Actualización del valor del parámetro.
							parametroDeNumeroDeDetectores.Set(numeroEnteroDeDetectores);

							// Incremento del contador.
							contadorDeHabitacionesActualizadas++;
						}
					}
					transaction.Commit();
				}
			}

			// Publicación del mensaje final.
			TaskDialog.Show("Cálculo de número de detectores", $"Se han actualizado {contadorDeHabitacionesActualizadas} habitaciones");
			return Result.Succeeded;
		}
	}
}
