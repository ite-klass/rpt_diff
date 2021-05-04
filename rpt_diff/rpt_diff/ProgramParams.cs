using System;
using System.IO;

namespace rpt_diff
{
    public class ProgramParams
    {
        public static ProgramParams Parse(string[] args)
        {
            if (args.Length != 3 && args.Length != 4) return null;

            var modelType = (ModelType)(int.TryParse(args.Length == 4 ? args[3] : args[2], out var value) ? value : -1);

            return new ProgramParams
            {
                DiffToolPath = args[0],
                Rpt1Path = args[1],
                Rpt2Path = args.Length == 4 ? args[2] : null,
                ModelType = modelType,
            };
        }

        public static void WriteUsage()
        {
            Console.WriteLine("Usage: rpt_diff.exe DiffUtilPath RPTPath1 [RPTPath2] ModelNumber");
            Console.WriteLine("       DiffUtilPath - Full path to external diff application .exe file that can compare two xml files (for example KDiff)");
            Console.WriteLine("       RPTPath1 - Full path to first .rpt file to be converted to xml");
            Console.WriteLine("       RPTPath2 - Full path to second .rpt file to be converted to xml and compared with first file");
            Console.WriteLine("       ModelNumber - Select between object model - 0: ReportDocument, 1: ReportClientDocument (recomended)");
        }

        public string DiffToolPath { get; set; }
        public string Rpt1Path { get; set; }
        public string Rpt2Path { get; set; }
        public ModelType ModelType { get; set; }

        private ProgramParams()
        {
        }

        internal ProgramExitCode? Validate()
        {
            if (!File.Exists(DiffToolPath))
            {
                Console.Error.WriteLine($"Error: Diff tool does not exist at {DiffToolPath}");
                return ProgramExitCode.WrongDiffApp;
            }

            if (!File.Exists(Rpt1Path))
            {
                Console.Error.WriteLine("Error: Can't find RPT file - Bad RPTPath1");
                return ProgramExitCode.WrongRptFile;
            }

            if (!Enum.IsDefined(typeof(ModelType), ModelType))
            {
                Console.Error.WriteLine("Error: Wrong ModelNumber select - must be 0 or 1");
                return ProgramExitCode.WrongModel;
            }

            return null;
        }
    }
}
