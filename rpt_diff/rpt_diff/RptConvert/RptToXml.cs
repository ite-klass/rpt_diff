using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace rpt_diff.RptConvert
{
    public class RptToXml
    {
        /*
         * ConvertRptToXml
         * Opens report file found in rptPath and converts it to xml using model specified by parameter model.
         * params:
         *  rptPath - full path to rpt file to be converted to xml
         *  model   - specifies which object model use to convert
         *          - 0 = ReportDocumentModel
         *          - 1 = ReportClientDocumentModel (RAS)
         */
        public static string ConvertRptToXml(string rptPath, ModelType model)
        {
            var report = new ReportDocument();
            report.Load(rptPath, OpenReportMethod.OpenReportByTempCopy);

            var xmlPath = Path.ChangeExtension(rptPath, "xml");
            using (var converter = new RptConverter(report, xmlPath, model))
            {
                converter.Convert();
            }

            return xmlPath;
        }
    }
}
