using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscrituraDeArchivo
{
	[Transaction(TransactionMode.Manual)]
	public class ComandoExterno : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			Document documento = commandData.Application.ActiveUIDocument.Document;
			Selection seleccion = commandData.Application.ActiveUIDocument.Selection;

			FilteredElementCollector colectorDeMobiliario = new FilteredElementCollector(documento).OfCategory(BuiltInCategory.OST_Furniture).WhereElementIsNotElementType();

			List<string> lineasDeTexto = new List<string>();

			foreach (Element cualquierElemento in colectorDeMobiliario)
			{
				Parameter parametroComentarios = cualquierElemento.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
				string comentarios = parametroComentarios.AsString();
				if (comentarios is null)
				{
					comentarios = string.Empty;
				}

				if (parametroComentarios != null)
				{
					string lineaDeTexto = $"{cualquierElemento.Id.IntegerValue};{cualquierElemento.Name};{comentarios}";
					lineasDeTexto.Add(lineaDeTexto);
				}
			}

			File.WriteAllLines(@"C:\Users\chema.jimenez\Documents\_BORRAR\Morph.txt", lineasDeTexto);


			return Result.Succeeded;
		}
	}
}
