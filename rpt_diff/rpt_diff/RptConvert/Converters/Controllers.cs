using System.Xml;
using CrystalDecisions.ReportAppServer.Controllers;

namespace rpt_diff.RptConvert.Converters
{
    public static partial class Controllers
    {
        public static void Process(CustomFunctionController value, XmlWriter xmlw)
        {
            DataDefModel.Process(value.GetCustomFunctions(), xmlw);
        }

        public static void Process(DatabaseController dc, XmlWriter xmlw) => DataDefModel.Process(dc.Database, xmlw);

        public static void Process(DataDefController ddc, XmlWriter xmlw) => DataDefModel.Process(ddc.DataDefinition, xmlw);

        public static void Process(PrintOutputController poc, XmlWriter xmlw)
        {
            ReportDefModel.Process(poc.GetPrintOptions(), xmlw);
            ReportDefModel.Process(poc.GetSavedXMLExportFormats(), xmlw);
        }

        public static void Process(ReportDefController2 rdc, XmlWriter xmlw)
        {
            ReportDefModel.Process(rdc.ReportDefinition, xmlw);
        }

        public static void Process(SubreportController sc, XmlWriter xmlw)
        {
            foreach (string Subreport in sc.GetSubreportNames())
            {
                xmlw.WriteStartElement("Subreport");
                Process(sc.GetSubreport(Subreport), xmlw);
                ReportDefModel.ProcessSubreportLinks(sc.GetSubreportLinks(Subreport), xmlw);
                xmlw.WriteEndElement();
            }
        }

        private static void Process(SubreportClientDocument scd, XmlWriter xmlw)
        {
            xmlw.WriteAttributeString("EnableOnDemand", scd.EnableOnDemand.ToStringSafe());
            xmlw.WriteAttributeString("EnableReimport", scd.EnableReimport.ToStringSafe());
            xmlw.WriteAttributeString("IsImported", scd.IsImported.ToStringSafe());
            xmlw.WriteAttributeString("Name", scd.Name);
            xmlw.WriteAttributeString("SubreportLocation", scd.SubreportLocation);
            Process(scd.DatabaseController, xmlw);
            Process(scd.DataDefController, xmlw);
            Process(scd.ReportDefController, xmlw);
            ReportDefModel.ProcessReportOptions(scd.ReportOptions, xmlw);
        }
    }
}
