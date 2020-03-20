using System;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace rpt_diff.RptConvert.Converters
{
    /*
    *  ReportDocumentModel
    *  - older
    *  - not include custom formulas
    */
    public class ReportDocumentModel
    {
        public static void ProcessReport(ReportDocument report, XmlWriter xmlw, string reportDoc = "ReportDocument")
        {
            xmlw.WriteStartElement(reportDoc);
            if (!report.IsSubreport) // not supported in subreportt
            {
                xmlw.WriteElementString("DefaultXmlExportSelection", report.DefaultXmlExportSelection.ToStringSafe());
                xmlw.WriteElementString("FileName", report.FileName);
                xmlw.WriteElementString("FilePath", report.FilePath);
                xmlw.WriteElementString("HasSavedData", report.HasSavedData.ToStringSafe());
                xmlw.WriteElementString("IsLoaded", report.IsLoaded.ToStringSafe());
                xmlw.WriteElementString("IsRPTR", report.IsRPTR.ToStringSafe());
                xmlw.WriteElementString("ReportAppServer", report.ReportAppServer);// <EMBEDDED_REPORT_ENGINE>
            }
            xmlw.WriteElementString("IsSubreport", report.IsSubreport.ToStringSafe());
            xmlw.WriteElementString("Name", report.Name);
            xmlw.WriteElementString("RecordSelectionFormula", report.RecordSelectionFormula);

            ProcessDatabase(report.Database, xmlw);
            ProcessDataDefinition(report.DataDefinition, xmlw);
            ProcessDataSourceConnections(report.DataSourceConnections, xmlw);
            ProcessReportDefinition(report.ReportDefinition, xmlw);
            if (!report.IsSubreport) // not supported in subreportt
            {
                ProcessPrintOptions(report.PrintOptions, xmlw);
                ProcessReportOptions(report.ReportOptions, xmlw);
                ProcessSubreports(report.Subreports, xmlw);
                ProcessSummaryInfo(report.SummaryInfo, xmlw);
                ProcessSavedXmlExportFormats(report.SavedXmlExportFormats, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessDatabase(Database db, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Database");
            ProcessTables(db.Tables, xmlw);
            ProcessLinks(db.Links, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessTables(Tables tbls, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Tables");
            xmlw.WriteElementString("Count", tbls.Count.ToStringSafe());
            foreach (Table tbl in tbls)
            {
                ProcessTable(tbl, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessTable(Table tbl, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Table");
            xmlw.WriteElementString("Location", tbl.Location);
            xmlw.WriteElementString("Name", tbl.Name);
            ProcessDatabaseFieldDefinitions(tbl.Fields, xmlw);
            ProcessTableLogOnInfo(tbl.LogOnInfo, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessDatabaseFieldDefinitions(DatabaseFieldDefinitions flds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DatabaseFieldDefinitions");
            xmlw.WriteElementString("Count", flds.Count.ToStringSafe());
            foreach (DatabaseFieldDefinition fld in flds)
            {
                ProcessDatabaseFieldDefinition(fld, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessDatabaseFieldDefinition(DatabaseFieldDefinition fld, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DatabaseFieldDefinition");
            xmlw.WriteElementString("FormulaName", fld.FormulaName);
            xmlw.WriteElementString("Kind", fld.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", fld.Name);
            xmlw.WriteElementString("NumberOfBytes", fld.NumberOfBytes.ToStringSafe());
            xmlw.WriteElementString("TableName", fld.TableName);
            xmlw.WriteElementString("UseCount", fld.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", fld.ValueType.ToStringSafe());
            xmlw.WriteEndElement();
        }
        private static void ProcessTableLogOnInfo(TableLogOnInfo tloi, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TableLogOnInfo");
            xmlw.WriteElementString("ReportName", tloi.ReportName);
            xmlw.WriteElementString("TableName", tloi.TableName);
            ProcessConnectionInfo(tloi.ConnectionInfo, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessConnectionInfo(ConnectionInfo ci, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ConnectionInfo");
            xmlw.WriteElementString("AllowCustomConnection", ci.AllowCustomConnection.ToStringSafe());
            xmlw.WriteElementString("DatabaseName", ci.DatabaseName);
            xmlw.WriteElementString("DBConnHandler", ci.DBConnHandler.ToStringSafe());
            xmlw.WriteElementString("IntegratedSecurity", ci.IntegratedSecurity.ToStringSafe());
            xmlw.WriteElementString("Password", ci.Password);
            xmlw.WriteElementString("ServerName", ci.ServerName);
            xmlw.WriteElementString("Type", ci.Type.ToStringSafe());
            xmlw.WriteElementString("UserID", ci.UserID);
            xmlw.WriteEndElement();
        }
        private static void ProcessLinks(TableLinks links, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TableLinks");
            xmlw.WriteElementString("Count", links.Count.ToStringSafe());
            foreach (TableLink link in links)
            {
                ProcessTableLink(link, xmlw);
            }

            xmlw.WriteEndElement();
        }
        private static void ProcessTableLink(TableLink link, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TableLink");
            xmlw.WriteElementString("JoinType", link.JoinType.ToStringSafe());
            xmlw.WriteElementString("SourceTable", link.SourceTable.Name);
            xmlw.WriteElementString("DestinationTable", link.DestinationTable.Name);
            foreach (DatabaseFieldDefinition sfld in link.SourceFields)
            {
                xmlw.WriteStartElement("SourceField");
                xmlw.WriteElementString("FormulaName", sfld.FormulaName);
                xmlw.WriteEndElement();
            }
            foreach (DatabaseFieldDefinition dfld in link.DestinationFields)
            {
                xmlw.WriteStartElement("DestinationField");
                xmlw.WriteElementString("FormulaName", dfld.FormulaName);
                xmlw.WriteEndElement();
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessDataDefinition(DataDefinition dd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DataDefinition");
            xmlw.WriteElementString("GroupSelectionFormula", dd.GroupSelectionFormula);
            xmlw.WriteElementString("GroupSelectionFormulaRaw", dd.GroupSelectionFormulaRaw);
            xmlw.WriteElementString("RecordSelectionFormula", dd.RecordSelectionFormula);
            xmlw.WriteElementString("RecordSelectionFormulaRaw", dd.RecordSelectionFormulaRaw);
            xmlw.WriteElementString("SavedDataSelectionFormula", dd.SavedDataSelectionFormula);
            xmlw.WriteElementString("SavedDataSelectionFormulaRaw", dd.SavedDataSelectionFormulaRaw);
            ProcessFormulaFieldDefinitions(dd.FormulaFields, xmlw);
            ProcessGroupNameFieldDefinitions(dd.GroupNameFields, xmlw);
            ProcessGroups(dd.Groups, xmlw);
            ProcessParameterFieldDefinitions(dd.ParameterFields, xmlw);
            ProcessRunningTotalFieldDefinitions(dd.RunningTotalFields, xmlw);
            ProcessSortFields(dd.SortFields, xmlw);
            ProcessSQLExpressionFields(dd.SQLExpressionFields, xmlw);
            ProcessSummaryFieldDefinitions(dd.SummaryFields, xmlw);

            xmlw.WriteEndElement();
        }
        private static void ProcessFormulaFieldDefinitions(FormulaFieldDefinitions ffds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("FormulaFieldDefinitions");
            xmlw.WriteElementString("Count", ffds.Count.ToStringSafe());
            foreach (FormulaFieldDefinition ffd in ffds)
            {
                ProcessFormulaFieldDefinition(ffd, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessFormulaFieldDefinition(FormulaFieldDefinition ffd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("FormulaFieldDefinition");
            xmlw.WriteElementString("FormulaName", ffd.FormulaName);
            xmlw.WriteElementString("Kind", ffd.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", ffd.Name);
            xmlw.WriteElementString("NumberOfBytes", ffd.NumberOfBytes.ToStringSafe());
            xmlw.WriteElementString("Text", ffd.Text);
            xmlw.WriteElementString("UseCount", ffd.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", ffd.ValueType.ToStringSafe());
            xmlw.WriteEndElement();
        }
        private static void ProcessGroupNameFieldDefinitions(GroupNameFieldDefinitions gnfds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("GroupNameFieldDefinitions");
            xmlw.WriteElementString("Count", gnfds.Count.ToStringSafe());
            foreach (GroupNameFieldDefinition gnfd in gnfds)
            {
                ProcessGroupNameFieldDefinition(gnfd, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessGroupNameFieldDefinition(GroupNameFieldDefinition gnfd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("GroupNameFieldDefinition");
            xmlw.WriteElementString("FormulaName", gnfd.FormulaName);
            xmlw.WriteElementString("Group", gnfd.Group.ConditionField.FormulaName);
            xmlw.WriteElementString("GroupNameFieldName", gnfd.GroupNameFieldName);
            xmlw.WriteElementString("Kind", gnfd.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", gnfd.Name);
            xmlw.WriteElementString("NumberOfBytes", gnfd.NumberOfBytes.ToStringSafe());
            //xmlw.WriteElementString("UseCount", gnfd.UseCount.ToStringSafe()); //This member is now obsolete.
            xmlw.WriteElementString("ValueType", gnfd.ValueType.ToStringSafe());
            xmlw.WriteEndElement();
        }
        private static void ProcessGroups(Groups groups, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Groups");
            xmlw.WriteElementString("Count", groups.Count.ToStringSafe());
            foreach (Group group in groups)
            {
                ProcessGroup(group, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessGroup(Group group, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Group");
            xmlw.WriteElementString("ConditionField", group.ConditionField.FormulaName);
            xmlw.WriteElementString("GroupOptionsCondition", group.GroupOptions.Condition.ToStringSafe());
            xmlw.WriteEndElement();
        }
        private static void ProcessFieldDefinition(FieldDefinition cf, XmlWriter xmlw, string field)
        {
            xmlw.WriteStartElement(field);
            xmlw.WriteElementString("FormulaName", cf.FormulaName);
            xmlw.WriteElementString("Kind", cf.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", cf.Name);
            xmlw.WriteElementString("NumberOfBytes", cf.NumberOfBytes.ToStringSafe());
            //xmlw.WriteElementString("UseCount", cf.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", cf.ValueType.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessParameterFieldDefinitions(ParameterFieldDefinitions pfds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ParameterFieldDefinitions");
            xmlw.WriteElementString("Count", pfds.Count.ToStringSafe());
            foreach (ParameterFieldDefinition pfd in pfds)
            {
                ProcessParameterFieldDefinition(pfd, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessParameterFieldDefinition(ParameterFieldDefinition pfd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ParameterFieldDefinition");
            xmlw.WriteElementString("DefaultValueDisplayType", pfd.DefaultValueDisplayType.ToStringSafe());
            xmlw.WriteElementString("DefaultValueSortMethod", pfd.DefaultValueSortMethod.ToStringSafe());
            xmlw.WriteElementString("DefaultValueSortOrder", pfd.DefaultValueSortOrder.ToStringSafe());
            xmlw.WriteElementString("DiscreteOrRangeKind", pfd.DiscreteOrRangeKind.ToStringSafe());
            xmlw.WriteElementString("EditMask", pfd.EditMask);
            xmlw.WriteElementString("EnableAllowEditingDefaultValue", pfd.EnableAllowEditingDefaultValue.ToStringSafe());
            xmlw.WriteElementString("EnableAllowMultipleValue", pfd.EnableAllowMultipleValue.ToStringSafe());
            xmlw.WriteElementString("EnableNullValue", pfd.EnableNullValue.ToStringSafe());
            xmlw.WriteElementString("FormulaName", pfd.FormulaName);
            xmlw.WriteElementString("HasCurrentValue", pfd.HasCurrentValue.ToStringSafe());
            xmlw.WriteElementString("IsOptionalPrompt", pfd.IsOptionalPrompt.ToStringSafe());
            try
            {
                xmlw.WriteElementString("IsLinked", pfd.IsLinked().ToStringSafe());
            }
            catch (NotSupportedException) //IsLinked not supported in subreport
            { }

            xmlw.WriteElementString("Kind", pfd.Kind.ToStringSafe());
            xmlw.WriteElementString("MaximumValue", pfd.MaximumValue.ToStringSafe());
            xmlw.WriteElementString("MinimumValue", pfd.MinimumValue.ToStringSafe());
            xmlw.WriteElementString("Name", pfd.Name);
            xmlw.WriteElementString("NumberOfBytes", pfd.NumberOfBytes.ToStringSafe());
            xmlw.WriteElementString("ParameterFieldName", pfd.ParameterFieldName);
            xmlw.WriteElementString("ParameterFieldUsage2", pfd.ParameterFieldUsage2.ToStringSafe());
            xmlw.WriteElementString("ParameterType", pfd.ParameterType.ToStringSafe());
            xmlw.WriteElementString("ParameterValueKind", pfd.ParameterValueKind.ToStringSafe());
            xmlw.WriteElementString("PromptText", pfd.PromptText);
            xmlw.WriteElementString("ReportName", pfd.ReportName);
            xmlw.WriteElementString("UseCount", pfd.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", pfd.ValueType.ToStringSafe());
            ProcessParameterValues(pfd.CurrentValues, xmlw, "CurrentValues");
            ProcessParameterValues(pfd.DefaultValues, xmlw, "DefaultValues");
            xmlw.WriteEndElement();
        }
        private static void ProcessParameterValues(ParameterValues pvs, XmlWriter xmlw, string values)
        {
            xmlw.WriteStartElement(values);
            xmlw.WriteElementString("Count", pvs.Count.ToStringSafe());
            xmlw.WriteElementString("Capacity", pvs.Capacity.ToStringSafe());
            xmlw.WriteElementString("IsNoValue", pvs.IsNoValue.ToStringSafe());
            foreach (ParameterValue pv in pvs)
            {
                ProcessParameterValue(pv, xmlw, values.Remove(values.Length - 1, 1));
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessParameterValue(ParameterValue pv, XmlWriter xmlw, string value)
        {
            xmlw.WriteStartElement(value);
            xmlw.WriteElementString("Description", pv.Description);
            xmlw.WriteElementString("IsRange", pv.IsRange.ToStringSafe());
            xmlw.WriteElementString("Kind", pv.Kind.ToStringSafe());
            if (pv.IsRange)
            {
                ParameterRangeValue prv = (ParameterRangeValue)pv;
                xmlw.WriteElementString("EndValue", prv.EndValue.ToStringSafe());
                xmlw.WriteElementString("LowerBoundType", prv.LowerBoundType.ToStringSafe());
                xmlw.WriteElementString("StartValue", prv.StartValue.ToStringSafe());
                xmlw.WriteElementString("UpperBoundType", prv.UpperBoundType.ToStringSafe());
            }
            else
            {
                ParameterDiscreteValue pdv = (ParameterDiscreteValue)pv;
                xmlw.WriteElementString("Value", pdv.Value.ToStringSafe());
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessRunningTotalFieldDefinitions(RunningTotalFieldDefinitions rtfds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("RunningTotalFieldDefinitions");
            xmlw.WriteElementString("Count", rtfds.Count.ToStringSafe());
            foreach (RunningTotalFieldDefinition rtfd in rtfds)
            {
                ProcessRunningTotalFieldDefinition(rtfd, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessRunningTotalFieldDefinition(RunningTotalFieldDefinition rtfd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("RunningTotalFieldDefinition");
            xmlw.WriteElementString("EvaluationCondition", ProcessCondition(rtfd.EvaluationCondition));
            xmlw.WriteElementString("EvaluationConditionType", rtfd.EvaluationConditionType.ToStringSafe());
            xmlw.WriteElementString("IsPercentageSummary", rtfd.IsPercentageSummary.ToStringSafe());
            xmlw.WriteElementString("FormulaName", rtfd.FormulaName);
            xmlw.WriteElementString("Kind", rtfd.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", rtfd.Name);
            xmlw.WriteElementString("NumberOfBytes", rtfd.NumberOfBytes.ToStringSafe());
            xmlw.WriteElementString("Operation", rtfd.Operation.ToStringSafe());
            xmlw.WriteElementString("OperationParameter", rtfd.OperationParameter.ToStringSafe());
            xmlw.WriteElementString("ResetCondition", ProcessCondition(rtfd.ResetCondition));
            xmlw.WriteElementString("ResetConditionType", rtfd.ResetConditionType.ToStringSafe());
            xmlw.WriteElementString("SummarizedField", rtfd.SummarizedField.FormulaName);
            xmlw.WriteElementString("UseCount", rtfd.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", rtfd.ValueType.ToStringSafe());
            if (rtfd.Group != null)
            {
                xmlw.WriteElementString("Group", rtfd.Group.ConditionField.FormulaName);
            }
            if (rtfd.SecondarySummarizedField != null)
            {
                xmlw.WriteElementString("SecondarySummarizedField", rtfd.SecondarySummarizedField.FormulaName);
            }
            xmlw.WriteEndElement();
        }
        private static string ProcessCondition(object condition)
        {
            if (condition is FieldDefinition dfdCondition) // Field
            {
                return dfdCondition.FormulaName;
            }
            else if (condition is Group gCondition) // Group
            {
                return gCondition.ConditionField.FormulaName;
            }
            else // Custom formula
            {
                return condition.ToStringSafe();
            }
        }

        private static void ProcessSortFields(SortFields sfs, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SortFields");
            xmlw.WriteElementString("Count", sfs.Count.ToStringSafe());
            foreach (SortField sf in sfs)
            {
                ProcessSortField(sf, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessSortField(SortField sf, XmlWriter xmlw)
        {
            if (sf is TopBottomNSortField tbnsf)
            {
                xmlw.WriteStartElement("TopBottomNSortField");
                xmlw.WriteElementString("EnableDiscardOtherGroups", tbnsf.EnableDiscardOtherGroups.ToStringSafe());
                xmlw.WriteElementString("Field", tbnsf.Field.FormulaName);
                xmlw.WriteElementString("NotInTopBottomNName", tbnsf.NotInTopBottomNName);
                xmlw.WriteElementString("NumberOfTopOrBottomNGroups", tbnsf.NumberOfTopOrBottomNGroups.ToStringSafe());
                xmlw.WriteElementString("SortDirection", tbnsf.SortDirection.ToStringSafe());
                xmlw.WriteElementString("SortType", tbnsf.SortType.ToStringSafe());
            }
            else
            {
                xmlw.WriteStartElement("SortField");
                xmlw.WriteElementString("Field", sf.Field.FormulaName);
                xmlw.WriteElementString("SortDirection", sf.SortDirection.ToStringSafe());
                xmlw.WriteElementString("SortType", sf.SortType.ToStringSafe());
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessSQLExpressionFields(SQLExpressionFieldDefinitions sexfds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SQLExpressionFieldDefinitions");
            xmlw.WriteElementString("Count", sexfds.Count.ToStringSafe());
            foreach (SQLExpressionFieldDefinition sefd in sexfds)
            {
                ProcessSQLExpressionFieldDefinition(sefd, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessSQLExpressionFieldDefinition(SQLExpressionFieldDefinition sefd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SQLExpressionFieldDefinition");
            xmlw.WriteElementString("FormulaName", sefd.FormulaName);
            xmlw.WriteElementString("Kind", sefd.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", sefd.Name);
            xmlw.WriteElementString("NumberOfBytes", sefd.NumberOfBytes.ToStringSafe());
            xmlw.WriteElementString("Text", sefd.Text);
            xmlw.WriteElementString("UseCount", sefd.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", sefd.ValueType.ToStringSafe());
            xmlw.WriteEndElement();
        }
        private static void ProcessSummaryFieldDefinitions(SummaryFieldDefinitions sfds, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SummaryFieldDefinitions");
            xmlw.WriteElementString("Count", sfds.Count.ToStringSafe());
            foreach (SummaryFieldDefinition sfd in sfds)
            {
                ProcessSummaryFieldDefinition(sfd, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessSummaryFieldDefinition(SummaryFieldDefinition sfd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SummaryFieldDefinition");
            xmlw.WriteElementString("FormulaName", sfd.FormulaName);
            xmlw.WriteElementString("Kind", sfd.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", sfd.Name);
            xmlw.WriteElementString("NumberOfBytes", sfd.NumberOfBytes.ToStringSafe());
            xmlw.WriteElementString("Operation", sfd.Operation.ToStringSafe());
            xmlw.WriteElementString("OperationParameter", sfd.OperationParameter.ToStringSafe());
            xmlw.WriteElementString("SummarizedField", sfd.SummarizedField.FormulaName);
            xmlw.WriteElementString("UseCount", sfd.UseCount.ToStringSafe());
            xmlw.WriteElementString("ValueType", sfd.ValueType.ToStringSafe());
            if (sfd.Group != null)
            {
                xmlw.WriteElementString("Group", sfd.Group.ConditionField.FormulaName);
            }
            if (sfd.SecondarySummarizedField != null)
            {
                xmlw.WriteElementString("SecondarySummarizedField", sfd.SecondarySummarizedField.FormulaName);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessDataSourceConnections(DataSourceConnections dscs, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DataSourceConnections");
            foreach (IConnectionInfo ici in dscs)
            {
                ProcessIConnectionInfo(ici, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessIConnectionInfo(IConnectionInfo ici, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DataSourceConnections");
            xmlw.WriteElementString("DBConnHandler", ici.DBConnHandler.ToStringSafe());
            xmlw.WriteElementString("DatabaseName", ici.DatabaseName);
            xmlw.WriteElementString("IntegratedSecurity", ici.IntegratedSecurity.ToStringSafe());
            xmlw.WriteElementString("Password", ici.Password);
            xmlw.WriteElementString("ServerName", ici.ServerName);
            xmlw.WriteElementString("Type", ici.Type.ToStringSafe());
            xmlw.WriteElementString("UserID", ici.UserID);
            xmlw.WriteEndElement();
        }
        private static void ProcessPrintOptions(PrintOptions po, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("PrintOptions");
            xmlw.WriteElementString("CustomPaperSource", po.CustomPaperSource.ToStringSafe());
            xmlw.WriteElementString("PageContentHeight", po.PageContentHeight.ToStringSafe());
            xmlw.WriteElementString("PageContentWidth", po.PageContentWidth.ToStringSafe());
            xmlw.WriteElementString("PaperOrientation", po.PaperOrientation.ToStringSafe());
            xmlw.WriteElementString("PaperSize", po.PaperSize.ToStringSafe());
            xmlw.WriteElementString("PaperSource", po.PaperSource.ToStringSafe());
            xmlw.WriteElementString("PrinterDuplex", po.PrinterDuplex.ToStringSafe());
            xmlw.WriteElementString("PrinterName", po.PrinterName);
            ProcessPageMargins(po.PageMargins, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessPageMargins(PageMargins pm, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("PageMargins");
            xmlw.WriteElementString("bottomMargin", pm.bottomMargin.ToStringSafe());
            xmlw.WriteElementString("leftMargin", pm.leftMargin.ToStringSafe());
            xmlw.WriteElementString("rightMargin", pm.rightMargin.ToStringSafe());
            xmlw.WriteElementString("topMargin", pm.topMargin.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessReportDefinition(ReportDefinition rd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportDefinition");
            ProcessAreas(rd.Areas, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessAreas(Areas areas, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Areas");
            xmlw.WriteElementString("Count", areas.Count.ToStringSafe());
            foreach (Area area in areas)
            {
                ProcessArea(area, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessArea(Area area, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Area");
            xmlw.WriteElementString("Kind", area.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", area.Name);
            ProcessAreaFormat(area.AreaFormat, xmlw);
            ProcessSections(area.Sections, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessAreaFormat(AreaFormat af, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("AreaFormat");
            xmlw.WriteElementString("EnableHideForDrillDown", af.EnableHideForDrillDown.ToStringSafe());
            xmlw.WriteElementString("EnableKeepTogether", af.EnableKeepTogether.ToStringSafe());
            xmlw.WriteElementString("EnableNewPageAfter", af.EnableNewPageAfter.ToStringSafe());
            xmlw.WriteElementString("EnableNewPageBefore", af.EnableNewPageBefore.ToStringSafe());
            xmlw.WriteElementString("EnablePrintAtBottomOfPage", af.EnablePrintAtBottomOfPage.ToStringSafe());
            xmlw.WriteElementString("EnableResetPageNumberAfter", af.EnableResetPageNumberAfter.ToStringSafe());
            xmlw.WriteElementString("EnableSuppress", af.EnableSuppress.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessSections(Sections sections, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Sections");
            xmlw.WriteElementString("Count", sections.Count.ToStringSafe());
            foreach (Section section in sections)
            {
                ProcessSection(section, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessSection(Section section, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Section");
            xmlw.WriteElementString("Height", section.Height.ToStringSafe());
            xmlw.WriteElementString("Kind", section.Kind.ToStringSafe());
            xmlw.WriteElementString("Name", section.Name);
            ProcessReportObjects(section.ReportObjects, xmlw);
            ProcessSectionFormat(section.SectionFormat, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessReportObjects(ReportObjects ros, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportObjects");
            xmlw.WriteElementString("Count", ros.Count.ToStringSafe());
            foreach (ReportObject ro in ros)
            {
                ProcessReportObjects(ro, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessReportObjects(ReportObject ro, XmlWriter xmlw)
        {
            xmlw.WriteStartElement(ro.Kind.ToStringSafe());
            // for all types the same properties
            xmlw.WriteElementString("Height", ro.Height.ToStringSafe());
            xmlw.WriteElementString("Kind", ro.Kind.ToStringSafe());
            xmlw.WriteElementString("Left", ro.Left.ToStringSafe());
            xmlw.WriteElementString("Name", ro.Name);
            xmlw.WriteElementString("Top", ro.Top.ToStringSafe());
            xmlw.WriteElementString("Width", ro.Width.ToStringSafe());
            // kind specific properties
            switch (ro.Kind)
            {
                case ReportObjectKind.BlobFieldObject:
                    {
                        BlobFieldObject bfo = (BlobFieldObject)ro;
                        ProcessDatabaseFieldDefinition(bfo.DataSource, xmlw);
                        break;
                    }
                case ReportObjectKind.BoxObject:
                    {
                        BoxObject bo = (BoxObject)ro;
                        xmlw.WriteElementString("Bottom", bo.Bottom.ToStringSafe());
                        xmlw.WriteElementString("EnableExtendToBottomOfSection", bo.EnableExtendToBottomOfSection.ToStringSafe());
                        xmlw.WriteElementString("EndSectionName", bo.EndSectionName);
                        xmlw.WriteElementString("FillColor", bo.FillColor.ToStringSafe());
                        xmlw.WriteElementString("LineColor", bo.LineColor.ToStringSafe());
                        xmlw.WriteElementString("LineStyle", bo.LineStyle.ToStringSafe());
                        xmlw.WriteElementString("LineThickness", bo.LineThickness.ToStringSafe());
                        xmlw.WriteElementString("Right", bo.Right.ToStringSafe());

                        break;
                    }
                case ReportObjectKind.FieldHeadingObject:
                    {
                        FieldHeadingObject fho = (FieldHeadingObject)ro;
                        xmlw.WriteElementString("Color", fho.Color.ToStringSafe());
                        xmlw.WriteElementString("Font", fho.Font.ToStringSafe());
                        xmlw.WriteElementString("Text", fho.Text);
                        xmlw.WriteElementString("FieldObjectName", fho.FieldObjectName);
                        break;
                    }
                case ReportObjectKind.FieldObject:
                    {
                        FieldObject fo = (FieldObject)ro;
                        xmlw.WriteElementString("Color", fo.Color.ToStringSafe());
                        xmlw.WriteElementString("Font", fo.Font.ToStringSafe());
                        if (fo.DataSource != null)
                        {
                            ProcessFieldDefinition(fo.DataSource, xmlw, "DataSource");
                        }
                        ProcessFieldFormat(fo.FieldFormat, xmlw);
                        break;
                    }
                case ReportObjectKind.LineObject:
                    {
                        LineObject lo = (LineObject)ro;
                        xmlw.WriteElementString("Bottom", lo.Bottom.ToStringSafe());
                        xmlw.WriteElementString("EnableExtendToBottomOfSection", lo.EnableExtendToBottomOfSection.ToStringSafe());
                        xmlw.WriteElementString("EndSectionName", lo.EndSectionName);
                        xmlw.WriteElementString("LineColor", lo.LineColor.ToStringSafe());
                        xmlw.WriteElementString("LineStyle", lo.LineStyle.ToStringSafe());
                        xmlw.WriteElementString("LineThickness", lo.LineThickness.ToStringSafe());
                        xmlw.WriteElementString("Right", lo.Right.ToStringSafe());
                        break;
                    }
                case ReportObjectKind.SubreportObject:
                    {
                        SubreportObject so = (SubreportObject)ro;
                        xmlw.WriteElementString("EnableOnDemand", so.EnableOnDemand.ToStringSafe());
                        xmlw.WriteElementString("SubreportName", so.SubreportName);
                        break;
                    }
                case ReportObjectKind.TextObject:
                    {
                        TextObject to = (TextObject)ro;
                        xmlw.WriteElementString("Color", to.Color.ToStringSafe());
                        xmlw.WriteElementString("Font", to.Font.ToStringSafe());
                        xmlw.WriteElementString("Text", to.Text);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            ProcessBorder(ro.Border, xmlw);
            ProcessObjectFormat(ro.ObjectFormat, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessFieldFormat(FieldFormat ff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("FieldFormat");
            //if (ff.BooleanFormat != null)
            //{
            ProcessBooleanFormat(ff.BooleanFormat, xmlw);
            //}
            //else if(ff.CommonFormat!=null)
            //{
            ProcessCommonFormat(ff.CommonFormat, xmlw);
            //}
            //else if (ff.DateTimeFormat != null)//datetime before date and time
            //{
            ProcessDateTimeFormat(ff.DateTimeFormat, xmlw);
            //}
            //else if (ff.DateFormat != null)
            //{
            ProcessDateFormat(ff.DateFormat, xmlw);
            //}
            //else if (ff.NumericFormat != null)
            //{
            ProcessNumericFormat(ff.NumericFormat, xmlw);
            //}
            //else if (ff.TimeFormat != null)
            //{
            ProcessTimeFormat(ff.TimeFormat, xmlw);
            //}
            //else
            //{
            //    ((Action)(() => { }))();
            //}
            xmlw.WriteEndElement();
        }

        private static void ProcessBooleanFormat(BooleanFieldFormat bff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("BooleanFieldFormat");
            xmlw.WriteElementString("OutputType", bff.OutputType.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessCommonFormat(CommonFieldFormat cff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("CommonFieldFormat");
            xmlw.WriteElementString("EnableSuppressIfDuplicated", cff.EnableSuppressIfDuplicated.ToStringSafe());
            xmlw.WriteElementString("EnableUseSystemDefaults", cff.EnableUseSystemDefaults.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessDateTimeFormat(DateTimeFieldFormat dtff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DateTimeFieldFormat");
            xmlw.WriteElementString("DateTimeSeparator", dtff.DateTimeSeparator);
            ProcessDateFormat(dtff.DateFormat, xmlw);
            ProcessTimeFormat(dtff.TimeFormat, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessDateFormat(DateFieldFormat dff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DateFieldFormat");
            xmlw.WriteElementString("DayFormat", dff.DayFormat.ToStringSafe());
            xmlw.WriteElementString("MonthFormat", dff.MonthFormat.ToStringSafe());
            xmlw.WriteElementString("YearFormat", dff.YearFormat.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessNumericFormat(NumericFieldFormat nff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("NumericFieldFormat");
            xmlw.WriteElementString("CurrencySymbolFormat", nff.CurrencySymbolFormat.ToStringSafe());
            xmlw.WriteElementString("DecimalPlaces", nff.DecimalPlaces.ToStringSafe());
            xmlw.WriteElementString("EnableUseLeadingZero", nff.EnableUseLeadingZero.ToStringSafe());
            xmlw.WriteElementString("NegativeFormat", nff.NegativeFormat.ToStringSafe());
            xmlw.WriteElementString("RoundingFormat", nff.RoundingFormat.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessTimeFormat(TimeFieldFormat tff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TimeFieldFormat");
            xmlw.WriteElementString("AMPMFormat", tff.AMPMFormat.ToStringSafe());
            xmlw.WriteElementString("AMString", tff.AMString);
            xmlw.WriteElementString("HourFormat", tff.HourFormat.ToStringSafe());
            xmlw.WriteElementString("HourMinuteSeparator", tff.HourMinuteSeparator);
            xmlw.WriteElementString("MinuteFormat", tff.MinuteFormat.ToStringSafe());
            xmlw.WriteElementString("MinuteSecondSeparator", tff.MinuteSecondSeparator);
            xmlw.WriteElementString("PMString", tff.PMString);
            xmlw.WriteElementString("SecondFormat", tff.SecondFormat.ToStringSafe());
            xmlw.WriteElementString("TimeBase", tff.TimeBase.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessBorder(Border border, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Border");
            xmlw.WriteElementString("BackgroundColor", border.BackgroundColor.ToStringSafe());
            xmlw.WriteElementString("BorderColor", border.BorderColor.ToStringSafe());
            xmlw.WriteElementString("BottomLineStyle", border.BottomLineStyle.ToStringSafe());
            xmlw.WriteElementString("HasDropShadow", border.HasDropShadow.ToStringSafe());
            xmlw.WriteElementString("LeftLineStyle", border.LeftLineStyle.ToStringSafe());
            xmlw.WriteElementString("RightLineStyle", border.RightLineStyle.ToStringSafe());
            xmlw.WriteElementString("TopLineStyle", border.TopLineStyle.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessObjectFormat(ObjectFormat of, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ObjectFormat");
            xmlw.WriteElementString("CssClass", of.CssClass);
            xmlw.WriteElementString("EnableCanGrow", of.EnableCanGrow.ToStringSafe());
            xmlw.WriteElementString("EnableCloseAtPageBreak", of.EnableCloseAtPageBreak.ToStringSafe());
            xmlw.WriteElementString("EnableKeepTogether", of.EnableKeepTogether.ToStringSafe());
            xmlw.WriteElementString("EnableSuppress", of.EnableSuppress.ToStringSafe());
            xmlw.WriteElementString("HorizontalAlignment", of.HorizontalAlignment.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessSectionFormat(SectionFormat sf, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SectionFormat");
            xmlw.WriteElementString("BackgroundColor", sf.BackgroundColor.ToStringSafe());
            xmlw.WriteElementString("CssClass", sf.CssClass);
            xmlw.WriteElementString("EnableKeepTogether", sf.EnableKeepTogether.ToStringSafe());
            xmlw.WriteElementString("EnableNewPageAfter", sf.EnableNewPageAfter.ToStringSafe());
            xmlw.WriteElementString("EnableNewPageBefore", sf.EnableNewPageBefore.ToStringSafe());
            xmlw.WriteElementString("EnablePrintAtBottomOfPage", sf.EnablePrintAtBottomOfPage.ToStringSafe());
            xmlw.WriteElementString("EnableResetPageNumberAfter", sf.EnableResetPageNumberAfter.ToStringSafe());
            xmlw.WriteElementString("EnableSuppress", sf.EnableSuppress.ToStringSafe());
            xmlw.WriteElementString("EnableSuppressIfBlank", sf.EnableSuppressIfBlank.ToStringSafe());
            xmlw.WriteElementString("EnableUnderlaySection", sf.EnableUnderlaySection.ToStringSafe());
            xmlw.WriteEndElement();
        }
        private static void ProcessReportOptions(ReportOptions ro, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportOptions");
            xmlw.WriteElementString("EnableSaveDataWithReport", ro.EnableSaveDataWithReport.ToStringSafe());
            xmlw.WriteElementString("EnableSavePreviewPicture", ro.EnableSavePreviewPicture.ToStringSafe());
            xmlw.WriteElementString("EnableSaveSummariesWithReport", ro.EnableSaveSummariesWithReport.ToStringSafe());
            xmlw.WriteElementString("EnableUseDummyData", ro.EnableUseDummyData.ToStringSafe());
            xmlw.WriteElementString("InitialDataContext", ro.InitialDataContext);
            xmlw.WriteElementString("InitialReportPartName", ro.InitialReportPartName);
            xmlw.WriteEndElement();
        }
        private static void ProcessSubreports(Subreports subs, XmlWriter xmlw)
        {
            foreach (ReportDocument sub in subs)
            {
                ProcessReport(sub, xmlw, "Subreport");
            }
        }
        private static void ProcessSummaryInfo(SummaryInfo si, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SummaryInfo");
            xmlw.WriteElementString("KeywordsInReport", si.KeywordsInReport);
            xmlw.WriteElementString("ReportAuthor", si.ReportAuthor);
            xmlw.WriteElementString("ReportComments", si.ReportComments);
            xmlw.WriteElementString("ReportSubject", si.ReportSubject);
            xmlw.WriteElementString("ReportTitle", si.ReportTitle);
            xmlw.WriteEndElement();
        }
        private static void ProcessSavedXmlExportFormats(XmlExportFormats xefs, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("XmlExportFormats");
            xmlw.WriteElementString("Count", xefs.Count.ToStringSafe());
            foreach (XmlExportFormat xef in xefs)
            {
                ProcessSavedXmlExportFormat(xef, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessSavedXmlExportFormat(XmlExportFormat xef, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("XmlExportFormat");
            xmlw.WriteElementString("Description", xef.Description);
            xmlw.WriteElementString("ExportBlobField", xef.ExportBlobField.ToStringSafe());
            xmlw.WriteElementString("FileExtension", xef.FileExtension);
            xmlw.WriteElementString("Name", xef.Name);
            xmlw.WriteEndElement();
        }
    }
}
