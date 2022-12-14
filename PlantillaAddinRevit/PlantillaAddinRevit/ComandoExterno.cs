using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace PlantillaAddinRevit
{
    [Transaction(TransactionMode.Manual)]
    public class ComandoExterno : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document documento = commandData.Application.ActiveUIDocument.Document;
            Selection seleccion = commandData.Application.ActiveUIDocument.Selection;

            TaskDialog.Show("Plantilla comando externo", "El comando externo se ha creado correctamente.");

            return Result.Succeeded;
        }
    }
}
