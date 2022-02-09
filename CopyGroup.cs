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

namespace Z2_CopyGroup
{
    [Transaction(TransactionMode.Manual)]
    public class CopyGroup : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Получаем доступ к Revit, активному документу, базе данных документа
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // выбор группы
            Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
            Element element = doc.GetElement(reference);
            Group group = element as Group;

            // выбор точки
            XYZ point = uidoc.Selection.PickPoint("Укажите точку вставки");

            //размещение группы
            Transaction transaction = new Transaction(doc);
            transaction.Start("Копирование группы");
            doc.Create.PlaceGroup(point, group.GroupType);
            transaction.Commit();

            return Result.Succeeded;
        }
    }
}
