namespace TfsReports.SelfHost
{
    using System.Diagnostics;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            var versionControl = new VersionControl(new TfsProjectSelector());

            Trace.WriteLine("Retrieving change set history in project {0}".With(versionControl.ProjectName));

            var fileName = "Commit history for {0}.csv".With(versionControl.ProjectName);


            using (var fileStream = File.Create(fileName))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.GetEncoding("GB2312")))
                {
                    streamWriter.WriteLine("Commit Id,Creation Date,User,Comment");
                    versionControl.GetAllChangesets().ForEach(streamWriter.WriteLine);
                }
            } 

            Trace.WriteLine("History written to file '{0}'".With(fileName));
        }
    }
}