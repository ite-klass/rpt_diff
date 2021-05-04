namespace rpt_diff
{
    /*
     * All return codes
     */
    internal enum ProgramExitCode : int
    {
        Success = 0,
        WrongDiffApp = 1,
        WrongRptFile = 2,
        WrongArgs = 3,
        WrongModel = 4,
        ConvertError = 5
    }
}
