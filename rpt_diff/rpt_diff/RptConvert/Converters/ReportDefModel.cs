using System.Xml;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using CrystalDecisions.ReportAppServer.DataDefModel;
using System;

namespace rpt_diff.RptConvert.Converters
{
    public class ReportDefModel
    {
        public static void ProcessReportOptions(ReportOptions ro, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportOptions");
            xmlw.WriteElementString("CanSelectDistinctRecords", ro.CanSelectDistinctRecords.ToStringSafe());
            xmlw.WriteElementString("ConvertDateTimeType", ro.ConvertDateTimeType.ToStringSafe());
            xmlw.WriteElementString("ConvertNullFieldToDefault", ro.ConvertNullFieldToDefault.ToStringSafe());
            xmlw.WriteElementString("ConvertOtherNullsToDefault", ro.ConvertOtherNullsToDefault.ToStringSafe());
            xmlw.WriteElementString("EnableAsyncQuery", ro.EnableAsyncQuery.ToStringSafe());
            xmlw.WriteElementString("EnablePushDownGroupBy", ro.EnablePushDownGroupBy.ToStringSafe());
            xmlw.WriteElementString("EnableSaveDataWithReport", ro.EnableSaveDataWithReport.ToStringSafe());
            xmlw.WriteElementString("EnableSelectDistinctRecords", ro.EnableSelectDistinctRecords.ToStringSafe());
            xmlw.WriteElementString("EnableUseIndexForSpeed", ro.EnableUseIndexForSpeed.ToStringSafe());
            xmlw.WriteElementString("ErrorOnMaxNumOfRecords", ro.ErrorOnMaxNumOfRecords.ToStringSafe());
            xmlw.WriteElementString("InitialDataContext", ro.InitialDataContext);
            xmlw.WriteElementString("InitialReportPartName", ro.InitialReportPartName);
            xmlw.WriteElementString("MaxNumOfRecords", ro.MaxNumOfRecords.ToStringSafe());
            xmlw.WriteElementString("RefreshCEProperties", ro.RefreshCEProperties.ToStringSafe());
            xmlw.WriteEndElement();
        }

        public static void Process(PrintOptions po, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("PrintOptions");
            xmlw.WriteElementString("DissociatePageSizeAndPrinterPaperSize", po.DissociatePageSizeAndPrinterPaperSize.ToStringSafe());
            xmlw.WriteElementString("DriverName", po.DriverName);
            xmlw.WriteElementString("PageContentHeight", po.PageContentHeight.ToStringSafe());
            xmlw.WriteElementString("PageContentWidth", po.PageContentWidth.ToStringSafe());
            xmlw.WriteElementString("PaperOrientation", po.PaperOrientation.ToStringSafe());
            xmlw.WriteElementString("PaperSize", po.PaperSize.ToStringSafe());
            xmlw.WriteElementString("PaperSource", po.PaperSource.ToStringSafe());
            xmlw.WriteElementString("PortName", po.PortName);
            xmlw.WriteElementString("PrinterDuplex", po.PrinterDuplex.ToStringSafe());
            xmlw.WriteElementString("PrinterName", po.PrinterName);
            xmlw.WriteElementString("SavedDriverName", po.SavedDriverName);
            xmlw.WriteElementString("SavedPaperName", po.SavedPaperName);
            xmlw.WriteElementString("SavedPortName", po.SavedPortName);
            xmlw.WriteElementString("SavedPrinterName", po.SavedPrinterName);
            ProcessPageMargins(po.PageMargins, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessPageMargins(PageMargins pm, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("PageMargins");
            xmlw.WriteElementString("Bottom", pm.Bottom.ToStringSafe());
            xmlw.WriteElementString("Left", pm.Left.ToStringSafe());
            xmlw.WriteElementString("Right", pm.Right.ToStringSafe());
            xmlw.WriteElementString("Top", pm.Top.ToStringSafe());
            xmlw.WriteElementString("BottomFormula", pm.PageMarginConditionFormulas[CrPageMarginConditionFormulaTypeEnum.crPageMarginConditionFormulaTypeBottom].Text);
            xmlw.WriteElementString("LeftFormula", pm.PageMarginConditionFormulas[CrPageMarginConditionFormulaTypeEnum.crPageMarginConditionFormulaTypeLeft].Text);
            xmlw.WriteElementString("RightFormula", pm.PageMarginConditionFormulas[CrPageMarginConditionFormulaTypeEnum.crPageMarginConditionFormulaTypeRight].Text);
            xmlw.WriteElementString("TopFormula", pm.PageMarginConditionFormulas[CrPageMarginConditionFormulaTypeEnum.crPageMarginConditionFormulaTypeTop].Text);
            xmlw.WriteEndElement();
        }
        public static void Process(XMLExportFormats xefs, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("XmlExportFormats");
            xmlw.WriteElementString("Count", xefs.Count.ToStringSafe());
            xmlw.WriteElementString("DefaultExportSelection", xefs.DefaultExportSelection.ToStringSafe());
            foreach (XMLExportFormat xef in xefs)
            {
                ProcessSavedXMLExportFormat(xef, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessSavedXMLExportFormat(XMLExportFormat xef, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("XmlExportFormat");
            xmlw.WriteElementString("Description", xef.Description);
            xmlw.WriteElementString("ExportBlobField", xef.ExportBlobField.ToStringSafe());
            xmlw.WriteElementString("FileExtension", xef.FileExtension);
            xmlw.WriteElementString("Name", xef.Name);
            xmlw.WriteEndElement();
        }

        public static void ProcessSubreportLinks(SubreportLinks sls, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SubreportLinks");
            xmlw.WriteElementString("Count", sls.Count.ToStringSafe());
            foreach (SubreportLink sl in sls)
            {
                ProcessSubreportLink(sl, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessSubreportLink(SubreportLink sl, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SubreportLink");
            xmlw.WriteElementString("LinkedParameterName", sl.LinkedParameterName);
            xmlw.WriteElementString("MainReportFieldName", sl.MainReportFieldName);
            xmlw.WriteElementString("SubreportFieldName", sl.SubreportFieldName);
            xmlw.WriteEndElement();
        }

        public static void Process(ReportDefinition rd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportDefinition");
            xmlw.WriteElementString("ReportKind", rd.ReportKind.ToStringSafe());
            xmlw.WriteElementString("RptrReportFlag", rd.RptrReportFlag.ToStringSafe());
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
            ProcessAreaFormat(area.Format, xmlw);
            ProcessSections(area.Sections, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessAreaFormat(ISCRAreaFormat af, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("AreaFormat");
            xmlw.WriteElementString("EnableClampPageFooter", af.EnableClampPageFooter.ToStringSafe());
            xmlw.WriteElementString("EnableHideForDrillDown", af.EnableHideForDrillDown.ToStringSafe());
            xmlw.WriteElementString("EnableKeepTogether", af.EnableKeepTogether.ToStringSafe());
            xmlw.WriteElementString("EnableNewPageAfter", af.EnableNewPageAfter.ToStringSafe());
            xmlw.WriteElementString("EnableNewPageBefore", af.EnableNewPageBefore.ToStringSafe());
            xmlw.WriteElementString("EnablePrintAtBottomOfPage", af.EnablePrintAtBottomOfPage.ToStringSafe());
            xmlw.WriteElementString("EnableResetPageNumberAfter", af.EnableResetPageNumberAfter.ToStringSafe());
            xmlw.WriteElementString("EnableSuppress", af.EnableSuppress.ToStringSafe());
            xmlw.WriteElementString("VisibleRecordNumberPerPage", af.VisibleRecordNumberPerPage.ToStringSafe());

            xmlw.WriteElementString("BackgroundColor", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeBackgroundColor].Text);
            xmlw.WriteElementString("EnableClampPageFooterFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableClampPageFooter].Text);
            xmlw.WriteElementString("EnableHideForDrillDownFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableHideForDrillDown].Text);
            xmlw.WriteElementString("EnableKeepTogetherFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableKeepTogether].Text);
            xmlw.WriteElementString("EnableNewPageAfterFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableNewPageAfter].Text);
            xmlw.WriteElementString("EnableNewPageBeforeFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableNewPageBefore].Text);
            xmlw.WriteElementString("EnablePrintAtBottomOfPageFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnablePrintAtBottomOfPage].Text);
            xmlw.WriteElementString("EnableResetPageNumberAfterFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableResetPageNumberAfter].Text);
            xmlw.WriteElementString("EnableSuppressFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableSuppress].Text);
            xmlw.WriteElementString("EnableSuppressIfBlankFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableSuppressIfBlank].Text);
            xmlw.WriteElementString("EnableUnderlaySectionFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableUnderlaySection].Text);
            xmlw.WriteElementString("GroupNumberPerPageFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeGroupNumberPerPage].Text);
            xmlw.WriteElementString("RecordNumberPerPageFormula", af.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeRecordNumberPerPage].Text);
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
            ProcessSectionFormat(section.Format, xmlw);
            xmlw.WriteEndElement();
        }
        private static void ProcessReportObjects(ReportObjects ros, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportObjects");
            xmlw.WriteElementString("Count", ros.Count.ToStringSafe());
            foreach (ReportObject ro in ros)
            {
                ProcessReportObject(ro, xmlw);
            }
            xmlw.WriteEndElement();
        }
        private static void ProcessReportObject(ReportObject ro, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ReportObject");
            // for all types the same properties

            xmlw.WriteElementString("Height", ro.Height.ToStringSafe());
            xmlw.WriteElementString("Kind", ro.Kind.ToStringSafe());
            xmlw.WriteElementString("Left", ro.Left.ToStringSafe());
            xmlw.WriteElementString("LinkedURI", ro.LinkedURI);
            xmlw.WriteElementString("Name", ro.Name);
            //xmlw.WriteElementString("ReportPartIDDataContext", ro.ReportPartBookmark.ReportPartID.DataContext);
            //xmlw.WriteElementString("ReportPartIDName", ro.ReportPartBookmark.ReportPartID.Name);
            //xmlw.WriteElementString("ReportURI", ro.ReportPartBookmark.ReportURI);
            xmlw.WriteElementString("Top", ro.Top.ToStringSafe());
            xmlw.WriteElementString("Width", ro.Width.ToStringSafe());
            // kind specific properties
            switch (ro.Kind)
            {
                case CrReportObjectKindEnum.crReportObjectKindBlobField:
                    {
                        BlobFieldObject bfo = (BlobFieldObject)ro;
                        xmlw.WriteElementString("DataSourceName", bfo.DataSourceName);
                        xmlw.WriteElementString("OriginalHeight", bfo.OriginalHeight.ToStringSafe());
                        xmlw.WriteElementString("OriginalWidth", bfo.OriginalWidth.ToStringSafe());
                        xmlw.WriteElementString("XScaling", bfo.XScaling.ToStringSafe());
                        xmlw.WriteElementString("YScaling", bfo.YScaling.ToStringSafe());
                        ProcessPictureFormat(bfo.PictureFormat, xmlw);
                        break;
                    }
                case CrReportObjectKindEnum.crReportObjectKindBox:
                    {
                        BoxObject bo = (BoxObject)ro;
                        xmlw.WriteElementString("Bottom", bo.Bottom.ToStringSafe());
                        xmlw.WriteElementString("CornerEllipseHeight", bo.CornerEllipseHeight.ToStringSafe());
                        xmlw.WriteElementString("CornerEllipseWidth", bo.CornerEllipseWidth.ToStringSafe());
                        xmlw.WriteElementString("EnableExtendToBottomOfSection", bo.EnableExtendToBottomOfSection.ToStringSafe());
                        xmlw.WriteElementString("EndSectionName", bo.EndSectionName);
                        xmlw.WriteElementString("FillColor", bo.FillColor.ToStringSafe());
                        xmlw.WriteElementString("LineColor", bo.LineColor.ToStringSafe());
                        xmlw.WriteElementString("LineStyle", bo.LineStyle.ToStringSafe());
                        xmlw.WriteElementString("LineThickness", bo.LineThickness.ToStringSafe());
                        xmlw.WriteElementString("Right", bo.Right.ToStringSafe());
                        break;
                    }
                case CrReportObjectKindEnum.crReportObjectKindFieldHeading:
                    {
                        FieldHeadingObject fho = (FieldHeadingObject)ro;
                        xmlw.WriteElementString("FieldObjectName", fho.FieldObjectName);
                        xmlw.WriteElementString("MaxNumberOfLines", fho.MaxNumberOfLines.ToStringSafe());
                        xmlw.WriteElementString("ReadingOrder", fho.ReadingOrder.ToStringSafe());
                        xmlw.WriteElementString("Text", fho.Text);
                        ProcessFontColor(fho.FontColor, xmlw);
                        ProcessParagraphs(fho.Paragraphs, xmlw);
                        break;
                    }
                case CrReportObjectKindEnum.crReportObjectKindField:
                    {
                        FieldObject fo = (FieldObject)ro;
                        xmlw.WriteElementString("DataSourceName", fo.DataSourceName);
                        xmlw.WriteElementString("FieldValueType", fo.FieldValueType.ToStringSafe());
                        ProcessFieldFormat(fo.FieldFormat, xmlw, fo.FieldValueType);
                        ProcessFontColor(fo.FontColor, xmlw);
                        break;
                    }
                case CrReportObjectKindEnum.crReportObjectKindLine:
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
                case CrReportObjectKindEnum.crReportObjectKindSubreport:
                    {
                        SubreportObject so = (SubreportObject)ro;
                        xmlw.WriteElementString("EnableOnDemand", so.EnableOnDemand.ToStringSafe());
                        xmlw.WriteElementString("IsImported", so.IsImported.ToStringSafe());
                        xmlw.WriteElementString("SubreportLocation", so.SubreportLocation);
                        xmlw.WriteElementString("SubreportName", so.SubreportName);
                        break;
                    }
                case CrReportObjectKindEnum.crReportObjectKindText:
                    {
                        TextObject to = (TextObject)ro;
                        xmlw.WriteElementString("MaxNumberOfLines", to.MaxNumberOfLines.ToStringSafe());
                        xmlw.WriteElementString("ReadingOrder", to.ReadingOrder.ToStringSafe());
                        xmlw.WriteElementString("Text", to.Text);
                        ProcessFontColor(to.FontColor, xmlw);
                        ProcessParagraphs(to.Paragraphs, xmlw);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            ProcessBorder(ro.Border, xmlw);
            ProcessObjectFormat(ro.Format, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessParagraphs(Paragraphs paragraphs, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Paragraphs");
            xmlw.WriteElementString("Count", paragraphs.Count.ToStringSafe());
            foreach (Paragraph p in paragraphs)
            {
                ProcessParagraph(p, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessParagraph(Paragraph p, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Paragraph");
            xmlw.WriteElementString("Alignment", p.Alignment.ToStringSafe());
            xmlw.WriteElementString("ReadingOrder", p.ReadingOrder.ToStringSafe());
            ProcessFontColor(p.FontColor, xmlw);
            ProcessIndentAndSpacingFormat(p.IndentAndSpacingFormat, xmlw);
            ProcessParagraphElements(p.ParagraphElements, xmlw);
            ProcessTabStops(p.TabStops, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessParagraphElements(ParagraphElements pe, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ParagraphElements");
            xmlw.WriteElementString("Count", pe.Count.ToStringSafe());
            foreach (ParagraphElement p in pe)
            {
                ProcessParagraphElement(p, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessParagraphElement(ParagraphElement p, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ParagraphElement");
            xmlw.WriteElementString("Kind", p.Kind.ToStringSafe());
            ProcessFontColor(p.FontColor, xmlw);
            switch (p.Kind)
            {
                case CrParagraphElementKindEnum.crParagraphElementKindText:
                    if (p is ParagraphTextElement pText)
                    {
                        xmlw.WriteElementString("Text", pText.Text);
                    }
                    break;
                case CrParagraphElementKindEnum.crParagraphElementKindField:
                    if (p is ParagraphFieldElement pField)
                    {
                        if (pField is FieldObject pFieldObject)
                        {
                            ProcessFieldFormat(pField.FieldFormat, xmlw, pFieldObject.FieldValueType);
                        }
                        xmlw.WriteElementString("FieldDataSource", pField.DataSource);
                    }
                    break;
                case CrParagraphElementKindEnum.crParagraphElementKindTab:
                default:
                    Console.Error.WriteLine($"Unhandled ParagraphElement Kind {p.Kind} {p.ClassName}");
                    break;
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessFontColor(FontColor fc, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("FontColor");
            xmlw.WriteElementString("Color", fc.Color.ToStringSafe());
            xmlw.WriteElementString("Bold", fc.Font.Bold.ToStringSafe());
            xmlw.WriteElementString("Charset", fc.Font.Charset.ToStringSafe());
            xmlw.WriteElementString("Italic", fc.Font.Italic.ToStringSafe());
            xmlw.WriteElementString("Name", fc.Font.Name);
            xmlw.WriteElementString("Size", fc.Font.Size.ToStringSafe());
            xmlw.WriteElementString("Strikethrough", fc.Font.Strikethrough.ToStringSafe());
            xmlw.WriteElementString("Underline", fc.Font.Underline.ToStringSafe());
            xmlw.WriteElementString("Weight", fc.Font.Weight.ToStringSafe());
            xmlw.WriteElementString("ColorFormula", fc.ConditionFormulas[CrFontColorConditionFormulaTypeEnum.crFontColorConditionFormulaTypeColor].Text);
            xmlw.WriteElementString("NameFormula", fc.ConditionFormulas[CrFontColorConditionFormulaTypeEnum.crFontColorConditionFormulaTypeName].Text);
            xmlw.WriteElementString("SizeFormula", fc.ConditionFormulas[CrFontColorConditionFormulaTypeEnum.crFontColorConditionFormulaTypeSize].Text);
            xmlw.WriteElementString("StrikeoutFormula", fc.ConditionFormulas[CrFontColorConditionFormulaTypeEnum.crFontColorConditionFormulaTypeStrikeout].Text);
            xmlw.WriteElementString("StyleFormula", fc.ConditionFormulas[CrFontColorConditionFormulaTypeEnum.crFontColorConditionFormulaTypeStyle].Text);
            xmlw.WriteElementString("UnderlineFormula", fc.ConditionFormulas[CrFontColorConditionFormulaTypeEnum.crFontColorConditionFormulaTypeUnderline].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessTabStops(TabStops ts, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TabStops");
            xmlw.WriteElementString("Count", ts.Count.ToStringSafe());
            foreach (TabStop t in ts)
            {
                ProcessTabStop(t, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessTabStop(TabStop t, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TabStop");
            xmlw.WriteElementString("Alignment", t.Alignment.ToStringSafe());
            xmlw.WriteElementString("XOffset", t.XOffset.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessPictureFormat(PictureFormat pf, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("PictureFormat");
            xmlw.WriteElementString("BottomCropping", pf.BottomCropping.ToStringSafe());
            xmlw.WriteElementString("LeftCropping", pf.LeftCropping.ToStringSafe());
            xmlw.WriteElementString("RightCropping", pf.RightCropping.ToStringSafe());
            xmlw.WriteElementString("TopCropping", pf.TopCropping.ToStringSafe());
            xmlw.WriteEndElement();
        }

        private static void ProcessFieldFormat(FieldFormat ff, XmlWriter xmlw, CrFieldValueTypeEnum vt)
        {
            switch (vt)
            {
                case CrFieldValueTypeEnum.crFieldValueTypeBooleanField:
                    {
                        ProcessBooleanFormat(ff.BooleanFormat, xmlw);
                        break;
                    }
                case CrFieldValueTypeEnum.crFieldValueTypeDateField:
                    {
                        ProcessDateFormat(ff.DateFormat, xmlw);
                        break;
                    }
                case CrFieldValueTypeEnum.crFieldValueTypeDateTimeField:
                    {
                        ProcessDateTimeFormat(ff.DateTimeFormat, xmlw);
                        break;
                    }
                case CrFieldValueTypeEnum.crFieldValueTypeCurrencyField:
                case CrFieldValueTypeEnum.crFieldValueTypeInt16sField:
                case CrFieldValueTypeEnum.crFieldValueTypeInt16uField:
                case CrFieldValueTypeEnum.crFieldValueTypeInt32sField:
                case CrFieldValueTypeEnum.crFieldValueTypeInt32uField:
                case CrFieldValueTypeEnum.crFieldValueTypeInt8sField:
                case CrFieldValueTypeEnum.crFieldValueTypeInt8uField:
                case CrFieldValueTypeEnum.crFieldValueTypeNumberField:
                    {
                        ProcessNumericFormat(ff.NumericFormat, xmlw);
                        break;
                    }
                case CrFieldValueTypeEnum.crFieldValueTypeStringField:
                    {
                        ProcessStringFormat(ff.StringFormat, xmlw);
                        break;
                    }
                case CrFieldValueTypeEnum.crFieldValueTypeTimeField:
                    {
                        ProcessTimeFormat(ff.TimeFormat, xmlw);
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
            ProcessCommonFormat(ff.CommonFormat, xmlw);
        }

        private static void ProcessBooleanFormat(BooleanFieldFormat bff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("BooleanFieldFormat");
            xmlw.WriteElementString("OutputFormat", bff.OutputFormat.ToStringSafe());
            xmlw.WriteElementString("OutputFormatFormula", bff.ConditionFormulas[CrBooleanFieldFormatConditionFormulaTypeEnum.crBooleanFieldFormatConditionFormulaTypeOutputFormat].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessCommonFormat(CommonFieldFormat cff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("CommonFieldFormat");
            xmlw.WriteElementString("EnableSuppressIfDuplicated", cff.EnableSuppressIfDuplicated.ToStringSafe());
            xmlw.WriteElementString("EnableSystemDefault", cff.EnableSystemDefault.ToStringSafe());
            xmlw.WriteElementString("EnableSuppressIfDuplicatedFormula", cff.ConditionFormulas[CrCommonFieldFormatConditionFormulaTypeEnum.crCommonFieldFormatConditionFormulaTypeSuppressIfDuplicated].Text);
            xmlw.WriteElementString("EnableSystemDefaultFormula", cff.ConditionFormulas[CrCommonFieldFormatConditionFormulaTypeEnum.crCommonFieldFormatConditionFormulaTypeUseSystemDefault].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessDateTimeFormat(DateTimeFieldFormat dtff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DateTimeFieldFormat");
            xmlw.WriteElementString("DateTimeSeparator", dtff.DateTimeSeparator);
            xmlw.WriteElementString("DateTimeOrder", dtff.DateTimeOrder.ToStringSafe());
            xmlw.WriteElementString("DateTimeSeparatorFormula", dtff.ConditionFormulas[CrDateTimeFieldFormatConditionFormulaTypeEnum.crDateTimeFieldFormatConditionFormulaTypeDateTimeOrder].Text);
            xmlw.WriteElementString("DateTimeOrderFormula", dtff.ConditionFormulas[CrDateTimeFieldFormatConditionFormulaTypeEnum.crDateTimeFieldFormatConditionFormulaTypeDateTimeSeparator].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessDateFormat(DateFieldFormat dff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DateFieldFormat");
            xmlw.WriteElementString("CalendarType", dff.CalendarType.ToStringSafe());
            xmlw.WriteElementString("DateFirstSeparator", dff.DateFirstSeparator.ToStringSafe());
            xmlw.WriteElementString("DateOrder", dff.DateOrder.ToStringSafe());
            xmlw.WriteElementString("DatePrefixSeparator", dff.DatePrefixSeparator.ToStringSafe());
            xmlw.WriteElementString("DateSecondSeparator", dff.DateSecondSeparator.ToStringSafe());
            xmlw.WriteElementString("DateSuffixSeparator", dff.DateSuffixSeparator.ToStringSafe());
            xmlw.WriteElementString("DayFormat", dff.DayFormat.ToStringSafe());
            xmlw.WriteElementString("DayOfWeekPosition", dff.DayOfWeekPosition.ToStringSafe());
            xmlw.WriteElementString("DayOfWeekSeparator", dff.DayOfWeekSeparator.ToStringSafe());
            xmlw.WriteElementString("DayOfWeekType", dff.DayOfWeekType.ToStringSafe());
            xmlw.WriteElementString("EraType", dff.EraType.ToStringSafe());
            xmlw.WriteElementString("MonthFormat", dff.MonthFormat.ToStringSafe());
            xmlw.WriteElementString("SystemDefaultType", dff.SystemDefaultType.ToStringSafe());
            xmlw.WriteElementString("YearFormat", dff.YearFormat.ToStringSafe());
            xmlw.WriteElementString("CalendarTypeFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeCalendarType].Text);
            xmlw.WriteElementString("DateFirstSeparatorFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDateFirstSeparator].Text);
            xmlw.WriteElementString("DateOrderFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDateOrder].Text);
            xmlw.WriteElementString("DatePrefixSeparatorFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDatePrefixSeparator].Text);
            xmlw.WriteElementString("DateSecondSeparatorFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDateSecondSeparator].Text);
            xmlw.WriteElementString("DateSuffixSeparatorFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDateSuffixSeparator].Text);
            xmlw.WriteElementString("DayFormatFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDayFormat].Text);
            xmlw.WriteElementString("DayOfWeekPositionFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDayOfWeekPosition].Text);
            xmlw.WriteElementString("DayOfWeekSeparatorFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDayOfWeekSeparator].Text);
            xmlw.WriteElementString("DayOfWeekTypeFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeDayOfWeekType].Text);
            xmlw.WriteElementString("EraTypeFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeEraType].Text);
            xmlw.WriteElementString("MonthFormatFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeMonthFormat].Text);
            xmlw.WriteElementString("SystemDefaultTypeFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeSystemDefaultType].Text);
            xmlw.WriteElementString("YearFormatFormula", dff.ConditionFormulas[CrDateFieldFormatConditionFormulaTypeEnum.crDateFieldFormatConditionFormulaTypeYearFormat].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessNumericFormat(NumericFieldFormat nff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("NumericFieldFormat");
            xmlw.WriteElementString("CurrencyPosition", nff.CurrencyPosition.ToStringSafe());
            xmlw.WriteElementString("CurrencySymbol", nff.CurrencySymbol);
            xmlw.WriteElementString("CurrencySymbolFormat", nff.CurrencySymbolFormat.ToStringSafe());
            xmlw.WriteElementString("DecimalSymbol", nff.DecimalSymbol);
            xmlw.WriteElementString("DisplayReverseSign", nff.DisplayReverseSign.ToStringSafe());
            xmlw.WriteElementString("EnableSuppressIfZero", nff.EnableSuppressIfZero.ToStringSafe());
            xmlw.WriteElementString("EnableUseLeadZero", nff.EnableUseLeadZero.ToStringSafe());
            xmlw.WriteElementString("NDecimalPlaces", nff.NDecimalPlaces.ToStringSafe());
            xmlw.WriteElementString("NegativeFormat", nff.NegativeFormat.ToStringSafe());
            xmlw.WriteElementString("OneCurrencySymbolPerPage", nff.OneCurrencySymbolPerPage.ToStringSafe());
            xmlw.WriteElementString("RoundingFormat", nff.RoundingFormat.ToStringSafe());
            xmlw.WriteElementString("ThousandsSeparator", nff.ThousandsSeparator.ToStringSafe());
            xmlw.WriteElementString("ThousandSymbol", nff.ThousandSymbol);
            xmlw.WriteElementString("ZeroValueString", nff.ZeroValueString);
            xmlw.WriteElementString("CurrencyPositionFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeCurrencyPosition].Text);
            xmlw.WriteElementString("CurrencySymbolFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeCurrencySymbol].Text);
            xmlw.WriteElementString("CurrencySymbolFormatFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeCurrencySymbolFormat].Text);
            xmlw.WriteElementString("DecimalSymbolFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeDecimalSymbol].Text);
            xmlw.WriteElementString("DisplayReverseSignFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeDisplayReverseSign].Text);
            xmlw.WriteElementString("EnableSuppressIfZeroFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeEnableSuppressIfZero].Text);
            xmlw.WriteElementString("EnableUseLeadZeroFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeEnableUseLeadZero].Text);
            xmlw.WriteElementString("NDecimalPlacesFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeNDecimalPlaces].Text);
            xmlw.WriteElementString("NegativeFormatFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeNegativeFormat].Text);
            xmlw.WriteElementString("OneCurrencySymbolPerPageFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeOneCurrencySymbolPerPage].Text);
            xmlw.WriteElementString("RoundingFormatFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeRoundingFormat].Text);
            xmlw.WriteElementString("ThousandsSeparatorFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeThousandsSeparator].Text);
            xmlw.WriteElementString("ThousandSymbolFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeThousandSymbol].Text);
            xmlw.WriteElementString("ZeroValueStringFormula", nff.ConditionFormulas[CrNumericFieldFormatConditionFormulaTypeEnum.crNumericFieldFormatConditionFormulaTypeZeroValueString].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessStringFormat(StringFieldFormat sff, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("StringFieldFormat");
            xmlw.WriteElementString("EnableWordWrap", sff.EnableWordWrap.ToStringSafe());
            xmlw.WriteElementString("CharacterSpacing", sff.CharacterSpacing.ToStringSafe());
            xmlw.WriteElementString("MaxNumberOfLines", sff.MaxNumberOfLines.ToStringSafe());
            xmlw.WriteElementString("ReadingOrder", sff.ReadingOrder.ToStringSafe());
            xmlw.WriteElementString("TextFormat", sff.TextFormat.ToStringSafe());
            ProcessIndentAndSpacingFormat(sff.IndentAndSpacingFormat, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessIndentAndSpacingFormat(IndentAndSpacingFormat iasf, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("IndentAndSpacingFormat");
            xmlw.WriteElementString("FirstLineIndent", iasf.FirstLineIndent.ToStringSafe());
            xmlw.WriteElementString("LeftIndent", iasf.LeftIndent.ToStringSafe());
            xmlw.WriteElementString("LineSpacing", iasf.LineSpacing.ToStringSafe());
            xmlw.WriteElementString("LineSpacingType", iasf.LineSpacingType.ToStringSafe());
            xmlw.WriteElementString("RightIndent", iasf.RightIndent.ToStringSafe());
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
            xmlw.WriteElementString("AMPMFormatFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeAMPMFormat].Text);
            xmlw.WriteElementString("AMStringFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeAMString].Text);
            xmlw.WriteElementString("HourFormatFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeHourFormat].Text);
            xmlw.WriteElementString("HourMinuteSeparatorFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeHourMinuteSeparator].Text);
            xmlw.WriteElementString("MinuteFormatFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeMinuteFormat].Text);
            xmlw.WriteElementString("MinuteSecondSeparatorFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeMinuteSecondSeparator].Text);
            xmlw.WriteElementString("PMStringFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypePMString].Text);
            xmlw.WriteElementString("SecondFormatFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeSecondFormat].Text);
            xmlw.WriteElementString("TimeBaseFormula", tff.ConditionFormulas[CrTimeFieldFormatConditionFormulaTypeEnum.crTimeFieldFormatConditionFormulaTypeTimeBase].Text);
            xmlw.WriteEndElement();
        }

        private static void ProcessBorder(Border border, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Border");
            xmlw.WriteElementString("BackgroundColor", border.BackgroundColor.ToStringSafe());
            xmlw.WriteElementString("BorderColor", border.BorderColor.ToStringSafe());
            xmlw.WriteElementString("BottomLineStyle", border.BottomLineStyle.ToStringSafe());
            xmlw.WriteElementString("EnableTightHorizontal", border.EnableTightHorizontal.ToStringSafe());
            xmlw.WriteElementString("HasDropShadow", border.HasDropShadow.ToStringSafe());
            xmlw.WriteElementString("LeftLineStyle", border.LeftLineStyle.ToStringSafe());
            xmlw.WriteElementString("RightLineStyle", border.RightLineStyle.ToStringSafe());
            xmlw.WriteElementString("TopLineStyle", border.TopLineStyle.ToStringSafe());
            xmlw.WriteElementString("BackgroundColorFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeBackgroundColor].Text);
            xmlw.WriteElementString("BorderColorFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeBorderColor].Text);
            xmlw.WriteElementString("BottomLineStyleFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeBottomLineStyle].Text);
            xmlw.WriteElementString("HasDropShadowFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeHasDropShadow].Text);
            xmlw.WriteElementString("LeftLineStyleFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeLeftLineStyle].Text);
            xmlw.WriteElementString("RightLineStyleFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeRightLineStyle].Text);
            xmlw.WriteElementString("TightHorizontalFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeTightHorizontal].Text);
            xmlw.WriteElementString("TightVerticalFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeTightVertical].Text);
            xmlw.WriteElementString("TopLineStyleFormula", border.ConditionFormulas[CrBorderConditionFormulaTypeEnum.crBorderConditionFormulaTypeTopLineStyle].Text);
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
            xmlw.WriteElementString("HyperlinkText", of.HyperlinkText);
            xmlw.WriteElementString("HyperlinkType", of.HyperlinkType.ToStringSafe());
            xmlw.WriteElementString("TextRotationAngle", of.TextRotationAngle.ToStringSafe());
            xmlw.WriteElementString("ToolTipText", of.ToolTipText);
            xmlw.WriteElementString("VerticalAlignment", of.VerticalAlignment.ToStringSafe());
            xmlw.WriteElementString("CssClassFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeCssClass].Text);
            xmlw.WriteElementString("DeltaWidthFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeDeltaWidth].Text);
            xmlw.WriteElementString("DeltaXFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeDeltaX].Text);
            xmlw.WriteElementString("DisplayStringFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeDisplayString].Text);
            xmlw.WriteElementString("EnableCanGrowFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeEnableCanGrow].Text);
            xmlw.WriteElementString("EnableCloseAtPageBreakFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeEnableCloseAtPageBreak].Text);
            xmlw.WriteElementString("EnableKeepTogetherFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeEnableKeepTogether].Text);
            xmlw.WriteElementString("EnableSuppressFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeEnableSuppress].Text);
            xmlw.WriteElementString("HorizontalAlignmentFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeHorizontalAlignment].Text);
            xmlw.WriteElementString("HyperlinkFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeHyperlink].Text);
            xmlw.WriteElementString("TextRotationAngleFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeRotation].Text);
            xmlw.WriteElementString("ToolTipTextFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeToolTipText].Text);
            xmlw.WriteElementString("VerticalAlignmentFormula", of.ConditionFormulas[CrObjectFormatConditionFormulaTypeEnum.crObjectFormatConditionFormulaTypeVerticalAlignment].Text);
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
            xmlw.WriteElementString("PageOrientation", sf.PageOrientation.ToStringSafe());
            xmlw.WriteElementString("BackgroundColorFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeBackgroundColor].Text);
            xmlw.WriteElementString("EnableClampPageFooterFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableClampPageFooter].Text);
            xmlw.WriteElementString("EnableHideForDrillDownFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableHideForDrillDown].Text);
            xmlw.WriteElementString("EnableKeepTogetherFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableKeepTogether].Text);
            xmlw.WriteElementString("EnableNewPageAfterFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableNewPageAfter].Text);
            xmlw.WriteElementString("EnableNewPageBeforeFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableNewPageBefore].Text);
            xmlw.WriteElementString("EnablePrintAtBottomOfPageFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnablePrintAtBottomOfPage].Text);
            xmlw.WriteElementString("EnableResetPageNumberAfterFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableResetPageNumberAfter].Text);
            xmlw.WriteElementString("EnableSuppressFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableSuppress].Text);
            xmlw.WriteElementString("EnableSuppressIfBlankFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableSuppressIfBlank].Text);
            xmlw.WriteElementString("EnableUnderlaySectionFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeEnableUnderlaySection].Text);
            xmlw.WriteElementString("GroupNumberPerPageFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeGroupNumberPerPage].Text);
            xmlw.WriteElementString("RecordNumberPerPageFormula", sf.ConditionFormulas[CrSectionAreaFormatConditionFormulaTypeEnum.crSectionAreaConditionFormulaTypeRecordNumberPerPage].Text);
            xmlw.WriteEndElement();
        }

    }
}
