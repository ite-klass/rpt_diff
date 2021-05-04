using System;
using System.Text;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using rpt_diff.RptConvert.Converters;

namespace rpt_diff.RptConvert
{
    public class RptConverter : IDisposable
    {
        private readonly ReportDocument _doc;
        private readonly string _xmlPath;
        private readonly ModelType _model;
        private XmlTextWriter _writer;

        public RptConverter(ReportDocument doc, string xmlPath, ModelType model)
        {
            _doc = doc;
            _xmlPath = xmlPath;
            _model = model;
            _writer = new XmlTextWriter(xmlPath, Encoding.UTF8) { Formatting = Formatting.Indented };
        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        internal void Convert()
        {
            _writer.WriteStartDocument();
            if (_model == ModelType.ReportDocument)
            {
                ReportDocumentModel.ProcessReport(_doc, _writer);
            }
            else
            {
                ReportClientDocumentModel.ProcessReport(_doc.ReportClientDocument, _writer);
            }

            _writer.WriteEndDocument();
            _writer.Flush();
        }
    }
}
