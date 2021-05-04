using System.Xml;
using CrystalDecisions.ReportAppServer.ClientDoc;

namespace rpt_diff.RptConvert.Converters
{
    /*
    *  ReportClientDocumentModel
    *  - newer
    *  - contains all atributes availible from rpt file
    *  - can be used in future to merge changes or back convert from xml to rpt
    */
    public class ReportClientDocumentModel
    {
        public static void ProcessReport(ISCDReportClientDocument report, XmlWriter xmlw, string reportDoc = "ReportClientDocument")
        {
            xmlw.WriteStartElement(reportDoc);
            xmlw.WriteAttributeString("DisplayName", report.DisplayName);
            xmlw.WriteAttributeString("IsModified", report.IsModified.ToStringSafe());
            xmlw.WriteAttributeString("IsOpen", report.IsOpen.ToStringSafe());
            xmlw.WriteAttributeString("IsReadOnly", report.IsReadOnly.ToStringSafe());
            xmlw.WriteAttributeString("LocaleID", report.LocaleID.ToStringSafe());
            xmlw.WriteAttributeString("MajorVersion", report.MajorVersion.ToStringSafe());
            xmlw.WriteAttributeString("MinorVersion", report.MinorVersion.ToStringSafe());
            xmlw.WriteAttributeString("Path", report.Path);
            xmlw.WriteAttributeString("PreferredViewingLocaleID", report.PreferredViewingLocaleID.ToStringSafe());
            xmlw.WriteAttributeString("ProductLocaleID", report.ProductLocaleID.ToStringSafe());
            xmlw.WriteAttributeString("ReportAppServer", report.ReportAppServer);
            Controllers.Process(report.CustomFunctionController, xmlw);
            Controllers.Process(report.DatabaseController, xmlw);
            Controllers.Process(report.DataDefController, xmlw);
            Controllers.Process(report.PrintOutputController, xmlw);
            Controllers.Process(report.ReportDefController, xmlw);
            ReportDefModel.ProcessReportOptions(report.ReportOptions, xmlw);
            Controllers.Process(report.SubreportController, xmlw);
            DataDefModel.ProcessSummaryInfo(report.SummaryInfo, xmlw);
            xmlw.WriteEndElement();
        }
    }
}
