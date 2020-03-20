using System;
using System.Xml;
using CrystalDecisions.ReportAppServer.DataDefModel;

namespace rpt_diff.RptConvert.Converters
{
    public class DataDefModel
    {
        public static void Process(CustomFunctions cfs, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("CustomFunctions");
            xmlw.WriteElementString("Count", cfs.Count.ToStringSafe());
            foreach (CustomFunction cf in cfs)
            {
                Process(cf, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void Process(CustomFunction cf, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("CustomFunction");
            xmlw.WriteElementString("Name", cf.Name);
            xmlw.WriteElementString("Syntax", cf.Syntax.ToStringSafe());
            xmlw.WriteString(cf.Text);
            xmlw.WriteEndElement();
        }

        public static void Process(Database database, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Database");
            ProcessTables(database.Tables, xmlw);
            ProcessTableLinks(database.TableLinks, xmlw);
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
            xmlw.WriteElementString("Alias", tbl.Alias);
            xmlw.WriteElementString("Description", tbl.Description);
            xmlw.WriteElementString("Name", tbl.Name);
            xmlw.WriteElementString("QualifiedName", tbl.QualifiedName);

            ProcessFields(tbl.DataFields, xmlw, "Data");
            ProcessConnectionInfo(tbl.ConnectionInfo, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessFields(Fields flds, XmlWriter xmlw, string type)
        {
            xmlw.WriteStartElement(type + "Fields");
            xmlw.WriteElementString("Count", flds.Count.ToStringSafe());
            foreach (Field fld in flds)
            {
                ProcessField(fld, xmlw, type);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessField(Field fld, XmlWriter xmlw, string type)
        {
            xmlw.WriteStartElement(type + "Field");
            xmlw.WriteElementString("Description", fld.Description);
            xmlw.WriteElementString("FormulaForm", fld.FormulaForm);
            xmlw.WriteElementString("HeadingText", fld.HeadingText);
            xmlw.WriteElementString("IsRecurring", fld.IsRecurring.ToStringSafe());
            xmlw.WriteElementString("Kind", fld.Kind.ToStringSafe());
            xmlw.WriteElementString("Length", fld.Length.ToStringSafe());
            xmlw.WriteElementString("LongName", fld.LongName);
            xmlw.WriteElementString("Name", fld.Name);
            xmlw.WriteElementString("ShortName", fld.ShortName);
            xmlw.WriteElementString("Type", fld.Type.ToStringSafe());
            xmlw.WriteElementString("UseCount", fld.UseCount.ToStringSafe());
            switch (fld.Kind)
            {
                case CrFieldKindEnum.crFieldKindDBField:
                    {
                        DBField dbf = (DBField)fld;
                        xmlw.WriteElementString("TableAlias", dbf.TableAlias);
                        break;
                    }
                case CrFieldKindEnum.crFieldKindFormulaField:
                    {
                        FormulaField ff = (FormulaField)fld;
                        xmlw.WriteElementString("FormulaNullTreatment", ff.FormulaNullTreatment.ToStringSafe());
                        xmlw.WriteElementString("Text", ff.Text.ToStringSafe());
                        xmlw.WriteElementString("Syntax", ff.Syntax.ToStringSafe());

                        break;
                    }
                case CrFieldKindEnum.crFieldKindGroupNameField:
                    {
                        GroupNameField gnf = (GroupNameField)fld;
                        xmlw.WriteElementString("Group", gnf.Group.ConditionField.FormulaForm);
                        break;
                    }
                case CrFieldKindEnum.crFieldKindParameterField:
                    {
                        ParameterField pf = (ParameterField)fld;
                        xmlw.WriteElementString("AllowCustomCurrentValues", pf.AllowCustomCurrentValues.ToStringSafe());
                        xmlw.WriteElementString("AllowMultiValue", pf.AllowMultiValue.ToStringSafe());
                        xmlw.WriteElementString("AllowNullValue", pf.AllowNullValue.ToStringSafe());
                        if (pf.BrowseField != null)
                        {
                            xmlw.WriteElementString("BrowseField", pf.BrowseField.FormulaForm);
                        }
                        xmlw.WriteElementString("DefaultValueSortMethod", pf.DefaultValueSortMethod.ToStringSafe());
                        xmlw.WriteElementString("DefaultValueSortOrder", pf.DefaultValueSortOrder.ToStringSafe());
                        xmlw.WriteElementString("EditMask", pf.EditMask);
                        xmlw.WriteElementString("IsEditableOnPanel", pf.IsEditableOnPanel.ToStringSafe());
                        xmlw.WriteElementString("IsOptionalPrompt", pf.IsOptionalPrompt.ToStringSafe());
                        xmlw.WriteElementString("IsShownOnPanel", pf.IsShownOnPanel.ToStringSafe());

                        xmlw.WriteElementString("ParameterType", pf.ParameterType.ToStringSafe());
                        xmlw.WriteElementString("ReportName", pf.ReportName);
                        xmlw.WriteElementString("ValueRangeKind", pf.ValueRangeKind.ToStringSafe());


                        if (pf.CurrentValues != null)
                        {
                            ProcessValues(pf.CurrentValues, xmlw, "Current");
                        }
                        if (pf.DefaultValues != null)
                        {
                            ProcessValues(pf.DefaultValues, xmlw, "Default");
                        }
                        if (pf.InitialValues != null)
                        {
                            ProcessValues(pf.InitialValues, xmlw, "Initial");
                        }
                        if (pf.Values != null)
                        {
                            ProcessValues(pf.Values, xmlw, "");
                        }
                        ProcessValue(pf.MaximumValue as Value, xmlw, "Maximum");
                        ProcessValue(pf.MinimumValue as Value, xmlw, "Minimum");


                        break;
                    }
                case CrFieldKindEnum.crFieldKindRunningTotalField:
                    {
                        RunningTotalField rtf = (RunningTotalField)fld;
                        xmlw.WriteElementString("EvaluateCondition", ProcessCondition(rtf.EvaluateCondition));
                        xmlw.WriteElementString("EvaluateConditionType", rtf.EvaluateConditionType.ToStringSafe());
                        xmlw.WriteElementString("Operation", rtf.Operation.ToStringSafe());
                        xmlw.WriteElementString("ResetCondition", ProcessCondition(rtf.ResetCondition));
                        xmlw.WriteElementString("ResetConditionType", rtf.ResetConditionType.ToStringSafe());
                        xmlw.WriteElementString("SummarizedField", rtf.SummarizedField.FormulaForm);
                        break;
                    }
                case CrFieldKindEnum.crFieldKindSpecialField:
                    {
                        SpecialField sf = (SpecialField)fld;
                        xmlw.WriteElementString("SpecialType", sf.SpecialType.ToStringSafe());
                        break;
                    }
                case CrFieldKindEnum.crFieldKindSummaryField:
                    {
                        SummaryField smf = (SummaryField)fld;
                        if (smf.Group != null)
                        {
                            xmlw.WriteElementString("Group", smf.Group.ConditionField.FormulaForm);
                        }
                        xmlw.WriteElementString("Operation", smf.Operation.ToStringSafe());
                        xmlw.WriteElementString("SummarizedField", smf.SummarizedField.FormulaForm);
                        break;
                    }
                case CrFieldKindEnum.crFieldKindUnknownField:
                    {
                        break;
                    }
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessValues(Values values, XmlWriter xmlw, string type)
        {
            xmlw.WriteStartElement(type + "Values");
            xmlw.WriteElementString("Count", values.Count.ToStringSafe());
            foreach (Value val in values)
            {
                ProcessValue(val, xmlw);
            }
            xmlw.WriteEndElement();

        }

        private static void ProcessValue(Value val, XmlWriter xmlw, string type = "")
        {
            xmlw.WriteStartElement(type + "Value");
            if (val is ConstantValue cvc)
            {
                xmlw.WriteElementString("Value", cvc.Value.ToStringSafe());
            }
            else if (val is ExpressionValue ev)
            {
                xmlw.WriteElementString("Expression", ev.Expression);
            }
            else if (val is ParameterFieldDiscreteValue pfdv)
            {
                xmlw.WriteElementString("Description", pfdv.Description);
                xmlw.WriteElementString("Value", Convert.ToString(pfdv.Value));
            }
            else if (val is ParameterFieldRangeValue pfrv)
            {
                xmlw.WriteElementString("BeginValue", Convert.ToString(pfrv.BeginValue));
                xmlw.WriteElementString("Description", pfrv.Description);
                xmlw.WriteElementString("EndValue", Convert.ToString(pfrv.EndValue));
                xmlw.WriteElementString("LowerBoundType", Convert.ToString(pfrv.LowerBoundType));
                xmlw.WriteElementString("UpperBoundType", Convert.ToString(pfrv.UpperBoundType));
            }
            else if (val is ParameterFieldValue pfv)
            {
                xmlw.WriteElementString("Description", pfv.Description);
            }
            xmlw.WriteEndElement();
        }
        private static string ProcessCondition(object condition)
        {
            if (condition is Field dfdCondition) // Field
            {
                return dfdCondition.FormulaForm;
            }
            else if (condition is Group gCondition) // Group
            {
                return gCondition.ConditionField.FormulaForm;
            }
            else // Custom formula
            {
                return condition.ToStringSafe();
            }
        }

        private static void ProcessConnectionInfo(ConnectionInfo ci, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("ConnectionInfo");
            xmlw.WriteElementString("Kind", ci.Kind.ToStringSafe());
            xmlw.WriteElementString("Password", ci.Password);
            xmlw.WriteElementString("UserName", ci.UserName);
            ProcessPropertyBag(ci.Attributes, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessPropertyBag(PropertyBag pb, XmlWriter xmlw)
        {
            foreach (string pid in pb.PropertyIDs)
            {
                xmlw.WriteElementString(pid.Replace(" ", string.Empty), pb.StringValue[pid]);
            }
        }

        private static void ProcessTableLinks(TableLinks tls, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TableLinks");
            xmlw.WriteElementString("Count", tls.Count.ToStringSafe());
            foreach (TableLink tl in tls)
            {
                ProcessTableLink(tl, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessTableLink(TableLink tl, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("TableLink");
            xmlw.WriteElementString("JoinType", tl.JoinType.ToStringSafe());
            xmlw.WriteElementString("SourceTableAlias", tl.SourceTableAlias);
            xmlw.WriteElementString("TargetTableAlias", tl.TargetTableAlias);
            foreach (string sfn in tl.SourceFieldNames)
            {
                xmlw.WriteStartElement("SourceField");
                xmlw.WriteElementString("Name", sfn);
                xmlw.WriteEndElement();
            }
            foreach (string tfn in tl.TargetFieldNames)
            {
                xmlw.WriteStartElement("TargetField");
                xmlw.WriteElementString("Name", tfn);
                xmlw.WriteEndElement();
            }
            xmlw.WriteEndElement();
        }

        public static void Process(DataDefinition dd, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("DataDefinition");
            ProcessFields(dd.FormulaFields, xmlw, "Formula");
            ProcessFilter(dd.GroupFilter, xmlw, "GroupFilter");
            ProcessGroups(dd.Groups, xmlw);
            ProcessFields(dd.ParameterFields, xmlw, "Parameter");
            ProcessFilter(dd.RecordFilter, xmlw, "RecordFilter");
            //ProcessFields(dd.ResultFields, xmlw, "Result"); // redundant only shows fields that are used in the report
            ProcessFields(dd.RunningTotalFields, xmlw, "RunningTotal");
            ProcessFilter(dd.SavedDataFilter, xmlw, "SavedDataFilter");
            ProcessSorts(dd.Sorts, xmlw);
            ProcessFields(dd.SummaryFields, xmlw, "Summary");
            xmlw.WriteEndElement();
        }


        private static void ProcessFilter(Filter filter, XmlWriter xmlw, string type)
        {
            xmlw.WriteStartElement(type);
            xmlw.WriteElementString("FormulaNullTreatment", filter.FormulaNullTreatment.ToStringSafe());
            xmlw.WriteElementString("FreeEditingText", filter.FreeEditingText);
            xmlw.WriteElementString("Name", filter.Name);
            ProcessFilterItems(filter.FilterItems, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessFilterItems(FilterItems fis, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("FilterItems");
            xmlw.WriteElementString("Count", fis.Count.ToStringSafe());
            foreach (FilterItem fi in fis)
            {
                ProcessFilterItem(fi, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessFilterItem(FilterItem fi, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("FilterItem");
            xmlw.WriteElementString("ComputeText", fi.ComputeText());
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
            xmlw.WriteElementString("ConditionField", group.ConditionField.FormulaForm);
            ProcessGroupOptions(group.Options, xmlw);
            xmlw.WriteEndElement();
        }

        private static void ProcessGroupOptions(ISCRGroupOptions go, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("GroupOptions");

            xmlw.WriteElementString("GroupNameFormula", go.ConditionFormulas[CrGroupOptionsConditionFormulaTypeEnum.crGroupName].Text);
            xmlw.WriteElementString("SortDirectionFormula", go.ConditionFormulas[CrGroupOptionsConditionFormulaTypeEnum.crSortDirection].Text);
            if (go is DateGroupOptions dgo)
            {
                xmlw.WriteElementString("DateCondition", dgo.DateCondition.ToStringSafe());
            }
            if (go is SpecifiedGroupOptions sgo)
            {
                xmlw.WriteElementString("SpecifiedValueFilters", sgo.SpecifiedValueFilters.ToStringSafe());
                xmlw.WriteElementString("UnspecifiedValuesName", sgo.UnspecifiedValuesName.ToStringSafe());
                xmlw.WriteElementString("UnspecifiedValuesType", sgo.UnspecifiedValuesType.ToStringSafe());
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessSorts(Sorts sorts, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Sorts");
            xmlw.WriteElementString("Count", sorts.Count.ToStringSafe());
            foreach (Sort sort in sorts)
            {
                ProcessSort(sort, xmlw);
            }
            xmlw.WriteEndElement();
        }

        private static void ProcessSort(Sort sort, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("Sort");
            xmlw.WriteElementString("Direction", sort.Direction.ToStringSafe());
            xmlw.WriteElementString("SortField", sort.SortField.FormulaForm);
            xmlw.WriteEndElement();
        }

        public static void ProcessSummaryInfo(SummaryInfo si, XmlWriter xmlw)
        {
            xmlw.WriteStartElement("SummaryInfo");
            xmlw.WriteElementString("Author", si.Author);
            xmlw.WriteElementString("Comments", si.Comments);
            xmlw.WriteElementString("IsSavingWithPreview", si.IsSavingWithPreview.ToStringSafe());
            xmlw.WriteElementString("Keywords", si.Keywords);
            xmlw.WriteElementString("LastSavedBy", si.LastSavedBy);
            xmlw.WriteElementString("RevisionNumber", si.RevisionNumber);
            xmlw.WriteElementString("Subject", si.Subject);
            xmlw.WriteElementString("Title", si.Title);
            xmlw.WriteEndElement();
        }

    }
}
