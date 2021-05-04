using System;
using System.Diagnostics;
using System.IO;
using rpt_diff.RptConvert;

namespace rpt_diff
{
    static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            var par = ProgramParams.Parse(args);
            if (par == null)
            {
                Console.Error.WriteLine("Error: Wrong number of parameters");
                ProgramParams.WriteUsage();
                return (int)ProgramExitCode.WrongArgs;
            }
            var err = par.Validate();
            if (err != null) return (int)err.Value;

            if (!TryConvertRptFiles(par, out var xml1Path, out var xml2Path)) return ReturnConvertError(par.Rpt1Path);

            Debug.WriteLine(value: $@"Starting diff application: ""{par.DiffToolPath}""");
            Process diffProc = Process.Start(par.DiffToolPath, $@"""{xml1Path}"" ""{xml2Path}""");
            diffProc.WaitForExit();

            // delete xml files after closing diff application
            if (File.Exists(xml1Path))
            {
                Debug.WriteLine($@"Deleting file: ""{xml1Path}""");
                File.Delete(xml1Path);
            }
            if (File.Exists(xml2Path))
            {
                Debug.WriteLine($@"Deleting file: ""{xml2Path}""");
                File.Delete(xml2Path);
            }

            return (int)ProgramExitCode.Success;
        }

        private static bool TryConvertRptFiles(ProgramParams par, out string xml1Path, out string xml2Path)
        {
            xml2Path = null;

            if (!TryConvert(par.Rpt1Path, par.ModelType, out xml1Path)) return false;
            Debug.WriteLine($@"File ""{par.Rpt1Path}"" converted to ""{xml1Path}""");

            xml2Path = "";
            if (par.Rpt2Path != null && !TryConvert(par.Rpt2Path, par.ModelType, out xml2Path)) return false;

            return true;
        }

        private static int ReturnConvertError(string rptPath)
        {
            Console.Error.WriteLine($"Error: Convert to XML error on {rptPath}");
            return (int)ProgramExitCode.ConvertError;
        }

        private static bool TryConvert(string rpt1Path, ModelType modelType, out string xmlPath)
        {
            try
            {
                xmlPath = RptToXml.ConvertRptToXml(rpt1Path, modelType);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                xmlPath = null;
                return false;
            }
        }
    }
}
