using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Web;

namespace Glz.Infrastructure
{
    public class ReportingTools
    {
        public static string ExportXls(List<dynamic> list,string [] colNames)
        {
            var tempFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "template.xls";
            var templateFilePath = HttpContext.Current.Server.MapPath("~/template.xls");
            var filePath = HttpContext.Current.Server.MapPath("~/temp") + "\\" + tempFileName;
            File.Copy(templateFilePath, filePath);
            Workbook workbook = new Workbook(filePath);

            Worksheet worksheet = workbook.Worksheets[0];
            Cells cells = worksheet.Cells;
            //cells.ImportCustomObjects(list, 1, 0, null);
            cells.ImportObjectArray(colNames, 0, 0,false);
            cells.ImportCustomObjects(list, null, false, 1, 0, list.Count, true, "yyyy/mm/dd", false);
            workbook.Save(filePath);
            return tempFileName;
        }

    }
}
